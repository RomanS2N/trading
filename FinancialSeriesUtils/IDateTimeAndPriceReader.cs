using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinancialSeriesUtils
{
    public interface IDateTimeAndPriceReader
    {
        bool Next(out DateTime[] dateTime, out decimal[] price);
    }
}
