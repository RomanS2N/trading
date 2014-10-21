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

namespace Simulation.BruteForce {
  class SmaSimulation : BaseSimulation {
    double _threshold;
    bool _longEnabled;
    bool _shortEnabled;
    public SmaSimulation(decimal threshold, bool longEnabled, bool shortEnabled) {
      _threshold = (double)threshold;
      _longEnabled = longEnabled;
      _shortEnabled = shortEnabled;
    }
    public override void OnBar_UserCode(IBarContext context) {
      var bar = context.Bar;
      var seriesValues = context.SeriesValues;
      var smallEma = seriesValues["EMA_SMALL"];
      var bigEma = seriesValues["EMA_BIG"];
      // con cada nuevo precio se cierran las posiciones abiertas (una operación por día)
      ShortPositions.ForEach(x => ClosePosition(x));
      LongPositions.ForEach(x => ClosePosition(x));
      // up trend
      if (_longEnabled && smallEma.Value > (bigEma.Value + _threshold)) {
        if (LongPositions.Count == 0) {
          CreatePosition(PositionSide.Long, bar.Close, 1);
        }
      }
      // down trend
      if (_shortEnabled && bigEma.Value > (smallEma.Value + _threshold)) {
        if (ShortPositions.Count == 0) {
          CreatePosition(PositionSide.Short, bar.Close, 1);
        }
      }
    }
    public static SmaSimulation CreateBothSides(decimal threshold) {
      return new SmaSimulation(threshold, true, true);
    }
    public static SmaSimulation CreateLongOnly(decimal threshold) {
      return new SmaSimulation(threshold, true, false);
    }
    public static SmaSimulation CreateShortOnly(decimal threshold) {
      return new SmaSimulation(threshold, false, true);
    }
  }
}
