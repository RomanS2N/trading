using BarsReading;
using FinancialSeries.Shared;
using FinancialSeriesUtils;
using Indicators;
using SeriesReading.Descriptor.Quotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndicatorsReading.Program
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = new SeriesDescriptor()
                .InstrumentDescriptors.Single(x => x.Name == "EURUSD")
                .ProviderDescriptors.Single(x => x.Name == "Dukascopy")
                .Path;

            // creo un groupReader para leer los bars e indicadores de forma coordinada
            var groupReader = new DateTimeAndPriceGroupReader();

            // voy a consumir el barsReader a través del groupReader
            BarsReader barsReader = BarsReader.Create(TimeSpan.FromHours(1), path);
            groupReader.AddReader(barsReader);
            
            // también los indicadores basados en horas
            FinancialTimeSpans.Hours.ForEach(timeFrame =>
            {
                IndicatorsReader indicatorsReader = IndicatorsReader.Create(new RSI(25), timeFrame, path);
                groupReader.AddReader(indicatorsReader);
            });

            DateTime groupDateTime;
            decimal[] barAndIndicatorsPrices;

            while (groupReader.Next(out groupDateTime, out barAndIndicatorsPrices))
            {
                StringBuilder sb = new StringBuilder();
                barAndIndicatorsPrices.ToList().ForEach(price =>
                {
                    if (sb.Length > 0) sb.Append(", ");
                    sb.AppendFormat("{0:0.000000}", price);
                });
                Console.WriteLine("{0} -> [{1}]", groupDateTime, sb.ToString());
            }

            Console.ReadLine();
        }
    }
}
