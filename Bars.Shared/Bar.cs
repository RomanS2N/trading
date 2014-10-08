using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bars.Shared
{
    public class Bar
    {
        public long BarIndex { get; private set; }
        public TimeSpan TimeFrame { get; private set; }
        public DateTime DateTime { get; private set; }
        public decimal Open { get; private set; }
        public decimal High { get; private set; }
        public decimal Low { get; private set; }
        public decimal Close { get; private set; }
        public int SamplesCount { get; private set; }
        public DateTime LastDateTime { get; private set; }

        public Bar(TimeSpan timeFrame, DateTime dateTime, decimal price)
        {
            TimeFrame = timeFrame;
            BarIndex = dateTime.Ticks / TimeFrame.Ticks;
            LastDateTime = DateTime = new DateTime(TimeFrame.Ticks * BarIndex);
            Open = High = Low = Close = price;
            SamplesCount++;
        }

        public void Update(DateTime dateTime, decimal price)
        {
            if (dateTime < LastDateTime)
            {
                throw new Exception("Invalid DateTime for this Bar (mixed times).");
            }
            if (BarIndex != (dateTime.Ticks / TimeFrame.Ticks))
            {
                throw new Exception("Invalid DateTime for this Bar (out of bar sample).");
            }
            if (price > High) High = price;
            if (price < Low) Low = price;
            Close = price;
            LastDateTime = dateTime;
            SamplesCount++;
        }

        //public byte[] GetBytes()
        //{
        //    List<byte> bytes = new List<byte>();
        //    bytes.AddRange(BitConverter.GetBytes(DateTime.Ticks));
        //    bytes.AddRange(BitConverter.GetBytes((long)(Open * 1000000)));
        //    bytes.AddRange(BitConverter.GetBytes((long)(High * 1000000)));
        //    bytes.AddRange(BitConverter.GetBytes((long)(Low * 1000000)));
        //    bytes.AddRange(BitConverter.GetBytes((long)(Close * 1000000)));
        //    return bytes.ToArray();
        //}

        public override string ToString()
        {
            return string.Format(
                "Bar of {0} index [{1}] => {2}, samples {3:000} -> {4:0.000000} / {5:0.000000} / {6:0.000000} / {7:0.000000}",
                TimeFrame, BarIndex, DateTime, SamplesCount, Open, High, Low, Close);
        }
    }
}
