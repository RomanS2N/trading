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
using System.IO;
using System.Linq;
using System.Text;

namespace IndicatorsReading {
  /// <summary>
  /// Hay que recordar que la información de indicadores se basa en una serie original de bars. De ahí que 
  /// guarde relación en el nombrado de variables con aspectos de las bars.
  /// </summary>
  public class IndicatorFileInfo {
    public string Name { get; private set; }
    public int Period { get; private set; }
    public string FilePath { get; private set; }
    public TimeSpan TimeFrame { get; private set; }
    private FileInfo _fileInfo;
    public long FirstQuoteBarIndex { get; private set; }
    public long SamplesInFile { get; private set; }
    public long LastQuoteBarIndex { get; private set; }
    public DateTime FirstBarDateTime { get; private set; }
    public DateTime LastBarDateTime { get; private set; }
    public IndicatorFileInfo(string filePath, TimeSpan timeFrame) {
      FilePath = filePath;
      TimeFrame = timeFrame;
      _fileInfo = new FileInfo(filePath);
      var fileNameParts = _fileInfo.Name.Split(new char[] { '_', '.' }, StringSplitOptions.RemoveEmptyEntries);
      Name = fileNameParts[0];
      Period = int.Parse(fileNameParts[1]);
      if (timeFrame.Ticks != long.Parse(fileNameParts[3])) throw new Exception("Invalid timeFrame.");
      FirstQuoteBarIndex = long.Parse(fileNameParts[5]);
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
