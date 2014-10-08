using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeriesTransformation.Shared;
using System.IO;
using QuotesConversion;

namespace SeriesTransformation {
	public class SeriesConverter : ISeriesConverter {
		public void ImportQuotes(string sourceFilePath, IQuotesProcessor quotesProcessor, DateTime begin, DateTime end) {
			using (StreamReader reader = File.OpenText(sourceFilePath)) {
				string line;
				bool endOfSearchSpaceReached = false;
				while (!endOfSearchSpaceReached && (line = reader.ReadLine()) != null) {
					DateTime currentDateTime = QuotesConverter.GetQuoteDateTimeFromString(line);
					if (currentDateTime > end) {
						endOfSearchSpaceReached = true;
					}
					else if (currentDateTime >= begin) {
						quotesProcessor.StoreQuoteFromString(line);
					}
				}
			}
		}
	}
}
