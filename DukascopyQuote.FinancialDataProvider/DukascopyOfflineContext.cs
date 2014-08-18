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
using FinancialData.Shared;

namespace DukascopyQuote.FinancialDataProvider {
	public class DukascopyOfflineContext : IProvisionContext {

		public string Source { get; set; }
		public SampleType SampleType { get; set; }
		public TimeSpan Period { get; set; }

		public DukascopyOfflineContext(string filepath, SampleType sampleType, TimeSpan period) {
			Source = filepath;
			SampleType = sampleType;
			Period = period;
		}
	}
}
