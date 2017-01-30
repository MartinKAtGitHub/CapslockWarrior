using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Nodes {

	//node info
	public int[] _PathfindingNodeID;


	public bool Used = false;

	public int _MapCollision = 1;//this is what decides what object the player is on(a wall, water ....)
	float[,] _NodeID = new float[1, 2];//Node ID, which is the nodes coordinates in the collision map
	Nodes _ParentNode = null;
	List<Nodes> _Neighbours = new List<Nodes>();
	public float _GCost = 0;//GCost is the cost that have been used to get to this node
	float _HCost = 0;//HCost is how far away the end node is from this node
	public float _FCost = 0;

	float _XValue = 0, _YValue = 0; //just some values

	RoomConnectorCreating _MyRooms;

	public Nodes[,] NeighbourNodes = new Nodes[3, 3]; 

	float[,] getidsaver;

	/// <summary>
	/// Initializes a new instance of the <see cref="Nodes"/> class.
	/// </summary>
	/// <param name="ID">is the parameterID in XY coordinates,</param>
	/// <param name="collision">is the collisionid the node will have, so when someone collides with it then we know what to apply</param>
	public Nodes(float[,] ID, int collision) {
		_NodeID = ID;
		_MapCollision = collision;
	}

	public Nodes(float[,] ID, int collision, int[] pathnodeid) {
		_NodeID = ID;
		_MapCollision = collision;
		_PathfindingNodeID = pathnodeid;
	}

	public void SetRooms(RoomConnectorCreating room){
		_MyRooms = room;
	}

	public RoomConnectorCreating GetRooms(){
		return _MyRooms;
	}


	public int GetCollision() {
		return _PathfindingNodeID[_MapCollision];
	}

	public float[,] GetID() {
		return _NodeID;
	}

	public Nodes GetParent() {
		return _ParentNode;
	}

	public List<Nodes> GetNeighbours() {
		return _Neighbours;
	}

	public float GetGCost() {
		return _GCost;
	}

	public float GetJustMoveGCost(Nodes nodeToCheck) {//Gets how expencive it is to travel to nodetocheck

		if (nodeToCheck.GetID () [0, 0] == _NodeID [0, 0] || nodeToCheck.GetID () [0, 1] == _NodeID [0, 1]) {
			return nodeToCheck._PathfindingNodeID[_MapCollision];
		} else {
			return ( nodeToCheck._PathfindingNodeID[_MapCollision] * 1.4f);
		}
	}

	public void SetParentAndEndCorners(Nodes theParent, Nodes theEnd) {//setting parent gcost and hcost
		Used = true;
		_ParentNode = theParent;
		getidsaver = theEnd.GetID ();

		_XValue = getidsaver [0, 0] - _NodeID [0, 0];
		_YValue = getidsaver [0, 1] - _NodeID [0, 1];

		if (_XValue < 0)
			_XValue *= -1;
		if (_YValue < 0)
			_YValue *= -1;

		_HCost = _XValue + _YValue;
		_GCost = (_PathfindingNodeID[_MapCollision] * 1.4f) + _ParentNode._GCost;
		_FCost = _HCost + _GCost;
	}

	public void SetParentAndEndMiddle(Nodes theParent, Nodes theEnd) {//setting parent gcost and hcost
		Used = true;
		_ParentNode = theParent;
		getidsaver = theEnd.GetID ();

		_XValue = getidsaver [0, 0] - _NodeID [0, 0];
		_YValue = getidsaver [0, 1] - _NodeID [0, 1];

		if (_XValue < 0)
			_XValue *= -1;
		if (_YValue < 0)
			_YValue *= -1;

		_HCost = _XValue + _YValue;
		_GCost = _PathfindingNodeID[_MapCollision] + _ParentNode._GCost;
		_FCost = _HCost + _GCost;
	}


		
	public float GetFValue() {//This is the total cost to move for the parent which is the F value
		return _FCost;
	}

	public void ClearAll() {// Clears all data to preprair for the next search
		Used = false;
		_ParentNode = null;
		_GCost = 0;
		_HCost = 0;
		_FCost = 0;
	}

	public void SetParentAndEnd(Nodes theParent, Nodes theEnd) {//setting parent gcost and hcost
		Used = true;
		_ParentNode = theParent;

		_XValue = getidsaver [0, 0] - _NodeID [0, 0];
		_YValue = getidsaver [0, 1] - _NodeID [0, 1];

		if (_XValue < 0)
			_XValue *= -1;
		if (_YValue < 0)
			_YValue *= -1;

		_HCost = _XValue + _YValue;
		getidsaver = theParent.GetID ();

		_XValue = getidsaver [0, 0] - _NodeID [0, 0];
		_YValue = getidsaver [0, 1] - _NodeID [0, 1];

		if (_XValue < 0) {
			_XValue *= -1;
		}
		if (_YValue < 0) {
			_YValue *= -1;
		}

		if (_XValue == 0 || _YValue == 0) {//if not at the corners of the 3x3 neighbours
			_GCost = _PathfindingNodeID[_MapCollision] + _ParentNode._GCost;
		} else {
			_GCost = (_PathfindingNodeID[_MapCollision] * 1.4f) + _ParentNode._GCost;
		}
		_FCost = _HCost + _GCost;
	}

	public void SetParent(Nodes theParent) {//Adding the parent GCost to this nodes gcost and adding the distance the parent had to travel to this node gcost

		_ParentNode = theParent;

		if (theParent.GetID () [0, 0] == _NodeID [0, 0] || theParent.GetID () [0, 1] == _NodeID [0, 1]) {
			_GCost = _PathfindingNodeID[_MapCollision] + _ParentNode._GCost;
		} else {
			_GCost = (_PathfindingNodeID[_MapCollision] * 1.4f) + _ParentNode._GCost;
		}
		_FCost = _HCost + _GCost;
	}
	public void SetParentCorner(Nodes theParent) {//Adding the parent GCost to this nodes gcost and adding the distance the parent had to travel to this node gcost

		_ParentNode = theParent;
		_GCost = (_PathfindingNodeID[_MapCollision] * 1.4f) + _ParentNode._GCost;
		_FCost = _HCost + _GCost;
	}

	public void SetParentMiddle(Nodes theParent) {//Adding the parent GCost to this nodes gcost and adding the distance the parent had to travel to this node gcost

		_ParentNode = theParent;
		_GCost = _PathfindingNodeID[_MapCollision] + _ParentNode._GCost;
		_FCost = _HCost + _GCost;
	}
	public void SetHCost(Nodes endNode) {//Distance from this node and end node
		_XValue = endNode.GetID () [0, 0] - _NodeID [0, 0];
		_YValue = endNode.GetID () [0, 1] - _NodeID [0, 1];
		
		if (_XValue < 0)
			_XValue *= -1;
		if (_YValue < 0)
			_YValue *= -1;
		
		_HCost = _XValue + _YValue;
		_FCost = _HCost + _GCost;
	}

	public void SetNeighbors(Nodes theNeighbor) {
		_Neighbours.Add(theNeighbor);
	}	

	public void SetCollision(int collision){
		_MapCollision = collision;
	}

	public float GetJustWorldSpaceDistance(Nodes theothernode){
		getidsaver = theothernode.GetID ();
	
		_XValue = getidsaver[0, 0] - _NodeID[0, 0];
		_YValue = getidsaver[0, 1] - _NodeID[0, 1];

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
		Used = true;
		_ParentNode = theParent;
		getidsaver = endNode.GetID ();

		_XValue = getidsaver [0, 0] - _NodeID [0, 0];
		_YValue = getidsaver [0, 1] - _NodeID [0, 1];


		if (_XValue < 0) {
			_XValue *= -1;
		}
		if (_YValue < 0) {
			_YValue *= -1;
		}

		_HCost = _XValue + _YValue;
		getidsaver = theParent.GetID ();

		_XValue = getidsaver [0, 0] - _NodeID [0, 0];
		_YValue = getidsaver [0, 1] - _NodeID [0, 1];

		if (_XValue < 0) {
			_XValue *= -1;
		}
		if (_YValue < 0) {
			_YValue *= -1;
		}

		_GCost = _ParentNode._GCost + _XValue + _YValue;
		_FCost = _HCost + _GCost;
	}

	public void SetParentRoom(Nodes theParent) {//Adding the parent GCost to this nodes gcost and adding the distance the parent had to travel to this node gcost
		Used = true;
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

			_GCost =  _ParentNode._GCost + _XValue + _YValue;
			_FCost = _HCost + _GCost;
		}
	}
}