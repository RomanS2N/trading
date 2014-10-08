using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeriesTransformation.Automation.Program {
	class Program {
		static void Main(string[] args) {
			Robot robot = new Robot();
			robot.Start();

			Console.WriteLine("Press a key to exit...");
			Console.ReadKey(true);
		}
	}
}
