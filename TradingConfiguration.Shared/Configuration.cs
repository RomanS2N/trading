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
using System.IO;
using System.Xml.Serialization;

namespace TradingConfiguration.Shared {
	[Serializable]
	public class Configuration {

		private static string filepath = @"C:\Configuration.xml";

		private static Configuration _instance;
		private static object _instanceLock = new object();

		public static Configuration Instance {
			get {
				if (_instance == null) {
					lock (_instanceLock) {
						if (_instance == null) {
							_instance = Configuration.Deserialize();
						}
					}
				}
				return _instance;
			}
		}

		private Configuration() {
			AssetsConfiguration = new AssetsConfiguration();
		}

		public AssetsConfiguration AssetsConfiguration { get; set; }

		private static Configuration Deserialize() {
			if (File.Exists(filepath)) {
				XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
				using (StreamReader reader = new StreamReader(filepath)) {
					Configuration configuration = (Configuration)serializer.Deserialize(reader);
					return configuration;
				}
			}
			return new Configuration();
		}

		public void Save() {
			XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
			TextWriter textWriter = new StreamWriter(filepath);
			serializer.Serialize(textWriter, this);
			textWriter.Close();
		}
	}
}
