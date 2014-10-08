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
using System.IO;

namespace SeriesAcquisition.Shared {
  public class RawDataInformation {

    public DateTime Begin { get; set; }
    public DateTime End { get; set; }

    private RawDataInformation() { }

    public static RawDataInformation FromPath(string filename) {
      if (File.Exists(filename)) {
        string[] parts = new FileInfo(filename).Name.Split(new string[] { "__", "." }, StringSplitOptions.RemoveEmptyEntries);
        return new RawDataInformation {
          Begin = DateTime.ParseExact(parts[0], "yyyy_MM_dd_HH_mm_ss", null),
          End = DateTime.ParseExact(parts[1], "yyyy_MM_dd_HH_mm_ss", null),
        };
      }
      return null;
    }
  }
}
