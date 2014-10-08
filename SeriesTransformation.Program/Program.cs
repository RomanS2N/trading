using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeriesTransformation.Shared;

namespace SeriesTransformation.Program {
	class Program {
		static void Main(string[] args) {
			ISeriesConverter converter = new SeriesConverter();
			QuotesProcessor processor = new QuotesProcessor(@"C:\quotes\EURUSD\Dukascopy\");
			DateTime begin = new DateTime(2012, 1, 1);
			DateTime end = new DateTime(2013, 1, 1);
			converter.ImportQuotes(@"C:\quotes\EURUSD\Dukascopy\EURUSD_DUKAS_TICKS.csv", processor, begin, end);
		}
	}
}
