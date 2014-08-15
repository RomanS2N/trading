using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinancialData.Shared {
	public interface IProvisionContext {
		string Source { get; }
		SampleType SampleType { get; }
		TimeSpan Period { get; set; }
	}
}
