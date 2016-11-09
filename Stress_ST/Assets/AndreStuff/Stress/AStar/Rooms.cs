using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rooms : MonoBehaviour {

	//room info

	int _MapCollision = 0;//this is what decides what object the player is on(a wall, water ....)
	float[,] _NodeID = new float[1, 2];//Node ID, which is the nodes coordinates in the collision map
	Rooms _ParentNode = null;
	float _GCost = 0;//GCost is the cost that have been used to get to this node
	float _HCost = 0;//HCost is how far away the end node is from this node

	float _XValue = 0, _YValue = 0; //just some values

	float[] NeighboursGCost;
	public List<GameObject> Neighbours = new List<GameObject>();
	List<KeyValuePair<GameObject, float>> TheNeighbours = new List<KeyValuePair<GameObject, float>> ();

	void Awake(){
		NeighboursGCost = new float[Neighbours.Count];
		float XCost = 0;
		float YCost = 0;
		for (int i = 0; i < Neighbours.Count; i++) {
			XCost = transform.position.x - Neighbours [i].transform.position.x;
			YCost = transform.position.y - Neighbours [i].transform.position.y;

			if (XCost < 0)
				XCost *= -1;
			if (YCost < 0)
				YCost *= -1;


			TheNeighbours.Add (new KeyValuePair<GameObject,float>(Neighbours[i],NeighboursGCost [i] = XCost + YCost));
		}

		_NodeID [0, 0] = transform.position.x;
		_NodeID [0, 1] = transform.position.y;
	}

	public int GetCollision() {
		return _MapCollision;
	}

	public float[,] GetID() {
		return _NodeID;
	}

	public Rooms GetParent() {
		return _ParentNode;
	}

	public List<KeyValuePair<GameObject,float>> GetNeighbours() {
		return TheNeighbours;
	}

	public float GetGCost() {
		return _GCost;
	}
		
	public float GetFValue() {//This is the total cost to move for the parent which is the F value
		return _GCost + _HCost;
	}

	public void ClearAll() {// Clears all data to preprair for the next search
		_ParentNode = null;
		_GCost = 0;
		_HCost = 0;
	}

	public void SetParent(Rooms theParent) {//Adding the parent GCost to this nodes gcost and adding the distance the parent had to travel to this node gcost
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

	public void SetHCost(Rooms endNode) {//Distance from this node and end node
		_XValue = endNode.GetID()[0, 0] - _NodeID[0, 0];
		_YValue = endNode.GetID()[0, 1] - _NodeID[0, 1];
		if(_XValue < 0){
			_XValue *= -1;
		}
		if(_YValue < 0){
			_YValue *= -1;
		}

		_HCost = _XValue + _YValue;
	}
		
	public void SetCollision(int collision){
		_MapCollision = collision;
	}
}