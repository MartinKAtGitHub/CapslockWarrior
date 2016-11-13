using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Nodes {

	//node info


	float noe = 0;
	int _MapCollision = 0;//this is what decides what object the player is on(a wall, water ....)
	float[,] _NodeID = new float[1, 2];//Node ID, which is the nodes coordinates in the collision map
	Nodes _ParentNode = null;
	List<Nodes> _Neighbours = new List<Nodes>();
	float _GCost = 0;//GCost is the cost that have been used to get to this node
	float _HCost = 0;//HCost is how far away the end node is from this node

	float _XValue = 0, _YValue = 0; //just some values

	Paths _Paths = new Paths();
	RoomsPathCalculation _MyRoom;

	public Nodes(float[,] ID, int collision) {
		_NodeID = ID;
		_MapCollision = collision;
	}

	public void SetRoom(RoomsPathCalculation room){
		_MyRoom = room;
	}
	public RoomsPathCalculation GetRoom(){
		return _MyRoom;
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

		if(nodeToCheck.GetCollision() == 0){
			noe = 1;
		}else if(nodeToCheck.GetCollision() == 1){
			noe = 10;
		}
		if (nodeToCheck.GetID () [0, 0] == _NodeID [0, 0] || nodeToCheck.GetID () [0, 1] == _NodeID [0, 1]) {
			return noe;
		} else {
			return noe + (noe * 0.4f);
		}
	}

	public float GetJustSideGCost(Nodes nodeToCheck) {//Gets how expencive it is to travel to nodetocheck

	
		if(nodeToCheck.GetCollision() == 0){
			return noe = 1;
		}else if(nodeToCheck.GetCollision() == 1){
			return noe = 10;
		}
		if (nodeToCheck.GetID () [0, 0] == _NodeID [0, 0] || nodeToCheck.GetID () [0, 1] == _NodeID [0, 1]) {
			return noe;
		} else {
			return noe + (noe * 0.4f);
		}
	}


	public float GetFValue() {//This is the total cost to move for the parent which is the F value
		return _GCost + _HCost;
	}

	public void ClearAll() {// Clears all data to preprair for the next search
		_ParentNode = null;
		_GCost = 0;
		_HCost = 0;
	}

	public void SetParent(Nodes theParent, int collision) {//Adding the parent GCost to this nodes gcost and adding the distance the parent had to travel to this node gcost

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
				}
				_GCost = (noe) + _ParentNode.GetGCost();
			} else {
				if(_MapCollision == 0){
					noe = 1.4f;
				}else if(_MapCollision == 1){
					noe = 10.4f;
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

	public Paths GetPaths(){
		return _Paths;
	}
}

public class Paths {

	List<Nodes> Neighbour = new List<Nodes>();
	List<Nodes> ParentPath;
	List<KeyValuePair <List<Nodes>,float>> _Paths = new List<KeyValuePair <List<Nodes>,float>>();
	List<KeyValuePair<KeyValuePair< List<Nodes>, float >, Nodes>> _NeighbourPaths = new List<KeyValuePair<KeyValuePair< List<Nodes>, float >, Nodes>>();

	float GCost = 0;
	float HCost = new float();
	float[,] _NodeID;

	public void SetNodeID(float[,] id){
		_NodeID = id;
	}

	public float[,] getid(){
		return _NodeID;
	}

	public void ClearPerSearch(){
		ParentPath = null;
		GCost = 0;
		HCost = 0;
	}

	public float GetGCost(){
		return GCost;
	}

	public void SetEverything(KeyValuePair <List<Nodes>,float> path, Nodes first, float[,] end, float nextNodeGCost){
		ParentPath = path.Key;
			
		GCost =	path.Value + first.GetPaths().GetGCost() + nextNodeGCost;

		float _XValue = end [0, 0] - _NodeID [0, 0];
		float _YValue = end [0, 1] - _NodeID [0, 1];

		if (_XValue < 0)
			_XValue *= -1;
		if (_YValue < 0)
			_YValue *= -1;

		HCost = _XValue + _YValue;
	}

	public void SetEverythingNew(KeyValuePair <List<Nodes>,float> path, float gCost, float[,] end){
		ParentPath = path.Key;

		GCost =	path.Value + gCost;

		float _XValue = end [0, 0] - _NodeID [0, 0];
		float _YValue = end [0, 1] - _NodeID [0, 1];

		if (_XValue < 0)
			_XValue *= -1;
		if (_YValue < 0)
			_YValue *= -1;

		HCost = _XValue + _YValue;
	}

	public void SetParentAndGCost(KeyValuePair <List<Nodes>,float> path, Nodes first, float nextNodeGCost){
		ParentPath = path.Key;

		GCost =	path.Value + first.GetPaths().GetGCost() + nextNodeGCost;
	
	}
		
	public void SetImAtEnd(KeyValuePair<List<Nodes>, float> path){
		ParentPath = path.Key;
		GCost = path.Value + ParentPath.First().GetPaths().GetGCost();
	}

	public void SetFirstPath(KeyValuePair<List<Nodes>, float> path, float[,] end, float nextNodeGCost){
		ParentPath = path.Key;
		GCost = path.Value + nextNodeGCost;

		float _XValue = end [0, 0] - _NodeID [0, 0];
		float _YValue = end [0, 1] - _NodeID [0, 1];

		if (_XValue < 0)
			_XValue *= -1;
		if (_YValue < 0)
			_YValue *= -1;

		HCost = _XValue + _YValue;
	}

	public void ClearEverything(){
		ParentPath = null;
		GCost = 0;
		HCost = 0;
		Neighbour = new List<Nodes> ();
		_Paths = new List<KeyValuePair<List<Nodes>, float>> ();
		_NeighbourPaths = new List<KeyValuePair<KeyValuePair<List<Nodes>, float>, Nodes>> ();
	}



	public List<Nodes> GetNeighbours(){
		return Neighbour;
	}


	public void SetHCost(float[,] id){
		float x = _NodeID [0,0] - id[0,0];
		float y = _NodeID [0,1] - id[0,1];

		if (x < 0)
			x *= -1;
		if (y < 0)
			y *= -1;

		HCost = x + y;
	}

	public void SetNeighbour(Nodes neighbour){
		Neighbour.Add(neighbour);
	}

	public void AddPaths(KeyValuePair<List<Nodes>, float> path){
		_Paths.Add (path);
	}

	public void AddNeighbourPaths(KeyValuePair<KeyValuePair< List<Nodes>, float >, Nodes> tran){
		_NeighbourPaths.Add (tran);
	}

	public List<KeyValuePair<List<Nodes>, float>> GetPaths(){
		return _Paths;
	}

	public List<KeyValuePair<KeyValuePair< List<Nodes>, float >, Nodes>> GetNeighbourPaths(){
		return _NeighbourPaths;
	}

	public float GetFCost(){
		return (GCost + HCost);
	}
	public List<Nodes> GetParentPath(){
		return ParentPath;
	}
}

