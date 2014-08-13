using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradingConfiguration.Shared;

namespace TradingConfiguration.UnitTest {
	[TestClass]
	public class UnitTest {
		[TestMethod]
		public void TestMethod() {
			var configuration = Configuration.Load();
			var assetsConfiguration = configuration.AssetsConfiguration;
			var assets = assetsConfiguration.Assets;
			assets.Add(new AssetConfiguration { Name = "EUR/USD", Type = AssetType.Currency });
			assets.Add(new AssetConfiguration { Name = "GBP/USD", Type = AssetType.Currency });
			configuration.Save();
		}
	}
}
