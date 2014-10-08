﻿/*
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

using SeriesReading;
using SeriesReading.Descriptor.Quotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarsReading.Program {
  class Program {
    static void Main(string[] args) {
      string path = new SeriesDescriptor()
          .InstrumentDescriptors.Single(x => x.Name == "EURUSD")
          .ProviderDescriptors.Single(x => x.Name == "Dukascopy")
          .Path;

      BarsReader reader = BarsReader.Create(TimeSpan.FromHours(1), path/*, new DateTime(2010, 1, 1), new DateTime(2010, 2, 1)*/);

      DateTime dateTime;
      decimal price;

      while (reader.Next(out dateTime, out price)) {
        Console.WriteLine("{0} -> {1}", dateTime, price);
      }

      Console.ReadLine();
    }
  }
}
