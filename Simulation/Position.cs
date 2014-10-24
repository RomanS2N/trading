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

using Simulation.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation {
  class Position : IPosition {
    public PositionSide Side { get; private set; }
    public DateTime OpenDateTime { get; private set; }
    public decimal OpenPrice { get; private set; }
    public DateTime CloseDateTime { get; private set; }
    public decimal ClosePrice { get; private set; }
    public int Size { get; private set; }
    public bool Closed { get; private set; }
    public Position(PositionSide positionSide, DateTime dateTime, decimal price, int size) {
      Side = positionSide;
      OpenDateTime = dateTime;
      OpenPrice = price;
      Size = size;
    }
    public void Close(DateTime dateTime, decimal price) {
      if (!Closed) {
        CloseDateTime = dateTime;
        ClosePrice = price;
        Closed = true;
      }
    }
    public decimal Earnings {
      get {
        return Side == PositionSide.Long ? ClosePrice - OpenPrice : OpenPrice - ClosePrice;
      }
    }
  }
}
