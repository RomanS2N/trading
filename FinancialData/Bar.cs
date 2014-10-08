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

using FinancialData.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialData {
  public class Bar : IBar {
    public SampleType SampleType {
      get { return SampleType.Bar; }
    }
    public Asset Asset { get; set; }
    public IDataSource Source { get; set; }
    public long BarIndex { get; private set; }
    public TimeSpan TimeFrame { get; set; }
    public DateTime DateTime { get; set; }
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Close { get; set; }
    public long Volume { get; set; }
    public decimal AdjClose { get; set; }
    public int SamplesCount { get; private set; }
    public DateTime LastDateTime { get; private set; }

    public Bar(Asset asset, IDataSource source, TimeSpan timeFrame, DateTime dateTime) {
      TimeFrame = timeFrame;
      BarIndex = dateTime.Ticks / TimeFrame.Ticks;
      LastDateTime = DateTime = new DateTime(TimeFrame.Ticks * BarIndex);
    }

    public Bar(Asset asset, IDataSource source, TimeSpan timeFrame, DateTime dateTime, decimal price)
      : this(asset, source, timeFrame, dateTime) {
      Open = High = Low = Close = price;
      SamplesCount++;
    }

    public Bar(Asset asset, IDataSource source, TimeSpan timeFrame, DateTime dateTime, decimal open, decimal high, decimal low, decimal close, long volume)
      : this(asset, source, timeFrame, dateTime) {
      Open = open;
      High = high;
      Low = low;
      Close = close;
      Volume = volume;
      SamplesCount++;
    }

    public Bar(Asset asset, IDataSource source, TimeSpan timeFrame, DateTime dateTime, decimal open, decimal high, decimal low, decimal close, long volume, decimal adjClose)
      : this(asset, source, timeFrame, dateTime, open, high, low, close, volume) {
      AdjClose = adjClose;
    }

    public void Update(DateTime dateTime, decimal price) {
      if (dateTime < LastDateTime) {
        throw new Exception("Invalid DateTime for this Bar (mixed times).");
      }
      if (BarIndex != (dateTime.Ticks / TimeFrame.Ticks)) {
        throw new Exception("Invalid DateTime for this Bar (out of bar sample).");
      }
      if (price > High) High = price;
      if (price < Low) Low = price;
      Close = price;
      LastDateTime = dateTime;
      SamplesCount++;
    }

    public byte[] GetBytes() {
      List<byte> bytes = new List<byte>();
      bytes.AddRange(BitConverter.GetBytes(DateTime.Ticks));
      bytes.AddRange(BitConverter.GetBytes((long)(Open * 1000000)));
      bytes.AddRange(BitConverter.GetBytes((long)(High * 1000000)));
      bytes.AddRange(BitConverter.GetBytes((long)(Low * 1000000)));
      bytes.AddRange(BitConverter.GetBytes((long)(Close * 1000000)));
      return bytes.ToArray();
    }

    public override string ToString() {
      return string.Format(
          "Bar of {0} index [{1}] => {2}, samples {3:000} -> {4:0.000000} / {5:0.000000} / {6:0.000000} / {7:0.000000} - {8} - {9:0.000000}",
          TimeFrame, BarIndex, DateTime, SamplesCount, Open, High, Low, Close, Volume, AdjClose);
    }
  }
}
