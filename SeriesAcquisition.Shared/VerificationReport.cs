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
using System.Xml.Serialization;
using System.IO;

namespace SeriesAcquisition.Shared {
  [Serializable]
  public class VerificationReport {
    public static VerificationReport LoadFromFile(string filename) {
      return Deserialize(filename);
    }
    public void SaveToFile(string filename) {
      Serialize(filename);
    }

    #region Serialization

    private void Serialize(string filename) {
      XmlSerializer serializer = new XmlSerializer(typeof(VerificationReport));
      TextWriter textWriter = new StreamWriter(filename);
      serializer.Serialize(textWriter, this);
      textWriter.Close();
    }

    private static VerificationReport Deserialize(string filename) {
      if (File.Exists(filename)) {
        XmlSerializer deserializer = new XmlSerializer(typeof(VerificationReport));
        TextReader textReader = new StreamReader(filename);
        VerificationReport config = (VerificationReport)deserializer.Deserialize(textReader);
        textReader.Close();
        return config;
      }
      return null;
    }

    #endregion

    public bool Verified { get; set; }
    public bool TransformationCompleted { get; set; }
  }
}
