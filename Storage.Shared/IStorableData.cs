using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Storage.Shared {
	public interface IStorableData {

		byte[] GetBytes();

		void SetBytes(byte[] bytes);
	}
}
