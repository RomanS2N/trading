using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DukascopyQuote.UnitTest {
	[TestClass]
	public class UnitTest {
		[TestMethod]
		public void TestMethod() {
			int samples = 0;
			DukascopyOfflineReader.GetHistoricalPrices(
				@"C:\hfdata\Temp\EURUSD_DUKAS_TICKS.txt",
				(Func<string, bool>)(line => {
					Console.WriteLine(line);
					Assert.IsNotNull(line);
					Assert.IsTrue(line.Length > 0);
					return (++samples < 5);
				}));
			Assert.AreEqual(5, samples);
		}
	}
}
