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
