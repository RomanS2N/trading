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
using System.IO;
using System.Linq;
using System.Text;

namespace SeriesReading.Descriptor.Quotes {
  public class DayDescriptor : IDescriptor {
    private string _dayFile;
    public DayDescriptor(string dayFile) {
      _dayFile = dayFile;
      Name = new FileInfo(_dayFile).Name;
    }
    public string Name { get; private set; }
    public string Path { get { return _dayFile; } }
    public List<IDescriptor> ChildDescriptors {
      get { return null; }
    }
  }
}
