using FinancialData;
using FinancialData.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaLib.Extension {
  public class TaResult {
    public TaResult(double[] values, int firstValidSample/*, int valuesCount*/) {
      InstantValues = values.Select(x => new InstantValue<double>(x) as IInstantValue<double>)
          .ToList();
      FirstValidSample = firstValidSample;
      //ValuesCount = valuesCount;
    }
    public List<IInstantValue<double>> InstantValues { get; private set; }
    public int FirstValidSample { get; private set; }
    //public int ValuesCount { get; private set; }
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

    public void Normalize() {
      // normalize specified cols by computing (x - mean) / sd for each value
      double sum = 0.0;
      for (int i = 0; i < Count; ++i) {
        sum += InstantValues[i].Value;
      }
      double mean = sum / Count;
      sum = 0.0;
      for (int i = 0; i < Count; ++i) {
        sum += (InstantValues[i].Value - mean) * (InstantValues[i].Value - mean);
      }
      // thanks to Dr. W. Winfrey, Concord Univ., for catching bug in original code
      double sd = Math.Sqrt(sum / (Count - 1));
      for (int i = 0; i < Count; ++i) {
        InstantValues[i].NormalizedValue = (InstantValues[i].Value - mean) / sd;
      }
    }
    public void DiscardFirstSamples(int count) {
      InstantValues.RemoveRange(0, count);
      FirstValidSample = 0;
      //ValuesCount -= count;
    }
  }
}
