using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Shared {
	public interface IStorageMediaManager {

		void RegisterMedia(string name, IStorageMedia storageMedia);

		IStorageMedia this[string index] { get; }
	}
}
