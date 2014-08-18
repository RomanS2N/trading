using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserInterface.Nodes {
	class Node : INode {

		private string _label;
		private List<INode> _nodes = new List<INode>();

		public string Label { get { return _label; } }
		public List<INode> Nodes { get { return _nodes; } }

		public Node(string label) {
			_label = label;
		}

		public void AddNode(INode node) {
			_nodes.Add(node);
		}
	}
}
