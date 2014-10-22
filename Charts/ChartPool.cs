using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Charts {
  public class ChartPool {
    static object creationLock = new object();
    static bool creationRequested = false;
    //static bool applictionActivated = false;
    static Application _app;
    static MainWindow _mainWindow;
    private static bool WindowLoaded {
      get {
        return _app != null && _app.Dispatcher.Invoke(() => { return _mainWindow != null && _mainWindow.IsLoaded; });
      }
    }
    private static void CreateWindow() {
      _app = new Application();
      _mainWindow = new Charts.MainWindow();
      //_mainWindow.Loaded += ((object sender, RoutedEventArgs e) => { /*applictionActivated = true;*/ });
      _app.Run(_mainWindow);
    }
    public static void CreateChart() {
      lock (creationLock) {
        if (!creationRequested) {
          creationRequested = true;
          Thread thread = new Thread(new ThreadStart(CreateWindow));
          thread.SetApartmentState(ApartmentState.STA);
          thread.Start();
        }
      }
    }
    public static void ClearSeries() {
      if (creationRequested) {
        while (!WindowLoaded) {
          Thread.Sleep(100);
        }
        _mainWindow.ClearSeries();
      }
    }
    public static void AddSeries(List<Series> seriesList) {
      if (creationRequested) {
        while (!WindowLoaded) {
          Thread.Sleep(100);
        }
        if (seriesList != null) {
          _mainWindow.Begin = seriesList[0].Samples.First().Instant;
          _mainWindow.End = seriesList[0].Samples.Last().Instant;
          _mainWindow.Maximum = seriesList[0].Samples.Max(x => x.Value);
          _mainWindow.Minimum = seriesList[0].Samples.Min(x => x.Value);
          seriesList.ForEach(x => _mainWindow.AddSeries(x));
        }
      }
    }
  }
}
