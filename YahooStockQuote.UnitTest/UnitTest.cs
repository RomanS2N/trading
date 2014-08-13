using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringUtils;

namespace YahooStockQuote.UnitTest {
	[TestClass]
	public class UnitTest {
		[TestMethod]
		public void TestMethod() {
			string price = YSQReader.GetPrice("MSFT");
			price = Cleaner.CleanWebResult(price);
			Assert.IsNotNull(price);
			Assert.IsTrue(float.Parse(price) > 0);
		}
	}
}
