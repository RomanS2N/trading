using FinancialData.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YahooStockQuote.FinancialDataProvider;

namespace Indices.Elder.Research {
  class Program {
    static void Main(string[] args) {
      var symbol = YSQSymbol.YSQIndex.SNP;
      var begin = new DateTime(2004, 1, 1);
      var end = new DateTime(2008, 1, 1);
      var samplePackage = new YSQProvider().GetHistory(new Asset(symbol, AssetType.Index), begin, end, null);
      var barPackage = (IBarPackage)samplePackage;
      var bars = barPackage.Samples;
    }
  }
}
