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
					new Asset { Name = "EUR/USD", Type = AssetType.Currency },
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
			Assert.IsNotNull(barPackage.Asset);
			Assert.IsNotNull(barPackage.Source);
			Assert.IsNotNull(barPackage.Period);
		}
	}
}
