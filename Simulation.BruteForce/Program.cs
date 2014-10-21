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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YahooStockQuote.FinancialDataProvider;
using TaLib.Extension;
using Simulation.Shared;

namespace Simulation.BruteForce {
  class Program {
    static void Main(string[] args) {
      string symbol = YSQSymbol.YSQIndex.SNP;
      DateTime begin = new DateTime(2000, 1, 1);
      DateTime end = new DateTime(2015, 1, 1);
      ISamplePackage samplePackage = new YSQProvider()
        .GetHistory(new Asset { Name = symbol, Type = AssetType.Index }, begin, end, null);
      IBarPackage barPackage = (IBarPackage)samplePackage;
      List<IBar> bars = barPackage.Samples;
      // simulation
      ISimulation bestSimulation = default(ISimulation);
      for (int small = 10; small < 20; small += 1) {
        for (int big = 30; big < 80; big += 2) {
          var simulation = SmaSimulation.CreateLongOnly(100);
          simulation.SimulationInfo = string.Format("EMA_SMALL: {0}, EMA_BIG: {1}", small, big);
          SimulationRunner simulationRunner = new SimulationRunner(bars, simulation);
          simulationRunner.AddSerie("EMA_SMALL", bars.EMA(small));
          simulationRunner.AddSerie("EMA_BIG", bars.EMA(big));
          simulationRunner.Execute();
          //Console.WriteLine(simulation.GetReport());
          if (bestSimulation == default(ISimulation) || simulation.Earnings > bestSimulation.Earnings) {
            bestSimulation = simulation;
          }
        }
      }
      Console.WriteLine(bestSimulation.GetReport());
      Console.ReadKey(true);
    }
  }
}
