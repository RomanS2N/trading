using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UserInterface {
	class ViewModel : INotifyPropertyChanged {

		private List<INode> _nodes = new List<INode>();

		public ViewModel() {

		}

		public List<INode> Nodes {
			get { return _nodes; }
			set {
				_nodes = value;
				NotifiyPropertyChanged("Nodes");
			}
		}

		void NotifiyPropertyChanged(string property) {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(property));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
