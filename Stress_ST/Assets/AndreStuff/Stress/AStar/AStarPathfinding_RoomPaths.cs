using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class AStarPathfinding_RoomPaths{

	RoomConnectorCreating[] OpenList = new RoomConnectorCreating[54];
	RoomConnectorCreating[] ClosedList = new RoomConnectorCreating[54];
	RoomConnectorCreating[] ThePath = new RoomConnectorCreating[54];

	int a = 0; //holding previous nodesaver which now is null 
	int b = 0; //holds the length of the array, optimalization reasons :D
	int c = 0; //closed list index holder
	int remakeIndex = 0;
	int[] remakeindexlist = new int[1];
	int theSize = 0;

	RoomConnectorCreating _NodsSaver;


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

	List<RoomConnectorCreating> SaverList;
	RoomConnectorCreating SavedObject;
	RoomConnectorCreating[] ThePath2;

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

	public RoomConnectorCreating[] GetListRef(){
		return ThePath;
	}
	public int[] GetListindexref(){
		return remakeindexlist;
	}



	#region A* For paths

	public void CreatePath() {//Starts A* and clears all the nodes so that they are rdy for the next search
		theSize = 54;
		a = 0;
		b = 0;
		c = 0;
		remakeIndex = 0;

		AStartAlgorithm ();

		remakeindexlist [0] = theSize - remakeIndex;

		for (int i = 0; i < b; i++) {
			OpenList [i].RoomNode.ClearAll ();
			OpenList [i].RoomNode.Used = false;
		}

		for (int i = 0; i < c; i++) {
			ClosedList [i].RoomNode.ClearAll ();
			ClosedList [i].RoomNode.Used = false;
		}
	}

	public void AStartAlgorithm()  {//A*. 
		SaverList = StartRoom;
		for (int i = 0; i < SaverList.Count; i++) {
			SaverList[i].RoomNode.Used = true;
			OpenList [b++] = SaverList [i];
		}

		while (c < theSize) {

			_LowerstFScore = 100000;

			for (int i = 0; i < b; i++) {
				SavedObject = OpenList[i];
				if (SavedObject.RoomNode._FCost < _LowerstFScore) {
					_NodSaver = SavedObject;
					a = i;
					_LowerstFScore = SavedObject.RoomNode._FCost;
				}
			}

		
			SaverList = EndRoom;
			for (int i = 0; i < SaverList.Count; i++) {
				SavedObject = SaverList [i];
				if (_NodSaver == SavedObject) {
					EndNode[0].SetParentRoom (_NodSaver.RoomNode);
					RemakePath (_NodSaver);
					return;
				}
			}

			ClosedList [c++] = _NodSaver;
			OpenList [a] = OpenList [--b];//works just fine :D

			SaverList = _NodSaver.GetNeighbourGroupOne ();
			for (int i = 0; i < SaverList.Count; i++) {
				SavedObject = SaverList [i];
				if (SavedObject.RoomNode.Used == false) {//if n dont have a parent and n isnt the starting node (starts must be there else it will cause an FATAL error ;-P, your warned :D)
					SavedObject.RoomNode.SetParentAndEndRoom (_NodSaver.RoomNode, EndNode [0]);
					OpenList [b++] = SavedObject;
				} else if (SavedObject.RoomNode._GCost > _NodSaver.RoomNode._GCost + _NodSaver.RoomNode.GetJustWorldSpaceDistance (SavedObject.RoomNode)) {
					SavedObject.RoomNode.SetParentRoom (_NodSaver.RoomNode);
				}
			}

			SaverList = _NodSaver.GetNeighbourGroupTwo ();
			for (int i = 0; i < SaverList.Count; i++) {
				SavedObject = SaverList [i];
				if (SavedObject.RoomNode.Used == false) {//if n dont have a parent and n isnt the starting node (starts must be there else it will cause an FATAL error ;-P, your warned :D)
					SavedObject.RoomNode.SetParentAndEndRoom (_NodSaver.RoomNode, EndNode [0]);
					OpenList [b++] = SavedObject;
				} else if (SavedObject.RoomNode._GCost > _NodSaver.RoomNode._GCost + _NodSaver.RoomNode.GetJustWorldSpaceDistance (SavedObject.RoomNode)) {
					SavedObject.RoomNode.SetParentRoom (_NodSaver.RoomNode);
				}
			}
		}
	}


	public void RemakePath (RoomConnectorCreating checkedNodes){//going backwards and getting the path that led to this node, then on that node im getting the path that led to that and going further back 

		ThePath [theSize - (++remakeIndex)] = checkedNodes;

		if (checkedNodes.RoomNode.GetParent () == null) {
			return;
		} else {
			_NodsSaver = checkedNodes.RoomNode.GetParent ().GetRooms ();
		}

		while (true) {
			if (_NodsSaver.RoomNode.GetParent() != null) {
				ThePath [theSize - (++remakeIndex)] = _NodsSaver;
				_NodsSaver = _NodsSaver.RoomNode.GetParent().GetRooms();
			} else {
				if(StartRoom.Count != 1)
					ThePath [theSize - (++remakeIndex)] = _NodsSaver;
				return;
			}
		}

	}
	#endregion


}

















	/*
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


}*/