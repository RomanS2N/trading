using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinancialData.Shared {
	[Serializable]
	public enum AssetType {
		Currency,
		CurrencyFuture,
		CurrencyForward,
		Stock,
	}
}
