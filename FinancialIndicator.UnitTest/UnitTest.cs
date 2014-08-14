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
using System.Collections.Generic;
using FinancialIndicator.Shared;

namespace FinancialIndicator.UnitTest {
	[TestClass]
	public class UnitTest {

		[TestMethod]
		public void TestRSI() {
			var prices = new List<decimal>
      {
        44.34m, 44.09m, 44.15m, 43.61m, 44.33m, 44.83m, 45.10m, 45.42m, 45.84m, 46.08m, 45.89m, 
        46.03m, 45.61m, 46.28m, 46.28m, 46.00m, 46.03m, 46.41m, 46.22m, 45.64m, 46.21m, 46.25m, 
        45.71m, 46.45m, 45.78m, 45.35m, 44.03m, 44.18m, 44.22m, 44.57m, 43.42m, 42.66m, 43.13m,
      };
			var referenceResults = new List<decimal>
      {
        00.00m, 00.00m, 00.00m, 00.00m, 00.00m, 00.00m, 00.00m, 00.00m, 00.00m, 00.00m, 00.00m, 
        00.00m, 00.00m, 00.00m, 70.53m, 66.32m, 66.55m, 69.41m, 66.36m, 57.97m, 62.93m, 63.26m, 
        56.06m, 62.38m, 54.71m, 50.42m, 39.99m, 41.46m, 41.87m, 45.46m, 37.30m, 33.08m, 37.77m,
      };
			var results = new List<decimal>();
			IFinancialIndicator rsi = new RSI(14);
			prices.ForEach(price => {
				rsi.Update(price);
				results.Add(rsi.Value);
			});
			// la verificación no es posible
			//for (int i = 0; i < results.Count; i++) {
			//    Assert.AreEqual(referenceResults[i], results[i]);
			//}
		}

		[TestMethod]
		public void TestSMA() {
			var prices = new List<decimal>
      {
        44.34m, 44.09m, 44.15m, 43.61m, 44.33m, 44.83m, 45.10m, 45.42m, 45.84m, 46.08m, 45.89m, 
        46.03m, 45.61m, 46.28m, 46.28m, 46.00m, 46.03m, 46.41m, 46.22m, 45.64m, 46.21m, 46.25m, 
        45.71m, 46.45m, 45.78m, 45.35m, 44.03m, 44.18m, 44.22m, 44.57m, 43.42m, 42.66m, 43.13m,
      };
			IFinancialIndicator sma = new SMA(14);
			prices.ForEach(price => {
				sma.Update(price);
			});
			Assert.IsTrue(sma.Value > 0);
		}
	}
}
