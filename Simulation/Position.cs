using Simulation.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation {
  class Position : IPosition {
    public PositionSide PositionSide { get; private set; }
    public decimal Price { get; private set; }
    public int Size { get; private set; }
    public Position(PositionSide positionSide, decimal price, int size) {
      PositionSide = positionSide;
      Price = price;
      Size = size;
    }
    public void Close() {
      throw new NotImplementedException();
    }
    public PositionSide Side {
      get { throw new NotImplementedException(); }
    }
  }
}
