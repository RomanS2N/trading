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

using FinancialData.Shared;
using Simulation.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.UnitTest {
  class TestSimulation : BaseSimulation {
    public TestSimulation(decimal? takeProfitPoints, decimal? stopLossPoints, bool isTrailingStop = false) : base(takeProfitPoints, stopLossPoints, isTrailingStop) { }
    public override void OnBar_UserCode(IBarContext context) {
      var bar = context.Bar;
      var seriesValues = context.SeriesValues;
      var ema10 = seriesValues["EMA_SMALL"];
      var ema20 = seriesValues["EMA_BIG"];

      //Debug.WriteLine("{0}, bar: {1}, ema9: {2}, ema13: {3}", bar.DateTime, bar.Close, ema9.Value, ema13.Value);
      if (ema10.Value > ema20.Value) {
        // up trend
        if (LongPositions.Count == 0) {
          CreatePosition(PositionSide.Long, bar.DateTime, bar.Close, 1);
        }
      }

      if (ema20.Value > ema10.Value) {
        // down trend
        if (ShortPositions.Count == 0) {
          CreatePosition(PositionSide.Short, bar.DateTime, bar.Close, 1);
        }
      }
    }
  }
}
