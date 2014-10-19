using Simulation.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation {
  class Position : IPosition {
    public PositionSide Side { get; private set; }
    public decimal Price { get; private set; }
    public decimal ClosePrice { get; private set; }
    public int Size { get; private set; }
    public bool Closed { get; private set; }
    public Position(PositionSide positionSide, decimal price, int size) {
      Side = positionSide;
      Price = price;
      Size = size;
    }
    public void Close(decimal price) {
      if (!Closed) {
        ClosePrice = price;
        Closed = true;
      }
    }
  }
}
