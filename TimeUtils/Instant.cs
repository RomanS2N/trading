using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeUtils {
  public class Instant {
    public static DateTime Now {
      get {
        return DateTime.UtcNow;
      }
    }
  }
}
