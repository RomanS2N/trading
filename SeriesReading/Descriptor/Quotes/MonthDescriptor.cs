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
  public class MonthDescriptor : IDescriptor {
    private string _monthFolder;
    private List<DayDescriptor> _dayDescriptors = new List<DayDescriptor>();
    public MonthDescriptor(string monthFolder) {
      _monthFolder = monthFolder;
      Name = new DirectoryInfo(_monthFolder).Name;
      Directory.EnumerateFiles(_monthFolder).ToList().ForEach(dayFile => {
        _dayDescriptors.Add(new DayDescriptor(dayFile));
      });
    }
    public string Name { get; private set; }
    public string Path { get { return _monthFolder; } }
    public List<IDescriptor> ChildDescriptors {
      get { return _dayDescriptors.Cast<IDescriptor>().ToList(); }
    }
    public List<DayDescriptor> DayDescriptors { get { return _dayDescriptors; } }
  }
}
