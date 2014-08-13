using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebReader {
  public class Reader {
    public static string ReadString(string url) {
      using (var client = new WebClient()) {
        var stream = client.OpenRead(url);
        var reader = new System.IO.StreamReader(stream);
        return reader.ReadToEnd();
      }
    }
  }
}
