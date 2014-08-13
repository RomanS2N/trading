using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Storage.Shared {
	public class RawBytesStorableData : IStorableData {

		byte[] _bytes;

		public RawBytesStorableData(byte[] bytes) {
			SetBytes(bytes);
		}

		public byte[] GetBytes() {
			return _bytes;
		}

		public void SetBytes(byte[] bytes) {
			_bytes = bytes;
		}
	}
}
