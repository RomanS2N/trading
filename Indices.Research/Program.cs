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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YahooStockQuote.FinancialDataProvider;
using TaLib.Extension;

namespace Indices.Research {
  class Program {
    static void Main(string[] args) {
      var symbol = YSQSymbol.YSQIndex.SNP;
      var begin = new DateTime(2000, 1, 1);
      var end = new DateTime(2015, 1, 1);
      var samplePackage = new YSQProvider().GetHistory(new Asset(symbol, AssetType.Index), begin, end, null);
      var barPackage = (IBarPackage)samplePackage;
      var bars = barPackage.Samples;
      //bars.ForEach(x => Console.WriteLine(x));

      IBar lastBar = null;
      List<double> deltas = new List<double>();
      foreach (var bar in bars) {
        if (lastBar != null) {
          var delta = lastBar.Close - bar.Open;
          deltas.Add((double)delta);
        }
        lastBar = bar;
      }

      // TODO solve this
      //var smoothDeltas = deltas.ToArray().SMA(6);

      //Console.WriteLine("Press a key to exit...");
      //Console.ReadKey(true);
    }
  }
}
