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
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Session.Shared;
using Storage.Shared;

namespace Session.UnitTest {
	[TestClass]
	public class UnitTest {
		[TestMethod]
		public void TestMethod() {
			IStorageContext sessionStorageContext = SessionFactory.CurrentSession.GetStorageContext(InformationUnitType.Temporal);
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
			// los almaceno en el storage context
			sessionStorageContext.Save(informationUnit, sampleData);
			// los recupero del storage context
			SampleStorableData recoveredSampleData = new SampleStorableData(sessionStorageContext.Read(informationUnit));
			for (int i = 0; i < 100; i++) {
				Assert.AreEqual(sampleData[i], recoveredSampleData[i]);
			}
		}
	}
}
