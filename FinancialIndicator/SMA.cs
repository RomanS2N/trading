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

using FinancialIndicator.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialIndicator {
  public class SMA : IFinancialIndicator {
    private int _period;
    private decimal[] _prices;
    private int _index = 0;
    private int _samplesCount = 0;
    private decimal _sma = 0;
    public int Period { get { return _period; } }
    public decimal Sma { get { return _sma; } }
    public decimal Value { get { return _sma; } }
    public SMA(int period) {
      _period = period;
      _prices = new decimal[_period];
    }
    public void Update(decimal price) {
      _prices[_index] = price;
      _samplesCount++;
      if (_samplesCount > _period) {
        _sma = _prices.Average();
      }
      _index++;
      if (_index == _period) {
        _index = 0;
      }
    }


	public bool HasValue {
		get { throw new NotImplementedException(); }
	}
  }
}
