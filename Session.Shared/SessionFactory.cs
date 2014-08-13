/*
   Copyright 2014 Samuel Pets

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
using Storage.Shared;

namespace Session.Shared {
	public class SessionFactory {
		private static Session _session;
		private static object _sessionLock = new object();
		public static Session CurrentSession {
			get {
				if (_session == null) {
					lock (_sessionLock) {
						if (_session == null) {
							_session = new Session();
						}
					}
				}
				return _session;
			}
		}
	}
}
