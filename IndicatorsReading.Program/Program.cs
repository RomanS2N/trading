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
using FinancialSeries.Shared;
using FinancialSeriesUtils;
using SeriesReading.Descriptor.Quotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndicatorsReading.Program {
  class Program {
    static void Main(string[] args) {
      string path = new SeriesDescriptor()
          .InstrumentDescriptors.Single(x => x.Name == "EURUSD")
          .ProviderDescriptors.Single(x => x.Name == "Dukascopy")
          .Path;

      // creo un groupReader para leer los bars e indicadores de forma coordinada
      var groupReader = new DateTimeAndPriceGroupReader();

      // voy a consumir el barsReader a través del groupReader
      BarsReader barsReader = BarsReader.Create(TimeSpan.FromHours(1), path);
      groupReader.AddReader(barsReader);

      // también los indicadores basados en horas
      FinancialTimeSpans.Hours.ForEach(timeFrame => {
        IndicatorsReader indicatorsReader = IndicatorsReader.Create(new RSI(25), timeFrame, path);
        groupReader.AddReader(indicatorsReader);
      });

      DateTime groupDateTime;
      decimal[] barAndIndicatorsPrices;

      while (groupReader.Next(out groupDateTime, out barAndIndicatorsPrices)) {
        StringBuilder sb = new StringBuilder();
        barAndIndicatorsPrices.ToList().ForEach(price => {
          if (sb.Length > 0) sb.Append(", ");
          sb.AppendFormat("{0:0.000000}", price);
        });
        Console.WriteLine("{0} -> [{1}]", groupDateTime, sb.ToString());
      }

      Console.ReadLine();
    }
  }
}
