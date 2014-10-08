using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeriesTransformation.Shared {
	public interface ISeriesConverter {
		void ImportQuotes(string sourceFilePath, IQuotesProcessor quotesProcessor, DateTime begin, DateTime end);
	}
}
