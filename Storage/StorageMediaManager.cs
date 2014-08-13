using Storage.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage {
	public class StorageMediaManager : IStorageMediaManager {

		Dictionary<string, IStorageMedia> _media = new Dictionary<string, IStorageMedia>();

		public void RegisterMedia(string name, IStorageMedia storageMedia) {
			_media[name] = storageMedia;
		}

		public IStorageMedia this[string index] {
			get { return _media[index]; }
		}
	}
}
