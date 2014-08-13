using FinancialData;
using FinancialData.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeUtils;

namespace YahooStockQuote.FinancialDataProvider {
	public class YSQProvider : IFinancialDataProvider {
		public ISample GetPrice(string symbol) {
			var price = YSQReader.GetPrice(symbol);
			return new Quote() { Symbol = symbol, DateTime = Instant.Now, Ask = decimal.Parse(price) };
		}
		private Bar BuildBar(string symbol, TimeSpan period, string text) {
			// Date,        Open,   High,   Low,    Close,  Volume,   Adj Close
			// 2014-07-01,  41.86,  42.15,  41.69,  41.87,  26917000, 41.87
			var parts = text.Split(new char[] { ',' });
			return new Bar {
				Symbol = symbol,
				Period = period,
				DateTime = DateTime.ParseExact(parts[0], "yyyy-MM-dd", CultureInfo.InvariantCulture),
				Open = decimal.Parse(parts[1]),
				High = decimal.Parse(parts[2]),
				Low = decimal.Parse(parts[3]),
				Close = decimal.Parse(parts[4]),
				Volume = int.Parse(parts[5]),
				AdjClose = decimal.Parse(parts[6]),
			};
		}
		public ISamplePackage GetHistory(string symbol, DateTime start, DateTime end) {
			List<string> data = YSQReader.GetHistoricalPrices(symbol, start, end);
			// first line must be discarded (titles)
			data.RemoveAt(0);
			TimeSpan period = TimeSpan.FromMinutes(1);
			return new BarPackage {
				Symbol = symbol,
				Period = period,
				Samples = data.Select(line => (IBar)BuildBar(symbol, period, line)).ToList()
			};
		}
	}
}
