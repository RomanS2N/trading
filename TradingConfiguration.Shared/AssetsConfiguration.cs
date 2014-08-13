using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TradingConfiguration.Shared {
	[Serializable]
	public class AssetsConfiguration {
		public List<AssetConfiguration> Assets { get; set; }
	}
}
