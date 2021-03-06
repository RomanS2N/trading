﻿/*
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
using Storage.Shared;

namespace Session.Shared {
	public class Session : IStorageContextProvider {
		private object _contextsLock = new object();
		private Dictionary<InformationUnitType, IStorageContext> _storageContexts = new Dictionary<InformationUnitType, IStorageContext>();
		public string Name { get; private set; }
		public Session() {
			Name = SessionNameGenerator.CreateName();
		}
		public IStorageContext GetStorageContext(InformationUnitType informationUnitType) {
			if (!_storageContexts.ContainsKey(informationUnitType)) {
				lock (_contextsLock) {
					if (!_storageContexts.ContainsKey(informationUnitType)) {
						_storageContexts[informationUnitType] = Factory.Shared.Factory.Create<IStorageContext>("SessionStorageContext");
						if (!_storageContexts[informationUnitType].TryToInitialize(informationUnitType.ToString(), StorageMediaFactory.DefaultMedia)) {
							throw new Exception("There was an error building the default storage context based on default media.");
						}
					}
				}
			}
			return _storageContexts[informationUnitType];
		}
	}
}
