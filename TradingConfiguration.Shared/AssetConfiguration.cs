﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinancialData.Shared;

namespace TradingConfiguration.Shared {
	[Serializable]
	public class AssetConfiguration {
		public string Name { get; set; }
		public string ShortName { get; set; }
		public AssetType Type { get; set; }
	}
}
