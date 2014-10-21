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

namespace DukascopyQuote.UnitTest {
	[TestClass]
	public class UnitTest {
		[TestMethod]
		public void TestMethod() {
			int samples = 0;
			DukascopyOfflineReader.GetHistoricalPrices(
				@"C:\hfdata\Temp\EURUSD_DUKAS_TICKS.txt",
				(Func<string, bool>)(line => {
					Console.WriteLine(line);
					Assert.IsNotNull(line);
					Assert.IsTrue(line.Length > 0);
					return (++samples < 5);
				}));
			Assert.AreEqual(5, samples);
		}
	}
}
