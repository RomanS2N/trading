using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialData.Shared {
  public interface IBar : ISample {
    string Symbol { get; }
    DateTime DateTime { get; }
    decimal Open { get; }
    decimal High { get; }
    decimal Low { get; }
    decimal Close { get; }
    int Volume { get; }
    decimal AdjClose { get; }
  }
}
