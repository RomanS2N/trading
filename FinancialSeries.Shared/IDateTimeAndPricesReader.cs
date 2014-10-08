using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinancialSeries.Shared
{
    public interface IDateTimeAndPricesReader
    {
        bool Next(out DateTime dateTime, out decimal[] prices);
    }
}
