using FinancialData.Shared;
using Simulation.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.BruteForce {
  class TestSimulation : BaseSimulation {
    public override void OnBar_UserCode(IBarContext context) {
      var bar = context.Bar;
      var seriesValues = context.SeriesValues;
      var smallEma = seriesValues["EMA_SMALL"];
      var bigEma = seriesValues["EMA_BIG"];

      //Debug.WriteLine("{0}, bar: {1}, ema9: {2}, ema13: {3}", bar.DateTime, bar.Close, ema9.Value, ema13.Value);
      if (smallEma.Value > bigEma.Value) {
        // up trend
        ShortPositions.ForEach(x => ClosePosition(x));
        if (LongPositions.Count == 0) {
          CreatePosition(PositionSide.Long, bar.Close, 1);
        }
      }

      if (bigEma.Value > smallEma.Value) {
        // down trend
        LongPositions.ForEach(x => ClosePosition(x));
        if (ShortPositions.Count == 0) {
          CreatePosition(PositionSide.Short, bar.Close, 1);
        }
      }
    }
  }
}
