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

namespace FinancialSeries.Shared {
  public class DateTimeAndPriceGroupReader : IDateTimeAndPricesReader {
    List<IDateTimeAndPriceReader> _readers = new List<IDateTimeAndPriceReader>();

    public void AddReader(IDateTimeAndPriceReader reader) {
      _readers.Add(reader);
    }

    struct Reading {
      public DateTime DateTime;
      public decimal Price;
      public bool Result;
    }

    public bool Next(out DateTime dateTime, out decimal[] prices) {
      var readings = _readers.Select(x => {
        Reading reading = new Reading();
        reading.Result = x.Next(out reading.DateTime, out reading.Price);
        return reading;
      }).ToList();

      if (readings.Count == 0) throw new Exception("Empty reader.");
      if (readings.Exists(x => x.Result != readings[0].Result)) throw new Exception("Corrupted files.");
      if (readings.Exists(x => x.DateTime != readings[0].DateTime)) throw new Exception("Corrupted files.");
      if (readings[0].Result) {
        dateTime = readings[0].DateTime;
        prices = new decimal[readings.Count];
        Array.ConstrainedCopy(readings.Select(x => x.Price).ToArray(), 0, prices, 0, readings.Count);
      }
      else {
        dateTime = default(DateTime);
        prices = default(decimal[]);
      }

      return readings[0].Result;
    }
  }
}
