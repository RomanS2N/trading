﻿/*
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

#define DRAW_ELLIPSE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Windows.Controls;

namespace Charts {
  public class Series {
    public bool IsRenderable { get { return this.Name != null; } }
    public bool IsPrimary { get; set; }
    public string Name { get; set; }
    public Brush Brush {
      get {
        if (this.IsPrimary) {
          return this._primarybrush;
        }
        else {
          return this.Presenter != null ? this.Presenter.Brush : Brushes.Blue;
        }
      }
    }
    public SeriesPresenter Presenter { get; set; }
    private Brush _primarybrush = Brushes.BlueViolet;
    public List<Sample> Samples { get; set; }
    public Series(string name, IEnumerable<Sample> samples) {
      this.Name = name;
      this.Samples = new List<Sample>(samples);
    }
    public Series(string name) {
      this.Name = name;
      this.Samples = new List<Sample>();
    }
    public Series() {
      this.Samples = new List<Sample>();
    }
    public Series Clone() {
      return new Series(this.Name) { IsPrimary = this.IsPrimary };
    }
    public void SetSamples(IEnumerable<Sample> samples) {
      this.Samples = new List<Sample>(samples);
    }
    public static FrameworkElement GetRenderingLine(Series series, double actualWidth, double actualHeight, DateTime begin, DateTime end, double min, double max, bool DrawPoints) {
      Grid grid = new Grid();
      Polyline line = new Polyline();
      line.ToolTip = series.Name;
      line.Points = new PointCollection();
      line.Stroke = series.Brush;
      line.StrokeThickness = series.IsPrimary ? 4 : 1;
      grid.Children.Add(line);
      double deltatimePeriod = (end - begin).TotalMilliseconds;
      double deltaMaxMin = max - min;
      series.Samples.ForEach(sample => {
        if (sample.Instant >= begin && sample.Instant <= end) {
          Point point = new Point();
          double deltatime = (sample.Instant - begin).TotalMilliseconds;
          point.X = deltatime * actualWidth / deltatimePeriod;
          double deltaValue = sample.Value - min;
          point.Y = deltaValue * actualHeight / deltaMaxMin;
          point.Y = actualHeight - point.Y; // inversion
          line.Points.Add(point);
          if (DrawPoints) {
            Ellipse ellipse = new Ellipse();
            ellipse.ToolTip = string.Format("{0}: {1} {2:F4}", series.Name, sample.Instant, sample.Value);
            double ellipseDiameter = 5;
            ellipse.HorizontalAlignment = HorizontalAlignment.Left;
            ellipse.VerticalAlignment = VerticalAlignment.Top;
            ellipse.Margin = new Thickness(point.X - (ellipseDiameter / 2.0), point.Y - (ellipseDiameter / 2.0), 0, 0);
            ellipse.Stroke = Brushes.Black;
            ellipse.Fill = Brushes.White;
            ellipse.Width = ellipse.Height = ellipseDiameter;
            grid.Children.Add(ellipse);
          }
        }
      });
      return grid;
    }
    public static FrameworkElement GetRenderingColumns(Series series, double actualWidth, double actualHeight, DateTime begin, DateTime end, double min, double max) {
      if (min > 0 || max < 0) {
        throw new InvalidDataException("GetRenderingColumns: min must be smaller than zero and max must be greater than zero.");
      }
      Grid grid = new Grid();
      double deltatimePeriod = (end - begin).TotalMilliseconds;
      double deltaMaxMin = max - min;
      series.Samples.ForEach(sample => {
        if (sample.Instant >= begin && sample.Instant <= end && sample.Value != 0) {
          Rectangle rectangle = new Rectangle();
          rectangle.Width = 10; // calculate
          rectangle.Fill = rectangle.Stroke = sample.Value > 0 ? Brushes.Green : Brushes.Red;
          rectangle.ToolTip = string.Format("{0}: {1} {2:F4}", series.Name, sample.Instant, sample.Value);
          //position computation
          double deltatime = (sample.Instant - begin).TotalMilliseconds;
          double xPosition = deltatime * actualWidth / deltatimePeriod;
          double deltaValue = sample.Value - min;
          double zeroPosition = -min * actualHeight / deltaMaxMin;
          double YPosition = deltaValue * actualHeight / deltaMaxMin;
          YPosition = actualHeight - YPosition; // inversion
          //position setting
          rectangle.HorizontalAlignment = HorizontalAlignment.Left;
          rectangle.VerticalAlignment = VerticalAlignment.Top;
          rectangle.Margin = new Thickness(xPosition - 1, sample.Value < 0 ? zeroPosition : YPosition, 0, 0);
          //size adjustment
          //TODO: generate negative columns
          double rectangleHeight = Math.Abs(zeroPosition - YPosition);
          rectangle.Height = rectangleHeight;
          if (rectangle.Height == 0) rectangle.Height = 0.5;
          //label
          grid.Children.Add(new TextBlock() {
            Text = string.Format(@"{0:F1}", sample.Value * 100.0),
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(
              xPosition - (sample.Value > 0 ? 1 : 3),
              YPosition + (sample.Value > 0 ? -12 : 0),
              0, 0),
            FontFamily = new FontFamily("Calibri"),
            FontSize = 8,
          });
          grid.Children.Add(rectangle);
        }
      });
      return grid;
    }
    public double MaxValue { get { return this.Samples.Max(sample => sample.Value); } }
    public double MinValue { get { return this.Samples.Min(sample => sample.Value); } }
    public static Series CompressSeries(Series series, bool useFirstSample) {
      // ...X][..X][...X]
      Series newSeries = series.Clone();
      Dictionary<string, List<Sample>> bag = new Dictionary<string, List<Sample>>();
      series.Samples.ForEach(sample => {
        string key = sample.Instant.Month + "/" + sample.Instant.Year;
        if (!bag.ContainsKey(key)) bag[key] = new List<Sample>();
        bag[key].Add(new Sample(sample.Instant, sample.Value));
      });
      foreach (var item in bag) {
        newSeries.Samples.Add(useFirstSample ? item.Value.First() : item.Value.Last());
      }
      return newSeries;
    }
  }
}

