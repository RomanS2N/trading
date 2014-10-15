using FinancialData.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation.Shared {
  public interface IBarContext {
    IBar Bar { get; }
    Dictionary<string, IInstantValue<double>> SeriesValues { get; }
    List<IPosition> Positions { get; }
    List<IPosition> LongPositions { get; }
    List<IPosition> ShortPositions { get; }
    void CreatePosition(PositionSide positionSide, decimal price, int size);
  }
}
