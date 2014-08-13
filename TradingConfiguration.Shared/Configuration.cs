using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TradingConfiguration.Shared {
	[Serializable]
	public class Configuration {

		private static Configuration _instance;
		private static object _instanceLock = new object();

		public static Configuration Instance {
			get {
				if (_instance == null) {
					lock (_instanceLock) {
						if (_instance == null) {
							_instance = Configuration.Deserialize();
						}
					}
				}
				return _instance;
			}
		}

		private Configuration() { }

		public AssetsConfiguration AssetsConfiguration { get; set; }

		private static Configuration Deserialize() {
			throw new NotImplementedException();
		}
	}
}
