﻿/*
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
