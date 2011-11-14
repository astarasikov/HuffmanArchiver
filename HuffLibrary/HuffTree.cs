//  HuffTree.cs
//  
//  Author:
//       Alexander Tarasikov <alexander.tarasikov@gmail.com>
// 
//  Copyright (c) 2010
// 
//  This program is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 2 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//  
//  You should have received a copy of the GNU General Public License
//  along with this program; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// 

using System;
using System.Collections;
using System.Collections.Generic;
using HuffLibrary;

namespace HuffLibrary {
	
	/// <summary>
	/// Class to represent the Huffman tree Node
	/// </summary>
	public class Node {
		public int sym;
		public int freq;
		public int order;
		public Node right,left,parent;
		public NodeType nt;
		public Node (NodeType nt,int sym,int freq,int order) {
			this.nt = nt;
			this.sym = sym;
			this.freq = freq;
			this.order = order;
		}

		private Node () {
		}
	}
	public enum NodeType {
		SYM,
		NYT,
		EOF,
		INT,
	}
	
	/// <summary>
	/// The class representing the adaptive Huffman tree
	/// providing the full range of needed operations
	/// </summary>
	public class Tree {
		
		public const int CharIsEof = -1;

		//Reference to the root node
		private Node root;
		//Array of pointers to nodes to avoid expensive DFS
		private Node[] nodes;
		int nodeCount = 0;	
		/// <summary>
		/// The method to perform tree reconfiguration
		/// </summary>
		/// <param name="n1">
		/// A <see cref="Node"/>
		/// Reference to the first node
		/// </param>
		/// <param name="n2">
		/// A <see cref="Node"/>
		/// Reference to the second node
		/// </param>
		private void SwapNodes (Node n1,Node n2) {
			int tmp = n1.freq;
			n1.freq = n2.freq;
			n2.freq = tmp;
			
			Node t1 = n1.parent.left;
			Node t2 = n2.parent.left;
			
			//Determine if n1 was the left or the right child
			if (t1 == n1)
				n1.parent.left = n2;
			else
				n1.parent.right = n2;
			
			//Same thing for the second node
			if (t2 == n2)
				n2.parent.left = n1;
			else
				n2.parent.right = n1;
			
			//Swap the nodes
			Node t3 = n1.parent;
			n1.parent = n2.parent;
			n2.parent = t3;
		}

		/// <summary>
		/// Returns the reference to the node that has the same frequency
		/// as the argument and highest order
		/// </summary>
		/// <param name="nd">
		/// A <see cref="Node"/>
		/// Reference to the node
		/// </param>
		/// <returns>
		/// A <see cref="Node"/>
		/// </returns>
		private Node FindHighestWithSameFreq (Node nd) {
			Node current = nd;
			if (nd.parent != null) {
				Node nd2 = current.parent;
				if ((nd2.left == current) && (nd2.right.freq == current.freq))
					current = nd2.right;
				
				if (nd2.parent != null) {
					Node nd3 = nd2.parent;
					if ((nd3.left == nd2) && (nd3.right.freq == current.freq))
						current = nd3.right;
					else if ((nd3.right == nd2) && (nd3.left.freq == current.freq))
						current = nd3.left;
				}
			}
			
			return current;
		}

		/// <summary>
		/// Returns the current Not-Yet-Transfered node
		/// </summary>
		/// <returns>
		/// A <see cref="Node"/>
		/// </returns>
		private Node GetNytNode () {
			return nodes[257];
		}
		/// <summary>
		/// Adds a node to the tree and returns the reference to it
		/// </summary>
		/// <param name="sym">
		/// A <see cref="System.Char"/>
		/// The code of added symbol
		/// </param>
		/// <param name="count">
		/// A <see cref="System.Int32"/>
		/// The initial frequency (typically is 1)
		///</param>
		/// <returns>
		/// A <see cref="Node"/>
		/// </returns>
		private Node AddToTree (int sym,int count) {
			Node nyt = GetNytNode ();
			nyt.nt = NodeType.INT;
			
			nyt.right = new Node (NodeType.NYT,257,0,nyt.order - 1);
			nyt.left = new Node (NodeType.SYM,sym,count,nyt.order - 2);
			nyt.left.parent = nyt.right.parent = nyt;
			nyt.sym = 259;
			nodes[257] = nyt.right;
			nodes[sym] = nyt.left;
			return nyt.right;
		}

		/// <summary>
		/// Updates the tree with the provided symbol and performs the rebuild
		/// when necessary
		/// </summary>
		/// <param name="sym">
		/// A <see cref="System.Char"/>
		/// </param>
		public void UpdateTree (int sym) {
			if (sym > nodeCount) return;
			Node temp = nodes[sym];
			if (temp == null)
				temp = AddToTree (sym,0);

			do {
				Node same = FindHighestWithSameFreq (temp);
				if ((same != temp) && (temp.parent != same))
					SwapNodes (temp,same);
				temp.freq++;
				temp = temp.parent;
			} while (temp != null);
		}

		/// <summary>
		/// The constructor for the Tree class
		/// </summary>
		public Tree () {
			
			root = new Node (NodeType.INT, 258, 0, 516);
			root.right = new Node (NodeType.NYT, 257, 0, root.order - 1);
			root.left = new Node (NodeType.EOF, 256, 0, root.order - 2);
			root.left.parent = root.right.parent = root;
			nodes = new Node[259];
			nodes[256] = root.left;
			nodes[257] = root.right;
			nodeCount = 258;
		}

		public bool contains (int sym) {
			return (sym <= nodeCount && nodes[sym] != null);
		}

		//FIXME : DIRTY HACK
		public Node GetRootNode () {
			return root;
		}
		
		private Node ptr;
		int _tempcode = 0, _count = 0;
		bool InNyt = false;
		public int DecodeBinary(int bit) {
			try {
			if (ptr == null) ptr = root;
			if (InNyt) {
				_tempcode <<= 1;
				_tempcode |= bit;
				_count++;
				if (_count == 8) {
					UpdateTree(_tempcode);
					int sym = _tempcode;
					_tempcode = _count = 0;
					InNyt = false;
					return sym;
				}
				return CharIsEof;
			}
			
			if (bit == 1) ptr = ptr.right;
			else ptr = ptr.left;
			
			if (ptr.nt == NodeType.NYT && ptr.sym == 257) {
				ptr = root;
				InNyt = true;
				return CharIsEof;
			}
			if (ptr.nt == NodeType.SYM) {
				int sym = ptr.sym;
				UpdateTree(sym);
				ptr = root;
				return sym;
			}
			return CharIsEof;
			}
			catch (System.NullReferenceException) {
				throw new Exception("Corrupted Huffman sequence supplied for decoding");
			}
		}

		public Stack<int> GetCode(int sym) {
			Stack<int> bits = new Stack<int>();
			Node pointer = nodes[sym];
			while (pointer != null && pointer.parent != null) {
				if (pointer.parent.left == pointer) {
					bits.Push(0);
				}
				else {
					bits.Push(1);
				}
				pointer = pointer.parent;
			}
			return bits;
		}
		
	}
}
