using FinancialData.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialData {
  public class Bar : IBar {
    public SampleType SampleType {
      get { return SampleType.Bar; }
    }
    public string Symbol { get; set; }
    public TimeSpan Period { get; set; }
    public DateTime DateTime { get; set; }
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Close { get; set; }
    public int Volume { get; set; }
    public decimal AdjClose { get; set; }
  }
}
