using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinancialData.Shared;

namespace YahooStockQuote.FinancialDataProvider {
	public class YQSDataSource : IDataSource {
		public DataProvider Provider {
			get { return DataProvider.YahooStockQuote; }
		}

		public decimal ConvertPrice(decimal price) {
			return price;
		}
	}
}
