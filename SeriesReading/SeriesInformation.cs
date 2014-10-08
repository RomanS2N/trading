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
using SeriesReading.Descriptor.Quotes;

namespace SeriesReading {
  public class SeriesInformation {

    public SeriesInformation(string providerFolder) {
      ProviderDescriptor descriptor = new ProviderDescriptor(providerFolder);
      var filePaths = descriptor.YearDescriptors
        .SelectMany(y => y.MonthDescriptors
          .SelectMany(m => m.DayDescriptors
            .Select(d => d.Path)));
      DateTime dateTime;
      decimal ask, bid;
      SeriesReader.CreateReaderForSingleFile(filePaths.First()).Next(out dateTime, out ask, out bid);
      Begin = dateTime;
      var reader = SeriesReader.CreateReaderForSingleFile(filePaths.Last());
      while (reader.Next(out dateTime, out ask, out bid)) {
        End = dateTime;
      }
    }

    public DateTime Begin { get; set; }

    public DateTime End { get; set; }
  }
}
