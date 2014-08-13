using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinancialData.Shared;
using System.Collections.Generic;

namespace YahooStockQuote.FinancialDataProvider.UnitTest {
	[TestClass]
	public class UnitTest {
		[TestMethod]
		public void TestActualPrice() {
			ISample sample = new YSQProvider().GetPrice("MSFT");
			Assert.IsTrue(sample is IQuote);
			IQuote quote = (IQuote)sample;
			Assert.IsNotNull(quote.Symbol);
			Assert.IsNotNull(quote.DateTime);
			Assert.IsTrue(quote.Ask > 0);
		}

		[TestMethod]
		public void TestHistoralPrices() {
			ISamplePackage samplePackage = new YSQProvider().GetHistory("MSFT", new DateTime(2014, 1, 1), new DateTime(2014, 7, 1));
			Assert.IsTrue(samplePackage is ISamplePackage<IBar>, "samplePackage no es instancia de ISamplePackage<IBar>");
			IBarPackage barPackage = (IBarPackage)samplePackage;
			Assert.IsNotNull(barPackage.Samples);
			Assert.IsTrue(barPackage.Samples.Count > 0);
			Assert.IsNotNull(barPackage.Symbol);
			Assert.IsNotNull(barPackage.Period);
		}
	}
}
