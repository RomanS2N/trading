using FinancialData.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialData {
  public class BarPackage : IBarPackage {
    public SampleType SampleType {
      get { return SampleType.Bar; }
    }
    public TimeSpan Period { get; set; }
    public string Symbol { get; set; }
    public List<IBar> Samples { get; set; }
  }
}
