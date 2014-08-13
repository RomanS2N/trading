using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialData.Shared {
  public interface IQuote : ISample {
    string Symbol { get; }
    DateTime DateTime { get; }
    decimal Ask { get; }
    int AskSize { get; }
    decimal Bid { get; }
    int BidSize { get; }
  }
}
