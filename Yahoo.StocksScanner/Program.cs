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

using FinancialData.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahoo.YQL.StocksProvider;
using YahooStockQuote;
using YahooStockQuote.FinancialDataProvider;

namespace Yahoo.StocksScanner {
  class Program {
    static void Main(string[] args) {
      var socks = Provider.GetStocks();
      socks.ForEach(stock => {
        stock.Update();
        var symbol = stock.Symbol;
        var begin = new DateTime(2014, 12, 1);
        var end = new DateTime(2014, 12, 31);
        var asset = new Asset(symbol, AssetType.Stock);
        try {
          ISamplePackage samplePackage = new YSQProvider().GetHistory(asset, begin, end, null);
          IBarPackage barPackage = (IBarPackage)samplePackage;
          stock.Update(barPackage);
          var samples = barPackage.Samples;
          int samplesCount = samples.Count;
          if (samplesCount > 0) {
            var lastValue = samples.Last().Close;
            Console.WriteLine("Stock: {0} - Samples: {1} - LastValue: {2}", symbol, samplesCount, lastValue);
          }
          else {
            Console.WriteLine("Stock: {0} - Samples: {1}", symbol, samplesCount);
          }
        }
        catch {
          Console.WriteLine("Stock: {0} - NO DATA FOUND", symbol);
        }
        stock.Save();
      });
      Console.ReadKey(true);
    }
  }
}
