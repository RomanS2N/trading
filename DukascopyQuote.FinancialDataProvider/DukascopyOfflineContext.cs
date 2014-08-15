using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinancialData.Shared;

namespace DukascopyQuote.FinancialDataProvider {
	public class DukascopyOfflineContext : IProvisionContext {

		public string Source { get; set; }
		public SampleType SampleType { get; set; }
		public TimeSpan Period { get; set; }

		public DukascopyOfflineContext(string filepath, SampleType sampleType, TimeSpan period) {
			Source = filepath;
			SampleType = sampleType;
			Period = period;
		}
	}
}
