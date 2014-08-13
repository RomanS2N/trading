using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Storage.Shared {
	public interface IStorageContext {
		string Name { get; }
		void Save(InformationUnitId informationUnit, IStorableData data);
		IStorableData Read(InformationUnitId informationUnit);
		bool TryToInitialize(string name, IStorageMedia storageMedia);
	}
}
