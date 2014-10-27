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
using System.Linq;
using System.Text;

namespace FinancialData {
  public class InstantValue<T> : IInstantValue<T> {
    public DateTime DateTime { get; set; }
    public T Value { get; set; }
    public T NormalizedValue { get; set; }
    public InstantValue(T value) {
      Value = value;
    }
    public InstantValue(DateTime dateTime, T value)
      : this(value) {
      DateTime = dateTime;
    }
    public SampleType SampleType {
      get { return SampleType.Raw; }
    }
  }
}
