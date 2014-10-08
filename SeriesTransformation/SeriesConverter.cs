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
using SeriesTransformation.Shared;
using System.IO;
using QuotesConversion;

namespace SeriesTransformation {
  public class SeriesConverter : ISeriesConverter {
    public void ImportQuotes(string sourceFilePath, IQuotesProcessor quotesProcessor, DateTime begin, DateTime end) {
      using (StreamReader reader = File.OpenText(sourceFilePath)) {
        string line;
        bool endOfSearchSpaceReached = false;
        while (!endOfSearchSpaceReached && (line = reader.ReadLine()) != null) {
          DateTime currentDateTime = QuotesConverter.GetQuoteDateTimeFromString(line);
          if (currentDateTime > end) {
            endOfSearchSpaceReached = true;
          }
          else if (currentDateTime >= begin) {
            quotesProcessor.StoreQuoteFromString(line);
          }
        }
      }
    }
  }
}
