//#define _VERBOSE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BarsBuilder.Shared;
using Bars.Shared;
using System.IO;

namespace BarsBuilder
{
    public class BarsCreator : IBarsCreator
    {
        public TimeSpan TimeFrame { get; private set; }
        private List<Bar> _bars = new List<Bar>();
        public int BarsCount { get; private set; }
        public int StoredBars { get; private set; }
        private Bar _lastBar;
        private long _timeFrameTicks;
        public BarsCreator(TimeSpan timeFrame, string storagePath)
        {
            TimeFrame = timeFrame;
            _timeFrameTicks = TimeFrame.Ticks;
            StoragePath = storagePath;
            if (storagePath == null)
                throw new Exception("storagePath can't be null.");
        }
        private void AddBar(Bar newBar)
        {
#if _VERBOSE
            if (BarsCount > 0)
                Console.WriteLine(_lastBar);
#endif
            _bars.Add(newBar);
            _lastBar = newBar;
            BarsCount++;

            BackupBars(1000000);
        }

        private void BackupBars(int barsToRemove, bool removeAll = false)
        {
            if (BarsCount > barsToRemove || removeAll)
            {
                CreateFile();
                Console.WriteLine("Saving {0} bars to {1}", barsToRemove, StoragePath);
                for (int i = 0; i < barsToRemove; i++)
                {
                    _writer.Write(_bars[i].Close/*.GetBytes()*/);
                }
                _bars.RemoveRange(0, barsToRemove);
                BarsCount -= barsToRemove;
                StoredBars += barsToRemove;

                if (removeAll)
                {
                    _writer.Flush();
                    _writer.Close();
                }
            }
        }

        private void UpdateLastBar(DateTime dateTime, decimal price)
        {
            _lastBar.Update(dateTime, price);
        }

        private void FillTheGap(long lastBarIndex, long newQuoteBarIndex, decimal priceToFillWith)
        {
            long suposedNextBarIndex = lastBarIndex + 1;

            if (newQuoteBarIndex != suposedNextBarIndex)
            {
                long barIndex = suposedNextBarIndex;
                while (barIndex < newQuoteBarIndex)
                {
                    DateTime barBegin = new DateTime(barIndex * TimeFrame.Ticks);
                    AddBar(new Bar(TimeFrame, barBegin, priceToFillWith));
                    barIndex++;
                }
            }
        }

        public void AddQuote(DateTime newQuoteDateTime, decimal newQuotePrice)
        {
            if (BarsCount == 0)
            {
                AddBar(new Bar(TimeFrame, newQuoteDateTime, newQuotePrice));
                return;
            }

            long lastBarIndex = _lastBar.BarIndex;
            long newQuoteBarIndex = newQuoteDateTime.Ticks / TimeFrame.Ticks;

            if (newQuoteBarIndex == lastBarIndex)
            {
                UpdateLastBar(newQuoteDateTime, newQuotePrice);
                return;
            }

            FillTheGap(lastBarIndex, newQuoteBarIndex, _lastBar.Close);
            // todo bar empieza con el close del anterior
            DateTime begin = new DateTime(newQuoteBarIndex * TimeFrame.Ticks);
            AddBar(new Bar(TimeFrame, begin, _lastBar.Close));
            // agrego la info nueva
            UpdateLastBar(newQuoteDateTime, newQuotePrice);
        }

        //public void Save(string filePath)
        //{
        //    if (File.Exists(filePath)) File.Delete(filePath);
        //    using (BinaryWriter writer = new BinaryWriter(File.Create(filePath)))
        //    {
        //        _bars.ForEach(bar => writer.Write(bar.Close/*.GetBytes()*/));
        //    }
        //}

        BinaryWriter _writer;

        public string StoragePath { get; private set; }

        public string FilePath { get; private set; }

        private long _firstQuoteBarIndex;

        private void CreateFile()
        {
            if (FilePath == null)
            {
                if (BarsCount <= 0)
                    throw new Exception("Unable to create the filePath based on first bar information.");
                Bar firstBar = _bars[0];
                _firstQuoteBarIndex = firstBar.DateTime.Ticks / _timeFrameTicks;
                FilePath = string.Format(@"{0}\BARS_TF_{1}__IX_{2}.bin", StoragePath, _timeFrameTicks, _firstQuoteBarIndex);
                if (File.Exists(FilePath)) File.Delete(FilePath);
                if (_writer != null) _writer.Close();
                _writer = new BinaryWriter(File.OpenWrite(FilePath));
            }
        }

        public void Finish()
        {
            BackupBars(BarsCount, true);
            VerifyConsistency();
        }

        private void VerifyConsistency()
        {
            if (_lastBar != null)
            {
                // -1 porque la primera bar también fue almaceneda
                long theoreticalLastBarIndex = _firstQuoteBarIndex + StoredBars - 1;
                if (_lastBar.BarIndex != theoreticalLastBarIndex)
                    throw new Exception("Inconsistent bars creation.");
            }
        }
    }
}
