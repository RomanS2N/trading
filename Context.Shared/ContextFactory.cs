using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Context.Shared {
	public class ContextFactory {
		private static Dictionary<UniqueContextType, Context> _contexts = new Dictionary<UniqueContextType, Context>();
		private static object _contextsLock = new object();
		public static Context Create(UniqueContextType type) {
			if (!_contexts.ContainsKey(type)) {
				lock (_contextsLock) {
					if (!_contexts.ContainsKey(type)) {
						_contexts[type] = new Context(type);
					}
				}
			}
			return _contexts[type];
		}
	}
}
