using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation.Shared {
  public interface IPosition {
    PositionSide Side { get; }
    void Close(decimal price);
    decimal Earnings { get; }
  }
}
