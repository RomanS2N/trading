using Simulation.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charts {
  public class Trade : Sample {
    public DateTime BeginInstant { get; set; }
    public DateTime EndInstant { get; set; }
    public PositionSide Side { get; set; }
    public double OpenPrice { get; set; }
    public double ClosePrice { get; set; }
  }
}
