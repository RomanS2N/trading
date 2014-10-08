using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeriesReading.Shared {
  public interface ISeriesReader {
    bool Next(out DateTime dateTime, out decimal ask, out decimal bid);
  }
}
