using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AStartPathfinding_RoomPaths {

	/*

	this creates the path from room a to room b, which is connected by preset paths
	used A* to calculate the path

	*/


	RoomsPathCalculation[] StartRoom;
	Nodes[] StartNode;
	
	RoomsPathCalculation[] EndRoom;
	Nodes[] EndNode;

	AStarPathfinding_Nodes _AStart_Nodes = new AStarPathfinding_Nodes ();
	HashSet<Nodes> _NodesToSeeThrough = new HashSet<Nodes> ();
	HashSet<Nodes> _NodesHaveSeenThrough = new HashSet<Nodes> ();

	List<List<Nodes>> _ThePaths = new List<List<Nodes>>();//contains the path to the target if not null
	Nodes _NodSaver;
	float _LowerstFScore = 100000; 

	public void SetStartRoomAndNode(RoomsPathCalculation[] startRooms, Nodes[] startNodes){
		StartRoom = startRooms;
		StartNode = startNodes;
	}
	
	public void SetEndRoomAndNode(RoomsPathCalculation[] endRoom, Nodes[] endNode){
		EndRoom = endRoom;
		EndNode = endNode;
	}

	public bool IsSomethingNull(){//if anything is null return false (start or end points)
		if (StartRoom == null || EndRoom == null) {
			return false;
		} else {
			if (StartRoom [0] == null || StartNode [0] == null || EndRoom [0] == null || EndNode [0] == null)
				return false;
			else {
				return true;
			}
		}
	}

	
	#region A* For paths

	public List<List<Nodes>> CreatePath() {//Starts A* and clears all the nodes so that they are rdy for the next search

		_ThePaths = new List<List<Nodes>> ();

		AStartAlgorithm ();

		foreach (Nodes n in _NodesToSeeThrough) {
			n.GetPaths().ClearPerSearch ();
		}
		foreach (Nodes n in _NodesHaveSeenThrough) {
			n.GetPaths().ClearPerSearch ();
		}
		_NodesToSeeThrough = new HashSet<Nodes> ();
		_NodesHaveSeenThrough = new HashSet<Nodes> ();;

		return _ThePaths;
	}


	public  void AStartAlgorithm()  {//A*. 


		foreach (Nodes s in StartRoom[0].GetComponent<RoomsPathCalculation>().GetSearchThroughNodes()) {//doing a presearch to add the lenght from this object to the first nodes in the path
			foreach (Nodes x in s.GetPaths ().GetNeighbours()) {//adding next nodes to the list
				x.GetPaths ().SetFirstPath (_AStart_Nodes.CreatePath (StartNode[0], s), EndNode [0].GetID (), _AStart_Nodes.GetNeighbourGoToMoveCost(s, x));//giving the path to the nodes neighbours + setting hcost (distance to end node)
				if (_NodesToSeeThrough.Contains (x) == false){
					_NodesToSeeThrough.Add (x);
				}
			}
		
		}
		if (StartRoom[0] == EndRoom[0]) {//if im at the same room at the start im doing an additional search to get the distance
			EndNode [0].GetPaths ().SetImAtEnd (_AStart_Nodes.CreatePath (StartNode[0], EndNode [0]));
			_NodesToSeeThrough.Add (EndNode [0]);
		}


		while (_NodesToSeeThrough.Count > 0) {

			_LowerstFScore = 100000;

			foreach (Nodes n in _NodesToSeeThrough) {//getting the node with the lowest FScore
				if (n.GetPaths ().GetFCost () < _LowerstFScore) {
					_NodSaver = n;
					_LowerstFScore = n.GetPaths ().GetFCost ();
				}
			}


			_NodesToSeeThrough.Remove (_NodSaver);
			_NodesHaveSeenThrough.Add (_NodSaver);

			if (_NodSaver == EndNode [0]) {//If _NodeSaver == EndNode then the search is complete and sending _closedlist to calculate the path from start to end
				RemakePath (_NodesHaveSeenThrough.Last ());
				return;
			}


			foreach (KeyValuePair<KeyValuePair< List<Nodes>, float >, Nodes> a in _NodSaver.GetPaths().GetNeighbourPaths()) {//adding all neighbours to this node if they are not added
				if (a.Value.GetPaths ().GetParentPath () == null)
					a.Value.GetPaths ().SetEverything (a.Key, _NodSaver, EndNode [0].GetID (), 0);
				else if (a.Value.GetPaths ().GetGCost () > _NodSaver.GetPaths ().GetGCost () + a.Key.Value) {
					a.Value.GetPaths ().SetParentAndGCost (a.Key, _NodSaver, 0);
				}
				if (_NodesHaveSeenThrough.Contains (a.Value) == false && _NodesToSeeThrough.Contains (a.Value) == false) {
					_NodesToSeeThrough.Add (a.Value);
				}
			}

			foreach (KeyValuePair <List<Nodes>,float> s in _NodSaver.GetPaths ().GetPaths ()) {//going through all paths that this node have, and adds the neighbour node to the last node on the path to openlist
				if (s.Key.First () == _NodSaver) {
					foreach (Nodes a in s.Key.Last().GetPaths().GetNeighbours()) {
						if (a.GetPaths ().GetParentPath () == null)
							a.GetPaths ().SetEverything (s, s.Key.First (), EndNode [0].GetID (), _AStart_Nodes.GetNeighbourGoToMoveCost (s.Key.Last (), a));
						else if (a.GetPaths ().GetGCost () > _NodSaver.GetPaths ().GetGCost () + s.Value) {
							a.GetPaths ().SetParentAndGCost (s, s.Key.First (), _AStart_Nodes.GetNeighbourGoToMoveCost (s.Key.Last (), a));
						}
						if (_NodesHaveSeenThrough.Contains (a) == false && _NodesToSeeThrough.Contains (a) == false) {
							_NodesToSeeThrough.Add (a);
						} 
					}
				} else if (s.Key.Last () == _NodSaver) {
					foreach (Nodes a in s.Key.First().GetPaths().GetNeighbours()) {
						if (a.GetPaths ().GetParentPath () == null)
							a.GetPaths ().SetEverything (s, s.Key.Last (), EndNode [0].GetID (), _AStart_Nodes.GetNeighbourGoToMoveCost (s.Key.First (), a));
						else if (a.GetPaths ().GetGCost () > _NodSaver.GetPaths ().GetGCost () + s.Value) {
							a.GetPaths ().SetParentAndGCost (s, s.Key.Last (), _AStart_Nodes.GetNeighbourGoToMoveCost (s.Key.First (), a));
						}
						if (_NodesHaveSeenThrough.Contains (a) == false && _NodesToSeeThrough.Contains (a) == false) {
							_NodesToSeeThrough.Add (a);
						}
					}
				}
			}


			if (_NodSaver.GetRoom () == EndNode [0].GetRoom ()) {//if the node is at the same room as the target do an additional search to se if its closer to go straight to the target

				KeyValuePair<List<Nodes>, float> t = _AStart_Nodes.CreatePath (_NodSaver, EndNode [0]);

				if (EndNode [0].GetPaths ().GetParentPath () == null)
					EndNode [0].GetPaths ().SetImAtEnd (t);
				else if (EndNode [0].GetPaths ().GetGCost () > t.Key.First ().GetPaths ().GetGCost () + t.Value) {
					EndNode [0].GetPaths ().SetParentAndGCost (t, t.Key.First (), 0);
				}
				if (_NodesHaveSeenThrough.Contains (EndNode [0]) == false && _NodesToSeeThrough.Contains (EndNode [0]) == false) {
					_NodesToSeeThrough.Add (EndNode [0]);
				}
			}



		}
		RemakePath (_NodesHaveSeenThrough.Last ());
	}

	Nodes _NodsSaver;

	public void RemakePath (Nodes checkedNodes){//going backwards and getting the path that led to this node, then on that node im getting the path that led to that and going further back 

		_ThePaths.Add (checkedNodes.GetPaths ().GetParentPath ());
		_NodsSaver = null;
		foreach (Nodes s in checkedNodes.GetPaths().GetNeighbours()) {
			if (s == checkedNodes.GetPaths ().GetParentPath ().First ()) {
				_NodsSaver = checkedNodes.GetPaths ().GetParentPath ().Last ();

			} else if (s == checkedNodes.GetPaths ().GetParentPath ().Last ()) {
				_NodsSaver = checkedNodes.GetPaths ().GetParentPath ().First ();
			}
		}
		if (_NodsSaver == null) {
			_NodsSaver = checkedNodes.GetPaths ().GetParentPath ().First ();
		}

		while (true) {
			if (_NodsSaver != StartNode[0]) {

				_ThePaths.Add (_NodsSaver.GetPaths ().GetParentPath ());

				foreach (Nodes s in _NodsSaver.GetPaths().GetNeighbours()) {
					if (s == _NodsSaver.GetPaths ().GetParentPath ().First ()) {
						_NodsSaver = _NodsSaver.GetPaths ().GetParentPath ().Last ();
						break;
					} else if (s == _NodsSaver.GetPaths ().GetParentPath ().Last ()) {

						_NodsSaver = _NodsSaver.GetPaths ().GetParentPath ().First ();
						break;
					}
				}
			} else {
				_ThePaths.Reverse ();
				return;
			}
		}

	}
	#endregion
}