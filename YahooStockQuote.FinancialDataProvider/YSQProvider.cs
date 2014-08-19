/*
   Copyright 2014 Samuel Pets (internetuser0x00@gmail.com)

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using FinancialData;
using FinancialData.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeUtils;

namespace YahooStockQuote.FinancialDataProvider {
	public class YSQProvider : IFinancialDataProvider {
		private string GetSymbolForAsset(Asset asset) {
			switch (asset.Type) {
				case AssetType.Stock:
					return asset.Name;
				default:
					throw new NotImplementedException();
			}
		}
		public ISample GetPrice(Asset asset) {
			var price = YSQReader.GetPrice(GetSymbolForAsset(asset));
			return new Quote() { Asset = asset, Source = new YQSDataSource(), DateTime = Instant.Now, Ask = decimal.Parse(price) };
		}
		private Bar BuildBar(Asset asset, IDataSource source, TimeSpan period, string text) {
			// Date,        Open,   High,   Low,    Close,  Volume,   Adj Close
			// 2014-07-01,  41.86,  42.15,  41.69,  41.87,  26917000, 41.87
			var parts = text.Split(new char[] { ',' });
			return new Bar {
				Asset = asset,
				Source = source,
				Period = period,
				DateTime = DateTime.ParseExact(parts[0], "yyyy-MM-dd", CultureInfo.InvariantCulture),
				Open = decimal.Parse(parts[1]),
				High = decimal.Parse(parts[2]),
				Low = decimal.Parse(parts[3]),
				Close = decimal.Parse(parts[4]),
				Volume = int.Parse(parts[5]),
				AdjClose = decimal.Parse(parts[6]),
			};
		}
		public ISamplePackage GetHistory(Asset asset, DateTime start, DateTime end, IProvisionContext provisionContext) {
			List<string> data = YSQReader.GetHistoricalPrices(GetSymbolForAsset(asset), start, end);
			IDataSource source = new YQSDataSource();
			// first line must be discarded (titles)
			data.RemoveAt(0);
			TimeSpan period = TimeSpan.FromMinutes(1);
			return new BarPackage {
				Asset = asset,
				Source = source,
				Period = period,
				Samples = data.Select(line => (IBar)BuildBar(asset, source, period, line)).ToList()
			};
		}
		public void AsyncGetHistory(Asset asset, DateTime start, DateTime end, IProvisionContext provisionContext, Func<ISample, bool> func) {
			throw new NotImplementedException();
		}
	}
}
