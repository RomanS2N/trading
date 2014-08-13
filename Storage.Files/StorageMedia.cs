using Storage.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Configuration.Shared;
using System.IO;

namespace Storage.Files {
	public class StorageMedia : IStorageMedia {

		private string _rootFolder;

		public StorageMedia() {
			_rootFolder = ConfigurationFactory.Configuration["FileStorageMediaRootFolder"];
		}

		public void Save(IStorageContext context, InformationUnitId informationUnit, IStorableData data) {
			string path = string.Format(@"{0}\{1}\{2}", _rootFolder, context.Name, informationUnit.Name);
			File.WriteAllBytes(path, data.GetBytes());
		}

		public IStorableData Read(IStorageContext context, InformationUnitId informationUnit) {
			string path = string.Format(@"{0}\{1}\{2}", _rootFolder, context.Name, informationUnit.Name);
			return new RawBytesStorableData(File.ReadAllBytes(path));
		}
	}
}
