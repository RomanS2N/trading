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
using FinancialData.Manager.Shared;
using TradingConfiguration.Shared;
using FinancialData.Shared;
using DukascopyQuote.FinancialDataProvider;

namespace FinancialData.Manager.UnitTest {
	[TestClass]
	public class UnitTest {
		[TestMethod]
		public void FinancialDataManagerTest() {
			Asset asset = new Asset("EUR/USD", AssetType.Currency);
			IDataSource source = new DukascopyDataSource();
			DukascopyOfflineProvider.Instance.AsyncGetHistory(
				asset,
				new DateTime(2000, 1, 1),
				new DateTime(2015, 1, 1),
				new DukascopyOfflineContext(@"C:\hfdata\Temp\EURUSD_DUKAS_TICKS.txt", SampleType.Quote, TimeSpan.Zero),
				(Func<ISample, bool>)(sample => {
					FinancialDataManager.Instance.AddQuote((IQuote)sample);
					return true;
				}));
		}
		[TestMethod]
		public void FinancialDataBufferTest() {
			Asset asset = new Asset("EUR/USD", AssetType.Currency);
			IDataSource source = new DukascopyDataSource();
			FinancialDataBuffer buffer = new FinancialDataBuffer(10000);
			DukascopyOfflineProvider.Instance.AsyncGetHistory(
				asset,
				new DateTime(2000, 1, 1),
				new DateTime(2015, 1, 1),
				new DukascopyOfflineContext(@"C:\hfdata\Temp\EURUSD_DUKAS_TICKS.txt", SampleType.Quote, TimeSpan.Zero),
				(Func<ISample, bool>)(sample => {
					buffer.AddQuote((IQuote)sample);
					return true;
				}));
		}
	}
}
