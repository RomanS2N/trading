using Factory.Lib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Shared {
  public class ConfigurationFactory {
    private static IConfiguration _configuration;

    private static object _configurationLock = new object();

    public static IConfiguration Configuration {
      get {
        if (_configuration == null) {
          lock (_configurationLock) {
            if (_configuration == null) {
              string typeName = ConfigurationManager.AppSettings["IConfiguration"];

              if (typeName == null) {
                throw new Exception("Entry IConfiguration not found in app.config");
              }

              _configuration = TypeBuilder.CreateType<IConfiguration>(typeName);

            }
          }
        }

        return _configuration;
      }
    }
  }
}
