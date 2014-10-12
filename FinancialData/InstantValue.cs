using FinancialData.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinancialData {
  public class InstantValue<T> : IInstantValue<T> {
    public DateTime DateTime { get; set; }
    public T Value { get; set; }
    public InstantValue(T value) {
      Value = value;
    }
    public InstantValue(DateTime dateTime, T value)
      : this(value) {
      DateTime = dateTime;
    }
    public SampleType SampleType {
      get { return SampleType.Raw; }
    }
  }
}
