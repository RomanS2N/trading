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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Storage.Shared;

namespace Storage.UnitTest {
	[TestClass]
	public class UnitTest {
		[TestMethod]
		public void TestMethod() {
#if _
			// creo y recupero el storage media
			//IStorageMediaManager mediaManager = StorageMediaFactory.StorageMediaManager;
			//mediaManager.RegisterMedia("default", StorageMediaFactory.DefaultMedia);
			//IStorageMedia storageMedia = mediaManager["default"];
			IStorageContext storageContext = StorageMediaFactory.DefaultStorageContext;
			// creo una unidad de información
			InformationUnitId informationUnit = new InformationUnitId {
				Name = "SampleData",
				Type = InformationUnitType.Temporal,
			};
			// creo los datos para esa unidad de información
			SampleStorableData sampleData = new SampleStorableData();
			for (int i = 0; i < 100; i++) {
				sampleData.AddValue(i);
			}
			// los almaceno en el storage media
			storageContext.Save(informationUnit, sampleData);
#endif
		}
	}
}
