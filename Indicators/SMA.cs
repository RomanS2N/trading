﻿using Indicators.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Indicators
{
    public class SMA : IFinancialIndicator
    {
        private int _period;
        private decimal[] _prices;
        private int _index = 0;
        private int _samplesCount = 0;
        private decimal _sma = 0;
        public int Period { get { return _period; } }
        public decimal Sma { get { return _sma; } }
        public decimal Value { get { return _sma; } }
        public SMA(int period)
        {
            _period = period;
            _prices = new decimal[_period];
        }
        public void Update(decimal price)
        {
            _prices[_index] = price;
            _samplesCount++;
            if (_samplesCount > _period)
            {
                _sma = _prices.Average();
            }
            _index++;
            if (_index == _period)
            {
                _index = 0;
            }
        }

        public bool HasValue
        {
            get { throw new NotImplementedException(); }
        }

        public string Identifier
        {
            get { return string.Format("SMA_{0}", _period); }
        }
    }
}
