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

using BarsReading;
using FinancialIndicator;
using FinancialSeriesUtils;
using SeriesReading.Descriptor.Quotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Indicators.Automation {
  public class Robot {
    public void Start() {
      FinancialTimeSpans.All.ForEach(timeFrame => {
        string path = new SeriesDescriptor()
            .InstrumentDescriptors.Single(x => x.Name == "EURUSD")
            .ProviderDescriptors.Single(x => x.Name == "Dukascopy")
            .Path;

        BarsReader reader = BarsReader.Create(timeFrame, path);

        if (reader != null) {
          IndicatorsCreator creator = new IndicatorsCreator(timeFrame, path);
          creator.AddIndicator(new RSI(15));
          creator.AddIndicator(new RSI(20));
          creator.AddIndicator(new RSI(25));

          DateTime dateTime;
          decimal price;
          int lastMonth = -1;

          while (reader.Next(out dateTime, out price)) {
            creator.Update(dateTime, price);
            if (dateTime.Month != lastMonth) {
              Console.WriteLine("{0} -> {1} -> {2}", dateTime, price, CreateString(creator.Values));
              lastMonth = dateTime.Month;
            }
          }

          creator.Finish();
        }
      });
    }

    private string CreateString(List<decimal> values) {
      StringBuilder sb = new StringBuilder();
      values.ForEach(value => sb.AppendFormat("{0:0.000000} ", value));
      return sb.ToString();
    }
  }
}
