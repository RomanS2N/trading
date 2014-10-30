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
using TaLib.Extension;

namespace Indices.Elder.Research {
  public class BullPower {
    public List<IInstantValue<double>> InstantValues { get; private set; }
    public int FirstValidSample { get; private set; }
    public int Count { get { return InstantValues.Count; } }
    public BullPower(List<IBar> bars, int period) {
      InstantValues = new List<IInstantValue<double>>();
      TaResult ema = bars.EMA(period);
      int count = ema.Count;
      if (count != bars.Count) {
        throw new Exception("Samples count can't be different");
      }
      FirstValidSample = ema.FirstValidSample;
      for (int i = 0; i < count; i++) {
        DateTime dateTime = bars[i].DateTime;
        double bullPower = 0;
        if (i >= FirstValidSample) {
          double high = (double)bars[i].High;
          double emaValue = ema.InstantValues[i].Value;
          bullPower = high - emaValue;
        }
        InstantValues.Add(new InstantValue<double>(dateTime, bullPower));
      }
    }
  }
}
