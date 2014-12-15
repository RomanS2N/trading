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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WebReader;

namespace Yahoo.YQL.StocksProvider {
  public class Provider {
    public static List<YahooStock> GetStocks() {
      var url = string.Format("http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.industry%20where%20id%20in%20(select%20industry.id%20from%20yahoo.finance.sectors)&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys");
      var xml = Reader.ReadString(url);
      XmlDocument doc = new XmlDocument();
      doc.LoadXml(xml);
      XmlNodeList nodes = doc.DocumentElement.SelectNodes("/query/results/industry/company");

      List<YahooStock> yahooStocks = new List<YahooStock>();

      foreach (XmlNode node in nodes) {
        YahooStock yahooStock = new YahooStock();
        yahooStock.Name = node.Attributes["name"].InnerText;
        yahooStock.Symbol = node.Attributes["symbol"].InnerText;
        yahooStocks.Add(yahooStock);
      }
      return yahooStocks;
    }
  }
}
