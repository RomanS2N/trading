/*
   Copyright 2014 Samuel Pets (internetuser0x00@gmail.com)

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Context.Shared {
	public class ContextFactory {
		private static Dictionary<UniqueContextType, Context> _contexts = new Dictionary<UniqueContextType, Context>();
		private static object _contextsLock = new object();
		public static Context GetContext(UniqueContextType type) {
			if (!_contexts.ContainsKey(type)) {
				lock (_contextsLock) {
					if (!_contexts.ContainsKey(type)) {
						_contexts[type] = new Context(type);
					}
				}
			}
			return _contexts[type];
		}

		public static object GetSubcontext(Context context, ContextType contextType) {
			//context.Subcontexts.
			throw new NotImplementedException();
		}
	}
}
