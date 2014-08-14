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
using Storage.Shared;

namespace Storage.Files {
	public class StorageContext : IStorageContext {

		private IStorageMedia _storageMedia;
		private object _storageMediaLock = new object();

		public string Name { get; private set; }

		public bool TryToInitialize(string name, IStorageMedia storageMedia) {
			if (_storageMedia == null) {
				lock (_storageMediaLock) {
					if (_storageMedia == null) {
						Name = name;
						_storageMedia = storageMedia;
						return true;
					}
				}
			}
			return false;
		}

		public void Save(InformationUnitId informationUnit, IStorableData data) {
			_storageMedia.Save(this, informationUnit, data);
		}

		public IStorableData Read(InformationUnitId informationUnit) {
			return _storageMedia.Read(this, informationUnit);
		}
	}
}
