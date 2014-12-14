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
using System.Threading.Tasks;

namespace Nasdaq.StocksProvider {
  public class NasdaqStock {
    public string Symbol { get; private set; }
    public string Name { get; private set; }
    public string LastSale { get; private set; }
    public string MarketCap { get; private set; }
    public string ADR_TSO { get; private set; }
    public string IPOyear { get; private set; }
    public string Sector { get; private set; }
    public string Industry { get; private set; }
    public string SummaryQuote { get; private set; }
    public NasdaqStock(string line) {
      //"Symbol","Name","LastSale","MarketCap","ADR TSO","IPOyear","Sector","industry","Summary Quote",
      var parts = line.Split(new char[] { '"', ',' }, StringSplitOptions.RemoveEmptyEntries);
      int i = 0;
      Symbol = parts[i++];
      Name = parts[i++];
      LastSale = parts[i++];
      MarketCap = parts[i++];
      ADR_TSO = parts[i++];
      IPOyear = parts[i++];
      Sector = parts[i++];
      Industry = parts[i++];
      SummaryQuote = parts[i++];
    }
  }
}
