#define _VERBOSE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BarsBuilder.Shared;
using SeriesReading.Descriptor.Quotes;
using SeriesReading;
using FinancialSeriesUtils;

namespace BarsBuilder.Automation
{
    public class Robot
    {
        public void Start()
        {
            new List<TimeSpan> { FinancialTimeSpans.M1 }.ForEach(timeFrame =>
            {
                IBarsCreator creator = new BarsCreator(timeFrame, @"C:\quotes\EURUSD\Dukascopy\");
                string path = new SeriesDescriptor()
                    .InstrumentDescriptors.Single(x => x.Name == "EURUSD")
                    .ProviderDescriptors.Single(x => x.Name == "Dukascopy")
                    .Path;
                SeriesReader reader = new SeriesReader(path);
                DateTime dateTime;
                decimal ask, bid;
                int lastMonth = -1;
                while (reader.Next(out dateTime, out ask, out bid))
                {
                    creator.AddQuote(dateTime, ask);
#if _VERBOSE
                    if (dateTime.Month != lastMonth)
                    {
                        Console.WriteLine("{0} -> {1}/{2} -> {3}", dateTime, ask, bid, creator.BarsCount);
                        lastMonth = dateTime.Month;
                    }
#endif
                }
                creator.Finish();
            });
        }
    }
}
