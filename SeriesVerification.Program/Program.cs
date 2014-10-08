using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeriesVerification.Program {
	class Program {
		static void Main(string[] args) {
			if (!Verifier.VerifySequentiality(@"C:\quotes\EURUSD\Dukascopy\EURUSD_DUKAS_TICKS.txt")) {
				Console.WriteLine("Error on text file.");
			}
			else {
				Console.WriteLine("The text file is fine.");
			}
			Console.WriteLine("Press a key to exit...");
			Console.ReadKey(true);
		}
	}
}
