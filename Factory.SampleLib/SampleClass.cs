using Factory.SampleLib.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.SampleLib {
  public class SampleClass : ISampleInterface {
    public int SampleMethod(int a, int b) {
      return a + b;
    }
  }
}
