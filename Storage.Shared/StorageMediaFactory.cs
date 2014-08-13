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
using System.Threading.Tasks;

namespace Storage.Shared {
	public class StorageMediaFactory {

		private static IStorageMediaManager _storageMediaManager;
		private static object _storageMediaManagerLock = new object();
		public static IStorageMediaManager StorageMediaManager {
			get {
				if (_storageMediaManager == null) {
					lock (_storageMediaManagerLock) {
						if (_storageMediaManager == null) {
							_storageMediaManager = Factory.Shared.Factory.Create<IStorageMediaManager>("IStorageMediaManager");
						}
					}
				}
				return _storageMediaManager;
			}
		}

		private static IStorageMedia _defaultStorageMedia;
		private static object _defaultStorageMediaLock = new object();
		public static IStorageMedia DefaultMedia {
			get {
				if (_defaultStorageMedia == null) {
					lock (_defaultStorageMediaLock) {
						if (_defaultStorageMedia == null) {
							_defaultStorageMedia = Factory.Shared.Factory.Create<IStorageMedia>("DefaultStorageMedia");
						}
					}
				}
				return _defaultStorageMedia;
			}
		}

		//private static IStorageContext _sessionStorageContext;
		//private static object _sessionStorageContextLock = new object();
		//public static IStorageContext DefaultStorageContext {
		//    get {
		//        if (_sessionStorageContext == null) {
		//            lock (_sessionStorageContextLock) {
		//                if (_sessionStorageContext == null) {
		//                    _sessionStorageContext = Factory.Shared.Factory.Create<IStorageContext>("SessionStorageContext");
		//                    if (!_sessionStorageContext.TryToInitialize(StorageMediaFactory.DefaultMedia)) {
		//                        throw new Exception("There was an error building the default storage context based on default media.");
		//                    }
		//                }
		//            }
		//        }
		//        return _sessionStorageContext;
		//    }
		//}
	}
}

