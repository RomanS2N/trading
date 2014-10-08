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
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarsBuilder.Shared;
using SeriesReading.Descriptor.Quotes;
using SeriesReading;

namespace BarsBuilder.UnitTest {
  [TestClass]
  public class UnitTest {
    [TestMethod]
    public void TestMethod() {
      IBarsCreator creator = new BarsCreator(null, null, TimeSpan.FromMinutes(1), null);
      string path = new SeriesDescriptor()
          .InstrumentDescriptors.Single(x => x.Name == "EURUSD")
          .ProviderDescriptors.Single(x => x.Name == "Dukascopy")
          .Path;
      SeriesReader reader = new SeriesReader(path);
      DateTime dateTime;
      decimal ask, bid;
      while (reader.Next(out dateTime, out ask, out bid))
        creator.AddQuote(dateTime, ask);
    }
  }
}
