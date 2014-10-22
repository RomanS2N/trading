using Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Chart.Test {
  class Program {
    static void Main(string[] args) {
      var serie1 = CreateRandomLine("Prices", new DateTime(2008, 1, 1), new DateTime(2012, 1, 1));
      ChartPool.CreateChart();
      ChartPool.AddSeries(new List<Series> { serie1 });
      Pause();
      ChartPool.ClearSeries();
      Pause();
      ChartPool.AddSeries(new List<Series> { serie1 });
      Pause();
      ChartPool.ClearSeries();
      Pause();
    }
    static Series CreateRandomLine(string name, DateTime begin, DateTime end) {
      Random random = new Random();
      List<Sample> samples = new List<Sample>();
      for (DateTime dt = begin; dt < end; dt += TimeSpan.FromDays(1)) {
        samples.Add(new Sample(dt, random.NextDouble()));
      }
      return new Series(name, samples, ChartType.Lines, Colors.Red);
    }
    static void Pause() {
      Console.WriteLine("Press a key to continue...");
      Console.ReadKey(true);
    }
  }
}
