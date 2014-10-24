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
using Charts;
using System.Windows.Media;

namespace Simulation.BruteForce {
  class Program {
    static void Main(string[] args) {
      string symbol = YSQSymbol.YSQIndex.SNP;
      DateTime begin = new DateTime(2013, 1, 1);
      DateTime end = new DateTime(2014, 1, 1);
      ISamplePackage samplePackage = new YSQProvider()
        .GetHistory(new Asset { Name = symbol, Type = AssetType.Index }, begin, end, null);
      IBarPackage barPackage = (IBarPackage)samplePackage;
      List<IBar> bars = barPackage.Samples;
      // simulation
      ISimulation bestSimulation = default(ISimulation);
      TaResult bestSimulationSmallEma = default(TaResult);
      TaResult bestSimulationBigEma = default(TaResult);
      ChartPool.CreateChart();
      for (int small = 2; small < 14; small += 1) {
        for (int big = 30; big < 80; big += 1) {
          var simulation = SmaSimulation.CreateBothSides(2, 30, 5);
          simulation.SimulationInfo = string.Format("EMA_SMALL: {0}, EMA_BIG: {1}", small, big);
          SimulationRunner simulationRunner = new SimulationRunner(bars, simulation);
          var smallEma = bars.EMA(small);
          var bigEma = bars.EMA(big);
          simulationRunner.AddSerie("EMA_SMALL", smallEma);
          simulationRunner.AddSerie("EMA_BIG", bigEma);
          simulationRunner.Execute();
          Console.WriteLine(simulation.GetReport());
          if (bestSimulation == default(ISimulation) || simulation.Earnings > bestSimulation.Earnings) {
            bestSimulation = simulation;
            bestSimulationSmallEma = smallEma;
            bestSimulationBigEma = bigEma;
          }
          //ShowSimulation(simulation, bars, smallEma, bigEma);
          //Console.ReadKey(true);
        }
      }
      Console.WriteLine("Best simulation");
      ShowSimulation(bestSimulation, bars, bestSimulationSmallEma, bestSimulationBigEma);
      Console.ReadKey(true);
    }

    static void ShowSimulation(ISimulation simulation, List<IBar> bars, TaResult smallEma, TaResult bigEma) {
      Console.WriteLine(simulation.GetReport());
      ChartPool.ClearSeries();
      ChartPool.AddSeries(
        new List<Series> {
          new Series("Prices", ChartType.Lines, Colors.Black, bars.Select(x => new Sample(x.DateTime, (double)x.Close))),
          new Series("EMA Small", ChartType.Lines, Colors.Blue, smallEma.InstantValues.Select(x => new Sample(x.DateTime, x.Value))),
          new Series("EMA Big", ChartType.Lines, Colors.DarkGreen, bigEma.InstantValues.Select(x => new Sample(x.DateTime, x.Value))),
          new Series("Trades", ChartType.Trades, Colors.Purple, simulation.ClosedPositions.Select(x => new Trade { Begin=x.OpenDateTime, End=x.CloseDateTime, OpenPrice=(double)x.OpenPrice, ClosePrice=(double)x.ClosePrice })),
        }
      );
    }
  }
}
