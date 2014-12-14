using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahoo.YQL.StocksProvider;

namespace Yahoo.StocksScanner {
  class Program {
    static void Main(string[] args) {
      var socks = Provider.GetStocks();
      socks.ForEach(stock => {
        Console.WriteLine("{0} - {1}", stock.Symbol, stock.Name);
      });
      Console.ReadKey(true);
    }
  }
}
