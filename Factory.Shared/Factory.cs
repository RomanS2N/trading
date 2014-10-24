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

using Configuration.Shared;
using Factory.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Shared {
  public static class Factory {
    #region Fields

    private static readonly Dictionary<string, object> _instances = new Dictionary<string, object>();

    #endregion

    #region Constructors

    static Factory() {
      _instances = new Dictionary<string, object>();
    }

    #endregion

    public static T Create<T>(bool isSingleton = false) {
      return Create<T>(typeof(T).Name, isSingleton);
    }

    public static T Create<T>(string configurationEntry, bool isSingleton = false) {
      //TODO: remove ConfigurationManager dependency (must depend on Configuration)
      string typeName = ConfigurationFactory.Configuration[configurationEntry];

      if (typeName == null) {
        throw new Exception(string.Format("Entry {0} not found in app.config", configurationEntry));
      }

      return TypeBuilder.CreateType<T>(typeName, isSingleton);
    }
  }
}

