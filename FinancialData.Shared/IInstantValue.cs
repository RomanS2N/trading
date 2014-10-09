using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinancialData.Shared {
  public interface IInstantValue<T> : ISample {
    DateTime DateTime { get; }
    T Value { get; }
  }
}
