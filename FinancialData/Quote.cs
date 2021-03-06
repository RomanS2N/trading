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

using FinancialData.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeUtils;

namespace FinancialData {
  public class Quote : IQuote {
    private static CultureInfo _culture = CultureInfo.GetCultureInfo("en-US");
    public Asset Asset { get; set; }
    public IDataSource Source { get; set; }
    public DateTime DateTime { get; set; }
    public decimal Ask { get; set; }
    public int AskSize { get; set; }
    public decimal Bid { get; set; }
    public int BidSize { get; set; }

    public SampleType SampleType {
      get { return SampleType.Quote; }
    }

    public static Quote From3PartsString(string line, Shared.Asset asset, IDataSource source) {
      var parts = line.Split(new char[] { ',' });
      return new Quote {
        Asset = asset,
        Source = source,
        DateTime = Instant.FromMillisAfterEpoch(long.Parse(parts[0])),
        Ask = source.ConvertPrice(decimal.Parse(parts[1], _culture)),
        AskSize = 0,
        Bid = source.ConvertPrice(decimal.Parse(parts[2], _culture)),
        BidSize = 0,
      };
    }
  }
}
