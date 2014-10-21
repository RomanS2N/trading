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

namespace Charts {
  public class BrushesFactory {
    static int index = 0;
    static List<Brush> BrushesList = new List<Brush>()
    {
      new SolidColorBrush(Color.FromRgb(208,118,30)),
      new SolidColorBrush(Color.FromRgb(14,20,179)),
      //new SolidColorBrush(Color.FromRgb(216,195,88)),
      new SolidColorBrush(Color.FromRgb(109,8,57)),
      //new SolidColorBrush(Color.FromRgb(208,231,153)),
      new SolidColorBrush(Color.FromRgb(37,39,30)),
      new SolidColorBrush(Color.FromRgb(145,36,178)),
      new SolidColorBrush(Color.FromRgb(95,189,171)),
      new SolidColorBrush(Color.FromRgb(115,54,18)),
      new SolidColorBrush(Color.FromRgb(184,107,223)),
    
      new SolidColorBrush(Color.FromRgb(197,42,12)),
      new SolidColorBrush(Color.FromRgb(9,41,107)),
    
      new SolidColorBrush(Color.FromRgb(13,135,228)),
      new SolidColorBrush(Color.FromRgb(8,102,127)),
      //new SolidColorBrush(Color.FromRgb()),
      //new SolidColorBrush(Color.FromRgb()),
      new SolidColorBrush(Color.FromRgb(208,118,30)),
      new SolidColorBrush(Color.FromRgb(14,20,179)),
      //new SolidColorBrush(Color.FromRgb(216,195,88)),
      new SolidColorBrush(Color.FromRgb(109,8,57)),
      //new SolidColorBrush(Color.FromRgb(208,231,153)),
      new SolidColorBrush(Color.FromRgb(37,39,30)),
      new SolidColorBrush(Color.FromRgb(145,36,178)),
      new SolidColorBrush(Color.FromRgb(95,189,171)),
      new SolidColorBrush(Color.FromRgb(115,54,18)),
      new SolidColorBrush(Color.FromRgb(184,107,223)),
    
      new SolidColorBrush(Color.FromRgb(197,42,12)),
      new SolidColorBrush(Color.FromRgb(9,41,107)),
    
      new SolidColorBrush(Color.FromRgb(13,135,228)),
      new SolidColorBrush(Color.FromRgb(8,102,127)),
      new SolidColorBrush(Color.FromRgb(208,118,30)),
      new SolidColorBrush(Color.FromRgb(14,20,179)),
      //new SolidColorBrush(Color.FromRgb(216,195,88)),
      new SolidColorBrush(Color.FromRgb(109,8,57)),
      //new SolidColorBrush(Color.FromRgb(208,231,153)),
      new SolidColorBrush(Color.FromRgb(37,39,30)),
      new SolidColorBrush(Color.FromRgb(145,36,178)),
      new SolidColorBrush(Color.FromRgb(95,189,171)),
      new SolidColorBrush(Color.FromRgb(115,54,18)),
      new SolidColorBrush(Color.FromRgb(184,107,223)),
    
      new SolidColorBrush(Color.FromRgb(197,42,12)),
      new SolidColorBrush(Color.FromRgb(9,41,107)),
    
      new SolidColorBrush(Color.FromRgb(13,135,228)),
      new SolidColorBrush(Color.FromRgb(8,102,127)),
    };
    public static Brush Next {
      get {
        if (index == 36) index = 0;
        return BrushesList[index++];
      }
    }
  }
}