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
  public class SimulationRunner {
    private List<IBar> _bars;
    private ISimulation _simulation;
    private Dictionary<string, List<IInstantValue<double>>> _series = new Dictionary<string, List<IInstantValue<double>>>();
    public SimulationRunner(List<IBar> bars, ISimulation simulation) {
      _bars = bars;
      _simulation = simulation;
    }
    public void AddSerie(string name, List<IInstantValue<double>> serie) {
      if (serie.Count != _bars.Count) {
        throw new Exception("Wrong samples qty.");
      }
      _series[name] = serie;
    }
    private Dictionary<string, IInstantValue<double>> GetSeriesValues(int index) {
      Dictionary<string, IInstantValue<double>> seriesValues = new Dictionary<string, IInstantValue<double>>();
      foreach (string key in _series.Keys) {
        seriesValues[key] = _series[key][index];
      }
      return seriesValues;
    }
    public void Execute() {
      int count = _bars.Count;
      for (int i = 0; i < count; i++) {
        var seriesValues = GetSeriesValues(i);
        _simulation.OnBar(new BarContext(_bars[i], seriesValues));
      }
    }
  }
}
