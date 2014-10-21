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
  }
}
