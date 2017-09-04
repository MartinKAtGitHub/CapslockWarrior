using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AStarPathFinding_Nodes {
	 
	Nodes[] _StartNode; //Will hold the refrence to the startnode 
	Nodes[] _EndNode; //Will hold the refrence to the endnode

	Nodes[] _OpenList;//list that holds nodes that i havent searched through
	Nodes[] _ClosedList;//list that have been searched through
	Nodes[] _ThePath;//the information to avoid absolute death

	int _NodeIndexSaved = 0; //used to store the index of a node
	int _OpenListAtIndex = 0; //this holds the next position which to add the next element
	int _ClosedListAtIndex = 0; //closed list index holder
	Nodes[,] _NeighbourSaver; //holds the refrence of the nodes neighbours that im searching through
	int[] _ListStartingPosition = new int[1];//this holds the index on which index the AI should start at

	float _MapCollisionCost = 0;//this is used to calculate the movecost to the searching through node
	Nodes _NodeHolder = null; //just to hold a node
	Nodes _CurrentNode; //Holds the node im currently searching with
	float _LowerstFScore = 100000; //This is just a value to get the "best" path to the target, if there is an enormous amount of nodes then this value must be increased
	int _RemakeIndex = 0;//used to calculate which index the node is stored at, when im reversing the path
	int _TheSize = 0;//size of the list

	public AStarPathFinding_Nodes(int size){
		_TheSize = size;
		_OpenList = new Nodes[size];
		_ClosedList = new Nodes[size]; 
		_ThePath = new Nodes[size];
		_ListStartingPosition [0] = size;
	}

	public void SetEndStartNode(Nodes[] start, Nodes[] end){
		_StartNode = start;
		_EndNode = end;
	}

	public Nodes[] GetListRef(){
		return _ThePath;
	}
	public int[] GetListindexref(){
		return _ListStartingPosition;
	}

	public void CreatePath() {//Starts A* and clears all the nodes so that they are rdy for the next search
		_NodeIndexSaved = 0;
		_OpenListAtIndex = 0;
		_ClosedListAtIndex = 0;
		_RemakeIndex = 0;

		if (AStartAlgorithm () == true) {//if true then the A* found a path
			_ListStartingPosition [0] = _TheSize - _RemakeIndex;
		} else {
			_ListStartingPosition [0] = _TheSize;//if this == thesize then the AI know that it cant walk
		}

		for (int i = 0; i < _OpenListAtIndex; i++) {//going through the list to se the bool to false to say that the node can be searched through again
			_OpenList [i].NodeSearchedThrough = false;
		}

		for (int i = 0; i < _ClosedListAtIndex; i++) {
			_ClosedList [i].NodeSearchedThrough = false;
		}

	}

	bool AStartAlgorithm() {//A*. 

		_StartNode [0].NodeSearchedThrough = true;
		_OpenList [_OpenListAtIndex++] = _StartNode [0];

		while (_ClosedListAtIndex < _TheSize) {//if the clostlistindex is the same as the size of the array stop
			_LowerstFScore = 10000000;

			for (int i = 0; i < _OpenListAtIndex; i++) {//itterating through to find the lowest fcost
				_NodeHolder = _OpenList [i];
				if (_NodeHolder.FCost < _LowerstFScore) {
					_CurrentNode = _NodeHolder;
					_NodeIndexSaved = i;
					_LowerstFScore = _CurrentNode.FCost;
				}
			} 
		

			_ClosedList [_ClosedListAtIndex++] = _CurrentNode;//adding to closedlist
			_OpenList [_NodeIndexSaved] = _OpenList [--_OpenListAtIndex];//replacind the node im currently searching with, with the last node in openlist

			if (_CurrentNode == _EndNode [0]) {//If _CurrentNode == ends then the search is completed
				RemakePath ();
				return true;
			}

			_NeighbourSaver = _CurrentNode.NeighbourNodes;//going through neighbours and adding them to openlist if they have not been added yet, if they have, then i see if it cheaper for the currentnode to go to them then the last were
			for (int i = 0; i < 3; i++) {
				for (int j = 0; j < 3; j++) {
					_NodeHolder = _NeighbourSaver [i,j];
					if (_NodeHolder != null && (_MapCollisionCost = _NodeHolder.GetCollision ()) != 100) {//100 == a solid undestroyable wall that cannot be walked on
						if (i != 1 && j != 1) {//if its a corner node
							if (_NodeHolder.NodeSearchedThrough == false) {//if true then i have not searched through this node
								_NodeHolder.SetParentAndEndCorners (_CurrentNode, _EndNode [0]);
								_OpenList [_OpenListAtIndex++] = _NodeHolder;
							} else if (_NodeHolder.GCost > _CurrentNode.GCost + (_MapCollisionCost * 1.4f)) {//if true then im setting currentnode to the nodes parent 
								_NodeHolder.SetParentCorner (_CurrentNode);
							}
						} else {
							if (_NodeHolder.NodeSearchedThrough == false) {//if true then i have not searched through this node
								_NodeHolder.SetParentAndEndMiddle (_CurrentNode, _EndNode [0]);
								_OpenList [_OpenListAtIndex++] = _NodeHolder;
							} else if (_NodeHolder.GCost > _CurrentNode.GCost + _MapCollisionCost ) {//if true then im setting currentnode to the nodes parent 
								_NodeHolder.SetParentMiddle (_CurrentNode);
							}
						}
					}
				}
			}
		}
		Debug.Log ("Could not find the end");//TODO remove befor FINAL BUILD !!!!!!!!!!!!!!
		return false;
	}

	void RemakePath() {//Makes the path by going to the end node and get the parent, then parent of the parent ....... until your at the start node

		_ThePath [_TheSize - (++_RemakeIndex)] = _EndNode [0];

		if (_EndNode [0] == _StartNode [0]) {
			return;
		} else {
			_CurrentNode = _EndNode [0].GetParent ();
		}
		
		while (true) {
			if (_CurrentNode.GetParent () != null) {
				_ThePath [_TheSize - (++_RemakeIndex)] = _CurrentNode;
				_CurrentNode = _CurrentNode.GetParent ();
			} else {
				return;
			}
		}
	}
}