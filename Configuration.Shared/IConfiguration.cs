using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Shared {
  public interface IConfiguration {
    string this[string index] { get; }
  }
}
