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
using TradingConfiguration.Shared;
using UserInterface.Nodes;

namespace UserInterface {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			var assetsConfiguration = Configuration.Instance.AssetsConfiguration;
			//assetsConfiguration.AssetsTypes.ForEach(
			ViewModel viewModel = new ViewModel();

			var assets = new CurrenciesNode("Assets");
			assets.AddNode(new CurrenciesNode("Currencies"));
			assets.AddNode(new CurrenciesNode("Stocks"));

			viewModel.Nodes.Add(assets);
			this.DataContext = viewModel;
		}

		private void ElementsTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
			INode node = (INode)e.NewValue;
			ShowNodeMenu(node);
		}

		private void ShowNodeMenu(INode node) {
			if (node is CurrencyNode) {
				ShowCurrencyEditor((CurrencyNode)node);
			}
		}

		private void ShowCurrencyEditor(CurrencyNode node) {
		}
	}
}
