using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesTransformation.Shared;

namespace SeriesTransformation.UnitTest {
	[TestClass]
	public class UnitTest {
		[TestMethod]
		public void TestImportQuotes() {
			ISeriesConverter converter = new SeriesConverter();
			QuotesProcessor processor = new QuotesProcessor(@"C:\quotes\EURUSD\Dukascopy\");
			DateTime begin = new DateTime(2010, 1, 1);
			DateTime end = new DateTime(2010, 7, 1);
			converter.ImportQuotes(@"C:\hfdata\Temp\EURUSD_DUKAS_TICKS.csv", processor, begin, end);
		}
	}
}
