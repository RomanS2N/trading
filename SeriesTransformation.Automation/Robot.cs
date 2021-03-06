﻿/*
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
using SeriesReading.Descriptor.Quotes;
using SeriesTransformation.Shared;
using System.IO;
using SeriesAcquisition.Shared;
using System.Threading.Tasks;
using System.Threading;

namespace SeriesTransformation.Automation {
  public class Robot {

    private void Convert(string providerPath) {
      Directory.GetFiles(providerPath, "*.xml").ToList().ForEach(reportFile => {
        string dataFile = reportFile.Replace(".xml", ".csv");
        if (File.Exists(dataFile)) {
          VerificationReport verificationReport = VerificationReport.LoadFromFile(reportFile);
          if (verificationReport.Verified && !verificationReport.TransformationCompleted) {
            ISeriesConverter converter = new SeriesConverter();
            QuotesProcessor processor = new QuotesProcessor(providerPath);
            RawDataInformation information = RawDataInformation.FromPath(reportFile);
            Console.WriteLine("Creating prices from {0}", dataFile);
            converter.ImportQuotes(dataFile, processor, information.Begin, information.End);
            verificationReport.TransformationCompleted = true;
            verificationReport.SaveToFile(reportFile);
          }
        }
        else {
          Console.WriteLine("{0} file does not exist!", dataFile);
        }
      });
    }

    private void ConvertSeries(SeriesDescriptor seriesDescriptor) {
      seriesDescriptor.InstrumentDescriptors.ForEach(instrumentDescriptor => {
        instrumentDescriptor.ProviderDescriptors.ForEach(providerDescriptor => {
          Convert(providerDescriptor.Path);
        });
      });
    }

    public void Start() {
      Task.Factory.StartNew(() => {
        while (true) {
          ConvertSeries(new SeriesDescriptor());
          Thread.Sleep(1000);
        }
      });
    }
  }
}
