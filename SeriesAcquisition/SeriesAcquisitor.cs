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
using System.Diagnostics;
using FinancialConfiguration;
using System.IO;

namespace SeriesAcquisition {
  public class SeriesAcquisitor {

    static DateTime _epoch = new DateTime(1970, 1, 1);

    public static void RequestTicks(string symbol, DateTime begin, DateTime end, string outputFolder) {
      if (!Directory.Exists(Configuration.Instace.JarsFolder)) {
        throw new Exception(string.Format("Jars folder not found!"));
      }
      string jarPath = string.Format(@"{0}\DukascopyAsyncHistoryManager.jar", Configuration.Instace.JarsFolder);
      string jnlpName = @"https://www.dukascopy.com/client/demo/jclient/jforex.jnlp";
      string username = @"DEMO2ysLbt";
      string password = @"ysLbt";
      long beginMs = (long)(begin - _epoch).TotalMilliseconds;
      long endMs = (long)(end - _epoch).TotalMilliseconds;
      string arguments = string.Format(@"-jar {0} {1} {2} {3} 1 {4} {5} {6} {7} ticks",
        jarPath, jnlpName, username, password, symbol, outputFolder, beginMs, endMs);
      Console.WriteLine("Starting acquisitor with arguments: {0}", arguments);
      if (!File.Exists(Configuration.Instace.JavaPath)) {
        throw new Exception(string.Format("Java interpreter not found!"));
      }
      ProcessStartInfo startInfo = new ProcessStartInfo(Configuration.Instace.JavaPath, arguments);
      startInfo.WindowStyle = ProcessWindowStyle.Minimized;
      Process process = Process.Start(startInfo);
      Console.WriteLine("Process Id: {0}", process.Id);
      process.WaitForExit();
      Console.WriteLine("Process exited");
    }

    public static void CreateFolders() {
      Configuration.Instace.Instruments.ForEach(instrument => {
        Configuration.Instace.Providers.ForEach(provider => {
          string symbol = instrument.Replace(@"/", "");
          string path = string.Format(@"{0}\{1}\{2}\", Configuration.Instace.DataRoot, symbol, provider);
          Directory.CreateDirectory(path);
        });
      });
    }
  }
}
