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
    public string ChartType { get; set; }

    public void AddSeries(Series series) {
      FrameworkElement element = null;
      if (this.ChartType == "Line") {
        element = Series.GetRenderingLine(series, this.ActualWidth, this.ActualHeight, this.Begin, this.End, this.Minimum, this.Maximum, /*series.IsPrimary*/ true);
      }
      else if (this.ChartType == "Columns") {
        element = Series.GetRenderingColumns(series, this.ActualWidth, this.ActualHeight, this.Begin, this.End, this.Minimum, this.Maximum);
      }
      else {
        throw new Exception(string.Format("ChartType {0} desconocido...", this.ChartType));
      }
      this.SeriesToFrameworkElementMapping[series] = element;
      this.RootPane.Children.Add(element);
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
      this.RootPane.Children.Clear();
      this.SeriesToFrameworkElementMapping.Keys.ToList().ForEach(series => {
        this.AddSeries(series);
      });
    }
  }
}
