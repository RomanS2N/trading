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
  enum Trend {
    Up, Down, Undefined
  }
  class SmaSimulation : BaseSimulation {
    private double _threshold;
    private bool _longEnabled;
    private bool _shortEnabled;
    private Trend _trend = Trend.Undefined;
    public SmaSimulation(decimal threshold, bool longEnabled, bool shortEnabled, decimal? takeProfitPoints, decimal? stopLossPoints, bool isTrailingStop = false)
      : base(takeProfitPoints, stopLossPoints, isTrailingStop) {
      _threshold = (double)threshold;
      _longEnabled = longEnabled;
      _shortEnabled = shortEnabled;
    }
    public override void OnBar_UserCode(IBarContext context) {
      IBar bar = context.Bar;
      var seriesValues = context.SeriesValues;
      var smallEma = seriesValues["EMA_SMALL"];
      var bigEma = seriesValues["EMA_BIG"];
      TryToOpenLongPositions(bar, smallEma, bigEma);
      TryToOpenShortPositions(bar, smallEma, bigEma);
    }
    private void TryToOpenLongPositions(IBar bar, IInstantValue<double> smallEma, IInstantValue<double> bigEma) {
      // up trend detected
      if (smallEma.Value > (bigEma.Value + _threshold)) {
        // is trend change
        if (_trend != Trend.Up) {
          _trend = Trend.Up;
          if (_longEnabled && LongPositions.Count == 0) {
            CreatePosition(PositionSide.Long, bar.DateTime, bar.Close, 1);
          }
        }
      }
    }
    private void TryToOpenShortPositions(IBar bar, IInstantValue<double> smallEma, IInstantValue<double> bigEma) {
      // down trend detected
      if (bigEma.Value > (smallEma.Value + _threshold)) {
        // is trend change
        if (_trend != Trend.Down) {
          _trend = Trend.Down;
          if (_shortEnabled && ShortPositions.Count == 0) {
            CreatePosition(PositionSide.Short, bar.DateTime, bar.Close, 1);
          }
        }
      }
    }
    public static SmaSimulation CreateBothSides(decimal threshold, decimal takeProfit, decimal stopLoss) {
      return new SmaSimulation(threshold, true, true, takeProfit, stopLoss, true);
    }
    public static SmaSimulation CreateLongOnly(decimal threshold, decimal takeProfit, decimal stopLoss) {
      return new SmaSimulation(threshold, true, false, takeProfit, stopLoss, true);
    }
    public static SmaSimulation CreateShortOnly(decimal threshold, decimal takeProfit, decimal stopLoss) {
      return new SmaSimulation(threshold, false, true, takeProfit, stopLoss, true);
    }
  }
}
