using Configuration.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Lib.AppConfig {
  public class Configuration : IConfiguration {
    public string this[string index] {
      get { return ConfigurationManager.AppSettings[index]; }
    }
  }
}
