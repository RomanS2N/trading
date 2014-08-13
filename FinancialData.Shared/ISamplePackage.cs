using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialData.Shared {
  public interface ISamplePackage {
    SampleType SampleType { get; }
  }
  public interface ISamplePackage<T> : ISamplePackage {
    List<T> Samples { get; }
  }
}
