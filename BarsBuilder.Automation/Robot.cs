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

#define _VERBOSE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BarsBuilder.Shared;
using SeriesReading.Descriptor.Quotes;
using SeriesReading;
using FinancialSeriesUtils;

namespace BarsBuilder.Automation {
  public class Robot {
    public void Start() {
      new List<TimeSpan> { FinancialTimeSpans.M1 }.ForEach(timeFrame => {
        IBarsCreator creator = new BarsCreator(null, null, timeFrame, @"C:\quotes\EURUSD\Dukascopy\");
        string path = new SeriesDescriptor()
            .InstrumentDescriptors.Single(x => x.Name == "EURUSD")
            .ProviderDescriptors.Single(x => x.Name == "Dukascopy")
            .Path;
        SeriesReader reader = new SeriesReader(path);
        DateTime dateTime;
        decimal ask, bid;
        int lastMonth = -1;
        while (reader.Next(out dateTime, out ask, out bid)) {
          creator.AddQuote(dateTime, ask);
#if _VERBOSE
          if (dateTime.Month != lastMonth) {
            Console.WriteLine("{0} -> {1}/{2} -> {3}", dateTime, ask, bid, creator.BarsCount);
            lastMonth = dateTime.Month;
          }
#endif
        }
        creator.Finish();
      });
    }
  }
}
