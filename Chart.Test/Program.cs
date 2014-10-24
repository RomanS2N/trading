using Charts;
using Simulation.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Chart.Test {
  class Program {
    static void Main(string[] args) {
      ChartPool.CreateChart();

      //var prices = CreateRandomLine("Prices", new DateTime(2008, 1, 1), new DateTime(2009, 1, 1));
      //ChartPool.AddSeries(new List<Series> { prices });
      //Pause();

      //ChartPool.ClearSeries();
      //Pause();

      var trades = new Series(
        "Trades",
        ChartType.Trades,
        Colors.Black,
        new List<IDrawable>{
          new Trade{
            Begin=new DateTime(2009, 6, 1),
            End=new DateTime(2009, 6, 7),
            OpenPrice=1542,
            ClosePrice=1670,
            Side=PositionSide.Long,
          }
        });
      ChartPool.AddSeries(new List<Series> { trades });
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
      return new Series(name, ChartType.Lines, Colors.Red, samples);
    }
    static void Pause() {
      Console.WriteLine("Press a key to continue...");
      Console.ReadKey(true);
    }
  }
}
