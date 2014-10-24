using FinancialData;
using FinancialData.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaLib.Extension {
  public class TaResult {
    public TaResult(double[] values, int firstValidSample, int lastValidSample) {
      InstantValues = values.Select(x => new InstantValue<double>(x) as IInstantValue<double>)
          .ToList();
      FirstValidSample = firstValidSample;
      LastValidSample = lastValidSample;
    }
    public List<IInstantValue<double>> InstantValues { get; private set; }
    public int FirstValidSample { get; private set; }
    public int LastValidSample { get; private set; }
    public void SetDateTimes(List<DateTime> dateTimes) {
      int count = InstantValues.Count;
      if (InstantValues.Count != dateTimes.Count) {
        throw new Exception("Wrong values qty.");
      }
      for (int i = 0; i < count; i++) {
        ((InstantValue<double>)InstantValues[i]).DateTime = dateTimes[i];
      }
    }
    public int Count { get { return InstantValues.Count; } }
  }
}
