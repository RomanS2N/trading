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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Storage.Shared;

namespace Storage.Shared {
	public class SampleStorableData : IStorableData {

		private List<int> _values = new List<int>();

		public SampleStorableData() {
		}

		public SampleStorableData(IStorableData storableData)
			: this() {
			SetBytes(storableData.GetBytes());
		}

		public byte[] GetBytes() {
			List<byte> bytesList = new List<byte>();
			_values.Select(value =>
				BitConverter.GetBytes(value))
					.ToList()
					.ForEach(bytes =>
						bytesList.AddRange(bytes));
			return bytesList.ToArray();
		}

		public void SetBytes(byte[] bytes) {
			int count = bytes.Length / 4;
			for (int i = 0; i < count; i++) {
				_values.Add(BitConverter.ToInt32(bytes, i * 4));
			}
		}

		public void AddValue(int value) {
			_values.Add(value);
		}

		public int this[int index] {
			get {
				return _values[index];
			}
		}
	}
}
