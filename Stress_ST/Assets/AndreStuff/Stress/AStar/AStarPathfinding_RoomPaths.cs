using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class AStarPathfinding_RoomPaths{

	RoomConnectorCreating[] _OpenList = new RoomConnectorCreating[54];//list that holds rooms that i havent searched through
	RoomConnectorCreating[] _ClosedList = new RoomConnectorCreating[54];//list that have been searched through
	RoomConnectorCreating[] _ThePath = new RoomConnectorCreating[54];//the information to avoid absolute death

	int _RoomIndexSaved = 0; //holding previous objectholder which now is null 
	int _OpenListAtIndex = 0; //this holds the next position which to add the next element
	int _ClosedListAtIndex = 0; //closed list index holder

	int _RemakeIndex = 0;//used to calculate which index the room is stored at, when im reversing the path
	int[] _ListStartingPosition = new int[1];//this holds the index on which index the AI should start at
	int _TheSize = 0;//size of lists

	RoomConnectorCreating _CurrentRoom;//room im searching with

	List<RoomConnectorCreating> _StartRoom;
	List<RoomConnectorCreating> _EndRoom;
	Nodes[] _EndNode ;//used to set roomconnector closest to the target
	Nodes[] MyWorldNode = new Nodes[1];

	float _LowerstFScore = 100000; 

	List<RoomConnectorCreating> _ListHoler = null;
	RoomConnectorCreating _ObjectHolder;
	Wall_ID _ConnectorHub; //is used to know which side of the pathconnector i have searched

	public AStarPathfinding_RoomPaths(int size, Nodes pos){
		_TheSize = size;
		_OpenList = new RoomConnectorCreating[size];
		_ClosedList = new RoomConnectorCreating[size]; 
		_ThePath = new RoomConnectorCreating[size];
		_ListStartingPosition [0] = size;
		MyWorldNode[0] = pos;
	}

	public void SetStartRoom(List<RoomConnectorCreating> startRooms ){
		_StartRoom = startRooms;
	}

	public void SetEndRoomAndNode(List<RoomConnectorCreating> endRoom, Nodes[] endNode){
		_EndRoom = endRoom;
		_EndNode = endNode;
	}
	public void SetEndRoom(List<RoomConnectorCreating> endRoom){
		_EndRoom = endRoom;
	}

	public RoomConnectorCreating[] GetListRef(){
		return _ThePath;
	}
	public int[] GetListindexref(){
		return _ListStartingPosition;
	}

	public Nodes[] GetPosNode (){
		return MyWorldNode;
	}

	#region A* For paths

	public bool CreatePath() {//Starts A* and clears all the nodes so that they are rdy for the next search

		_RoomIndexSaved = 0;
		_OpenListAtIndex = 0;
		_ClosedListAtIndex = 0;
		_RemakeIndex = 0;

		if (_StartRoom == null || _EndRoom == null) {
			return false;
		}

		for (int i = 0; i < _StartRoom.Count; i++) {
			_StartRoom [i].RoomNode.ClearAll ();
		}

		AStartAlgorithm ();

		_ListStartingPosition [0] = _TheSize - _RemakeIndex;

		for (int i = 0; i < _OpenListAtIndex; i++) {
			_OpenList [i].RoomNode.NodeSearchedThrough = false;
		}

		for (int i = 0; i < _ClosedListAtIndex; i++) {
			_ClosedList [i].RoomNode.NodeSearchedThrough = false;
		}

		return true;
	}


	public void AStartAlgorithm()  {//A*. 
		
		_ListHoler = _StartRoom;

		#region PreSearch

		if (_ListHoler.Count == 1) {//if im standing on the roomconnector, then this is true (and if im inside a room with only 1 roomconnector)
			_CurrentRoom = _ListHoler [0];
			_CurrentRoom.RoomNode.NodeSearchedThrough = true;
			_OpenList [_OpenListAtIndex++] = _CurrentRoom;//adding the only roomconnector

			_ListHoler = _CurrentRoom.ConnectorHubOne.Connectors;//one of the rooms connectorhubs connected to this roomconnector o_O		
			if (_ListHoler != null) {
				for (int i = 0; i < _ListHoler.Count; i++) {
					_ObjectHolder = _ListHoler [i];
					if (_ObjectHolder.RoomNode.NodeSearchedThrough == false) {
						_ObjectHolder.RoomNode.SetParentAndEndRoom (_CurrentRoom.RoomNode, _EndNode [0]);
						_ObjectHolder.GetLeftOrRight = _CurrentRoom.ConnectorHubOne;//this tells me that when im going through this room im going to search on the other side of the room (1 roomconnector have 2 sides)
						_OpenList [_OpenListAtIndex++] = _ObjectHolder;//adding the roomconnector to an array
					}
				}
			}

			_ListHoler = _CurrentRoom.ConnectorHubTwo.Connectors;
			if (_ListHoler != null) {
				for (int i = 0; i < _ListHoler.Count; i++) {
					_ObjectHolder = _ListHoler [i];
					if (_ObjectHolder.RoomNode.NodeSearchedThrough == false) {
						_ObjectHolder.RoomNode.SetParentAndEndRoom (_CurrentRoom.RoomNode, _EndNode [0]);
						_ObjectHolder.GetLeftOrRight = _CurrentRoom.ConnectorHubTwo;
						_OpenList [_OpenListAtIndex++] = _ObjectHolder;
					}
				}
			}
		} else {//if true then im inside a room which means that i must add all roomconnectors connected to this room

			if (_ListHoler == _ListHoler [0].ConnectorHubOne.Connectors) {
				_ConnectorHub = _ListHoler [0].ConnectorHubOne;
				for (int i = 0; i < _ListHoler.Count; i++) {
					_ListHoler [i].RoomNode.NodeSearchedThrough = true;
					_ListHoler [i].GetLeftOrRight = _ConnectorHub;
					_OpenList [_OpenListAtIndex++] = _ListHoler [i];
				}
			} else {
				_ConnectorHub = _ListHoler [0].ConnectorHubTwo;
				for (int i = 0; i < _ListHoler.Count; i++) {
					_ListHoler [i].RoomNode.NodeSearchedThrough = true;
					_ListHoler [i].GetLeftOrRight = _ConnectorHub;
					_OpenList [_OpenListAtIndex++] = _ListHoler [i];
				}
			}
		}

		#endregion

		while (_OpenListAtIndex > 0) {//if the loop has itterated enough times to fill the array then stop

			_LowerstFScore = 100000;

			for (int i = 0; i < _OpenListAtIndex; i++) {//searching through the array to find the room closest to the end
				_ObjectHolder = _OpenList[i];
				if (_ObjectHolder.RoomNode.FCost < _LowerstFScore) {//it was faster to make fcost public and just get the variable then go through a getmethod
					_CurrentRoom = _ObjectHolder;
					_RoomIndexSaved = i;
					_LowerstFScore = _ObjectHolder.RoomNode.FCost;
				}
			}
		
			_ClosedList [_ClosedListAtIndex++] = _CurrentRoom;//adding room to list
			_OpenList [_RoomIndexSaved] = _OpenList [--_OpenListAtIndex];//moving the last room in openlist to the index currentroom were at

			_ListHoler = _EndRoom;
			for (int i = 0; i < _ListHoler.Count; i++) {//checking if the room im in is connected to the end room
				if (_CurrentRoom != _ListHoler [i]) {
				} else {
					RemakePath (_CurrentRoom);
					return;
				}
			}

			if (_CurrentRoom.GetLeftOrRight != _CurrentRoom.ConnectorHubOne) {//checking which side of the pathconnector that have been searched, then go through its neighbours and see if something is closer to the target
				_ListHoler = _CurrentRoom.ConnectorHubOne.Connectors;
				for (int i = 0; i < _ListHoler.Count; i++) {
					_ObjectHolder = _ListHoler [i];
					if (_ObjectHolder.RoomNode.NodeSearchedThrough != true) {//if true then this object have been searched through , if not set info
						_ObjectHolder.RoomNode.SetParentAndEndRoom (_CurrentRoom.RoomNode, _EndNode [0]);
						_ObjectHolder.GetLeftOrRight = _CurrentRoom.ConnectorHubOne;
						_OpenList [_OpenListAtIndex++] = _ObjectHolder;
					} else if (_ObjectHolder.RoomNode.GCost > _CurrentRoom.RoomNode.GCost + _CurrentRoom.RoomNode.GetJustWorldSpaceDistance (_ObjectHolder.RoomNode)) {//checking if currentroom is closer to the objectholder then its parent is
						_ObjectHolder.RoomNode.SetParentRoom (_CurrentRoom.RoomNode);
					}
				}
			} else {//same logic just different side of the room
				_ListHoler = _CurrentRoom.ConnectorHubTwo.Connectors;
				for (int i = 0; i < _ListHoler.Count; i++) {
					_ObjectHolder = _ListHoler [i];
					if (_ObjectHolder.RoomNode.NodeSearchedThrough != true) {//if n dont have a parent and n isnt the starting node (starts must be there else it will cause an FATAL error ;-P, your warned :D)
						_ObjectHolder.RoomNode.SetParentAndEndRoom (_CurrentRoom.RoomNode, _EndNode [0]);
						_ObjectHolder.GetLeftOrRight =  _CurrentRoom.ConnectorHubTwo;
						_OpenList [_OpenListAtIndex++] = _ObjectHolder;
					} else if (_ObjectHolder.RoomNode.GCost > _CurrentRoom.RoomNode.GCost + _CurrentRoom.RoomNode.GetJustWorldSpaceDistance (_ObjectHolder.RoomNode)) {
						_ObjectHolder.RoomNode.SetParentRoom (_CurrentRoom.RoomNode);
					}
				}
			}
		}
	}


	public void RemakePath (RoomConnectorCreating checkedNodes){//going backwards and getting the path that led to this node, then on that node im getting the path that led to that and going further back 

		if (checkedNodes == _StartRoom [0]) {
			return;
		} else {
			_ThePath [_TheSize - (++_RemakeIndex)] = checkedNodes;
		}

		if (checkedNodes.RoomNode.GetParent () == null) {
			return;
		} else {
			_CurrentRoom = checkedNodes.RoomNode.GetParent ().GetRooms ();
		}

		while (true) {
			if (_CurrentRoom.RoomNode.GetParent() != null) {
				_ThePath [_TheSize - (++_RemakeIndex)] = _CurrentRoom;
				_CurrentRoom = _CurrentRoom.RoomNode.GetParent().GetRooms();
			} else {
				if(_StartRoom.Count != 1)
					_ThePath [_TheSize - (++_RemakeIndex)] = _CurrentRoom;
				return;
			}
		}

	}
	#endregion


}
