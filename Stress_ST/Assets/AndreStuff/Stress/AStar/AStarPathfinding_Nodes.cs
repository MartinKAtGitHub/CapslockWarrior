using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AStarPathFinding_Nodes {
	 
	List<Nodes> _OpenList = new List<Nodes>();//Holds the nodes that im going to search
	List<Nodes> _ClosedList = new List<Nodes>();//Holds the nodes that have been search

	List<Nodes> _ThePath = new List<Nodes>();//

	Nodes[] _StartNode; //This is the node that this object is an and starts searching from
	Nodes[] _EndNode; //end node, destination node
	Nodes _NodeSaver; //just to hold some nodes while searching

	float _LowerstFScore = 100000; //This is just a value to get the "best" path to the target, if there is an enormous amount of nodes then this value must be increased

//	Nodes[] OpenList;
//	Nodes[] ClosedList;
//	int a = 0; //holding previous nodesaver which now is null 
//	int b = 0; //holds the length of the array, optimalization reasons :D
//	int c = 0; //closed list index holder

	public AStarPathFinding_Nodes(int size){
//		OpenList = new Nodes[size];
//		ClosedList = new Nodes[size];
	}

	public void SetEndStartNode(Nodes[] start, Nodes[] end){
		_StartNode = start;
		_EndNode = end;
	}

	public List<Nodes> GetThePath(){
		return _ThePath;
	}

	//TODO maybe change list to array in nodes, might be faster to itterate through




	public List<Nodes> CreatePath() {//Starts A* and clears all the nodes so that they are rdy for the next search


		_ThePath = new List<Nodes> ();

		AStartAlgorithm();
	
		foreach (Nodes n in _OpenList) {
			n.Used = false;
		}
		foreach (Nodes n in _ClosedList) {
			n.Used = false;
		}

		_OpenList = new List<Nodes> ();
		_ClosedList = new List<Nodes> ();

		return _ThePath;
	}


	List<Nodes> AStartAlgorithm() {//A*. 

		_NodeSaver = null;//Holds the current node that im searching with

		_StartNode [0].Used = true;
		_StartNode[0].SetHCost(_EndNode[0]);
		_OpenList.Add(_StartNode[0]);

		while (_OpenList.Count > 0) {

			_LowerstFScore = 10000;

			foreach (Nodes n in _OpenList) {//Goes through the _OpenList to search after the lowest F value, 			TODO improve this in some way, if i can manage to somehow sort this in an efficient way then (Y) 
				if (n.GetFValue() < _LowerstFScore) {
					_NodeSaver = n;
					_LowerstFScore = n.GetFValue();
				}
			}

			if (_NodeSaver == _EndNode[0]) {//If _NodeSaver == ends then the search is complete and sending _closedlist to calculate the path from start to end
				return RemakePath();
			}

			_OpenList.Remove(_NodeSaver);
			_ClosedList.Add(_NodeSaver);

			foreach (Nodes n in _NodeSaver.GetNeighbours()) {
				if (n.GetCollision () != 100) {
					if (n.Used == false) {//if n dont have a parent and n isnt the starting node (starts must be there else it will cause an FATAL error ;-P, your warned :D)
						n.SetParentAndEnd(_NodeSaver, _EndNode[0]);
						_OpenList.Add (n);
					} else if (n.GetGCost () > _NodeSaver.GetGCost () + _NodeSaver.GetJustMoveGCost (n)) {//calculates best route through these values
						n.SetParent (_NodeSaver, n.GetCollision ());
					}
				}
			}
		}
		Debug.Log ("Could not find the end");
		return null;
	}

	List<Nodes> RemakePath() {//Makes the path by going to the end node and get the parent, then parent of the parent ....... until your at the start node

		_ThePath.Add(_EndNode[0]);

		if (_EndNode [0].GetParent () != null) {
			_NodeSaver = _EndNode [0].GetParent ();
		
			if (_NodeSaver == _StartNode [0]) {
				_ThePath.Reverse ();
				return _ThePath;
			}
		}else
			return _ThePath;
		
		while (true) {
			if (_NodeSaver.GetParent() != null) {
				_ThePath.Add(_NodeSaver);
				_NodeSaver = _NodeSaver.GetParent();
			} else {
				_ThePath.Reverse ();
				return _ThePath;
			}
		}
	}

}
/*
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AStarPathFinding_Nodes {

	List<Nodes> _OpenList = new List<Nodes>();//Holds the nodes that im going to search
	List<Nodes> _ClosedList = new List<Nodes>();//Holds the nodes that have been search

	List<Nodes> _ThePath = new List<Nodes>();//

	Nodes[] _StartNode; //This is the node that this object is an and starts searching from
	Nodes[] _EndNode; //end node, destination node
	Nodes _NodeSaver; //just to hold some nodes while searching

	float _LowerstFScore = 100000; //This is just a value to get the "best" path to the target, if there is an enormous amount of nodes then this value must be increased

//	Nodes[] OpenList;
//	Nodes[] ClosedList;
//	int a = 0; //holding previous nodesaver which now is null 
//	int b = 0; //holds the length of the array, optimalization reasons :D
//	int c = 0; //closed list index holder

	public AStarPathFinding_Nodes(int size){
//		OpenList = new Nodes[size];
//		ClosedList = new Nodes[size];
	}

	public void SetEndStartNode(Nodes[] start, Nodes[] end){
		_StartNode = start;
		_EndNode = end;
	}

	public List<Nodes> GetThePath(){
		return _ThePath;
	}

	//TODO maybe change list to array in nodes, might be faster to itterate through




	public List<Nodes> CreatePath() {//Starts A* and clears all the nodes so that they are rdy for the next search


		_ThePath = new List<Nodes> ();

		AStartAlgorithm();
	
		foreach (Nodes n in _OpenList) {
			n.Used = false;
		}
		foreach (Nodes n in _ClosedList) {
			n.Used = false;
		}

		_OpenList = new List<Nodes> ();
		_ClosedList = new List<Nodes> ();

		return _ThePath;
	}


	List<Nodes> AStartAlgorithm() {//A*. 

		_NodeSaver = null;//Holds the current node that im searching with

		_StartNode [0].Used = true;
		_StartNode[0].SetHCost(_EndNode[0]);
		_OpenList.Add(_StartNode[0]);

		while (_OpenList.Count > 0) {

			_LowerstFScore = 10000;

			foreach (Nodes n in _OpenList) {//Goes through the _OpenList to search after the lowest F value, 			TODO improve this in some way, if i can manage to somehow sort this in an efficient way then (Y) 
				if (n.GetFValue() < _LowerstFScore) {
					_NodeSaver = n;
					_LowerstFScore = n.GetFValue();
				}
			}

			if (_NodeSaver == _EndNode[0]) {//If _NodeSaver == ends then the search is complete and sending _closedlist to calculate the path from start to end
				return RemakePath();
			}

			_OpenList.Remove(_NodeSaver);
			_ClosedList.Add(_NodeSaver);

			foreach (Nodes n in _NodeSaver.GetNeighbours()) {
				if (n.GetCollision () != 100) {
					if (n.Used == false) {//if n dont have a parent and n isnt the starting node (starts must be there else it will cause an FATAL error ;-P, your warned :D)
						n.SetParentAndEnd(_NodeSaver, _EndNode[0]);
						_OpenList.Add (n);
					} else if (n.GetGCost () > _NodeSaver.GetGCost () + _NodeSaver.GetJustMoveGCost (n)) {//calculates best route through these values
						n.SetParent (_NodeSaver, n.GetCollision ());
					}
				}
			}
		}
		Debug.Log ("Could not find the end");
		return null;
	}

	List<Nodes> RemakePath() {//Makes the path by going to the end node and get the parent, then parent of the parent ....... until your at the start node

		_ThePath.Add(_EndNode[0]);

		if (_EndNode [0].GetParent () != null) {
			_NodeSaver = _EndNode [0].GetParent ();
		
			if (_NodeSaver == _StartNode [0]) {
				_ThePath.Reverse ();
				return _ThePath;
			}
		}else
			return _ThePath;
		
		while (true) {
			if (_NodeSaver.GetParent() != null) {
				_ThePath.Add(_NodeSaver);
				_NodeSaver = _NodeSaver.GetParent();
			} else {
				_ThePath.Reverse ();
				return _ThePath;
			}
		}
	}

}

*/