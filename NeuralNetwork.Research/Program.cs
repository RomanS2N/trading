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

// http://www.quaetrix.com/Build2013.html

// For 2013 Microsoft Build Conference attendees
// June 25-28, 2013
// San Francisco, CA
//
// This is source for a C# console application.
// To compile you can 1.) create a new Visual Studio
// C# console app project named BuildNeuralNetworkDemo
// then zap away the template code and replace with this code,
// or 2.) copy this code into notepad, save as NeuralNetworkProgram.cs
// on your local machine, launch the special VS command shell
// (it knows where the csc.exe compiler is), cd-navigate to
// the directory containing the .cs file, type 'csc.exe
// NeuralNetworkProgram.cs' and hit enter, and then after 
// the compiler creates NeuralNetworkProgram.exe, you can
// run from the command line.
//
// This is an enhanced neural network. It is fully-connected
// and feed-forward. The training algorithm is back-propagation
// with momentum and weight decay. The input data is normalized
// so training is quite fast.
//
// You can use this code however you wish subject to the usual disclaimers
// (use at your own risk, etc.)

using FinancialData.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YahooStockQuote.FinancialDataProvider;
using TaLib.Extension;

namespace NeuralNetwork.Research {
  class Program {

    static void TrainWithSampleData() {

      List<double[]> data = new List<double[]>();

      data.Add(new double[] { 5.1, 3.5, 1.4, 0.2, 0, 0, 1 });
      data.Add(new double[] { 4.9, 3.0, 1.4, 0.2, 0, 0, 1 });
      data.Add(new double[] { 4.7, 3.2, 1.3, 0.2, 0, 0, 1 });
      data.Add(new double[] { 4.6, 3.1, 1.5, 0.2, 0, 0, 1 });
      data.Add(new double[] { 5.0, 3.6, 1.4, 0.2, 0, 0, 1 });
      data.Add(new double[] { 5.4, 3.9, 1.7, 0.4, 0, 0, 1 });
      data.Add(new double[] { 4.6, 3.4, 1.4, 0.3, 0, 0, 1 });
      data.Add(new double[] { 5.0, 3.4, 1.5, 0.2, 0, 0, 1 });
      data.Add(new double[] { 4.4, 2.9, 1.4, 0.2, 0, 0, 1 });
      data.Add(new double[] { 4.9, 3.1, 1.5, 0.1, 0, 0, 1 });

      data.Add(new double[] { 5.4, 3.7, 1.5, 0.2, 0, 0, 1 });
      data.Add(new double[] { 4.8, 3.4, 1.6, 0.2, 0, 0, 1 });
      data.Add(new double[] { 4.8, 3.0, 1.4, 0.1, 0, 0, 1 });
      data.Add(new double[] { 4.3, 3.0, 1.1, 0.1, 0, 0, 1 });
      data.Add(new double[] { 5.8, 4.0, 1.2, 0.2, 0, 0, 1 });
      data.Add(new double[] { 5.7, 4.4, 1.5, 0.4, 0, 0, 1 });
      data.Add(new double[] { 5.4, 3.9, 1.3, 0.4, 0, 0, 1 });
      data.Add(new double[] { 5.1, 3.5, 1.4, 0.3, 0, 0, 1 });
      data.Add(new double[] { 5.7, 3.8, 1.7, 0.3, 0, 0, 1 });
      data.Add(new double[] { 5.1, 3.8, 1.5, 0.3, 0, 0, 1 });

      data.Add(new double[] { 5.4, 3.4, 1.7, 0.2, 0, 0, 1 });
      data.Add(new double[] { 5.1, 3.7, 1.5, 0.4, 0, 0, 1 });
      data.Add(new double[] { 4.6, 3.6, 1.0, 0.2, 0, 0, 1 });
      data.Add(new double[] { 5.1, 3.3, 1.7, 0.5, 0, 0, 1 });
      data.Add(new double[] { 4.8, 3.4, 1.9, 0.2, 0, 0, 1 });
      data.Add(new double[] { 5.0, 3.0, 1.6, 0.2, 0, 0, 1 });
      data.Add(new double[] { 5.0, 3.4, 1.6, 0.4, 0, 0, 1 });
      data.Add(new double[] { 5.2, 3.5, 1.5, 0.2, 0, 0, 1 });
      data.Add(new double[] { 5.2, 3.4, 1.4, 0.2, 0, 0, 1 });
      data.Add(new double[] { 4.7, 3.2, 1.6, 0.2, 0, 0, 1 });

      data.Add(new double[] { 4.8, 3.1, 1.6, 0.2, 0, 0, 1 });
      data.Add(new double[] { 5.4, 3.4, 1.5, 0.4, 0, 0, 1 });
      data.Add(new double[] { 5.2, 4.1, 1.5, 0.1, 0, 0, 1 });
      data.Add(new double[] { 5.5, 4.2, 1.4, 0.2, 0, 0, 1 });
      data.Add(new double[] { 4.9, 3.1, 1.5, 0.1, 0, 0, 1 });
      data.Add(new double[] { 5.0, 3.2, 1.2, 0.2, 0, 0, 1 });
      data.Add(new double[] { 5.5, 3.5, 1.3, 0.2, 0, 0, 1 });
      data.Add(new double[] { 4.9, 3.1, 1.5, 0.1, 0, 0, 1 });
      data.Add(new double[] { 4.4, 3.0, 1.3, 0.2, 0, 0, 1 });
      data.Add(new double[] { 5.1, 3.4, 1.5, 0.2, 0, 0, 1 });

      data.Add(new double[] { 5.0, 3.5, 1.3, 0.3, 0, 0, 1 });
      data.Add(new double[] { 4.5, 2.3, 1.3, 0.3, 0, 0, 1 });
      data.Add(new double[] { 4.4, 3.2, 1.3, 0.2, 0, 0, 1 });
      data.Add(new double[] { 5.0, 3.5, 1.6, 0.6, 0, 0, 1 });
      data.Add(new double[] { 5.1, 3.8, 1.9, 0.4, 0, 0, 1 });
      data.Add(new double[] { 4.8, 3.0, 1.4, 0.3, 0, 0, 1 });
      data.Add(new double[] { 5.1, 3.8, 1.6, 0.2, 0, 0, 1 });
      data.Add(new double[] { 4.6, 3.2, 1.4, 0.2, 0, 0, 1 });
      data.Add(new double[] { 5.3, 3.7, 1.5, 0.2, 0, 0, 1 });
      data.Add(new double[] { 5.0, 3.3, 1.4, 0.2, 0, 0, 1 });

      data.Add(new double[] { 7.0, 3.2, 4.7, 1.4, 0, 1, 0 });
      data.Add(new double[] { 6.4, 3.2, 4.5, 1.5, 0, 1, 0 });
      data.Add(new double[] { 6.9, 3.1, 4.9, 1.5, 0, 1, 0 });
      data.Add(new double[] { 5.5, 2.3, 4.0, 1.3, 0, 1, 0 });
      data.Add(new double[] { 6.5, 2.8, 4.6, 1.5, 0, 1, 0 });
      data.Add(new double[] { 5.7, 2.8, 4.5, 1.3, 0, 1, 0 });
      data.Add(new double[] { 6.3, 3.3, 4.7, 1.6, 0, 1, 0 });
      data.Add(new double[] { 4.9, 2.4, 3.3, 1.0, 0, 1, 0 });
      data.Add(new double[] { 6.6, 2.9, 4.6, 1.3, 0, 1, 0 });
      data.Add(new double[] { 5.2, 2.7, 3.9, 1.4, 0, 1, 0 });

      data.Add(new double[] { 5.0, 2.0, 3.5, 1.0, 0, 1, 0 });
      data.Add(new double[] { 5.9, 3.0, 4.2, 1.5, 0, 1, 0 });
      data.Add(new double[] { 6.0, 2.2, 4.0, 1.0, 0, 1, 0 });
      data.Add(new double[] { 6.1, 2.9, 4.7, 1.4, 0, 1, 0 });
      data.Add(new double[] { 5.6, 2.9, 3.6, 1.3, 0, 1, 0 });
      data.Add(new double[] { 6.7, 3.1, 4.4, 1.4, 0, 1, 0 });
      data.Add(new double[] { 5.6, 3.0, 4.5, 1.5, 0, 1, 0 });
      data.Add(new double[] { 5.8, 2.7, 4.1, 1.0, 0, 1, 0 });
      data.Add(new double[] { 6.2, 2.2, 4.5, 1.5, 0, 1, 0 });
      data.Add(new double[] { 5.6, 2.5, 3.9, 1.1, 0, 1, 0 });

      data.Add(new double[] { 5.9, 3.2, 4.8, 1.8, 0, 1, 0 });
      data.Add(new double[] { 6.1, 2.8, 4.0, 1.3, 0, 1, 0 });
      data.Add(new double[] { 6.3, 2.5, 4.9, 1.5, 0, 1, 0 });
      data.Add(new double[] { 6.1, 2.8, 4.7, 1.2, 0, 1, 0 });
      data.Add(new double[] { 6.4, 2.9, 4.3, 1.3, 0, 1, 0 });
      data.Add(new double[] { 6.6, 3.0, 4.4, 1.4, 0, 1, 0 });
      data.Add(new double[] { 6.8, 2.8, 4.8, 1.4, 0, 1, 0 });
      data.Add(new double[] { 6.7, 3.0, 5.0, 1.7, 0, 1, 0 });
      data.Add(new double[] { 6.0, 2.9, 4.5, 1.5, 0, 1, 0 });
      data.Add(new double[] { 5.7, 2.6, 3.5, 1.0, 0, 1, 0 });

      data.Add(new double[] { 5.5, 2.4, 3.8, 1.1, 0, 1, 0 });
      data.Add(new double[] { 5.5, 2.4, 3.7, 1.0, 0, 1, 0 });
      data.Add(new double[] { 5.8, 2.7, 3.9, 1.2, 0, 1, 0 });
      data.Add(new double[] { 6.0, 2.7, 5.1, 1.6, 0, 1, 0 });
      data.Add(new double[] { 5.4, 3.0, 4.5, 1.5, 0, 1, 0 });
      data.Add(new double[] { 6.0, 3.4, 4.5, 1.6, 0, 1, 0 });
      data.Add(new double[] { 6.7, 3.1, 4.7, 1.5, 0, 1, 0 });
      data.Add(new double[] { 6.3, 2.3, 4.4, 1.3, 0, 1, 0 });
      data.Add(new double[] { 5.6, 3.0, 4.1, 1.3, 0, 1, 0 });
      data.Add(new double[] { 5.5, 2.5, 4.0, 1.3, 0, 1, 0 });

      data.Add(new double[] { 5.5, 2.6, 4.4, 1.2, 0, 1, 0 });
      data.Add(new double[] { 6.1, 3.0, 4.6, 1.4, 0, 1, 0 });
      data.Add(new double[] { 5.8, 2.6, 4.0, 1.2, 0, 1, 0 });
      data.Add(new double[] { 5.0, 2.3, 3.3, 1.0, 0, 1, 0 });
      data.Add(new double[] { 5.6, 2.7, 4.2, 1.3, 0, 1, 0 });
      data.Add(new double[] { 5.7, 3.0, 4.2, 1.2, 0, 1, 0 });
      data.Add(new double[] { 5.7, 2.9, 4.2, 1.3, 0, 1, 0 });
      data.Add(new double[] { 6.2, 2.9, 4.3, 1.3, 0, 1, 0 });
      data.Add(new double[] { 5.1, 2.5, 3.0, 1.1, 0, 1, 0 });
      data.Add(new double[] { 5.7, 2.8, 4.1, 1.3, 0, 1, 0 });

      data.Add(new double[] { 6.3, 3.3, 6.0, 2.5, 1, 0, 0 });
      data.Add(new double[] { 5.8, 2.7, 5.1, 1.9, 1, 0, 0 });
      data.Add(new double[] { 7.1, 3.0, 5.9, 2.1, 1, 0, 0 });
      data.Add(new double[] { 6.3, 2.9, 5.6, 1.8, 1, 0, 0 });
      data.Add(new double[] { 6.5, 3.0, 5.8, 2.2, 1, 0, 0 });
      data.Add(new double[] { 7.6, 3.0, 6.6, 2.1, 1, 0, 0 });
      data.Add(new double[] { 4.9, 2.5, 4.5, 1.7, 1, 0, 0 });
      data.Add(new double[] { 7.3, 2.9, 6.3, 1.8, 1, 0, 0 });
      data.Add(new double[] { 6.7, 2.5, 5.8, 1.8, 1, 0, 0 });
      data.Add(new double[] { 7.2, 3.6, 6.1, 2.5, 1, 0, 0 });

      data.Add(new double[] { 6.5, 3.2, 5.1, 2.0, 1, 0, 0 });
      data.Add(new double[] { 6.4, 2.7, 5.3, 1.9, 1, 0, 0 });
      data.Add(new double[] { 6.8, 3.0, 5.5, 2.1, 1, 0, 0 });
      data.Add(new double[] { 5.7, 2.5, 5.0, 2.0, 1, 0, 0 });
      data.Add(new double[] { 5.8, 2.8, 5.1, 2.4, 1, 0, 0 });
      data.Add(new double[] { 6.4, 3.2, 5.3, 2.3, 1, 0, 0 });
      data.Add(new double[] { 6.5, 3.0, 5.5, 1.8, 1, 0, 0 });
      data.Add(new double[] { 7.7, 3.8, 6.7, 2.2, 1, 0, 0 });
      data.Add(new double[] { 7.7, 2.6, 6.9, 2.3, 1, 0, 0 });
      data.Add(new double[] { 6.0, 2.2, 5.0, 1.5, 1, 0, 0 });

      data.Add(new double[] { 6.9, 3.2, 5.7, 2.3, 1, 0, 0 });
      data.Add(new double[] { 5.6, 2.8, 4.9, 2.0, 1, 0, 0 });
      data.Add(new double[] { 7.7, 2.8, 6.7, 2.0, 1, 0, 0 });
      data.Add(new double[] { 6.3, 2.7, 4.9, 1.8, 1, 0, 0 });
      data.Add(new double[] { 6.7, 3.3, 5.7, 2.1, 1, 0, 0 });
      data.Add(new double[] { 7.2, 3.2, 6.0, 1.8, 1, 0, 0 });
      data.Add(new double[] { 6.2, 2.8, 4.8, 1.8, 1, 0, 0 });
      data.Add(new double[] { 6.1, 3.0, 4.9, 1.8, 1, 0, 0 });
      data.Add(new double[] { 6.4, 2.8, 5.6, 2.1, 1, 0, 0 });
      data.Add(new double[] { 7.2, 3.0, 5.8, 1.6, 1, 0, 0 });

      data.Add(new double[] { 7.4, 2.8, 6.1, 1.9, 1, 0, 0 });
      data.Add(new double[] { 7.9, 3.8, 6.4, 2.0, 1, 0, 0 });
      data.Add(new double[] { 6.4, 2.8, 5.6, 2.2, 1, 0, 0 });
      data.Add(new double[] { 6.3, 2.8, 5.1, 1.5, 1, 0, 0 });
      data.Add(new double[] { 6.1, 2.6, 5.6, 1.4, 1, 0, 0 });
      data.Add(new double[] { 7.7, 3.0, 6.1, 2.3, 1, 0, 0 });
      data.Add(new double[] { 6.3, 3.4, 5.6, 2.4, 1, 0, 0 });
      data.Add(new double[] { 6.4, 3.1, 5.5, 1.8, 1, 0, 0 });
      data.Add(new double[] { 6.0, 3.0, 4.8, 1.8, 1, 0, 0 });
      data.Add(new double[] { 6.9, 3.1, 5.4, 2.1, 1, 0, 0 });

      data.Add(new double[] { 6.7, 3.1, 5.6, 2.4, 1, 0, 0 });
      data.Add(new double[] { 6.9, 3.1, 5.1, 2.3, 1, 0, 0 });
      data.Add(new double[] { 5.8, 2.7, 5.1, 1.9, 1, 0, 0 });
      data.Add(new double[] { 6.8, 3.2, 5.9, 2.3, 1, 0, 0 });
      data.Add(new double[] { 6.7, 3.3, 5.7, 2.5, 1, 0, 0 });
      data.Add(new double[] { 6.7, 3.0, 5.2, 2.3, 1, 0, 0 });
      data.Add(new double[] { 6.3, 2.5, 5.0, 1.9, 1, 0, 0 });
      data.Add(new double[] { 6.5, 3.0, 5.2, 2.0, 1, 0, 0 });
      data.Add(new double[] { 6.2, 3.4, 5.4, 2.3, 1, 0, 0 });
      data.Add(new double[] { 5.9, 3.0, 5.1, 1.8, 1, 0, 0 });

      Trainer.Execute(data);
    }

    static void TrainWithIndexData() {
      List<double[]> data = new List<double[]>();

      var symbol = YSQSymbol.YSQIndex.SNP;
      var begin = new DateTime(2000, 1, 1);
      var end = new DateTime(2015, 1, 1);
      var samplePackage = new YSQProvider().GetHistory(new Asset { Name = symbol, Type = AssetType.Index }, begin, end, null);
      var barPackage = (IBarPackage)samplePackage;
      var samples = barPackage.Samples;

      //samples.ForEach(x => Console.WriteLine(x));

      //var openValues = samples.Select(x => (double)x.Open).ToArray();
      var indicator1 = samples.RSI(15);
      var indicator2 = samples.RSI(6);
      var indicator3 = samples.SMA(24);
      var indicator4 = samples.SMA(10);

      var stay = new double[] { 1, 0, 0 };
      var goLong = new double[] { 0, 1, 0 };
      var goShort = new double[] { 0, 0, 1 };

      var deltaPrices = samples.Select(x => x.Close - x.Open).ToList();
      var action = deltaPrices.Select(deltaPrice => {
        var abs = Math.Abs(deltaPrice);
        if (abs < 20) return stay;
        if (deltaPrice < 0) return goShort;
        return goLong;
      }).ToList();

      Console.WriteLine("Times to stay: {0}", action.Count(x => x == stay));
      Console.WriteLine("Times to go long: {0}", action.Count(x => x == goLong));
      Console.WriteLine("Times to go short: {0}", action.Count(x => x == goShort));

      int samplesCount = samples.Count;

      for (int i = 0; i < samplesCount; i++) {
        var entry = new double[] { 
          indicator1.InstantValues[i].Value, 
          indicator2.InstantValues[i].Value, 
          indicator3.InstantValues[i].Value, 
          indicator4.InstantValues[i].Value, 
          action[i][0], 
          action[i][1], 
          action[i][2] };
        data.Add(entry);
      }

      Trainer.Execute(data);
    }
    static void Main(string[] args) {
      TrainWithIndexData();
    }
  }
}
