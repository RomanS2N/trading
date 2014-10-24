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
using System.Threading.Tasks;

namespace Charts {
  public class Trade : IDrawable {
    public DateTime Begin { get; set; }
    public DateTime End { get; set; }
    public PositionSide Side { get; set; }
    public double OpenPrice { get; set; }
    public double ClosePrice { get; set; }
    public double MinValue { get { return OpenPrice < ClosePrice ? OpenPrice : ClosePrice; } }
    public double MaxValue { get { return OpenPrice > ClosePrice ? OpenPrice : ClosePrice; } }
  }
}
