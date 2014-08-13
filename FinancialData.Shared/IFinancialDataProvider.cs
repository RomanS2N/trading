using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialData.Shared {
  public interface IFinancialDataProvider {
    ISample GetPrice(string symbol);
    ISamplePackage GetHistory(string symbol, DateTime start, DateTime end);
  }
}
