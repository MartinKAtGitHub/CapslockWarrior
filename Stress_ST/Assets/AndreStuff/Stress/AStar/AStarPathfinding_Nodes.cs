using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AStarPathFinding_Nodes {
	 
	List<Nodes> _ThePath = new List<Nodes>();//

	Nodes[] _StartNode; //This is the node that this object is an and starts searching from
	Nodes[] _EndNode; //end node, destination node
	Nodes _NodeSaver; //just to hold some nodes while searching

	float _LowerstFScore = 100000; //This is just a value to get the "best" path to the target, if there is an enormous amount of nodes then this value must be increased

	Nodes[] OpenList;
	Nodes[] ClosedList;

	Nodes[] ThePath;

	int a = 0; //holding previous nodesaver which now is null 
	int b = 0; //holds the length of the array, optimalization reasons :D
	int c = 0; //closed list index holder
	int remakeIndex = 0;
	int theSize = 0;
	Nodes Saver = null; 
	Nodes inbetweensaver = null;
	Nodes[,] niegh;

	int[] remakeindexlist = new int[1];

	public AStarPathFinding_Nodes(int size){
		theSize = size;
		OpenList = new Nodes[size];
		ClosedList = new Nodes[size]; 
		ThePath = new Nodes[size];
		remakeindexlist [0] = remakeIndex;
	}

	public void SetEndStartNode(Nodes[] start, Nodes[] end){
		_StartNode = start;
		_EndNode = end;
	}

	public Nodes[] GetListRef(){
		return ThePath;
	}
	public int[] GetListindexref(){
		return remakeindexlist;
	}


	public Nodes[] openlist(){
		return OpenList;
	}

	public Nodes[] closedlist(){
		return ClosedList;
	}

	public Nodes[] thepath(){
		return ThePath;
	}


	public void CreatePath() {//Starts A* and clears all the nodes so that they are rdy for the next search
		a = 0;
		b = 0;
		c = 0;
		remakeIndex = 0;

		AStartAlgorithm();

		remakeindexlist [0] = theSize - remakeIndex;

		for (int i = 0; i < b; i++) {
			OpenList [i].Used = false;
		}

		for (int i = 0; i < c; i++) {
			ClosedList [i].Used = false;
		}
	}

	void AStartAlgorithm() {//A*. 

		_NodeSaver = null;//Holds the current node that im searching with
		_StartNode [0].Used = true;
		OpenList [b++] = _StartNode [0];

		while (c < theSize) {

			_LowerstFScore = 10000000;

			for (int i = 0; i < b; i++) {
				inbetweensaver = OpenList [i];
				if (inbetweensaver._FCost < _LowerstFScore) {
					_NodeSaver = inbetweensaver;
					a = i;
					_LowerstFScore = _NodeSaver._FCost;
				}
			} 
	
			if (_NodeSaver == _EndNode [0]) {//If _NodeSaver == ends then the search is complete and sending _closedlist to calculate the path from start to end
				RemakePath ();
				return;
			}

			ClosedList [c++] = _NodeSaver;
			OpenList [a] = OpenList [--b];//works just fine :D

			niegh = _NodeSaver.NeighbourNodes;
			for (int i = 0; i < 3; i++) {
				for (int j = 0; j < 3; j++) {
					Saver = niegh [i,j];
					if (Saver != null && Saver._MapCollision != 100) {
						if (i != 1 && j != 1) {
							if (Saver.Used == false) {//if n dont have a parent and n isnt the starting node (starts must be there else it will cause an FATAL error ;-P, your warned :D)
								Saver.SetParentAndEndCorners (_NodeSaver, _EndNode [0]);
								OpenList [b++] = Saver;
							} else if (Saver._GCost > _NodeSaver._GCost + (Saver._MapCollision * 1.4f)) {//calculates best route through these values
								Saver.SetParentCorner (_NodeSaver);
							}
						} else {
							if (Saver.Used == false) {//if n dont have a parent and n isnt the starting node (starts must be there else it will cause an FATAL error ;-P, your warned :D)
								Saver.SetParentAndEndMiddle (_NodeSaver, _EndNode [0]);
								OpenList [b++] = Saver;
							} else if (Saver._GCost > _NodeSaver._GCost + Saver._MapCollision) {//calculates best route through these values
								Saver.SetParentMiddle (_NodeSaver);
							}
						}
					}
				}
			}
		}
		Debug.Log ("Could not find the end");
		return;
	}

	void RemakePath() {//Makes the path by going to the end node and get the parent, then parent of the parent ....... until your at the start node

		ThePath [theSize - (++remakeIndex)] = _EndNode [0];

		if (_EndNode [0] == _StartNode [0]) {
			return;
		} else {
			_NodeSaver = _EndNode [0].GetParent ();
		}
		
		while (true) {
			if (_NodeSaver.GetParent () != null) {
				ThePath [theSize - (++remakeIndex)] = _NodeSaver;
				_NodeSaver = _NodeSaver.GetParent ();
			} else {
				return;
			}
		}
	}
}