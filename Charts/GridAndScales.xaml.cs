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
  /// Interaction logic for GridAndScales.xaml
  /// </summary>
  public partial class GridAndScales : UserControl {
    public GridAndScales() {
      InitializeComponent();
    }
    private DateTime? _begin = null;
    private DateTime? _end = null;
    public DateTime Begin { set { this._begin = value; this.ComposeAxes(); } }
    public DateTime End { set { this._end = value; this.ComposeAxes(); } }
    private double? _maximum = null;
    private double? _minimum = null;
    public double Maximum { set { this._maximum = value; this.ComposeAxes(); } }
    public double Minimum { set { this._minimum = value; this.ComposeAxes(); } }
    public int YSteps = 4;
    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
      this.ComposeAxes();
    }
    private void ComposeAxes() {
      this.RootPanel.Children.Clear();
      #region Y
      if (this._minimum.HasValue && this._maximum.HasValue) {
        double deltaY = this._maximum.Value - this._minimum.Value;
        double stepAmount = deltaY / YSteps;
        for (int step = 1; step < YSteps; step++) {
          double stepDisplayValue = this._minimum.Value + stepAmount * step;
          double stepValue = stepAmount * step;
          double y = stepValue * this.ActualHeight / deltaY;
          y = this.ActualHeight - y;

          Line line = new Line() {
            X1 = 0,
            X2 = this.ActualWidth,
            Y1 = y,
            Y2 = y,
            StrokeThickness = 1,
            Stroke = Brushes.Gray,
            StrokeDashArray = new DoubleCollection(new[] { 4.0, 4.0, }),
            Opacity = 0.7,
          };
          this.RootPanel.Children.Add(line);

          TextBlock text = new TextBlock() {
            Text = stepDisplayValue.ToString("0.00"),
            FontFamily = new FontFamily("Calibri"),
            FontSize = 10,
            Background = Brushes.White,
            //Opacity = 0.5,
            Padding = new Thickness(2, 0, 0, 0),
            Margin = new Thickness(0, y - 7, 0, 0),
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
          };
          this.RootPanel.Children.Add(text);
        }
      }
      #endregion
      #region X
      if (this._begin.HasValue && this._end.HasValue) {
        long ticksPeriod = (this._end.Value - this._begin.Value).Ticks;
        int year = this._begin.Value.Year + 1;
        while (year <= this._end.Value.Year) {
          DateTime dt = new DateTime(year, 1, 1);
          long ticksYear = (dt - this._begin.Value).Ticks;
          double x = ticksYear * this.ActualWidth / ticksPeriod;

          Line line = new Line() {
            X1 = x,
            X2 = x,
            Y1 = 0,
            Y2 = this.ActualHeight,
            StrokeThickness = 1,
            Stroke = Brushes.Gray,
            StrokeDashArray = new DoubleCollection(new[] { 4.0, 4.0, }),
            Opacity = 0.7,
          };
          this.RootPanel.Children.Add(line);

          TextBlock text = new TextBlock() {
            Text = year.ToString(),
            FontFamily = new FontFamily("Calibri"),
            FontSize = 10,
            Background = Brushes.White,
            Opacity = 0.5,
            Margin = new Thickness(x - 10, this.ActualHeight - 12, 0, 0),
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
          };
          this.RootPanel.Children.Add(text);

          year++;
        }
      }
      #endregion
    }
  }
}
