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

namespace Charts {
  public class Sample {
    public DateTime Instant { get; set; }
    public double Value { get; set; }
    public Sample() { }
    public Sample(DateTime instant, double value) {
      this.Instant = instant;
      this.Value = value;
    }
    public override string ToString() {
      return this.Instant + " " + this.Value;
    }
    //public const int SampleBinarySize = sizeof(long) + sizeof(double);
    //public Sample()
    //{
    //}
    //public List<byte> GetBytes()
    //{
    //  List<byte> bytes = new List<byte>();
    //  bytes.AddRange(BitConverter.GetBytes(this.Instant.Ticks));
    //  bytes.AddRange(BitConverter.GetBytes(this.Value));
    //  return bytes;
    //}
  }
}
