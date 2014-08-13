using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TradingConfiguration.Shared {
	[Serializable]
	public class AssetConfiguration {
		public string Name { get; set; }
		public AssetType Type { get; set; }
	}
}
