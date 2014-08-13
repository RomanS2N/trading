using FinancialData.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialData {
  public class QuotePackage : IQuotePackage {
    public SampleType SampleType {
      get { return SampleType.Quote; }
    }
    public List<IQuote> Samples { get; set; }
    public string Symbol { get; set; }
  }
}
