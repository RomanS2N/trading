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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinancialData.Shared;
using FinancialData;
using System.Globalization;

namespace DukascopyQuote.FinancialDataProvider {
	public class DukascopyOfflineProvider : IFinancialDataProvider {

		#region Singleton

		private static DukascopyOfflineProvider _instance;
		private static object _instanceLock = new object();
		public static DukascopyOfflineProvider Instance {
			get {
				if (_instance == null) {
					lock (_instanceLock) {
						if (_instance == null) {
							_instance = new DukascopyOfflineProvider();
						}
					}
				}
				return _instance;
			}
		}

		private DukascopyOfflineProvider() {
		}

		#endregion

		public ISample GetPrice(Asset asset) {
			throw new NotImplementedException();
		}
		private Bar BuildBar(Asset asset, DataSource source, TimeSpan period, string text) {
			// Time,Open,High,Low,Close,Volume
			// 01.01.2014 00:00:00.000,1.06180,1.06180,1.06180,1.06180,0.00
			var parts = text.Split(new char[] { ',' });
			return new Bar {
				Asset = asset,
				Source = source,
				Period = period,
				DateTime = DateTime.ParseExact(parts[0], "dd.MM.yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture),
				Open = decimal.Parse(parts[1]),
				High = decimal.Parse(parts[2]),
				Low = decimal.Parse(parts[3]),
				Close = decimal.Parse(parts[4]),
				Volume = (int)decimal.Parse(parts[5]),
			};
		}
		public ISamplePackage GetHistory(Asset asset, DateTime start, DateTime end, IProvisionContext provisionContext) {
			List<string> data = DukascopyOfflineReader.GetHistoricalPrices(provisionContext.Source);
			switch (provisionContext.SampleType) {
				case SampleType.Bar:
					// first line must be discarded (titles)
					data.RemoveAt(0);
					TimeSpan period = provisionContext.Period;
					DataSource source = new DataSource { Provider = DataProvider.Dukascopy };
					return new BarPackage {
						Asset = asset,
						Source = source,
						Period = period,
						Samples = data.Select(line => (IBar)BuildBar(asset, source, period, line)).ToList()
					};
				case SampleType.Quote:
				default:
					throw new NotImplementedException();
			}
		}


		public ISamplePackage GetHistory(Asset asset, DateTime start, DateTime end, IProvisionContext provisionContext, Func<ISample, bool> func) {
			throw new NotImplementedException();
		}
	}
}
