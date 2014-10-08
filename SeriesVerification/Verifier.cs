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
using QuotesConversion;
using System.IO;
using System.Threading;

namespace SeriesVerification {
  public class Verifier {
    public static bool VerifySequentiality(string filePath) {
      DateTime previousDateTime = DateTime.MinValue;
      DateTime dateTime;
      for (int i = 0; i < 10; i++) {
        try {
          using (StreamReader reader = File.OpenText(filePath)) {
            while (QuotesConverter.GetQuoteDateTimeFromStreamReader(reader, out dateTime)) {
              if (dateTime < previousDateTime) {
                return false;
              }
            }
          }
        }
        catch (IOException ioe) {
          Console.WriteLine(ioe.Message);
          Thread.Sleep(1000);
        }
      }
      return true;
    }
  }
}
