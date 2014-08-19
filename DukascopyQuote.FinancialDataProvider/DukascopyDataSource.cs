using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinancialData.Shared;

namespace DukascopyQuote.FinancialDataProvider {
	public class DukascopyDataSource : IDataSource {

		private decimal _priceScale = 100000;

		public DataProvider Provider { get { return DataProvider.Dukascopy; } }

		public decimal ConvertPrice(decimal price) {
			return price / _priceScale;
		}
	}
}
