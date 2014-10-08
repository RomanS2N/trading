using SeriesReading;
using SeriesReading.Descriptor.Quotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarsReading.Program
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = new SeriesDescriptor()
                .InstrumentDescriptors.Single(x => x.Name == "EURUSD")
                .ProviderDescriptors.Single(x => x.Name == "Dukascopy")
                .Path;

            BarsReader reader = BarsReader.Create(TimeSpan.FromHours(1), path/*, new DateTime(2010, 1, 1), new DateTime(2010, 2, 1)*/);

            DateTime dateTime;
            decimal price;

            while (reader.Next(out dateTime, out price))
            {
                Console.WriteLine("{0} -> {1}", dateTime, price);
            }

            Console.ReadLine();
        }
    }
}
