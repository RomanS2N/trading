using Bars.Shared;
using FinancialSeries.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BarsReading
{
    public class BarsReader : IDateTimeAndPriceReader
    {
        public TimeSpan TimeFrame { get; private set; }

        //private List<ThinBar> _bars = new List<ThinBar>();

        //public int BarsCount { get; private set; }

        //public int StoredBars { get; private set; }

        public string StoragePath { get; private set; }

        public string FilePath { get; private set; }

        private long _timeFrameTicks;

        private Queue<BarFileInfo> _barFileInfos = new Queue<BarFileInfo>();

        public static BarsReader Create(TimeSpan timeFrame, string storagePath)
        {
            try
            {
                return new BarsReader(timeFrame, storagePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private BarsReader(TimeSpan timeFrame, string storagePath)
        {
            TimeFrame = timeFrame;
            _timeFrameTicks = TimeFrame.Ticks;
            StoragePath = storagePath;
            //_begin = begin;
            //_end = end;
            if (storagePath == null)
                throw new Exception("storagePath can't be null.");
            MapFiles();
        }

        private void MapFiles()
        {
            if (FilePath == null)
            {
                string pattern = string.Format(@"BARS_TF_{0}__IX_*.bin", _timeFrameTicks);
                var files = Directory.EnumerateFiles(StoragePath, pattern, SearchOption.TopDirectoryOnly);
                files
                    .Select(filePath => new BarFileInfo(filePath, TimeFrame))
                    .OrderBy(barFileInfo => barFileInfo.FirstBarDateTime)
                    .ToList()
                    .ForEach(barFileInfo => _barFileInfos.Enqueue(barFileInfo));

                if (_barFileInfos.Count > 1)
                    throw new Exception("Many files case not supported yet (debug and test).");

                if (_barFileInfos.Count == 0)
                    throw new Exception(string.Format("Bar files to be readed ({0}) not found.", pattern));
            }
        }

        private BinaryReader _reader;
        private long _consumedBars = 0;

        private BarFileInfo _currentBarFileInfo;
        private BarFileInfo NextBarFileInfo()
        {
            _currentBarFileInfo = _barFileInfos.Dequeue();
            return _currentBarFileInfo;
        }

        public bool Next(out DateTime dateTime, out decimal price)
        {
            if (!GetPriceFromBinaryReader(_reader, out dateTime, out price))
            {
                if (_barFileInfos.Count == 0)
                {
                    dateTime = default(DateTime);
                    price = default(decimal);
                    return false;
                }
                if (_reader != null) _reader.Close();
                string path = NextBarFileInfo().FilePath;
                while (!File.Exists(path))
                {
                    if (_barFileInfos.Count > 0)
                    {
                        path = NextBarFileInfo().FilePath;
                    }
                    else
                    {
                        dateTime = default(DateTime);
                        price = default(decimal);
                        return false;
                    }
                }
                _reader = new BinaryReader(File.OpenRead(path));
                if (!GetPriceFromBinaryReader(_reader, out dateTime, out price))
                {
                    throw new Exception("Empty file found.");
                }
            }
            return true;
        }

        public bool GetPriceFromBinaryReader(BinaryReader reader, out DateTime dateTime, out decimal price)
        {
            if (reader == null)
            {
                dateTime = default(DateTime);
                price = default(decimal);
                return false;
            }
            try
            {
                price = reader.ReadDecimal();
                dateTime = new DateTime(_currentBarFileInfo.TimeFrame.Ticks * (_currentBarFileInfo.FirstQuoteBarIndex + _consumedBars++));
                return true;
            }
            catch (Exception)
            {
                dateTime = default(DateTime);
                price = default(decimal);
                return false;
            }
        }

        public int ReadAll(out DateTime[] dateTimes, out double[] prices)
        {
            var ldateTimes = new List<DateTime>();
            var lprices = new List<double>();
            DateTime dateTime;
            decimal price;
            while (Next(out dateTime, out price))
            {
                ldateTimes.Add(dateTime);
                lprices.Add((double)price);
            }
            dateTimes = ldateTimes.ToArray();
            prices = lprices.ToArray();
            return prices.Length;
        }
    }
}
