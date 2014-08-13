using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Storage.Shared;

namespace Storage.Shared {
	public class SampleStorableData : IStorableData {

		List<int> values = new List<int>();

		public SampleStorableData() {
		}

		public SampleStorableData(IStorableData storableData)
			: this() {
			SetBytes(storableData.GetBytes());
		}

		public byte[] GetBytes() {
			List<byte> bytesList = new List<byte>();
			values.Select(value =>
				BitConverter.GetBytes(value))
					.ToList()
					.ForEach(bytes =>
						bytesList.AddRange(bytes));
			return bytesList.ToArray();
		}

		public void SetBytes(byte[] bytes) {
			throw new NotImplementedException();
		}

		public void AddValue(int value) {
			values.Add(value);
		}

		public int this(int index) {
		}
	}
}
