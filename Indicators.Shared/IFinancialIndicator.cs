using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Indicators.Shared
{
    public interface IFinancialIndicator
    {
        string Identifier { get; }
        void Update(decimal price);
        decimal Value { get; }
        bool HasValue { get; }
    }
}
