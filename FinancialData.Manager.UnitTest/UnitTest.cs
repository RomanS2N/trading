using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinancialData.Manager.Shared;
using TradingConfiguration.Shared;
using FinancialData.Shared;

namespace FinancialData.Manager.UnitTest {
	[TestClass]
	public class UnitTest {
		[TestMethod]
		public void TestMethod() {
			// pre-configuro los assets
			var assetsConfiguration = Configuration.Instance.AssetsConfiguration;
			assetsConfiguration.Assets.Clear();
			assetsConfiguration.Assets.Add(
				new AssetConfiguration {
					Name = "EUR/USD",
					ShortName = "EURUSD",
					Type = AssetType.Currency
				});
			// instancio el FinancialDataManager
			IFinancialDataStore store = new FinancialDataManager();
		}
	}
}
