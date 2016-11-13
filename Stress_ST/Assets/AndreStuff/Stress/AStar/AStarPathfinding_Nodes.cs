using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AStarPathfinding_Nodes {

	HashSet<Nodes> _OpenList = new HashSet<Nodes>();//Holds the nodes that im going to search
	HashSet<Nodes> _ClosedList = new HashSet<Nodes>();//Holds the nodes that have been search
	KeyValuePair<List<Nodes>, float> _ThePath = new KeyValuePair<List<Nodes>, float>();//This is the path with the coordinates to the player/enemy
	List<Nodes> _NewPath = new List<Nodes>();//

	Nodes _StartNode; //This is the node that this object is an and starts searching from
	Nodes _EndNode; //end node, destination node
	Nodes _NodeSaver; //just to hold some nodes while searching

	float _LowerstFScore = 100000; //This is just a value to get the "best" path to the target, if there is an enormous amount of nodes then this value must be increased

	public KeyValuePair<List<Nodes>, float>  CreatePath(Nodes startNode, Nodes endNode) {//Starts A* and clears all the nodes so that they are rdy for the next search
		

		List<Nodes> saved = (AStartAlgorithm(startNode, endNode));
		if (saved == null) {
			_ThePath = new KeyValuePair<List<Nodes>, float> (saved, 0);
		} else {
			_ThePath = new KeyValuePair<List<Nodes>, float> (saved, saved.Last ().GetGCost ());
		}

		foreach (Nodes n in _OpenList) {
			n.ClearAll();
		}
		foreach (Nodes n in _ClosedList) {
			n.ClearAll();
		}

		_OpenList = new HashSet<Nodes>();
		_ClosedList = new HashSet<Nodes>();

		return _ThePath;
	}

	public KeyValuePair<KeyValuePair< List<Nodes>, float >, Nodes> CreateChildParentPath(Nodes startNode, Nodes endNode){
		return new KeyValuePair<KeyValuePair< List<Nodes>, float >, Nodes> (new KeyValuePair<List<Nodes>, float> (new List<Nodes> (){ startNode }, startNode.GetJustSideGCost (endNode)), endNode);
	}

	public float GetNeighbourGoToMoveCost(Nodes startNode, Nodes endNode){
		return startNode.GetJustSideGCost (endNode);
	}

	List<Nodes> AStartAlgorithm(Nodes starts, Nodes ends) {//A*. 
		
		_NodeSaver = null;//Holds the current node that im searching with

		starts.SetHCost(ends);
		_OpenList.Add(starts);

		while (_OpenList.Count > 0) {

			_LowerstFScore = 100000;

			foreach (Nodes n in _OpenList) {//Goes through the _OpenList to search after the lowest F value, 			TODO improve this in some way, if i can manage to somehow sort this in an efficient way then (Y) 
				if (n.GetFValue() < _LowerstFScore) {
					_NodeSaver = n;
					_LowerstFScore = n.GetFValue();
				}
			}

			_OpenList.Remove(_NodeSaver);
			_ClosedList.Add(_NodeSaver);

			if (_NodeSaver == ends) {//If _NodeSaver == ends then the search is complete and sending _closedlist to calculate the path from start to end
				return RemakePath(_ClosedList.Last());
			}

			foreach (Nodes n in _NodeSaver.GetNeighbours()) {
				if (n.GetParent () == null && n != starts) {//if n dont have a parent and n isnt the starting node (starts must be there else it will cause an FATAL error ;-P, your warned :D)
					n.SetParent (_NodeSaver, n.GetCollision ());
					n.SetHCost (ends);
				} else if (n.GetGCost () > _NodeSaver.GetGCost () + _NodeSaver.GetJustMoveGCost (n)) {//calculates best route through these values
					n.SetParent (_NodeSaver, n.GetCollision ());
				}
				if (!(_OpenList.Contains (n) == true || _ClosedList.Contains (n) == true)) {//adds all neighbour to openlist if it isnt already there or if it has already been searched through
					_OpenList.Add (n);
				}
			}
		}
		Debug.Log ("Could not find a path end " + ends.GetID()[0,0] + " | " +  ends.GetID()[0,1] );
		Debug.Log ("Could not find a path start " + starts.GetID()[0,0] + " | " +  starts.GetID()[0,1] );
		return null;
	}

	List<Nodes> RemakePath(Nodes checkedNodes) {//Makes the path by going to the end node and get the parent, then parent of the parent ....... until your at the start node
		_NewPath = new List<Nodes>();
		_NodeSaver = checkedNodes;

		_NewPath.Add(_NodeSaver);
		while (true) {
			if (_NodeSaver != null && _NodeSaver.GetParent() != null) {
				_NewPath.Add(_NodeSaver.GetParent());
				_NodeSaver = _NodeSaver.GetParent();
			} else {
				
				_NewPath.Reverse ();
				return _NewPath;
			}
		}
	}

}
