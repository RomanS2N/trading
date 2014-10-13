using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YahooStockQuote.FinancialDataProvider;
using FinancialData.Shared;
using System.Collections.Generic;
using TaLib.Extension;

namespace Simulation.UnitTest {
  [TestClass]
  public class UnitTest {
    [TestMethod]
    public void TestMethod() {
      string symbol = YSQSymbol.YSQIndex.SNP;
      DateTime begin = new DateTime(2000, 1, 1);
      DateTime end = new DateTime(2015, 1, 1);
      ISamplePackage samplePackage = new YSQProvider().GetHistory(new Asset { Name = symbol, Type = AssetType.Index }, begin, end, null);
      IBarPackage barPackage = (IBarPackage)samplePackage;
      List<IBar> samples = barPackage.Samples;

      SimulationRunner simulationRunner = new SimulationRunner();
      simulationRunner.AddSerie(samples.EMA(13));
    }
  }
}
