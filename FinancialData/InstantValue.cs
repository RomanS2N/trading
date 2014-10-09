using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinancialData {
  public class InstantValue<T> {
    public DateTime DateTime { get; set; }
    public T Value { get; set; }
    public InstantValue(DateTime dateTime, T value) {
      DateTime = dateTime;
      Value = value;
    }
  }
}
