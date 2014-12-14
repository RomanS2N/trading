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
using System.Threading.Tasks;

namespace Nasdaq.StocksProvider {
  public class Provider {
    public static List<NasdaqStock> GetStocks() {
      var files = new List<string> { @"C:\stocks\AMEX.csv", @"C:\stocks\NASDAQ.csv", @"C:\stocks\NYSE.csv" };
      var lines = files.SelectMany(file => {
        var _lines = File.ReadAllLines(file).ToList();
        _lines.RemoveAt(0);
        return _lines;
      });
      var stocks = lines.Select(line => new NasdaqStock(line));
      return stocks.ToList();
    }
  }
}
