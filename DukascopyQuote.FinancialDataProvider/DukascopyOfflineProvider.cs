﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinancialData.Shared;
using FinancialData;
using System.Globalization;

namespace DukascopyQuote.FinancialDataProvider {
	public class DukascopyOfflineProvider : IFinancialDataProvider {
		public ISample GetPrice(string symbol) {
			throw new NotImplementedException();
		}
		private Bar BuildBar(string symbol, TimeSpan period, string text) {
			// Time,Open,High,Low,Close,Volume
			// 01.01.2014 00:00:00.000,1.06180,1.06180,1.06180,1.06180,0.00
			var parts = text.Split(new char[] { ',' });
			return new Bar {
				Symbol = symbol,
				Period = period,
				DateTime = DateTime.ParseExact(parts[0], "dd.MM.yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture),
				Open = decimal.Parse(parts[1]),
				High = decimal.Parse(parts[2]),
				Low = decimal.Parse(parts[3]),
				Close = decimal.Parse(parts[4]),
				Volume = (int)decimal.Parse(parts[5]),
			};
		}
		public ISamplePackage GetHistory(string symbol, DateTime start, DateTime end, IProvisionContext provisionContext) {
			List<string> data = DukascopyOfflineReader.GetHistoricalPrices(provisionContext.Source);
			switch (provisionContext.SampleType) {
				case SampleType.Bar:
					// first line must be discarded (titles)
					data.RemoveAt(0);
					TimeSpan period = provisionContext.Period;
					return new BarPackage {
						Symbol = symbol,
						Period = period,
						Samples = data.Select(line => (IBar)BuildBar(symbol, period, line)).ToList()
					};
				case SampleType.Quote:
				default:
					throw new NotImplementedException();
			}
		}
	}
}