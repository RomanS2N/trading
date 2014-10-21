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

using SeriesReading;
using SeriesReading.Descriptor;
using SeriesReading.Descriptor.Quotes;
using SeriesTransformation.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SeriesVerification.Display {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			var seriesDescriptor = new SeriesDescriptor();
			this.InstrumentsPanel.ItemsSource = seriesDescriptor.ChildDescriptors;
		}

		private void InstrumentsPanel_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (e.AddedItems.Count > 0) {
				InstrumentDescriptor descriptor = (InstrumentDescriptor)e.AddedItems[0];
				this.ProvidersPanel.ItemsSource = descriptor.ChildDescriptors;
			}
		}

		private void ProvidersPanel_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (e.AddedItems.Count > 0) {
				ProviderDescriptor descriptor = (ProviderDescriptor)e.AddedItems[0];
				this.YearsPanel.ItemsSource = descriptor.ChildDescriptors;
			}
		}

		private void YearsPanel_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (e.AddedItems.Count > 0) {
				YearDescriptor descriptor = (YearDescriptor)e.AddedItems[0];
				this.MonthsPanel.ItemsSource = descriptor.ChildDescriptors;
			}
		}

		private void MonthsPanel_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (e.AddedItems.Count > 0) {
				MonthDescriptor descriptor = (MonthDescriptor)e.AddedItems[0];
				this.DaysPanel.ItemsSource = descriptor.ChildDescriptors;
			}
		}

		private void DaysPanel_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (e.AddedItems.Count > 0) {
				DayDescriptor descriptor = (DayDescriptor)e.AddedItems[0];
				this.ValuesGrid.Items.Clear();
				SeriesReader reader = SeriesReader.CreateReaderForSingleFile(descriptor.Path);
				DateTime dateTime;
				decimal ask, bid;
				while (reader.Next(out dateTime, out ask, out bid)) {
					this.ValuesGrid.Items.Add(new { DateTime = dateTime, Ask = ask, Bid = bid });
				}
				this.StatusText.Text = string.Format("{0} samples...", this.ValuesGrid.Items.Count);
			}
		}
	}
}
