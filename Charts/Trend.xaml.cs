/*
   Copyright 2014 Samuel Pets (internetuser0x00@gmail.com)

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Charts {
  /// <summary>
  /// Interaction logic for Trend.xaml
  /// </summary>
  public partial class Trend : UserControl {
    public Trend() {
      InitializeComponent();
    }

    Dictionary<Series, FrameworkElement> SeriesToFrameworkElementMapping = new Dictionary<Series, FrameworkElement>();
    //public string ChartType { get; set; }

    public void AddSeries(Series series) {
      //var begin = DateTime.Now;
      FrameworkElement element = null;
      if (series.ChartType == ChartType.Lines) {
        element = Series.GetRenderingLine(series, this.ActualWidth, this.ActualHeight, this.Begin, this.End, this.Minimum, this.Maximum, false);
      }
      else if (series.ChartType == ChartType.DotsAndLines) {
        element = Series.GetRenderingLine(series, this.ActualWidth, this.ActualHeight, this.Begin, this.End, this.Minimum, this.Maximum, true);
      }
      else if (series.ChartType == ChartType.Columns) {
        element = Series.GetRenderingColumns(series, this.ActualWidth, this.ActualHeight, this.Begin, this.End, this.Minimum, this.Maximum);
      }
      else if (series.ChartType == ChartType.Trades) {
        element = Series.GetRenderingTrades(series, this.ActualWidth, this.ActualHeight, this.Begin, this.End, this.Minimum, this.Maximum);
      }
      else {
        throw new Exception(string.Format("ChartType {0} desconocido...", series.ChartType));
      }
      this.SeriesToFrameworkElementMapping[series] = element;
      this.RootPane.Children.Add(element);
      //Console.WriteLine("Delay: {0}", DateTime.Now - begin);
    }

    public void RemoveSeries(Series series) {
      this.RootPane.Children.Remove(this.SeriesToFrameworkElementMapping[series]);
      this.SeriesToFrameworkElementMapping.Remove(series);
    }

    public void RemoveAllSeries() {
      this.RootPane.Children.Clear();
      this.SeriesToFrameworkElementMapping.Clear();
    }

    public DateTime Begin { get; set; }
    public DateTime End { get; set; }
    public double Maximum { get; set; }
    public double Minimum { get; set; }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
      Redraw();
    }
    public void Redraw() {
      //var begin = DateTime.Now;
      this.RootPane.Children.Clear();
      this.SeriesToFrameworkElementMapping.Keys.ToList().ForEach(series => {
        this.AddSeries(series);
      });
      //Console.WriteLine("OnRenderSizeChanged: {0}", DateTime.Now - begin);
    }
  }
}
