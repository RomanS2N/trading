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

namespace Charts {
  public partial class Chart : UserControl {
    public Chart() {
      InitializeComponent();
    }
    public const double DeltaBySide = 0.1;
    public bool FixedScales { get; set; }
    List<Series> Series = new List<Series>();
    //public string ChartType { get; set; }
    public string ChartTitle { get { return this.ChartTitleLabel.Text; } set { this.ChartTitleLabel.Text = value; } }
    public double FindAbsoluteMax() {
      double max = double.MinValue;
      this.Series.ForEach(series => {
        if (series.Drawables.Count > 0) {
          double amax = series.Drawables.FindAll(x => x.Begin >= this.Begin && x.End <= this.End).Max(x => x.MaxValue);
          if (amax > max) max = amax;
        }
      });
      return max;
    }
    public double FindAbsoluteMin() {
      double min = double.MaxValue;
      this.Series.ForEach(series => {
        if (series.Drawables.Count > 0) {
          double amin = series.Drawables.FindAll(x => x.Begin >= this.Begin && x.End <= this.End).Min(x => x.MinValue);
          if (amin < min) min = amin;
        }
      });
      return min;
    }
    public void AddSeries(Series series) {
      if (series.IsRenderable) {
        //this.trend.ChartType = this.ChartType;
        this.Series.Add(series);
        this.Dispatcher.Invoke((Action)(() => {
          if (!this.FixedScales) {
            double max = this.FindAbsoluteMax();
            double min = this.FindAbsoluteMin();
            double delta = max - min;
            this.Maximum = max + Chart.DeltaBySide * delta;
            this.Minimum = min - Chart.DeltaBySide * delta;
            this.trend.RemoveAllSeries();
          }
          this.Series.ForEach(x => this.trend.AddSeries(x));
        }));
      }
      else {
        throw new Exception("no es drawable");
      }
    }
    public void RemoveSeries(Series series) {
      Series fseries = this.Series.FirstOrDefault(aseries => aseries.Name == series.Name);
      if (fseries != null) {
        this.Series.Remove(fseries);
        this.Dispatcher.Invoke((Action)(() => this.trend.RemoveAllSeries()));
        if (this.Series.Count > 0) {
          this.Dispatcher.Invoke((Action)(() => {
            if (!this.FixedScales) {
              double max = this.FindAbsoluteMax();
              double min = this.FindAbsoluteMin();
              double delta = max - min;
              this.Maximum = max + Chart.DeltaBySide * delta;
              this.Minimum = min - Chart.DeltaBySide * delta;
              //this.trend.RemoveAllSeries();
            }
            this.Series.ForEach(x => this.trend.AddSeries(x));
          }));
        }
      }
    }
    public void ClearSeries() {
      this.Series.ToList().ForEach(fseries => {
        if (fseries != null) {
          this.Series.Remove(fseries);
          this.Dispatcher.Invoke((Action)(() => this.trend.RemoveAllSeries()));
          if (this.Series.Count > 0) {
            this.Dispatcher.Invoke((Action)(() => {
              if (!this.FixedScales) {
                double max = this.FindAbsoluteMax();
                double min = this.FindAbsoluteMin();
                double delta = max - min;
                this.Maximum = max + Chart.DeltaBySide * delta;
                this.Minimum = min - Chart.DeltaBySide * delta;
                //this.trend.RemoveAllSeries();
              }
              this.Series.ForEach(x => this.trend.AddSeries(x));
            }));
          }
        }
      });
    }
    public DateTime Begin { get { return this.trend.Begin; } set { this.gridAndScales.Begin = this.trend.Begin = value; } }
    public DateTime End { get { return this.trend.End; } set { this.gridAndScales.End = this.trend.End = value; } }
    public double Maximum { get { return this.trend.Maximum; } set { this.gridAndScales.Maximum = this.trend.Maximum = value; } }
    public double Minimum { get { return this.trend.Minimum; } set { this.gridAndScales.Minimum = this.trend.Minimum = value; } }

    #region Mouse events
    private void Grid_MouseMove(object sender, MouseEventArgs e) {
      if (e.LeftButton == MouseButtonState.Pressed) {
        var actualPosition = e.GetPosition(this);
        MoveTrend(origin, actualPosition);
        origin = actualPosition;
      }
      else if (e.RightButton == MouseButtonState.Pressed) {
        var actualPosition = e.GetPosition(this);
        ZoomTrend(origin, actualPosition);
        origin = actualPosition;
      }
    }
    Point origin;
    private void Grid_MouseDown(object sender, MouseButtonEventArgs e) {
      origin = e.GetPosition(this);
    }
    private void Grid_MouseUp(object sender, MouseButtonEventArgs e) {
    }
    private void MoveTrend(Point from, Point to) {
      this.Begin = MoveX(this.Begin, from.X, to.X);
      this.End = MoveX(this.End, from.X, to.X);
      this.Maximum = MoveY(this.Maximum, from.Y, to.Y);
      this.Minimum = MoveY(this.Minimum, from.Y, to.Y);
      this.trend.Redraw();
    }
    private void ZoomTrend(Point from, Point to) {
      ZoomX(from.X, to.X);
      ZoomY(from.Y, to.Y);
      this.trend.Redraw();
    }
    private DateTime MoveX(DateTime dateTime, double from, double to) {
      var movement = to - from;
      double screenMilliseconds = (this.End - this.Begin).TotalMilliseconds;
      var movementMilliseconds = movement * screenMilliseconds / this.ActualWidth;
      return dateTime.AddMilliseconds(-movementMilliseconds);
    }
    private double MoveY(double value, double from, double to) {
      var movement = to - from;
      double screenAmount = this.Maximum - this.Minimum;
      var movementAmount = movement * screenAmount / this.ActualHeight;
      return value + movementAmount;
    }
    private void ZoomX(double from, double to) {
      var movement = to - from;
      double screenMilliseconds = (this.End - this.Begin).TotalMilliseconds;
      var movementMilliseconds = (movement * screenMilliseconds / this.ActualWidth) / 2;
      this.Begin = this.Begin.AddMilliseconds(movementMilliseconds);
      this.End = this.End.AddMilliseconds(-movementMilliseconds);
    }
    private void ZoomY(double from, double to) {
      var movement = to - from;
      double screenAmount = this.Maximum - this.Minimum;
      var movementAmount = (movement * screenAmount / this.ActualHeight) / 2;
      this.Maximum += movementAmount;
      this.Minimum -= movementAmount;
    }
    #endregion
  }
}
