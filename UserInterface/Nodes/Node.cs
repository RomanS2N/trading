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
