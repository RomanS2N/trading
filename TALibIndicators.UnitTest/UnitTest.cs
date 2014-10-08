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
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesReading.Descriptor.Quotes;
using FinancialSeriesUtils;
using BarsReading;
using TALibIndicators;

namespace TALibIndicators.UnitTest {
  [TestClass]
  public class UnitTest {
    [TestMethod]
    public void TestSMA() {
      var path = new SeriesDescriptor()
          .InstrumentDescriptors.Single(x => x.Name == "EURUSD")
          .ProviderDescriptors.Single(x => x.Name == "Dukascopy")
          .Path;
      var timeFrame = FinancialTimeSpans.M1;
      var reader = BarsReader.Create(timeFrame, path);
      DateTime[] dateTimes;
      double[] prices;
      reader.ReadAll(out dateTimes, out prices);

      var smaValues = prices.SMA(60);

      Assert.AreEqual(prices.Length, smaValues.Length);
    }

    [TestMethod]
    public void TestBBands() {
      var path = new SeriesDescriptor()
          .InstrumentDescriptors.Single(x => x.Name == "EURUSD")
          .ProviderDescriptors.Single(x => x.Name == "Dukascopy")
          .Path;
      var timeFrame = FinancialTimeSpans.M1;
      var reader = BarsReader.Create(timeFrame, path);
      DateTime[] dateTimes;
      double[] prices;
      reader.ReadAll(out dateTimes, out prices);

      var smaValues = prices.Bbands(15, 2, 2, TicTacTec.TA.Library.Core.MAType.Ema);

      Assert.AreEqual(prices.Length, smaValues.Item1.Length);
      Assert.AreEqual(prices.Length, smaValues.Item2.Length);
      Assert.AreEqual(prices.Length, smaValues.Item3.Length);
    }
  }
}
