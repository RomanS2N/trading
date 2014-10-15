using FinancialData.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Shared {
  public interface ISimulation {
    void OnBar(IBarContext context);
  }
}
