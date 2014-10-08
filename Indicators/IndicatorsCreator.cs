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

using Indicators.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Indicators {
  public class IndicatorsCreator {
    private List<IndicatorsCreatorItem> _indicatorsCreatorItems = new List<IndicatorsCreatorItem>();
    public TimeSpan TimeFrame { get; private set; }
    public string StoragePath { get; private set; }

    public IndicatorsCreator(TimeSpan timeFrame, string storagePath) {
      TimeFrame = timeFrame;
      StoragePath = storagePath;
    }

    public void AddIndicator(IFinancialIndicator indicator) {
      var found = _indicatorsCreatorItems.SingleOrDefault(ind => ind.Indicator.Identifier == indicator.Identifier);
      if (found != null)
        _indicatorsCreatorItems.Remove(found);
      _indicatorsCreatorItems.Add(new IndicatorsCreatorItem(TimeFrame, StoragePath, indicator));
    }

    public void Update(DateTime dateTime, decimal price) {
      _indicatorsCreatorItems.ToList().ForEach(item => item.Update(dateTime, price));
    }

    public List<decimal> Values { get { return _indicatorsCreatorItems.Select(item => item.Indicator.Value).ToList(); } }

    public void Finish() {
      _indicatorsCreatorItems.ToList().ForEach(item => item.Finish());
      VerifyConsistency();
    }

    private void VerifyConsistency() {
    }
  }
}
