using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YahooStockQuote.FinancialDataProvider;
using FinancialData.Shared;
using System.Collections.Generic;
using TaLib.Extension;
using Simulation.Shared;

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
      simulationRunner.AddSerie("EMA9", bars.EMA(9));
      simulationRunner.AddSerie("EMA13", bars.EMA(13));
      simulationRunner.Execute();
    }
  }
}
