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

using FinancialConfiguration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SeriesAcquisition.Automation {
  public class Robot {
    public void Start() {

      // creo los directorios necesarios para dar soporte a los datos
      SeriesAcquisitor.CreateFolders();

      // para cada instrumento
      Configuration.Instace.Instruments.ForEach(ticker => {
        string symbol = ticker.Replace(@"/", "");
        // de cada proveedor configurado
        Configuration.Instace.Providers.ForEach(provider => {
          string providerPath = string.Format(@"{0}\{1}\{2}\", Configuration.Instace.DataRoot, symbol, provider);
          // verifico que existan los datos desde el StartYear configurado
          DateTime begin = new DateTime(Configuration.Instace.StartYear, 1, 1);
          while (begin < DateTime.Now) {
            DateTime end = begin.AddMonths(1);
            string monthFile = string.Format(@"{0}\{1}__{2}.csv", providerPath, begin.ToString("yyyy_MM_dd_HH_mm_ss"), end.ToString("yyyy_MM_dd_HH_mm_ss"));
            // si es el mes actual, lo borro y vuelvo a crear
            if (end > DateTime.Now) {
              Console.WriteLine("Deleting file {0}", monthFile);
              File.Delete(monthFile);
            }
            // si el archivo no existe, pido el mes entero
            if (!File.Exists(monthFile)) {
              Console.WriteLine("Requesting data for file {0}", monthFile);
              string sourceFile = string.Format(@"{0}\{1}_DUKAS_TICKS.txt", providerPath, symbol);
              if (File.Exists(sourceFile)) File.Delete(sourceFile);
              SeriesAcquisitor.RequestTicks(ticker, begin, end, providerPath);
              File.Move(sourceFile, monthFile);
            }
            else {
              Console.WriteLine("File {0} exists", monthFile);
            }
            begin = end;
          }
        });
      });
    }
  }
}
