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
using FinancialSeriesUtils;
using QuotesConversion;

namespace SeriesTransformation {
  public class QuotesProcessor : IQuotesProcessor {
    private char[] _separator = new char[] { ',' };
    private string _rootFolder;
    private DateTime _epoch = new DateTime(1970, 1, 1);
    private int _dayOfYear = -1;
    private BinaryWriter _writer;
    public QuotesProcessor(string targetFolderPath) {
      _rootFolder = targetFolderPath;
    }
    public void StoreQuoteFromString(string text) {
      long time;
      DateTime dateTime;
      decimal ask, bid;
      QuotesConverter.GetQuoteFromString(text, out dateTime, out time, out ask, out bid);
      if (_dayOfYear != dateTime.DayOfYear) {
        if (_writer != null) {
          _writer.Close();
        }
        string folderPath;
        string filePath;
        PathBuilder.CreatePaths(_rootFolder, dateTime, out folderPath, out filePath);
        Directory.CreateDirectory(folderPath);
        _writer = new BinaryWriter(File.Create(filePath));
        _dayOfYear = dateTime.DayOfYear;
      }
      _writer.Write((Int64)time);
      _writer.Write(ask);
      _writer.Write(bid);
    }

  }
}
