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
using SeriesReading.Shared;
using SeriesTransformation;

namespace SeriesReading.UnitTest {
  [TestClass]
  public class UnitTest {
    [TestMethod]
    public void TestMethod() {
      string rootFolder = @"C:\quotes\EURUSD\Dukascopy\";
      DateTime begin = new DateTime(2003, 06, 01, 15, 31, 49);
      DateTime end = new DateTime(2003, 06, 15, 18, 55, 16);
      ISeriesReader reader = new SeriesReader(rootFolder, begin, end);
      DateTime dateTime;
      decimal ask, bid;
      while (reader.Next(out dateTime, out ask, out bid)) {
        Assert.IsTrue(dateTime > begin);
        Assert.IsTrue(dateTime <= end);
        Assert.IsTrue(ask >= bid);
      }
    }
  }
}
