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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation.Shared {
  public interface IPosition {
    PositionSide Side { get; }
    DateTime OpenDateTime { get; }
    decimal OpenPrice { get; }
    DateTime CloseDateTime { get; }
    decimal ClosePrice { get; }
    //protected void Close(DateTime dateTime, decimal price);
    decimal Earnings { get; }
    PositionStatus Status { get; }
    void VerifyTakeProfitAndStopLoss(DateTime dateTime, decimal price);
    void AdjustTrailingStopLoss(decimal price);
  }
}
