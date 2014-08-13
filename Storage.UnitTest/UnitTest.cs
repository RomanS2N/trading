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
