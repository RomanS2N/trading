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
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Windows.Controls;
using Simulation.Shared;

namespace Charts {
  public class Series {
    public bool IsRenderable { get { return Name != null; } }
    public bool IsPrimary { get; set; }
    public string Name { get; set; }
    private Color _color;
    public Brush Brush {
      get {
        if (IsPrimary) {
          return _primarybrush;
        }
        else {
          return new SolidColorBrush(_color);
        }
      }
    }
    private static Brush _primarybrush = Brushes.BlueViolet;
    private static Color _undefinedColor = Colors.Black;
    public List<IDrawable> Drawables { get; set; }
    public Series(string name, ChartType chartType, Color color, IEnumerable<IDrawable> drawables) {
      Name = name;
      Drawables = new List<IDrawable>(drawables);
      ChartType = chartType;
      _color = color;
    }
    public Series(string name, ChartType chartType, Color color)
      : this(name, chartType, color, new List<IDrawable>()) {
    }
    public Series(string name, ChartType chartType)
      : this(name, chartType, _undefinedColor, new List<IDrawable>()) {
    }
    public Series Clone() {
      return new Series(Name, ChartType) { IsPrimary = IsPrimary };
    }
    public void SetDrawables(IEnumerable<IDrawable> drawables) {
      Drawables = new List<IDrawable>(drawables);
    }
    public static FrameworkElement GetRenderingLine(Series series, double actualWidth, double actualHeight, DateTime begin, DateTime end, double min, double max, bool DrawPoints) {
      var samples = series.Drawables.Cast<Sample>().ToList();
      Grid grid = new Grid();
      Polyline line = new Polyline();
      line.ToolTip = series.Name;
      line.Points = new PointCollection();
      line.Stroke = series.Brush;
      line.StrokeThickness = series.IsPrimary ? 4 : 1;
      grid.Children.Add(line);
      double deltatimePeriod = (end - begin).TotalMilliseconds;
      double deltaMaxMin = max - min;
      samples.ForEach(sample => {
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
      Console.WriteLine("actualWidth {0}, actualHeight {1}, begin {2}, end {3}, min {4}, max {5}", 
        actualWidth, actualHeight, begin, end, min, max);
      //if (min > 0 || max < 0) {
      //  throw new InvalidDataException("GetRenderingColumns: min must be smaller than zero and max must be greater than zero.");
      //}
      var samples = series.Drawables.Cast<Sample>().ToList();
      Grid grid = new Grid();
      double deltatimePeriod = (end - begin).TotalMilliseconds;
      double deltaMaxMin = max - min;
      var relevantSamples = samples.Where(sample => sample.Instant >= begin && sample.Instant <= end && sample.Value != 0).ToList();
      
      double reclangleWidth = 10;// actualWidth / relevantSamples.Count;
      if (relevantSamples.Count > 1) {
        TimeSpan implicitInterval = relevantSamples[1].Instant - relevantSamples[0].Instant;
        reclangleWidth = implicitInterval.TotalMilliseconds * actualWidth / deltatimePeriod;
      }
      
      relevantSamples.ForEach(sample => {
        Rectangle rectangle = new Rectangle();
        rectangle.HorizontalAlignment = HorizontalAlignment.Left;
        rectangle.VerticalAlignment = VerticalAlignment.Top;
        rectangle.Width = reclangleWidth;
        
        rectangle.Fill = rectangle.Stroke = sample.Value > 0 ? Brushes.Green : Brushes.Red;
        //rectangle.ToolTip = string.Format("{0}: {1} {2:F4}", series.Name, sample.Instant, sample.Value);

        double deltatime = (sample.Instant - begin).TotalMilliseconds;
        double xPosition = deltatime * actualWidth / deltatimePeriod;
        //---
        double deltaValue = sample.Value - min;
        double deltaZero = 0 - min;
        double zeroPosition = deltaZero * actualHeight / deltaMaxMin;
        double yPosition;
        if (sample.Value > 0) {
          yPosition = actualHeight - (deltaValue * actualHeight / deltaMaxMin);
        }
        else {
          yPosition = actualHeight - zeroPosition;
        }
        rectangle.Height = Math.Abs(sample.Value) * actualHeight / deltaMaxMin;
        if (rectangle.Height < 0.5) rectangle.Height = 0.5;
        
        //---
        rectangle.Margin = new Thickness(xPosition, yPosition, 0, 0);

        rectangle.ToolTip = string.Format("xPosition {0:F1}, yPosition {1:F1}, Width {2:F1}, Height {3:F1}, zeroPosition {4:F1}", xPosition, yPosition, rectangle.Width, rectangle.Height, zeroPosition);

        /*
        //var duration = sample.End - sample.Begin;
        
        //position computation
        double xPosition = deltatime * actualWidth / deltatimePeriod;
        double deltaValue = sample.Value - min;
        double zeroPosition = -min * actualHeight / deltaMaxMin;
        //double YPosition = deltaValue * actualHeight / deltaMaxMin;
        //YPosition = actualHeight - YPosition; // inversion
        //position setting
        
        double yPosition = zeroPosition;// < YPosition ? zeroPosition : YPosition;
        
        //size adjustment
        double rectangleHeight = 0;// Math.Abs(zeroPosition - YPosition);
        rectangle.Height = rectangleHeight;
        if (rectangle.Height == 0) rectangle.Height = 0.5;
        */
        grid.Children.Add(rectangle);
      });
      return grid;
    }
    public static FrameworkElement GetRenderingTrades(Series series, double actualWidth, double actualHeight, DateTime begin, DateTime end, double min, double max) {
      var trades = series.Drawables.Cast<Trade>().ToList();
      Grid grid = new Grid();
      double deltatimePeriod = (end - begin).TotalMilliseconds;
      double deltaMaxMin = max - min;
      trades.ForEach(trade => {
        if (trade.Begin >= begin && trade.End <= end) {
          Line line = new Line();
          line.StrokeThickness = 2;
          var sideColor = trade.Side == PositionSide.Long ? Brushes.Green : Brushes.Red;
          line.Fill = line.Stroke = sideColor;
          line.ToolTip = string.Format("Trade {0}  -  Duration {1}  -  Abs Delta {2:F6}",
            trade.Side, trade.End - trade.Begin, Math.Abs(trade.ClosePrice - trade.OpenPrice));
          //position computation
          double beginDeltatime = (trade.Begin - begin).TotalMilliseconds;
          double beginXPosition = beginDeltatime * actualWidth / deltatimePeriod;
          double endDeltatime = (trade.End - begin).TotalMilliseconds;
          double endXPosition = endDeltatime * actualWidth / deltatimePeriod;
          double zeroPosition = -min * actualHeight / deltaMaxMin;
          double beginDeltaValue = trade.OpenPrice - min;
          double beginYPosition = beginDeltaValue * actualHeight / deltaMaxMin;
          double endDeltaValue = trade.ClosePrice - min;
          double endYPosition = endDeltaValue * actualHeight / deltaMaxMin;
          beginYPosition = actualHeight - beginYPosition; // inversion
          endYPosition = actualHeight - endYPosition; // inversion
          //position setting
          line.HorizontalAlignment = HorizontalAlignment.Left;
          line.VerticalAlignment = VerticalAlignment.Top;
          line.X1 = beginXPosition;
          line.Y1 = beginYPosition;
          line.X2 = endXPosition;
          line.Y2 = endYPosition;
          grid.Children.Add(line);
          // open trade point
          Ellipse ellipse = new Ellipse();
          double ellipseDiameter = 6;
          ellipse.HorizontalAlignment = HorizontalAlignment.Left;
          ellipse.VerticalAlignment = VerticalAlignment.Top;
          ellipse.Margin = new Thickness(line.X1 - (ellipseDiameter / 2.0), line.Y1 - (ellipseDiameter / 2.0), 0, 0);
          ellipse.Stroke = sideColor;
          ellipse.Fill = sideColor;
          ellipse.Width = ellipse.Height = ellipseDiameter;
          ellipse.ToolTip = string.Format("Open {0}  -  {1:F6}", trade.Begin, trade.OpenPrice);
          grid.Children.Add(ellipse);
          // close trade point
          ellipse = new Ellipse();
          ellipse.HorizontalAlignment = HorizontalAlignment.Left;
          ellipse.VerticalAlignment = VerticalAlignment.Top;
          ellipse.Margin = new Thickness(line.X2 - (ellipseDiameter / 2.0), line.Y2 - (ellipseDiameter / 2.0), 0, 0);
          ellipse.Stroke = sideColor;
          ellipse.Fill = sideColor;
          ellipse.Width = ellipse.Height = ellipseDiameter;
          ellipse.ToolTip = string.Format("Open {0}  -  {1:F6}", trade.End, trade.ClosePrice);
          grid.Children.Add(ellipse);
        }
      });
      return grid;
    }
    public double MaxValue { get { return Drawables.Max(drawable => drawable.MaxValue); } }
    public double MinValue { get { return Drawables.Min(drawable => drawable.MinValue); } }
    public ChartType ChartType { get; set; }
  }
}

