using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Nodes {

	//node info

	int _MapCollision = 0;//this is what decides what object the player is on(a wall, water ....)
	float[,] _NodeID = new float[1, 2];//Node ID, which is the nodes coordinates in the collision map
	Nodes _ParentNode = null;
	List<Nodes> _Neighbours = new List<Nodes>();
	float _GCost = 0;//GCost is the cost that have been used to get to this node
	float _HCost = 0;//HCost is how far away the end node is from this node

	float _XValue = 0, _YValue = 0; //just some values

	public Nodes(float[,] ID, int collision) {
		_NodeID = ID;
		_MapCollision = collision;
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

			_XValue = nodeToCheck.GetID () [0, 0] - _NodeID [0, 0];
			_YValue = nodeToCheck.GetID () [0, 1] - _NodeID [0, 1];
			if (_XValue < 0) {
				_XValue *= -1;
			}
			if (_YValue < 0) {
				_YValue *= -1;
			}

			return (_XValue + _YValue);
	

	}

	public float GetFValue() {//This is the total cost to move for the parent which is the F value
		return _GCost + _HCost;
	}

	public void ClearAll() {// Clears all data to preprair for the next search
		_ParentNode = null;
		_GCost = 0;
		_HCost = 0;
	}

	public void SetParent(Nodes theParent) {//Adding the parent GCost to this nodes gcost and adding the distance the parent had to travel to this node gcost

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

			_GCost = (_XValue + _YValue) + _ParentNode.GetGCost();
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
}