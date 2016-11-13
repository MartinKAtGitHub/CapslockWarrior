using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RoomsPathCalculation : MonoBehaviour {

	/*if the collider x size is 8.5 then there will be 8 whole nodes (1x1 that is). 8 in width + an additional with size 0.5 = 9 nodes
	WARNING when adding colliders, if you add a collider with perfect sizes (like 5x 7y) decrease them to (4.99x 6.99y)then you reduce the amount of nodes with 13 (6x8 to 5x7),  
	 Node size is 1x1 so the following code was made with that in mind. so some changes must be made to change node size from 1x1 */

	public List<GameObject> Neighbours = new List<GameObject>();

	Nodes[,] _TheNodeMap; 
	Nodes[,] _CopyNodemap;

	int _NodesInHight = 0, _NodesInWidth = 0;
	int _RightPoint = 0, _LeftPoint = 0, _HighestPoint = 0, _LowestPoint = 0;

	float _FirstNodeInWorldSpaceWidth = 0, _FirstNodeInWorldSpaceHeight = 0; 
	float _LastNodePositionX = 0, _LastNodePositionY = 0;
	
	float _PointA = 0, _PointB = 0;
	float _Closestnode = 0.4f;//neighbour node distance    if a node is more then _Closestnode from a neighbour node then it isnt a neighbour

	Vector2 _MapHeigth = Vector2.zero;
	Vector2 _MapWidht = Vector2.zero;

	List<KeyValuePair<List<Nodes>, RoomsPathCalculation>> _WallNodes = new List<KeyValuePair<List<Nodes>, RoomsPathCalculation>> ();
	List<Nodes> SearchThroughNodes = new List<Nodes>();

	AStarPathfinding_Nodes _AStar = new AStarPathfinding_Nodes();

	public bool te = false;
	void Update(){
		if (te) {
			te = false;
			CalculateWalkablePaths ();
		}
	}



	#region Awake Making the nodemap

	void Awake(){//Creating TheNodeMap for this room and getting additional info	
		_MapWidht.x = GetComponent<BoxCollider2D> ().bounds.max.x;
		_MapWidht.y = GetComponent<BoxCollider2D> ().bounds.min.x;

		_MapHeigth.x = GetComponent<BoxCollider2D> ().bounds.max.y;
		_MapHeigth.y = GetComponent<BoxCollider2D> ().bounds.min.y;

		_NodesInHight = Mathf.CeilToInt ((GetComponent<BoxCollider2D> ().bounds.max.y - transform.position.y) * 2);
		_NodesInWidth = Mathf.CeilToInt ((GetComponent<BoxCollider2D> ().bounds.max.x - transform.position.x) * 2);

		_FirstNodeInWorldSpaceWidth = GetComponent<BoxCollider2D> ().bounds.min.x + 0.5f;
		_FirstNodeInWorldSpaceHeight = GetComponent<BoxCollider2D> ().bounds.max.y - 0.5f;

		_LastNodePositionX = GetComponent<BoxCollider2D> ().bounds.min.x + (_NodesInWidth - 1) + ((GetComponent<BoxCollider2D> ().bounds.max.x - transform.position.x) * 2) % 1 / 2;//last node position in x coordinates
		_LastNodePositionY = GetComponent<BoxCollider2D> ().bounds.max.y - (_NodesInHight - 1) - ((GetComponent<BoxCollider2D> ().bounds.max.y - transform.position.y) * 2) % 1 / 2;//last node position in y coordinates

		_TheNodeMap = new Nodes[_NodesInHight, _NodesInWidth];

		CreateNodeMap ();
	}

	void CreateNodeMap(){//creates the nodemap with nodes, with size boxcollider xy mathf.ceil
		if (_NodesInHight == 1 && _NodesInWidth == 1) {
			_TheNodeMap [0, 0] = new Nodes (new float[,]{ { _LastNodePositionX, _LastNodePositionY } }, 0);
			_TheNodeMap [0, 0].GetPaths ().SetNodeID (_TheNodeMap [0, 0].GetID ());
		} else if (_NodesInHight == 1) {
			for (int j = 0; j < _NodesInWidth - 1; j++) {
				_TheNodeMap [0, j] = new Nodes (new float[,]{ { _FirstNodeInWorldSpaceWidth + j, _LastNodePositionY } }, 0);
				_TheNodeMap [0, j].GetPaths ().SetNodeID (_TheNodeMap [0, j].GetID ());
			}
			_TheNodeMap [_NodesInHight - 1, _NodesInWidth - 1] = new Nodes (new float[,]{ {_LastNodePositionX,_LastNodePositionY} }, 0);
			_TheNodeMap [_NodesInHight - 1, _NodesInWidth - 1].GetPaths ().SetNodeID (_TheNodeMap [_NodesInHight - 1, _NodesInWidth - 1].GetID ());
		} else if (_NodesInWidth == 1) {
			for (int i = 0; i < _NodesInHight - 1; i++) {
				_TheNodeMap [i, 0] = new Nodes (new float[,]{ { _LastNodePositionX, _FirstNodeInWorldSpaceHeight - i } }, 0);
				_TheNodeMap [i, 0].GetPaths ().SetNodeID (_TheNodeMap [i, 0].GetID ());
			}
			_TheNodeMap [_NodesInHight - 1, _NodesInWidth - 1] = new Nodes (new float[,]{ {_LastNodePositionX,_LastNodePositionY} }, 0);
			_TheNodeMap [_NodesInHight - 1, _NodesInWidth - 1].GetPaths ().SetNodeID (_TheNodeMap [_NodesInHight - 1, _NodesInWidth - 1].GetID ());
		} else {

			for (int i = 0; i < _NodesInHight - 1; i++) {
				for (int j = 0; j < _NodesInWidth - 1; j++) {
					_TheNodeMap [i, j] = new Nodes (new float[,]{ { _FirstNodeInWorldSpaceWidth + j, _FirstNodeInWorldSpaceHeight - i } }, 0);
					_TheNodeMap [i, j].GetPaths ().SetNodeID (_TheNodeMap [i, j].GetID ());
				}
			}

			for (int j = 0; j < _NodesInWidth - 1; j++) {
				_TheNodeMap [_NodesInHight - 1, j] = new Nodes (new float[,]{ {	_FirstNodeInWorldSpaceWidth + j, _LastNodePositionY } }, 0);
				_TheNodeMap [_NodesInHight - 1, j].GetPaths ().SetNodeID (_TheNodeMap [_NodesInHight - 1, j].GetID ());
			}

			for (int i = 0; i < _NodesInHight - 1; i++) {
				_TheNodeMap [i, _NodesInWidth - 1] = new Nodes (new float[,]{ {_LastNodePositionX,_FirstNodeInWorldSpaceHeight - i} }, 0);
				_TheNodeMap [i, _NodesInWidth - 1].GetPaths ().SetNodeID (_TheNodeMap [i, _NodesInWidth - 1].GetID ());
			}

			_TheNodeMap [_NodesInHight - 1, _NodesInWidth - 1] = new Nodes (new float[,]{ {_LastNodePositionX,_LastNodePositionY} }, 0);
			_TheNodeMap [_NodesInHight - 1, _NodesInWidth - 1].GetPaths ().SetNodeID (_TheNodeMap [_NodesInHight - 1, _NodesInWidth - 1].GetID ());
		}

		for (int i = 0; i < _NodesInHight; i++) {//adding neighbours
			for (int j = 0; j < _NodesInWidth; j++) {
				_TheNodeMap [i, j].SetRoom (GetComponent<RoomsPathCalculation> ());
				for (int k = -1; k < 2; k++) {
					for (int h = -1; h < 2; h++) {
						if ((i + k >= 0 && j + h >= 0) && ((i + k < _NodesInHight) && j + h < _NodesInWidth) && !(k == 0 && h == 0)) {
							_TheNodeMap [i, j].SetNeighbors (_TheNodeMap [i + k, j + h]);
						}
					}
				}
			}
		}
	}

	#endregion


	#region Start Calculating path

	void Start(){
		CalculateSideNodes ();
		CalculateWalkablePaths ();
	}
		
	void CalculateSideNodes(){//Calculates where the neighbour room is compared to this, and then adds the nodes that are neighbour node to each other   current room a  |  b neigbour room      so a and b are neighbour in the same plane so add them, if b is above a then b isnt added 
		_WallNodes.Clear ();

		foreach (GameObject s in Neighbours) {//går igjennom og ser hvor naborommet er i forhold til dette rommet. (nord-sør-øst-vest) og legger til de nodene i en liste
			if (s.GetComponent<Collider2D> ().bounds.max.y < GetComponent<Collider2D> ().bounds.min.y) {
				GetEdgeNodes (s.GetComponent<RoomsPathCalculation> (), 2);
			} else if (s.GetComponent<Collider2D> ().bounds.max.x < GetComponent<Collider2D> ().bounds.min.x) {
				GetEdgeNodes (s.GetComponent<RoomsPathCalculation> (), 3);
			} else if (s.GetComponent<Collider2D> ().bounds.min.y > GetComponent<Collider2D> ().bounds.max.y) {
				GetEdgeNodes (s.GetComponent<RoomsPathCalculation> (), 0);
			} else if (s.GetComponent<Collider2D> ().bounds.min.x > GetComponent<Collider2D> ().bounds.max.x) {
				GetEdgeNodes (s.GetComponent<RoomsPathCalculation> (), 1);
			}
		}
	}
		
	void GetEdgeNodes(RoomsPathCalculation room, int side){//going through every side of the collider to see which nodes that are neighbours to another room
		List<Nodes> NodeList = new List<Nodes> ();	
		_CopyNodemap = room.GetNodeMap ();
	

		if (side == 0) {//North nodes, getting all nodes from _TheNodeMap that is neighbour to the neighbour north of this room
			_PointA = _CopyNodemap [0, 0].GetID () [0, 0];
			_PointB = _CopyNodemap [0, room.GetWidth () - 1].GetID () [0, 0];

			for (int i = 0; i < GetWidth (); i++) {
				if ((_TheNodeMap [0, i].GetID () [0, 0] - _PointA) > -_Closestnode && ((_TheNodeMap [0, i].GetID () [0, 0] - _PointB) < _Closestnode)) {
					NodeList.Add (_TheNodeMap [0, i]);
				}
			}
		} else if (side == 1) {//East nodes
			_PointA = _CopyNodemap [0, 0].GetID () [0, 1];
			_PointB = _CopyNodemap [room.Getheight () - 1, 0].GetID () [0, 1];

			for (int i = 0; i < Getheight (); i++) {
				if (_TheNodeMap [i, 0].GetID () [0, 1] - _PointA < _Closestnode && _TheNodeMap [i, 0].GetID () [0, 1] - _PointB > -_Closestnode) {
					NodeList.Add (_TheNodeMap [i, GetWidth () - 1]);
				}
			}
		} else if (side == 2) {//South nodes
			_PointA = _CopyNodemap [0, 0].GetID () [0, 0];
			_PointB = _CopyNodemap [0, room.GetWidth () - 1].GetID () [0, 0];

			for (int i = 0; i < GetWidth (); i++) {
				if ((_TheNodeMap [0, i].GetID () [0, 0] - _PointA) > -_Closestnode && ((_TheNodeMap [0, i].GetID () [0, 0] - _PointB) < _Closestnode)) {
					NodeList.Add (_TheNodeMap [Getheight () - 1, i]);
				}
			}
		} else if (side == 3) {//West nodes
			_PointA = _CopyNodemap [0, 0].GetID () [0, 1];
			_PointB = _CopyNodemap [room.Getheight () - 1, 0].GetID () [0, 1];

			for (int i = 0; i < Getheight (); i++) {
				if (_TheNodeMap [i, 0].GetID () [0, 1] - _PointA < _Closestnode && _TheNodeMap [i, 0].GetID () [0, 1] - _PointB > -_Closestnode) {
					NodeList.Add (_TheNodeMap [i, 0]);
				}
			}
		}

		_WallNodes.Add (new KeyValuePair<List<Nodes>, RoomsPathCalculation> (NodeList, room));
	}

	#endregion


	#region Colliders 

	void OnCollisionEnter2D(Collision2D coll) {//when a gameobject is inside the collider with tag == wall, then update the nodemap and recalculate the pathlist  TODO expand this to other sources of colliding objects?
		if (coll.gameObject.tag == "Wall") {
			UpdateCollisionID (coll.collider.bounds.max, coll.collider.bounds.min, 1);
			CalculateWalkablePaths ();
			//TODO if the endges is update update the neighbours to
		}			
	}

	void OnCollisionExit2D(Collision2D coll) {//when a gameobject is removed from the collider with tag == wall, then update the nodemap and recalculate the pathlist  TODO expand this to other sources of colliding objects?
		if (gameObject.tag == "Wall") {
			UpdateCollisionID (coll.collider.bounds.max, coll.collider.bounds.min, 0);
			CalculateWalkablePaths ();
			//TODO if the endges is update update the neighbours to

		}
	}

	void UpdateCollisionID(Vector2 upperRightCorner, Vector2 lowerLeftCorner, int collisionID){//calculating where a collider is colliding with this collider <(o_O)>
		_RightPoint = 0;
		_LeftPoint = 0;
		_HighestPoint = 0;
		_LowestPoint = 0;
		 
		if (_MapWidht.x <= upperRightCorner.x) {//getting how far im going in this direction   |      .  <-|
			_RightPoint = _NodesInWidth - 1;
		} else {
			_RightPoint = Mathf.FloorToInt (upperRightCorner.x - _MapWidht.y);
		}

		if (_MapWidht.y <= lowerLeftCorner.x) {//getting how far im going in this direction   |->  .       |
			_LeftPoint = Mathf.FloorToInt (lowerLeftCorner.x - _MapWidht.y);
		} else {
			_LeftPoint = 0;
		}

		if (_MapHeigth.x <= upperRightCorner.y) {//how far from top border am i
			_HighestPoint = 0;
		} else {
			_HighestPoint = Mathf.FloorToInt (_MapHeigth.x - upperRightCorner.y);
		}


		if (_MapHeigth.y <= lowerLeftCorner.y) {//how far from the bottom border am i
			_LowestPoint = Mathf.FloorToInt (_MapHeigth.x - lowerLeftCorner.y);
		} else {
			_LowestPoint = (_NodesInHight - 1);
		}
			
		for (int i = _HighestPoint; i <= _LowestPoint; i++) {//changing the nodes inside the coordinates i found to collisionID
			for (int j = _LeftPoint; j <= _RightPoint; j++) {
				_TheNodeMap [i, j].SetCollision (collisionID);
			}
		}
	}



	void CalculateWalkablePaths(){//clearing previous neighbour so that they can be updated with the new info, and setting new :D. connecting rooms together with nodes that are neighbour to another room
		foreach (Nodes s in 	SearchThroughNodes) {
			s.GetPaths ().ClearEverything ();
		}
		SearchThroughNodes.Clear ();
	
	
		foreach (KeyValuePair<List<Nodes>, RoomsPathCalculation> a in _WallNodes) {//going through all wallnodes and setting each node to get a neighbour (if it has one) TODO improvments (only 1 node on each roomwall (but need to make a personal A* on the object))
			List<Nodes> hold = a.Value.GetComponent<RoomsPathCalculation> ().GetEdgeNodes (GetComponent<RoomsPathCalculation> ()).Key;//neighbour nodes that borders to this room

			if (SearchThroughNodes.Contains (a.Key.First ())) {//adding the first nodes that are neighbours to the list
				a.Key.First ().GetPaths ().SetNeighbour (hold.First ());
			} else {
				a.Key.First ().GetPaths ().SetNeighbour (hold.First ());
				SearchThroughNodes.Add (a.Key.First ());
			}

			if (a.Key.Count <= hold.Count) {//if the room size is meating the criteria, then im adding another neighbour path in the middle of the room(edge)
				if (Mathf.RoundToInt (a.Key.Count / 2) > 1) {
					if (SearchThroughNodes.Contains (a.Key [Mathf.RoundToInt (a.Key.Count / 2)])) {
						a.Key [Mathf.RoundToInt (a.Key.Count / 2)].GetPaths ().SetNeighbour (hold [Mathf.RoundToInt (a.Key.Count / 2)]);
					} else {
						a.Key [Mathf.RoundToInt (a.Key.Count / 2)].GetPaths ().SetNeighbour (hold [Mathf.RoundToInt (a.Key.Count / 2)]);
						SearchThroughNodes.Add (a.Key [Mathf.RoundToInt (a.Key.Count / 2)]);
					}
				}
			} else {
				if (Mathf.RoundToInt (hold.Count / 2) > 1) {
					if (SearchThroughNodes.Contains (a.Key [Mathf.RoundToInt (hold.Count / 2)])) {
						a.Key [Mathf.RoundToInt (hold.Count / 2)].GetPaths ().SetNeighbour (hold [Mathf.RoundToInt (hold.Count / 2)]);
					} else {
						a.Key [Mathf.RoundToInt (hold.Count / 2)].GetPaths ().SetNeighbour (hold [Mathf.RoundToInt (hold.Count / 2)]);
						SearchThroughNodes.Add (a.Key [Mathf.RoundToInt (hold.Count / 2)]);
					}
				}
			}

			if (SearchThroughNodes.Contains (a.Key.Last ())) {//adding the last nodes that are neighours to the list
				a.Key.Last ().GetPaths ().SetNeighbour (hold.Last ());
			} else {
				a.Key.Last ().GetPaths ().SetNeighbour (hold.Last ());
				SearchThroughNodes.Add (a.Key.Last ());
			}
		}
	
		for (int i = 0; i < SearchThroughNodes.Count; i++) {//creating a path from each node to the other. node a - node b, node a - node c, node b - node c
			for (int j = 1 + i; j < SearchThroughNodes.Count; j++) {
				KeyValuePair<List<Nodes>, float> saver = _AStar.CreatePath (SearchThroughNodes [i], SearchThroughNodes [j]);
				SearchThroughNodes [i].GetPaths ().AddPaths (saver);
				SearchThroughNodes [j].GetPaths ().AddPaths (saver);	
			}
		}
	
		for (int i = 0; i < SearchThroughNodes.Count; i++) {//if the node have neighbours, then im making a path that connect the neighbour and this
			foreach (Nodes n in SearchThroughNodes[i].GetPaths().GetNeighbours()) {
				SearchThroughNodes [i].GetPaths ().AddNeighbourPaths (_AStar.CreateChildParentPath (SearchThroughNodes [i], n));
			}
		}

	}


	#endregion

	#region Getmethods

	public Nodes GetMyNode(GameObject me){
		int te1 = Mathf.FloorToInt(me.transform.position.x - GetComponent<Collider2D>().bounds.min.x);
		int te2 = Mathf.FloorToInt(GetComponent<Collider2D>().bounds.max.y - me.transform.position.y);

		if(te1 < 0){
			te1 = 0;
		}else if (te1 >= _NodesInWidth){
			te1 = _NodesInWidth - 1;
		}

		if(te2 < 0){
			te2 = 0;
		}else if (te2 >= _NodesInHight){
			te2 = _NodesInHight - 1;
		}
			
		return _TheNodeMap [te2,te1];
	}

	public KeyValuePair<List<Nodes>, RoomsPathCalculation> GetEdgeNodes(RoomsPathCalculation id){
		if (_WallNodes.Count  == 0) {
			CalculateSideNodes ();
		}
		foreach(KeyValuePair<List<Nodes>, RoomsPathCalculation> s in _WallNodes){
			if (s.Value == id)
				return s;
		}
		return _WallNodes.First();
	}

	public int Getheight(){
		return _NodesInHight;
	}

	public int GetWidth(){
		return _NodesInWidth;
	}

	public Nodes[,] GetNodeMap(){
		return _TheNodeMap;
	}

	public List<Nodes> GetSearchThroughNodes(){
		return SearchThroughNodes;
	}

	#endregion
}