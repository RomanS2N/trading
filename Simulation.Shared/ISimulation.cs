using FinancialData.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Shared {
  public interface ISimulation {
    List<IPosition> Positions { get; }
    List<IPosition> LongPositions { get; }
    List<IPosition> ShortPositions { get; }
    void CreatePosition(PositionSide positionSide, decimal price, int size);
    void ClosePosition(IPosition position);
    void OnBar(IBarContext context);
  }
}
