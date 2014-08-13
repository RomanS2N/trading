using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialData.Shared {
  public interface IBarPackage : ISamplePackage<IBar> {
    TimeSpan Period { get; set; }
    string Symbol { get; set; }
  }
}
