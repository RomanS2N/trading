using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Configuration.Shared;

namespace Configuration.UnitTest {
	[TestClass]
	public class UnitTest {
		[TestMethod]
		public void TestMethod() {
			var configurationValue = ConfigurationFactory.Configuration["ConfigurationKey"];
			Assert.AreEqual("ConfigurationValue", configurationValue);
		}
	}
}
