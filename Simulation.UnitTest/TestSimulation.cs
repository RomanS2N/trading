using FinancialData.Shared;
using Simulation.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.UnitTest {
  class TestSimulation : ISimulation {
    public void OnBar(IBar bar, Dictionary<string, IInstantValue<double>> seriesValues) {
      throw new NotImplementedException();
    }
  }
}
