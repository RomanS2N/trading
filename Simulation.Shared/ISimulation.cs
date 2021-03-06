﻿/*
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

using FinancialData.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Shared {
  public interface ISimulation {
    List<IPosition> Positions { get; }
    List<IPosition> ClosedPositions { get; }
    List<IPosition> LongPositions { get; }
    List<IPosition> ShortPositions { get; }
    void CreatePosition(PositionSide positionSide, DateTime dateTime, decimal price, int size);
    void OnBar(IBarContext context);
    string GetReport();
    decimal Earnings { get; }
    string SimulationInfo { get; set; }
  }
}
