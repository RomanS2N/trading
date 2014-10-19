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
      var ema9 = seriesValues["EMA9"];
      var ema13 = seriesValues["EMA13"];

      //Debug.WriteLine("{0}, bar: {1}, ema9: {2}, ema13: {3}", bar.DateTime, bar.Close, ema9.Value, ema13.Value);
      if (ema9.Value > ema13.Value) {
        // up trend
        ShortPositions.ForEach(x => ClosePosition(x));
        if (LongPositions.Count == 0) {
          CreatePosition(PositionSide.Long, bar.Close, 1);
        }
      }

      if (ema13.Value > ema9.Value) {
        // down trend
      }
    }
  }
}
