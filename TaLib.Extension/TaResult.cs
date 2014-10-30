/*
   Copyright 2014 Samuel Pets (internetuser0x00@gmail.com)

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using FinancialData;
using FinancialData.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaLib.Extension {
  public class TaResult {
    public TaResult(double[] values, int firstValidSample) {
      InstantValues = values.Select(x => new InstantValue<double>(x) as IInstantValue<double>)
          .ToList();
      FirstValidSample = firstValidSample;
    }
    public List<IInstantValue<double>> InstantValues { get; private set; }
    public int FirstValidSample { get; private set; }
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
      double sum = 0.0;
      for (int i = 0; i < Count; ++i) {
        sum += InstantValues[i].Value;
      }
      double mean = sum / Count;
      sum = 0.0;
      for (int i = 0; i < Count; ++i) {
        sum += (InstantValues[i].Value - mean) * (InstantValues[i].Value - mean);
      }
      double sd = Math.Sqrt(sum / (Count - 1));
      for (int i = 0; i < Count; ++i) {
        InstantValues[i].NormalizedValue = (InstantValues[i].Value - mean) / sd;
      }
    }
    public void DiscardFirstSamples(int count) {
      InstantValues.RemoveRange(0, count);
      FirstValidSample = 0;
    }
    public static int GetFirstValidSample(List<TaResult> resultsList) {
      int firstValidSampleOnSeries = 0;
      foreach (TaResult result in resultsList) {
        if (result.FirstValidSample > firstValidSampleOnSeries) {
          firstValidSampleOnSeries = result.FirstValidSample;
        }
      }
      return firstValidSampleOnSeries;
    }
    public static void DiscardFirstSamples(List<TaResult> resultsList, int firstValidSampleOnSeries) {
      foreach (TaResult result in resultsList) {
        result.DiscardFirstSamples(firstValidSampleOnSeries);
      }
    }
  }
}
