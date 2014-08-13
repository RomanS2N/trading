using FinancialData.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialData {
  public class Quote : IQuote {
    public string Symbol { get; set; }
    public DateTime DateTime { get; set; }
    public decimal Ask { get; set; }
    public int AskSize { get; set; }
    public decimal Bid { get; set; }
    public int BidSize { get; set; }

    public SampleType SampleType {
      get { return SampleType.Quote; }
    }
  }
}
