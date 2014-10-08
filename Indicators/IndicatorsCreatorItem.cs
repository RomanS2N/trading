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

using Indicators.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Indicators {
  public class IndicatorsCreatorItem {
    public IFinancialIndicator Indicator { get; private set; }
    private List<decimal> _values = new List<decimal>();
    BinaryWriter _writer;
    public TimeSpan TimeFrame { get; private set; }
    public string StoragePath { get; private set; }
    public string FilePath { get; private set; }
    private long _timeFrameTicks;
    private bool _receivedSamples = false;
    private DateTime _firstDateTime;
    private long _firstQuoteBarIndex;

    public IndicatorsCreatorItem(TimeSpan timeFrame, string storagePath, IFinancialIndicator indicator) {
      TimeFrame = timeFrame;
      _timeFrameTicks = timeFrame.Ticks;
      StoragePath = storagePath;
      Indicator = indicator;
    }

    public void Update(DateTime dateTime, decimal price) {
      // sólo para el primer sample
      if (!_receivedSamples) {
        _firstDateTime = dateTime;
        _firstQuoteBarIndex = _firstDateTime.Ticks / _timeFrameTicks;
        _receivedSamples = true;
      }

      // actualización y recuperación de valor de indicador
      Indicator.Update(price);
      _values.Add(Indicator.Value);

      // intento de persistencia
      BackupValues(false);
    }

    public void Finish() {
      // persistencia forzada
      BackupValues(true);

      // cierre de descriptores
      if (_writer != null) {
        _writer.Flush();
        _writer.Close();
        _writer = null;
      }
    }

    public void BackupValues(bool forceBackup) {
      if (_values.Count > 10000 || forceBackup) {
        // aseguro la construcción del path de archivo y la creación del descriptor
        CreateFile();

        //Console.WriteLine("Saving {0} indicator values to {1}", _values.Count, StoragePath);

        // guardo todo
        _values.ForEach(_writer.Write);
        _values.Clear();
      }
    }

    private void CreateFile() {
      if (FilePath == null) {
        if (_receivedSamples) {
          // construcción de path y creación de descriptor
          FilePath = string.Format(@"{0}\{1}_TF_{2}__IX_{3}.bin", StoragePath, Indicator.Identifier, _timeFrameTicks, _firstQuoteBarIndex);
          if (File.Exists(FilePath)) File.Delete(FilePath);
          if (_writer != null) _writer.Close();
          _writer = new BinaryWriter(File.OpenWrite(FilePath));
        }
      }
    }
  }
}
