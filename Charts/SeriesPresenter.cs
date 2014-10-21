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
using System.ComponentModel;
using System.Windows.Media;

namespace Charts {
  public class SeriesPresenter {
    public string SeriesName { get { return this.Series.Name; } }

    bool selected = false;
    [BindableAttribute(true)]
    public bool SeriesSelected {
      get { return this.selected; }
      set {
        this.selected = value;
        if (this.OnSelectionChanged != null) {
          foreach (OnSelectionChangedHandler item in this.OnSelectionChanged.GetInvocationList()) {
            item.BeginInvoke(this.Series, this.selected, null, this);
          }
        }
      }
    }

    public double MaxValue { get { return this.Series.MaxValue; } }
    public double MinValue { get { return this.Series.MinValue; } }

    public delegate void OnSelectionChangedHandler(Series series, bool selected);
    public event OnSelectionChangedHandler OnSelectionChanged;

    public Series Series { get; private set; }

    public SeriesPresenter(Series series) {
      this.Series = series;
      series.Presenter = this;
    }

    Brush _brush = BrushesFactory.Next;
    public Brush Brush { get { return _brush; } }
  }
}
