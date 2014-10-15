using FinancialData.Shared;
using Simulation.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation {
  class BarContext : IBarContext {
    public BarContext(IBar bar, Dictionary<string, IInstantValue<double>> seriesValues) {
      Bar = bar;
      SeriesValues = seriesValues;
      foreach (var value in SeriesValues.Values) {
        if (bar.DateTime != value.DateTime) {
          throw new Exception("Invalid DateTime.");
        }
      }
    }
    public IBar Bar { get; private set; }
    public Dictionary<string, IInstantValue<double>> SeriesValues { get; private set; }
    public List<IPosition> Positions { get; private set; }
    public List<IPosition> LongPositions { get { return Positions.Where(x => x.Side == PositionSide.Long).ToList(); } }
    public List<IPosition> ShortPositions { get { return Positions.Where(x => x.Side == PositionSide.Short).ToList(); } }
    public void CreatePosition(PositionSide positionSide, decimal price, int size) {
      Positions.Add(new Position(positionSide, price, size));
    }
  }
}
