using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinancialConfiguration.UnitTest {
  [TestClass]
  public class UnitTest1 {
    [TestMethod]
    public void TestMethod1() {
      Assert.IsNotNull(Configuration.Instace.DataRoot);
    }
  }
}
