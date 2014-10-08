using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarsBuilder.Shared;
using SeriesReading.Descriptor.Quotes;
using SeriesReading;

namespace BarsBuilder.UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod()
        {
            IBarsCreator creator = new BarsCreator(TimeSpan.FromMinutes(1), null);
            string path = new SeriesDescriptor()
                .InstrumentDescriptors.Single(x => x.Name == "EURUSD")
                .ProviderDescriptors.Single(x => x.Name == "Dukascopy")
                .Path;
            SeriesReader reader = new SeriesReader(path);
            DateTime dateTime;
            decimal ask, bid;
            while (reader.Next(out dateTime, out ask, out bid))
                creator.AddQuote(dateTime, ask);
        }
    }
}
