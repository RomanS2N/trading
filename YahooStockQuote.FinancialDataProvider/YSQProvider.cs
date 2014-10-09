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
    private static CultureInfo _culture = CultureInfo.GetCultureInfo("en-US");
    private string GetSymbolForAsset(Asset asset) {
      switch (asset.Type) {
        case AssetType.Stock:
          return asset.Name;
        case AssetType.Index:
          return asset.Name;
        default:
          throw new NotImplementedException();
      }
    }
    public ISample GetPrice(Asset asset) {
      var price = YSQReader.GetPrice(GetSymbolForAsset(asset));
      return new Quote() { Asset = asset, Source = new YQSDataSource(), DateTime = Instant.Now, Ask = decimal.Parse(price, _culture) };
    }
    private Bar BuildBar(Asset asset, IDataSource source, TimeSpan period, string text) {
      // Date,        Open,   High,   Low,    Close,  Volume,   Adj Close
      // 2014-07-01,  41.86,  42.15,  41.69,  41.87,  26917000, 41.87
      var parts = text.Split(new char[] { ',' });
      return new Bar(
        asset, source, period, DateTime.ParseExact(parts[0], "yyyy-MM-dd", CultureInfo.InvariantCulture),
        decimal.Parse(parts[1], _culture), decimal.Parse(parts[2], _culture), decimal.Parse(parts[3], _culture), decimal.Parse(parts[4], _culture),
        long.Parse(parts[5]), decimal.Parse(parts[6], _culture));
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
        Samples = data.Select(line => (IBar)BuildBar(asset, source, period, line)).OrderBy(x => x.DateTime).ToList()
      };
    }
    public void AsyncGetHistory(Asset asset, DateTime start, DateTime end, IProvisionContext provisionContext, Func<ISample, bool> func) {
      throw new NotImplementedException();
    }
  }
}
