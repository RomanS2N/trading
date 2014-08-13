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
			string folder = string.Format(@"{0}\{1}\", _rootFolder, context.Name);
			Directory.CreateDirectory(folder);
			string path = string.Format(@"{0}\{1}", folder, informationUnit.Name);
			File.WriteAllBytes(path, data.GetBytes());
		}

		public IStorableData Read(IStorageContext context, InformationUnitId informationUnit) {
			string path = string.Format(@"{0}\{1}\{2}", _rootFolder, context.Name, informationUnit.Name);
			return new RawBytesStorableData(File.ReadAllBytes(path));
		}
	}
}
