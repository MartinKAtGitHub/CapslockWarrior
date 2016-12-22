using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Nodes {

	//node info

	public bool Used = false;

	float noe = 0;
	int _MapCollision = 0;//this is what decides what object the player is on(a wall, water ....)
	float[,] _NodeID = new float[1, 2];//Node ID, which is the nodes coordinates in the collision map
	Nodes _ParentNode = null;
	List<Nodes> _Neighbours = new List<Nodes>();
	float _GCost = 0;//GCost is the cost that have been used to get to this node
	float _HCost = 0;//HCost is how far away the end node is from this node

	float _XValue = 0, _YValue = 0; //just some values

	RoomConnectorCreating _MyRooms;

	/// <summary>
	/// Initializes a new instance of the <see cref="Nodes"/> class.
	/// </summary>
	/// <param name="ID">is the parameterID in XY coordinates,</param>
	/// <param name="collision">is the collisionid the node will have, so when someone collides with it then we know what to apply</param>
	public Nodes(float[,] ID, int collision) {
		_NodeID = ID;
		_MapCollision = collision;
	}

	public void SetRooms(RoomConnectorCreating room){
		_MyRooms = room;
	}

	public RoomConnectorCreating GetRooms(){
		return _MyRooms;
	}


	public int GetCollision() {
		return _MapCollision;
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
			return nodeToCheck.GetCollision ();
		} else {
			return (nodeToCheck.GetCollision () * 1.4f);
		}
	}
		
	public float GetFValue() {//This is the total cost to move for the parent which is the F value
		return _GCost + _HCost;
	}

	public void ClearAll() {// Clears all data to preprair for the next search
		Used = false;
		_ParentNode = null;
		_GCost = 0;
		_HCost = 0;
	}

	public void SetParentAndEnd(Nodes theParent, Nodes theEnd) {//setting parent gcost and hcost
		Used = true;
		_ParentNode = theParent;

		_XValue = theEnd.GetID () [0, 0] - _NodeID [0, 0];
		_YValue = theEnd.GetID () [0, 1] - _NodeID [0, 1];

		if (_XValue < 0)
			_XValue *= -1;
		if (_YValue < 0)
			_YValue *= -1;

		_HCost = _XValue + _YValue;


		_XValue = theParent.GetID () [0, 0] - _NodeID [0, 0];
		_YValue = theParent.GetID () [0, 1] - _NodeID [0, 1];

		if (_XValue < 0) {
			_XValue *= -1;
		}
		if (_YValue < 0) {
			_YValue *= -1;
		}

		if (_XValue == 1 || _YValue == 1) {//if not at the corners of the 3x3 neighbours
			_GCost = _MapCollision + _ParentNode.GetGCost ();
		} else {
			_GCost = (_MapCollision * 1.4f) + _ParentNode.GetGCost ();
		}
	}

	public void SetParent(Nodes theParent, int collision) {//Adding the parent GCost to this nodes gcost and adding the distance the parent had to travel to this node gcost

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



			if (theParent.GetID () [0, 0] == _NodeID [0, 0] || theParent.GetID () [0, 1] == _NodeID [0, 1]) {
				if(_MapCollision == 0){
					noe = 1;
				}else if(_MapCollision == 1){
					noe = 10;
				}else if (_MapCollision == 2) {
					noe = 3;
				}else if (_MapCollision == 10) {
					noe = 20;
				}else if(_MapCollision == 100){
					noe = 100;
				}
				_GCost = (noe) + _ParentNode.GetGCost();
			} else {
				if(_MapCollision == 0){
					noe = 1.4f;
				}else if(_MapCollision == 1){
					noe = 14f;
				}else if (_MapCollision == 2) {
					noe = 4;
				}else if (_MapCollision == 10) {
					noe = 28;
				}else if(_MapCollision == 100){
					noe = 140f;
				}
				_GCost = (noe) + _ParentNode.GetGCost();
			}
		}
	}

	public void SetHCost(Nodes endNode) {//Distance from this node and end node
		if (_ParentNode != null) {
			_XValue = endNode.GetID()[0, 0] - _NodeID[0, 0];
			_YValue = endNode.GetID()[0, 1] - _NodeID[0, 1];
		
			if (_XValue < 0)
				_XValue *= -1;
			if (_YValue < 0)
				_YValue *= -1;
		
			_HCost = _XValue + _YValue;
		}
	}

	public void SetNeighbors(Nodes theNeighbor) {
		_Neighbours.Add(theNeighbor);
	}	

	public void SetCollision(int collision){
		_MapCollision = collision;
	}


	public void setGCost(float a){
		_GCost = a;
	}

	public float GetJustWorldSpaceDistance(Nodes theothernode){
		_XValue = theothernode.GetID()[0, 0] - _NodeID[0, 0];
		_YValue = theothernode.GetID()[0, 1] - _NodeID[0, 1];


		if(_XValue < 0){
			_XValue *= -1;
		}
		if(_YValue < 0){
			_YValue *= -1;
		}

		return (_XValue + _YValue);
	}

	public void setParent(Nodes theParent) {//Adding the parent GCost to this nodes gcost and adding the distance the parent had to travel to this node gcost

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


			_GCost =  _ParentNode.GetGCost() + _XValue + _YValue;

	
		}
	}
}