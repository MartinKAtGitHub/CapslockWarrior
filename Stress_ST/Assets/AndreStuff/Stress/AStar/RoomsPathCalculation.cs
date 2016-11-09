using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RoomsPathCalculation : MonoBehaviour {

	//TODO When nodemap is uppdated tell neighbours if changes have been made to a path(s)
		 

	//if the collider x size is 8.5 then there will be 8 whole nodes (1x1 that is). 1 in width + an additional with size 0.5
	//WARNING when adding colliders, if you add a collider with perfect sizes (like 5x 7y) decrease them to (4.99x 6.99y) TODO fix this,  
	// Node size is 1x1 so the following code was made with that in mind, in the future look on and adjust the nodesize if possible 

	Nodes[,] _TheNodeMap; 

	int _NodesInHight = 0, _NodesInWidth = 0;
	int _RightPoint = 0, _LeftPoint = 0, _HighestPoint = 0, _LowestPoint = 0;

	Vector2 _MapHeigth = Vector2.zero;
	Vector2 _MapWidht = Vector2.zero;

	float _FirstNodeInWorldSpaceWidth = 0, _FirstNodeInWorldSpaceHeight = 0; 
	float _LastNodePositionX = 0, _LastNodePositionY = 0;


	public int GoAfterPlayer = 1;//Might need to improve this when we start on the multiplayer part,

	List<KeyValuePair<Rooms, Rooms>> _NeighbourPath = new List<KeyValuePair<Rooms, Rooms>> ();


	float _PointA = 0, _PointB = 0;
	float _Closestnode = 0.4f;
	float _Saver = 0f;
	float _Previous = 0f;

	List<KeyValuePair<List<Nodes>, Rooms>> _PathNodes = new List<KeyValuePair<List<Nodes>, Rooms>> ();
	List<Nodes> _PathNode = new List<Nodes> ();

	List<KeyValuePair<List<Nodes>, Rooms>> _WallNodes = new List<KeyValuePair<List<Nodes>, Rooms>> ();
	List<KeyValuePair<KeyValuePair<Rooms, Rooms>, List<Nodes>>> _PathList = new List<KeyValuePair<KeyValuePair<Rooms, Rooms>, List<Nodes>>> ();

	Nodes[] _PlayerNode;

	#region Awake

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
		_PlayerNode = GameObject.FindGameObjectWithTag ("Player1").GetComponent<WhichRoomPlayerAt>().GetNodeImAt();

		CreateNodeMap ();
	}

	void CreateNodeMap(){//creates the nodemap with nodes, with size boxcollider xy mathf.ceil
		if (_NodesInHight == 1 && _NodesInWidth == 1) {
			_TheNodeMap [0, 0] = new Nodes (new float[,]{ { _LastNodePositionX, _LastNodePositionY } }, 0);
		} else if (_NodesInHight == 1) {
			for (int j = 0; j < _NodesInWidth - 1; j++) {
				_TheNodeMap [0, j] = new Nodes (new float[,]{ { _FirstNodeInWorldSpaceWidth + j, _LastNodePositionY } }, 0);
			}
			_TheNodeMap [_NodesInHight - 1, _NodesInWidth - 1] = new Nodes (new float[,]{ {_LastNodePositionX,_LastNodePositionY} }, 0);
		} else if (_NodesInWidth == 1) {
			for (int i = 0; i < _NodesInHight - 1; i++) {
				_TheNodeMap [i, 0] = new Nodes (new float[,]{ { _LastNodePositionX, _FirstNodeInWorldSpaceHeight - i } }, 0);
			}
			_TheNodeMap [_NodesInHight - 1, _NodesInWidth - 1] = new Nodes (new float[,]{ {_LastNodePositionX,_LastNodePositionY} }, 0);
		} else {

			for (int i = 0; i < _NodesInHight - 1; i++) {
				for (int j = 0; j < _NodesInWidth - 1; j++) {
					_TheNodeMap [i, j] = new Nodes (new float[,]{ { _FirstNodeInWorldSpaceWidth + j, _FirstNodeInWorldSpaceHeight - i } }, 0);
				}
			}

			for (int j = 0; j < _NodesInWidth - 1; j++) {
				_TheNodeMap [_NodesInHight - 1, j] = new Nodes (new float[,]{ {	_FirstNodeInWorldSpaceWidth + j, _LastNodePositionY } }, 0);
			}

			for (int i = 0; i < _NodesInHight - 1; i++) {
				_TheNodeMap [i, _NodesInWidth - 1] = new Nodes (new float[,]{ {_LastNodePositionX,_FirstNodeInWorldSpaceHeight - i} }, 0);
			}

			_TheNodeMap [_NodesInHight - 1, _NodesInWidth - 1] = new Nodes (new float[,]{ {_LastNodePositionX,_LastNodePositionY} }, 0);
		}

		for (int i = 0; i < _NodesInHight; i++) {//adding neighbours
			for (int j = 0; j < _NodesInWidth; j++) {
				for (int k = -1; k < 2; k++) {
					for (int h = -1; h < 2; h++) {
						if ((i + k >= 0 && j + h >= 0) && ((i + k < _NodesInHight) && j + h < _NodesInWidth) && !(k == 0 && h == 0)) {
							_TheNodeMap [i, j].SetNeighbors (_TheNodeMap [i + k, j + h]);
						}
					}
				}
			}
		}

		List<KeyValuePair<GameObject, float>> Neighbour = GetComponent<Rooms> ().GetNeighbours ();

		for (int i = 0; i < Neighbour.Count; i++) {//adding neighbours to a list that im going to search through
			for (int j = 1 + i; j < Neighbour.Count; j++) {
				_NeighbourPath.Add (new KeyValuePair<Rooms, Rooms>(Neighbour[i].Key.GetComponent<Rooms>(), Neighbour[j].Key.GetComponent<Rooms>()));
			}
		}
	}

	#endregion


	#region Start

	void Start(){
		CalculateSideNodes ();
		CalculatePathsList ();
	}
		
	void CalculateSideNodes(){//Calculates where the neighbour room is compared to this, and then adds the nodes that are neighbour node to each other   current room a  |  b neigbour room      so a and b are neighbour in the same plane so add them, if b is above a then b isnt added 
		foreach (KeyValuePair<GameObject, float> s in GetComponent<Rooms> ().GetNeighbours ()) {//går igjennom og ser hvor naborommet er i forhold til dette rommet. (nord-sør-øst-vest) og legger til de nodene i en liste
			if (s.Key.GetComponent<Collider2D> ().bounds.max.y < GetComponent<Collider2D> ().bounds.min.y) {
				_WallNodes.Add (new KeyValuePair<List<Nodes>, Rooms> (GetSideNodes (s.Key.GetComponent<RoomsPathCalculation> (), 2), s.Key.GetComponent<Rooms> ()));
			} else if (s.Key.GetComponent<Collider2D> ().bounds.max.x < GetComponent<Collider2D> ().bounds.min.x) {
				_WallNodes.Add (new KeyValuePair<List<Nodes>, Rooms> (GetSideNodes (s.Key.GetComponent<RoomsPathCalculation> (), 3), s.Key.GetComponent<Rooms> ()));
			} else if (s.Key.GetComponent<Collider2D> ().bounds.min.y > GetComponent<Collider2D> ().bounds.max.y) {
				_WallNodes.Add (new KeyValuePair<List<Nodes>, Rooms> (GetSideNodes (s.Key.GetComponent<RoomsPathCalculation> (), 0), s.Key.GetComponent<Rooms> ()));
			} else if (s.Key.GetComponent<Collider2D> ().bounds.min.x > GetComponent<Collider2D> ().bounds.max.x) {
				_WallNodes.Add (new KeyValuePair<List<Nodes>, Rooms> (GetSideNodes (s.Key.GetComponent<RoomsPathCalculation> (), 1), s.Key.GetComponent<Rooms> ()));
			}
		}
	}
		
	List<Nodes> GetSideNodes(RoomsPathCalculation room, int side){//calculating which nodes to add
		List<Nodes> node = new List<Nodes> ();	
		GetNodeMap ();
		Nodes[,] test = room.GetNodeMap ();
	
		if (side == 0) {//North nodes, getting all nodes from _TheNodeMap that is neighbour to the neighbour north of this room
			_PointA = test [0, 0].GetID () [0, 0];
			_PointB = test [0, room.GetWidth () - 1].GetID () [0, 0];

			for (int i = 0; i < GetWidth (); i++) {
				if ((_TheNodeMap [0, i].GetID () [0, 0] - _PointA) > -_Closestnode && ((_TheNodeMap [0, i].GetID () [0, 0] - _PointB) < _Closestnode)) {
					node.Add (_TheNodeMap [0, i]);
				}
			}
		} else if (side == 1) {//East nodes
			_PointA = test [0, 0].GetID () [0, 1];
			_PointB = test [room.Getheight () - 1, 0].GetID () [0, 1];

			for (int i = 0; i < Getheight (); i++) {
				if (_TheNodeMap [i, 0].GetID () [0, 1] - _PointA < _Closestnode && _TheNodeMap [i, 0].GetID () [0, 1] - _PointB > -_Closestnode) {
					node.Add (_TheNodeMap [i, GetWidth () - 1]);
				}
			}
		} else if (side == 2) {//South nodes
			_PointA = test [0, 0].GetID () [0, 0];
			_PointB = test [0, room.GetWidth () - 1].GetID () [0, 0];

			for (int i = 0; i < GetWidth (); i++) {
				if ((_TheNodeMap [0, i].GetID () [0, 0] - _PointA) > -_Closestnode && ((_TheNodeMap [0, i].GetID () [0, 0] - _PointB) < _Closestnode)) {
					node.Add (_TheNodeMap [Getheight () - 1, i]);
				}
			}
		} else if (side == 3) {//West nodes
			_PointA = test [0, 0].GetID () [0, 1];
			_PointB = test [room.Getheight () - 1, 0].GetID () [0, 1];

			for (int i = 0; i < Getheight (); i++) {
				if (_TheNodeMap [i, 0].GetID () [0, 1] - _PointA < _Closestnode && _TheNodeMap [i, 0].GetID () [0, 1] - _PointB > -_Closestnode) {
					node.Add (_TheNodeMap [i, 0]);
				}
			}
		}

		return node;
	}

	#endregion


	#region Colliders

	void OnCollisionEnter2D(Collision2D coll) {//when a gameobject is inside the collider with tag == wall, then update the nodemap and recalculate the pathlist  TODO expand this to other sources of colliding objects?
		if (coll.gameObject.tag == "Wall") {
			UpdateCollisionID (coll.collider.bounds.max, coll.collider.bounds.min, 1);
			CalculatePathsList ();
		}			
	}

	void OnCollisionExit2D(Collision2D coll) {//when a gameobject is removed from the collider with tag == wall, then update the nodemap and recalculate the pathlist  TODO expand this to other sources of colliding objects?
		if (gameObject.tag == "Wall") {
			UpdateCollisionID (coll.collider.bounds.max, coll.collider.bounds.min, 0);
			CalculatePathsList ();
		}
	}

	void UpdateCollisionID(Vector2 upperRightCorner, Vector2 lowerLeftCorner, int collisionID){//When a collider colides with this. then im calculating where the colliding collider is and sets those positions to being 1 (1 = wall)
		_RightPoint = 0;
		_LeftPoint = 0;
		_HighestPoint = 0;
		_LowestPoint = 0;


		if (_MapWidht.y <= upperRightCorner.x) {
			_RightPoint = _NodesInWidth - 1;
		} else {
			_RightPoint = Mathf.FloorToInt (upperRightCorner.x - _MapWidht.x);
		}

		if (_MapWidht.x >= lowerLeftCorner.x) {
			_LeftPoint = 0;
		} else {
			_LeftPoint = Mathf.FloorToInt (lowerLeftCorner.x - _MapWidht.x);
		}

		if (_MapHeigth.x <= upperRightCorner.y) {
			_HighestPoint = 0;
		} else {
			_HighestPoint = Mathf.FloorToInt (_MapHeigth.x - upperRightCorner.y);
		}

		if (_MapHeigth.y >= lowerLeftCorner.y) {
			_LowestPoint = (_NodesInHight - 1);
		} else {
			_LowestPoint = Mathf.FloorToInt (_MapHeigth.x - lowerLeftCorner.y);
		}

		for (int i = _HighestPoint; i <= _LowestPoint; i++) {
			for (int j = _LeftPoint; j <= _RightPoint; j++) {
				_TheNodeMap [i, j].SetCollision (collisionID);
			}
		}
	}

	void CalculatePathsList(){//The Path is calculated here,  TODO imrpove this to where they choose the shortest path not just the middle one, multiple searches pr node path
		_PathNode.Clear ();
		_PathNodes.Clear ();

		foreach (KeyValuePair<List<Nodes>, Rooms> a in _WallNodes) {//going through all wall nodes like the top nodes 00000000000   pos[0,0] [0,1] and checks if there are anything that separates the nodes, if there are then add 2 starting nodes from the north direction
			_Saver = 0;
			_Previous = 0;

			foreach (Nodes s in a.Key) {
				if (s.GetCollision () == 1) {
					if (_Previous < _Saver) {
						_PathNode.Add (a.Key [Mathf.CeilToInt (_Previous) + (Mathf.FloorToInt ((_Saver - _Previous) / 2))]);
					}
					_Previous = ++_Saver;
				 
				} else if (s == a.Key.Last ()) {
					_PathNode.Add (a.Key [Mathf.CeilToInt (_Previous) + Mathf.FloorToInt ((_Saver - _Previous) / 2)]);
				} else {
					_Saver++;
				}
			}
			if (_PathNode.Count () > 0) {
				_PathNodes.Add (new KeyValuePair<List<Nodes>, Rooms> (_PathNode, a.Value));
				_PathNode = new List<Nodes> ();
			}
		}

		for (int i = 0; i < _PathNodes.Count (); i++) {//goes throguh all nodes and adds a path from a to b and b to a   + a list with just starting positions

			foreach (Nodes a in  _PathNodes [i].Key) {

				_PathList.Add (new KeyValuePair<KeyValuePair<Rooms, Rooms>, List<Nodes>> (new KeyValuePair<Rooms, Rooms> (GetComponent<Rooms>(), _PathNodes [i].Value), CreatePath (a, a)));

				if (_PathNodes.Count () > 1) {
					for (int j = 1 + i; j < _PathNodes.Count (); j++) {
						foreach (Nodes b in _PathNodes [j].Key) {
							_PathList.Add (new KeyValuePair<KeyValuePair<Rooms, Rooms>, List<Nodes>> (new KeyValuePair<Rooms, Rooms> (_PathNodes [i].Value, _PathNodes [j].Value), CreatePath (a, b)));
							_PathList.Add (new KeyValuePair<KeyValuePair<Rooms, Rooms>, List<Nodes>> (new KeyValuePair<Rooms, Rooms> (_PathNodes [j].Value, _PathNodes [i].Value), CreatePath (b, a)));
						}
					}
				} 
			} 
		}
	}

	#endregion


	#region A* Pathfinding

	HashSet<Nodes> _OpenList = new HashSet<Nodes>();//Holds the nodes that im going to search
	HashSet<Nodes> _ClosedList = new HashSet<Nodes>();//Holds the nodes that have been search
	List<Nodes> _ThePath = new List<Nodes>();//This is the path with the coordinates to the player/enemy
	List<Nodes> _NewPath = new List<Nodes>();//

	Nodes _StartNode; //This is the node that this object is an and starts searching from
	Nodes _EndNode; //end node, destination node
	Nodes _NodeSaver; //just to hold some nodes while searching

	float _LowerstFScore = 100000; //This is just a value to get the "best" path to the target, if there is an enormous amount of nodes then this value must be increased

	public List<Nodes> CreatePath(Nodes startNode, Nodes endNode) {//Starts A* and clears all the nodes so that they are rdy for the next search
		_ThePath = new List<Nodes>();
		_ThePath = AStartAlgorithm(startNode, endNode);

		foreach (Nodes n in _OpenList) {
			n.ClearAll();
		}
		foreach (Nodes n in _ClosedList) {
			n.ClearAll();
		}

		return _ThePath;
	}

	public List<Nodes> AStartAlgorithm(Nodes starts, Nodes ends) {//A*. 
		_OpenList.Clear();
		_ClosedList.Clear();
		_NodeSaver = null;//Holds the current node that im searching with

		starts.SetHCost(ends);
		_OpenList.Add(starts);

		while (_OpenList.Count > 0) {

			_LowerstFScore = 100000;

			foreach (Nodes n in _OpenList) {//Goes through the _OpenList to search after the lowest F value, 			TODO improve this in some way, if i can manage to somehow sort this in an efficient way then (Y) 
				if (n.GetFValue() < _LowerstFScore) {
					_NodeSaver = n;
					_LowerstFScore = n.GetFValue();
				}
			}

			_OpenList.Remove(_NodeSaver);
			_ClosedList.Add(_NodeSaver);

			if (_NodeSaver == ends) {//If _NodeSaver == ends then the search is complete and sending _closedlist to calculate the path from start to end
				return RemakePath(_ClosedList.Last());
			}

			foreach (Nodes n in _NodeSaver.GetNeighbours()) {
				if (n.GetCollision() != 1) {
					if (n.GetParent() == null && n != starts) {//if n dont have a parent and n isnt the starting node (starts must be there else it will cause an FATAL error ;-P, your warned :D)
						n.SetParent(_NodeSaver);
						n.SetHCost(ends);
					} else if (n.GetGCost() > _NodeSaver.GetGCost() + _NodeSaver.GetJustMoveGCost(n)) {//calculates best route through these values
						n.SetParent(_NodeSaver);
					}
					if (_OpenList.Contains(n) == false && _ClosedList.Contains(n) == false) {//adds all neighbour to openlist if it isnt already there or if it has already been searched through
						_OpenList.Add(n);
					}
				}
			}
		}
		return RemakePath(_ClosedList.Last());
	}

	public List<Nodes> RemakePath(Nodes checkedNodes) {//Makes the path by going to the end node and get the parent, then parent of the parent ....... until your at the start node
		_NewPath = new List<Nodes>();
		_NodeSaver = checkedNodes;

		_NewPath.Add(_NodeSaver);
		while (true) {
			if (_NodeSaver != null && _NodeSaver.GetParent() != null) {
				_NewPath.Add(_NodeSaver.GetParent());
				_NodeSaver = _NodeSaver.GetParent();
			} else {
				_NewPath.Reverse ();
				return _NewPath;
			}
		}
	}

	#endregion

	public Nodes GetMyNode(GameObject me){
		float te1 = _TheNodeMap [0, 0].GetID () [0, 0] - me.transform.position.x;
		float te2 =_TheNodeMap [0, 0].GetID () [0, 1] - me.transform.position.y;
		if(te1 < 0){
			te1 = Mathf.FloorToInt( te1 * -1);
			if (te1 >= _NodesInWidth)
				te1 = _NodesInWidth - 1;
		}
		if(te2 < 0){
			te2 = Mathf.FloorToInt( te2 * -1);

			if (te2 >= _NodesInHight)
				te2 = _NodesInHight - 1;
		}
		if (te1 >= _NodesInWidth)
			te1 = _NodesInWidth - 1;
		if (te2 >= _NodesInHight)
			te2 = _NodesInHight - 1;
		
		return _TheNodeMap [Mathf.FloorToInt(te2), Mathf.FloorToInt(te1)];
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


	public List<Nodes> GetPath(Rooms FromRoom, Rooms NextNextRoom, Nodes CurrentPosition){
		List<Nodes> b = new List<Nodes>();

		float a = 100;
		float _XValue = 0, _YValue = 0;
	

		foreach (KeyValuePair<KeyValuePair<Rooms, Rooms>, List<Nodes>>  s in _PathList) {
			if ((s.Key.Key == FromRoom && s.Key.Value == NextNextRoom)) {
				_XValue = s.Value.First ().GetID () [0, 0] - CurrentPosition.GetID () [0, 0];
				_YValue = s.Value.First ().GetID () [0, 1] - CurrentPosition.GetID () [0, 1];

				if (_XValue < 0) {
					_XValue *= -1;
				}
				if (_YValue < 0) {
					_YValue *= -1;
				}
				if ((_XValue + _YValue) < a) {
					a = _XValue + _YValue;
					b = s.Value;
				}
			}
		}
		return b;
	}


	public List<Nodes> GetMiddleOfRoomPath(Rooms Current, Rooms NextRoom, Nodes CurrentPosition){//TODO come from out of wall  separate one way room   or something else ofc ;-p
		List<Nodes> b = new List<Nodes>();

		float a = 1000;
		float _XValue = 0, _YValue = 0;
		foreach (KeyValuePair<KeyValuePair<Rooms, Rooms>, List<Nodes>>  s in _PathList) {
			if (s.Key.Key == Current && s.Key.Value == NextRoom) {
				_XValue = s.Value.Last ().GetID () [0, 0] - CurrentPosition.GetID() [0, 0];
				_YValue = s.Value.Last ().GetID () [0, 1] - CurrentPosition.GetID() [0, 1];

				if (_XValue < 0) {
					_XValue *= -1;
				}
				if (_YValue < 0) {
					_YValue *= -1;
				}
				if ((_XValue + _YValue) < a) {
					a = _XValue + _YValue;
					b = s.Value;
				}
			}
		}
		return CreatePath (CurrentPosition, b.Last ());
	}

	public List<Nodes> GetLastRoomPath(Nodes CurrentPosition){//TODO come from out of wall  separate one way room   or something else ofc ;-p
		return CreatePath (CurrentPosition, _PlayerNode[0]);
	}







}