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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinancialData.Shared;
using System.Collections.Generic;

namespace YahooStockQuote.FinancialDataProvider.UnitTest {
  [TestClass]
  public class UnitTest {
    [TestMethod]
    public void TestActualPrice() {
      ISample sample = new YSQProvider().GetPrice(new Asset { Name = "MSFT", Type = AssetType.Stock });
      Assert.IsTrue(sample is IQuote);
      IQuote quote = (IQuote)sample;
      Assert.IsNotNull(quote.Asset);
      Assert.IsNotNull(quote.Source);
      Assert.IsNotNull(quote.DateTime);
      Assert.IsTrue(quote.Ask > 0);
    }

    [TestMethod]
    public void TestHistoricalPrices() {
      ISamplePackage samplePackage = new YSQProvider().GetHistory(new Asset { Name = "MSFT", Type = AssetType.Stock }, new DateTime(2014, 1, 1), new DateTime(2014, 7, 1), null);
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
