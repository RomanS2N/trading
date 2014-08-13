using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Shared {
	public interface IStorageMedia {
		void Save(IStorageContext context, InformationUnitId informationUnit, IStorableData data);
		IStorableData Read(IStorageContext context, InformationUnitId informationUnit);
	}
}
