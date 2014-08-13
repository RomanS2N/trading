/*
   Copyright 2014 Samuel Pets

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

namespace Factory.Lib {
  public static class TypeBuilder {
    public static T CreateType<T>(string typeName, bool isSingleton = false) {
      T instance = default(T);

      if (typeName == null) {
        throw new Exception("TypeName cannot be null.");
      }

      Type type = Type.GetType(typeName, true);

      if (type == null) {
        throw new Exception(string.Format("Unable to load type {0}", typeName));
      }

      if (isSingleton) {
        var info = type.GetProperty("Instance");
        instance = (T)info.GetValue(null, null);
      }
      else {
        // alternative
        //IEnumerable<ConstructorInfo> constructorsInfo = type.GetConstructors();
        //ConstructorInfo constructorInfo = constructorsInfo.Single(x => x.GetParameters().Count() == 0);
        //instance = (T)constructorInfo.Invoke(null);

        instance = (T)Activator.CreateInstance(type, null);
      }

      return instance;
    }
  }
}
