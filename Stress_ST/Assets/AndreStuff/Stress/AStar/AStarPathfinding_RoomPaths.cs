using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AStarPathfinding_RoomPaths{
	List<RoomConnectorCreating> PreviousStartRoom;
	List<RoomConnectorCreating> PreviousEndRoom;

	List<RoomConnectorCreating> StartRoom;
	Nodes[] StartNode ;

	List<RoomConnectorCreating> EndRoom;
	Nodes[] EndNode ;

	List<RoomConnectorCreating> _NodesToSeeThrough = new List<RoomConnectorCreating>(); 
	List<RoomConnectorCreating> _NodesHaveSeenThrough = new List<RoomConnectorCreating>(); 

	List<RoomConnectorCreating> _ThePaths = new List<RoomConnectorCreating>();//contains the path to the target if not null
	RoomConnectorCreating _NodSaver;
	float _LowerstFScore = 100000; 

	public void SetStartRoomAndNode(List<RoomConnectorCreating> startRooms, Nodes[] startNode ){
		StartRoom = startRooms;
		StartNode = startNode;
	}
	public void SetStartRoom(List<RoomConnectorCreating> startRooms){
		StartRoom = startRooms;
	}
	public void SetEndRoomAndNode(List<RoomConnectorCreating> endRoom, Nodes[] endNode){
		EndRoom = endRoom;
		EndNode = endNode;
	}
	public void SetEndRoom(List<RoomConnectorCreating> endRoom){
		EndRoom = endRoom;
	}
	#region A* For paths

	public List<RoomConnectorCreating> CreatePath() {//Starts A* and clears all the nodes so that they are rdy for the next search

		_ThePaths = new List<RoomConnectorCreating> ();

		AStartAlgorithm ();

		foreach (RoomConnectorCreating n in _NodesToSeeThrough) {
			n.RoomNode.ClearAll();
		}
		foreach (RoomConnectorCreating n in _NodesHaveSeenThrough) {
			n.RoomNode.ClearAll();
		}

		_NodesToSeeThrough = new List<RoomConnectorCreating> ();
		_NodesHaveSeenThrough = new List<RoomConnectorCreating> ();
		return _ThePaths;
	}


	public void AStartAlgorithm()  {//A*. 

		foreach (RoomConnectorCreating s in StartRoom) {//doing a presearch to add the lenght from this object to the first nodes in the path
			s.RoomNode.setParent (StartNode[0]);
			s.RoomNode.SetHCost (EndNode[0]);
			_NodesToSeeThrough.Add (s);
		}


		while (_NodesToSeeThrough.Count > 0) {

			_LowerstFScore = 100000;

			foreach (RoomConnectorCreating n in _NodesToSeeThrough) {//getting the node with the lowest FScore

				if (n.RoomNode.GetFValue () < _LowerstFScore) {
					_NodSaver = n;
					_LowerstFScore = n.RoomNode.GetFValue ();
				}
			}

			_NodesToSeeThrough.Remove (_NodSaver);
			_NodesHaveSeenThrough.Add (_NodSaver);

			foreach(RoomConnectorCreating n in EndRoom){
				if (_NodSaver == n) {
					EndNode[0].setParent (_NodSaver.RoomNode);
					RemakePath (_NodesHaveSeenThrough.Last ());
					return;
				}
			}
		

			foreach (RoomConnectorCreating a in _NodSaver.GetNeighbourGroupOne()) {//adding all neighbours to this node if they are not added
				if (a != _NodSaver) {
					
					if (a.RoomNode.GetParent () == null) {
						a.RoomNode.setParent (_NodSaver.RoomNode);
						a.RoomNode.SetHCost (EndNode [0]);
						_NodesToSeeThrough.Add (a);
					} else if (a.RoomNode.GetGCost () > _NodSaver.RoomNode.GetGCost () + _NodSaver.RoomNode.GetJustWorldSpaceDistance (a.RoomNode)) {
						a.RoomNode.setParent (_NodSaver.RoomNode);
					}
				}
			}

			foreach (RoomConnectorCreating a in _NodSaver.GetNeighbourGroupTwo()) {//adding all neighbours to this node if they are not added
				if (a != _NodSaver) {
					if (a.RoomNode.GetParent () == null) {
						a.RoomNode.setParent (_NodSaver.RoomNode);
						a.RoomNode.SetHCost (EndNode [0]);
						_NodesToSeeThrough.Add (a);
					} else if (a.RoomNode.GetGCost () > _NodSaver.RoomNode.GetGCost () + _NodSaver.RoomNode.GetJustWorldSpaceDistance (a.RoomNode)) {
						a.RoomNode.setParent (_NodSaver.RoomNode);
					}
				}
			}
		}
	}

	RoomConnectorCreating _NodsSaver;

	public void RemakePath (RoomConnectorCreating checkedNodes){//going backwards and getting the path that led to this node, then on that node im getting the path that led to that and going further back 

		_ThePaths.Add (checkedNodes);
		_NodsSaver = checkedNodes.RoomNode.GetParent().GetRooms();

		while (true) {
			if (_NodsSaver != null && _NodsSaver.RoomNode.GetParent() != null) {

				_ThePaths.Add (_NodsSaver);
				_NodsSaver = _NodsSaver.RoomNode.GetParent().GetRooms();
			
			} else {
				_ThePaths.Reverse ();
				return;
			}
		}

	}
	#endregion


}