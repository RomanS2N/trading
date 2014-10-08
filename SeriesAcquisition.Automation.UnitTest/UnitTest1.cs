using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SeriesAcquisition.Automation.UnitTest {
  [TestClass]
  public class UnitTest1 {
    [TestMethod]
    public void TestMethod1() {
      Robot robot = new Robot();
      robot.Start();
    }
  }
}
