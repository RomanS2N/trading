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
using System.IO;
using System.Globalization;

namespace QuotesConversion {
  public class QuotesConverter {
    private static char[] _separator = new char[] { ',' };
    private static CultureInfo _culture = CultureInfo.GetCultureInfo("en-US");
    private static DateTime _epoch = new DateTime(1970, 1, 1);
    public static DateTime GetQuoteDateTimeFromString(string text) {
      string[] parts = text.Split(_separator);
      long time = long.Parse(parts[0]);
      return _epoch + TimeSpan.FromMilliseconds(time);
    }
    public static void GetQuoteDateTimeFromString(string text, out DateTime dateTime) {
      string[] parts = text.Split(_separator);
      long time = long.Parse(parts[0]);
      dateTime = _epoch + TimeSpan.FromMilliseconds(time);
    }
    public static void GetQuoteDateTimeFromString(string text, out DateTime dateTime, out long time) {
      string[] parts = text.Split(_separator);
      time = long.Parse(parts[0]);
      dateTime = _epoch + TimeSpan.FromMilliseconds(time);
    }
    public static void GetQuoteFromString(string text, out DateTime dateTime, out decimal ask, out decimal bid) {
      string[] parts = text.Split(_separator);
      long time = long.Parse(parts[0]);
      dateTime = _epoch + TimeSpan.FromMilliseconds(time);
      ask = decimal.Parse(parts[1], _culture);
      bid = decimal.Parse(parts[2], _culture);
    }
    public static void GetQuoteFromString(string text, out DateTime dateTime, out long time, out decimal ask, out decimal bid) {
      string[] parts = text.Split(_separator);
      time = long.Parse(parts[0]);
      dateTime = _epoch + TimeSpan.FromMilliseconds(time);
      ask = decimal.Parse(parts[1], _culture);
      bid = decimal.Parse(parts[2], _culture);
    }
    public static bool GetQuoteDateTimeFromBinaryReader(BinaryReader reader, out DateTime dateTime) {
      if (reader == null) {
        dateTime = default(DateTime);
        return false;
      }
      try {
        long time = reader.ReadInt64();
        dateTime = _epoch + TimeSpan.FromMilliseconds(time);
        reader.ReadDecimal();
        reader.ReadDecimal();
        return true;
      }
      catch (Exception) {
        dateTime = default(DateTime);
        return false;
      }
    }
    public static bool GetQuoteDateTimeFromStreamReader(StreamReader reader, out DateTime dateTime) {
      if (reader == null) {
        dateTime = default(DateTime);
        return false;
      }
      try {
        string line = reader.ReadLine();
        GetQuoteDateTimeFromString(line, out dateTime);
        return true;
      }
      catch (Exception) {
        dateTime = default(DateTime);
        return false;
      }
    }
    public static bool GetQuoteFromBinaryReader(BinaryReader reader, out DateTime dateTime, out decimal ask, out decimal bid) {
      if (reader == null) {
        dateTime = default(DateTime);
        ask = default(decimal);
        bid = default(decimal);
        return false;
      }
      try {
        long time = reader.ReadInt64();
        dateTime = _epoch + TimeSpan.FromMilliseconds(time);
        ask = reader.ReadDecimal();
        bid = reader.ReadDecimal();
        return true;
      }
      catch (Exception) {
        dateTime = default(DateTime);
        ask = default(decimal);
        bid = default(decimal);
        return false;
      }
    }
  }
}
