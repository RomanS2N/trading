using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinancialData.Shared;

namespace DukascopyQuote.FinancialDataProvider.UnitTest {
	[TestClass]
	public class UnitTest {
		[TestMethod]
		public void TestActualPrice() {
			//ISample sample = new DukascopyOfflineProvider().GetPrice("EUR/USD");
		}

		[TestMethod]
		public void TestHistoricalPrices() {
			ISamplePackage samplePackage = new DukascopyOfflineProvider()
				.GetHistory(
					"EUR/USD",
					new DateTime(2014, 1, 1),
					new DateTime(2014, 1, 31),
					new DukascopyOfflineContext(
						@"C:\storage\USDCAD_Candlestick_1_m_BID_01.01.2014-31.01.2014.csv",
						SampleType.Bar,
						TimeSpan.FromMinutes(1)));
			Assert.IsTrue(samplePackage is ISamplePackage<IBar>, "samplePackage no es instancia de ISamplePackage<IBar>");
			IBarPackage barPackage = (IBarPackage)samplePackage;
			Assert.IsNotNull(barPackage.Samples);
			Assert.IsTrue(barPackage.Samples.Count > 0);
			Assert.IsNotNull(barPackage.Symbol);
			Assert.IsNotNull(barPackage.Period);
		}
	}
}
