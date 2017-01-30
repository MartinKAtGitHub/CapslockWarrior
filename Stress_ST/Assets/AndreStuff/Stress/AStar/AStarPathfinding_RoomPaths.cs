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

	List<RoomConnectorCreating> EndRoom;
	Nodes[] EndNode ;


	RoomConnectorCreating _NodSaver;
	float _LowerstFScore = 100000; 

	List<RoomConnectorCreating> SaverList = null;
	RoomConnectorCreating SavedObject;
	RoomConnectorCreating[] ThePath2;


	public void SetStartRoomAndNode(List<RoomConnectorCreating> startRooms, Nodes[] startNode ){
		StartRoom = startRooms;
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

		for (int i = 0; i < StartRoom.Count; i++) {
			StartRoom [i].RoomNode.ClearAll ();
		//	StartRoom [i].GetLeftOrRight = null;
		}

		AStartAlgorithm ();

		remakeindexlist [0] = theSize - remakeIndex;

		for (int i = 0; i < b; i++) {
		//	OpenList [i].RoomNode.ClearAll ();
		//	OpenList [i].GetLeftOrRight = null;
			OpenList [i].RoomNode.Used = false;
		}

		for (int i = 0; i < c; i++) {
		//	ClosedList [i].RoomNode.ClearAll ();
//ClosedList [i].GetLeftOrRight = null;
			ClosedList [i].RoomNode.Used = false;
		}

	}

	Wall_ID connectorhub;

	public void AStartAlgorithm()  {//A*. 
		
		SaverList = StartRoom;

		if (SaverList.Count == 1) {//if im standing on the roomconnector, then this is true (and if im inside a room with only 1 roomconnector)
			_NodSaver = SaverList [0];
			_NodSaver.RoomNode.Used = true;//adding the roomconnector im currently standing on
			OpenList [b++] = _NodSaver;

			SaverList = _NodSaver.ConnectorHubOne.Connectors;//one of the rooms connectorhubs connected to this roomconnector o_O
			if (SaverList != null) {
				for (int i = 0; i < SaverList.Count; i++) {
					SavedObject = SaverList [i];
					if (SavedObject.RoomNode.Used == false) {
						SavedObject.RoomNode.SetParentAndEndRoom (_NodSaver.RoomNode, EndNode [0]);
						SavedObject.GetLeftOrRight = _NodSaver.ConnectorHubOne;//this tells me that when im going through this room im going to search on the other side of the room (1 roomconnector have 2 sides)
					//	SavedObject.RoomNode.Used = true;//this tells me that this room have been searched through (which means that it has a parent and has been put in the search)
						OpenList [b++] = SavedObject;//adding the roomconnector to an array
					}
				}
			}

			SaverList = _NodSaver.ConnectorHubTwo.Connectors;
			if (SaverList != null) {
				for (int i = 0; i < SaverList.Count; i++) {
					SavedObject = SaverList [i];
					if (SavedObject.RoomNode.Used == false) {
						SavedObject.RoomNode.SetParentAndEndRoom (_NodSaver.RoomNode, EndNode [0]);
						SavedObject.GetLeftOrRight = _NodSaver.ConnectorHubTwo;
					//	SavedObject.RoomNode.Used = true;
						OpenList [b++] = SavedObject;
					}
				}
			}
		} else {//if true then im inside a room which means that i must add all roomconnectors connected to this room

			if (SaverList == SaverList [0].ConnectorHubOne.Connectors) {
				connectorhub = SaverList [0].ConnectorHubOne;
				for (int i = 0; i < SaverList.Count; i++) {
					SaverList [i].RoomNode.Used = true;
					SaverList [i].GetLeftOrRight = connectorhub;
					OpenList [b++] = SaverList [i];
				}
			} else {
				connectorhub = SaverList [0].ConnectorHubTwo;
				for (int i = 0; i < SaverList.Count; i++) {
					SaverList [i].RoomNode.Used = true;
					SaverList [i].GetLeftOrRight = connectorhub;
					OpenList [b++] = SaverList [i];
				}
			}
		}

		while (b > 0) {//if the loop has itterated enough times to fill the array then stop

			_LowerstFScore = 100000;

			for (int i = 0; i < b; i++) {//searching through the array to find the room closest to the end
				SavedObject = OpenList[i];
				if (SavedObject.RoomNode._FCost < _LowerstFScore) {//it was faster to make fcost public and just get the variable then go through a getmethod
					_NodSaver = SavedObject;
					a = i;
					_LowerstFScore = SavedObject.RoomNode._FCost;
				}
			}
		
			ClosedList [c++] = _NodSaver;
			OpenList [a] = OpenList [--b];//works just fine :D

			SaverList = EndRoom;
			for (int i = 0; i < SaverList.Count; i++) {
				if (_NodSaver != SaverList [i]) {
				} else {
					RemakePath (_NodSaver);
					return;
				}
			}

			if (_NodSaver.GetLeftOrRight != _NodSaver.ConnectorHubOne) {
				SaverList = _NodSaver.ConnectorHubOne.Connectors;
				for (int i = 0; i < SaverList.Count; i++) {
					SavedObject = SaverList [i];
					if (SavedObject.RoomNode.Used != true) {//if n dont have a parent and n isnt the starting node (starts must be there else it will cause an FATAL error ;-P, your warned :D)
						SavedObject.RoomNode.SetParentAndEndRoom (_NodSaver.RoomNode, EndNode [0]);
						SavedObject.GetLeftOrRight =  _NodSaver.ConnectorHubOne;
						OpenList [b++] = SavedObject;
					} else if (SavedObject.RoomNode._GCost > _NodSaver.RoomNode._GCost + _NodSaver.RoomNode.GetJustWorldSpaceDistance (SavedObject.RoomNode)) {
						SavedObject.RoomNode.SetParentRoom (_NodSaver.RoomNode);
					}
				}
			} else {
				SaverList = _NodSaver.ConnectorHubTwo.Connectors;
				for (int i = 0; i < SaverList.Count; i++) {
					SavedObject = SaverList [i];
					if (SavedObject.RoomNode.Used != true) {//if n dont have a parent and n isnt the starting node (starts must be there else it will cause an FATAL error ;-P, your warned :D)
						SavedObject.RoomNode.SetParentAndEndRoom (_NodSaver.RoomNode, EndNode [0]);
						SavedObject.GetLeftOrRight =  _NodSaver.ConnectorHubTwo;
						OpenList [b++] = SavedObject;
					} else if (SavedObject.RoomNode._GCost > _NodSaver.RoomNode._GCost + _NodSaver.RoomNode.GetJustWorldSpaceDistance (SavedObject.RoomNode)) {
						SavedObject.RoomNode.SetParentRoom (_NodSaver.RoomNode);
					}
				}
			}
		}
	}


	public void RemakePath (RoomConnectorCreating checkedNodes){//going backwards and getting the path that led to this node, then on that node im getting the path that led to that and going further back 
		if (checkedNodes == StartRoom [0]) {
			return;
		} else {
		ThePath [theSize - (++remakeIndex)] = checkedNodes;
		}

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