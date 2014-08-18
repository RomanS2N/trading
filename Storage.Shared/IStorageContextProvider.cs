using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Storage.Shared {
	public interface IStorageContextProvider {
		IStorageContext GetStorageContext(InformationUnitType informationUnitType);
	}
}
