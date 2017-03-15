using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Nodes {

	//node info
	public float[] PathfindingNodeID;//this array holds the move cost to each tile (wall , water, sand .....)
	public int MapCollision = 1;//this is used to set the correct id for the tile (wall, water, sand .....)

	public bool NodeSearchedThrough = false;//if bool is used to check if the node have been searched through

	public float GCost = 0;//GCost is the cost that have been used to get to this node
	public float FCost = 0;//Gcost+Hcost
	float _HCost = 0;//HCost is how far away the end node is from this node


	float[,] _NodeID = new float[1, 2];//Node ID, which is the nodes coordinates in the collision map

	Nodes _ParentNode = null;
	public Nodes[,] NeighbourNodes = new Nodes[3, 3];//neighbours for the nodes

	float _XValue = 0, _YValue = 0; //just some values that im using
	float[,] _GetIDSaver;//just an empty array that im using

	public DefaultBehaviour WhatIsNodeOccupiedWith;

	/// <summary>
	/// 1. Initializes a new instance of the <see cref="Nodes"/> class.
	/// </summary>
	/// <param name="ID">is the parameterID in XY coordinates,</param>
	/// <param name="collision">is the collisionid the node will have, so when someone collides with it then we know what to apply</param>
	public Nodes(float[,] ID, int collision) {
		_NodeID = ID;
		MapCollision = collision;
	}

	/// <summary>
	/// 2. Initializes a new instance of the <see cref="Nodes"/> class.
	/// </summary>
	/// <param name="ID">is the parameterID in XY coordinates,</param>
	/// <param name="collision">is the collisionid the node will have, so when someone collides with it then we know what to apply</param>
	/// <param name="pathnodeid">this is the array that holds the movecost to each tile</param>
	public Nodes(float[,] ID, int collision, float[] pathnodeid) {
		_NodeID = ID;
		MapCollision = collision;
		PathfindingNodeID = pathnodeid;
	}
		
	public float GetCollision() {
		return PathfindingNodeID[MapCollision];
	}

	public float[,] GetID() {
		return _NodeID;
	}

	public Nodes GetParent() {
		return _ParentNode;
	}

	public void SetParentAndEndCorners(Nodes theParent, Nodes theEnd) {//setting parent gcost and hcost
		NodeSearchedThrough = true;
		_ParentNode = theParent;
		_GetIDSaver = theEnd.GetID ();

		_XValue = _GetIDSaver [0, 0] - _NodeID [0, 0];
		_YValue = _GetIDSaver [0, 1] - _NodeID [0, 1];

		if (_XValue < 0)
			_XValue *= -1;
		if (_YValue < 0)
			_YValue *= -1;

		_HCost = _XValue + _YValue;
		GCost = (PathfindingNodeID[MapCollision] * 1.4f) + _ParentNode.GCost;
		FCost = _HCost + GCost;
	}

	public void SetParentAndEndMiddle(Nodes theParent, Nodes theEnd) {//setting parent gcost and hcost
		NodeSearchedThrough = true;
		_ParentNode = theParent;
		_GetIDSaver = theEnd.GetID ();

		_XValue = _GetIDSaver [0, 0] - _NodeID [0, 0];
		_YValue = _GetIDSaver [0, 1] - _NodeID [0, 1];

		if (_XValue < 0)
			_XValue *= -1;
		if (_YValue < 0)
			_YValue *= -1;

		_HCost = _XValue + _YValue;
		GCost = PathfindingNodeID[MapCollision] + _ParentNode.GCost;
		FCost = _HCost + GCost;
	}

	public void ClearAll() {// Clears all data to preprair for the next search
		NodeSearchedThrough = false;
		_ParentNode = null;
		GCost = 0;
		_HCost = 0;
		FCost = 0;
	}

	public void SetParentCorner(Nodes theParent) {//Adding the parent GCost to this nodes gcost and adding the distance the parent had to travel to this node gcost

		_ParentNode = theParent;
		GCost = (PathfindingNodeID[MapCollision] * 1.4f) + _ParentNode.GCost;
		FCost = _HCost + GCost;
	}

	public void SetParentMiddle(Nodes theParent) {//Adding the parent GCost to this nodes gcost and adding the distance the parent had to travel to this node gcost

		_ParentNode = theParent;
		GCost = PathfindingNodeID[MapCollision] + _ParentNode.GCost;
		FCost = _HCost + GCost;
	}

	#region RoomConnector

	RoomConnectorCreating _MyRooms;
	List<Nodes> _NeighboursRooms = new List<Nodes>();//neighbours for the roompath  (could also be public)


	public void SetRooms(RoomConnectorCreating room){
		_MyRooms = room;
	}

	public RoomConnectorCreating GetRooms(){
		return _MyRooms;
	}

	public void SetRoomNeighbours(Nodes theNeighbor) {
		_NeighboursRooms.Add(theNeighbor);
	}	

	public float GetJustWorldSpaceDistance(Nodes theothernode){
		_GetIDSaver = theothernode.GetID ();

		_XValue = _GetIDSaver[0, 0] - _NodeID[0, 0];
		_YValue = _GetIDSaver[0, 1] - _NodeID[0, 1];

		if(_XValue < 0){
			_XValue *= -1;
		}
		if(_YValue < 0){
			_YValue *= -1;
		}
		//Debug.Log (_XValue + " | " + _YValue);
		return (_XValue + _YValue);
	}

	public void SetParentAndEndRoom(Nodes theParent, Nodes endNode) {//Adding the parent GCost to this nodes gcost and adding the distance the parent had to travel to this node gcost
		NodeSearchedThrough = true;
		_ParentNode = theParent;
		_GetIDSaver = endNode.GetID ();

		_XValue = _GetIDSaver [0, 0] - _NodeID [0, 0];
		_YValue = _GetIDSaver [0, 1] - _NodeID [0, 1];


		if (_XValue < 0) {
			_XValue *= -1;
		}
		if (_YValue < 0) {
			_YValue *= -1;
		}

		_HCost = _XValue + _YValue;
		_GetIDSaver = theParent.GetID ();

		_XValue = _GetIDSaver [0, 0] - _NodeID [0, 0];
		_YValue = _GetIDSaver [0, 1] - _NodeID [0, 1];

		if (_XValue < 0) {
			_XValue *= -1;
		}
		if (_YValue < 0) {
			_YValue *= -1;
		}

		GCost = _ParentNode.GCost + _XValue + _YValue;
		FCost = _HCost + GCost;
	}

	public void SetParentRoom(Nodes theParent) {//Adding the parent GCost to this nodes gcost and adding the distance the parent had to travel to this node gcost
		NodeSearchedThrough = true;
		_ParentNode = theParent;

		if (_ParentNode != null) {
			_XValue = theParent.GetID()[0, 0] - _NodeID[0, 0];
			_YValue = theParent.GetID()[0, 1] - _NodeID[0, 1];

			if(_XValue < 0){
				_XValue *= -1;
			}
			if(_YValue < 0){
				_YValue *= -1;
			}

			GCost =  _ParentNode.GCost + _XValue + _YValue;
			FCost = _HCost + GCost;
		}
	}
	#endregion

}
