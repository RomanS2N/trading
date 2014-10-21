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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YahooStockQuote.FinancialDataProvider;
using FinancialData.Shared;
using System.Collections.Generic;
using TaLib.Extension;
using Simulation.Shared;
using System.Diagnostics;

namespace Simulation.UnitTest {
  [TestClass]
  public class UnitTest {
    [TestMethod]
    public void TestMethod() {
      // data
      string symbol = YSQSymbol.YSQIndex.SNP;
      DateTime begin = new DateTime(2000, 1, 1);
      DateTime end = new DateTime(2015, 1, 1);
      ISamplePackage samplePackage = new YSQProvider()
        .GetHistory(new Asset { Name = symbol, Type = AssetType.Index }, begin, end, null);
      IBarPackage barPackage = (IBarPackage)samplePackage;
      List<IBar> bars = barPackage.Samples;
      // simulation
      var simulation = new TestSimulation();
      SimulationRunner simulationRunner = new SimulationRunner(bars, simulation);
      simulationRunner.AddSerie("EMA_SMALL", bars.EMA(10));
      simulationRunner.AddSerie("EMA_BIG", bars.EMA(20));
      simulationRunner.Execute();
      Debug.WriteLine(simulation.GetReport());
    }
  }
}
