using FinancialData.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation.Shared {
  public interface IBarContext {
    IBar Bar { get; }
    Dictionary<string, IInstantValue<double>> SeriesValues { get; }
  }
}
