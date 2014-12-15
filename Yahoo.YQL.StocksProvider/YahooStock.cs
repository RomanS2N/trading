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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Yahoo.YQL.StocksProvider {
  public class YahooStock {
    public static string ROOT_PATH = @"C:\stocks\";
    public string Name { get; set; }
    public string Symbol { get; set; }
    public string Filepath { get { return ROOT_PATH + Symbol; } }
    public IBarPackage Bars { get; set; }

    public void Update() {
      var localInstance = Deserialize();
      Update(localInstance);
    }

    public void Update(YahooStock stock) {
      Name = stock.Name;
      Symbol = stock.Symbol;
      Bars = stock.Bars;
    }

    public void Update(IBarPackage barPackage) {
      barPackage.Samples.ForEach(bar => {
        var last = Bars.Samples.Last();
        if (bar.DateTime > last.DateTime) {
          Bars.Samples.Add(bar);
        }
        else if (bar.DateTime < last.DateTime) {
          Bars.Samples.Insert(0, bar);
        }
        else {
          throw new InvalidOperationException("Implementar...");
        }
      });
    }

    #region Serialization

    private YahooStock Deserialize() {
      if (File.Exists(Filepath)) {
        XmlSerializer serializer = new XmlSerializer(typeof(YahooStock));
        using (StreamReader reader = new StreamReader(Filepath)) {
          YahooStock instance = (YahooStock)serializer.Deserialize(reader);
          return instance;
        }
      }
      return new YahooStock();
    }

    public void Save() {
      XmlSerializer serializer = new XmlSerializer(typeof(YahooStock));
      TextWriter textWriter = new StreamWriter(Filepath);
      serializer.Serialize(textWriter, this);
      textWriter.Close();
    }

    #endregion
  }
}
