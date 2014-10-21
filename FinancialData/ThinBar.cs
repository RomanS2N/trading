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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinancialData {
  public class ThinBar {
    public long BarIndex { get; private set; }
    public TimeSpan TimeFrame { get; private set; }
    public DateTime DateTime { get; private set; }
    public decimal Close { get; private set; }
    public ThinBar(TimeSpan timeFrame, DateTime dateTime, decimal price) {
      TimeFrame = timeFrame;
      DateTime = dateTime;
      BarIndex = dateTime.Ticks / TimeFrame.Ticks;
      Close = price;
    }
    public override string ToString() {
      return string.Format("Bar of {0} index [{1}] => {2} -> {3:0.000000}", TimeFrame, BarIndex, DateTime, Close);
    }
  }
}
