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
    public override void OnBar_UserCode(IBarContext context) {
      var bar = context.Bar;
      var seriesValues = context.SeriesValues;
      var ema10 = seriesValues["EMA10"];
      var ema20 = seriesValues["EMA20"];

      //Debug.WriteLine("{0}, bar: {1}, ema9: {2}, ema13: {3}", bar.DateTime, bar.Close, ema9.Value, ema13.Value);
      if (ema10.Value > ema20.Value) {
        // up trend
        ShortPositions.ForEach(x => ClosePosition(x));
        if (LongPositions.Count == 0) {
          CreatePosition(PositionSide.Long, bar.Close, 1);
        }
      }

      if (ema20.Value > ema10.Value) {
        // down trend
        LongPositions.ForEach(x => ClosePosition(x));
        if (ShortPositions.Count == 0) {
          CreatePosition(PositionSide.Short, bar.Close, 1);
        }
      }
    }
  }
}
