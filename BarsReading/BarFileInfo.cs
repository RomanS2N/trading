using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BarsReading
{
    public class BarFileInfo
    {
        public string FilePath { get; private set; }
        public TimeSpan TimeFrame { get; private set; }
        private FileInfo _fileInfo;
        public long FirstQuoteBarIndex { get; private set; }
        public long SamplesInFile { get; private set; }
        public long LastQuoteBarIndex { get; private set; }
        public DateTime FirstBarDateTime { get; private set; }
        public DateTime LastBarDateTime { get; private set; }
        public BarFileInfo(string filePath, TimeSpan timeFrame)
        {
            FilePath = filePath;
            TimeFrame = timeFrame;
            _fileInfo = new FileInfo(filePath);
            var fileNameParts = _fileInfo.Name.Split(new char[] { '_', '.' }, StringSplitOptions.RemoveEmptyEntries);
            FirstQuoteBarIndex = long.Parse(fileNameParts[4]);
            var samplesInFileRemainder = _fileInfo.Length % (double)sizeof(decimal);
            if (samplesInFileRemainder > 0)
                throw new Exception("Corrupted file.");
            SamplesInFile = _fileInfo.Length / sizeof(decimal);
            // -1 porque entre los samples se encuentra el primero
            LastQuoteBarIndex = FirstQuoteBarIndex + (long)SamplesInFile - 1;

            FirstBarDateTime = new DateTime(TimeFrame.Ticks * FirstQuoteBarIndex);
            LastBarDateTime = new DateTime(TimeFrame.Ticks * LastQuoteBarIndex);
        }
    }
}
