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
using StringUtils;

namespace YahooStockQuote.UnitTest {
	[TestClass]
	public class UnitTest {
		[TestMethod]
        public void TestGetPrice()
        {
			var price = YSQReader.GetPrice("MSFT");
			price = Cleaner.CleanWebResult(price);
			Assert.IsNotNull(price);
			Assert.IsTrue(float.Parse(price) > 0);
		}

        [TestMethod]
        public void TestMethod()
        {
            var prices = YSQReader.GetHistoricalPrices("MSFT", new DateTime(2000, 1, 1), new DateTime(2015, 1, 1));
            Assert.IsNotNull(prices);
        }
	}
}
