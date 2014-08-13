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
