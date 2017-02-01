using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatingObjectNodeMap {

	AStarPathFinding_Nodes _AStar;

	List<BoxCollider2D> _WallColliders = new List<BoxCollider2D> ();
	List<BoxCollider2D> _EnemyColliders = new List<BoxCollider2D>(); 

	Vector3 _UpperRightCorner = Vector3.zero;
	Vector3 _LowerLeftCorner = Vector3.zero;

	Bounds _ColliderBounds;

	Nodes[] _StartNode = new Nodes[1];
	Nodes[] _EndNode = new Nodes[1];
	Nodes[,] _NodeMap;


	float[,] _CenterPos;//these three variables are used to store this objects positions at different stages, center is the current position of the object, current is where i were 
	float[,] _WhereToGo;
	float[,] _CurrentPosition = new float[1,2];

	int _XDimention = 6;//x2+1
	int _YDimention = 6;

	int	_RightPoint = 0;//used to calculate how far the colliding objects collider is inside my collider
	int	_LeftPoint = 0;
	int	_HighestPoint = 0;
	int	_LowestPoint = 0;

	int[] _PathNodeIDStorer;//storing this for later use

	float _Xpos = 0;
	float _Ypos = 0;
	int _ListLength = 0; //used to store the length of the list of the colliders

	public CreatingObjectNodeMap (int x, int y, int[] pathnodeid){
		_XDimention = x / 2;
		_YDimention = y / 2;

		_NodeMap = new Nodes[y, x];
		_PathNodeIDStorer = pathnodeid;
		_AStar = new AStarPathFinding_Nodes((x * y));
	}

	public void SetCenterPos(float[,] pos){
		_CenterPos = pos;
	}

	public void SetTargetPos(float[,] target){
		_WhereToGo = target; 
	}

	public void CreateNodeMap(){//creating the node map and adding neighbours to the nodes
		for (int x = 0; x < _XDimention * 2 + 1; x++) {
			for (int y = 0; y < _YDimention * 2 + 1; y++) {
				_NodeMap [y, x] = new Nodes (new float[,]{ { x - _XDimention, _YDimention - y } }, 0, _PathNodeIDStorer);
			}
		}

		for (int i = 0; i < _XDimention * 2 + 1; i++) {
			for (int j = 0; j < _YDimention * 2 + 1; j++) {
				for (int k = -1; k < 2; k++) {
					for (int h = -1; h < 2; h++) {
						if ((i + k >= 0 && j + h >= 0) && ((i + k < _XDimention * 2 + 1) && j + h < _YDimention * 2 + 1) && !(k == 0 && h == 0)) {
								_NodeMap [i, j].NeighbourNodes [1 + k,1 + h] = _NodeMap [i + k, j + h];
						}
					}
				}
			}
		}


		_AStar.SetEndStartNode (_StartNode, _EndNode);
	}

	public float[,] GetCenterPos(){
		return _CurrentPosition;
	}

	public Nodes[] GetNodeList(){
		return _AStar.GetListRef ();
	}

	public int[] GetNodeindex(){
		return _AStar.GetListindexref ();
	}

	public void SetInfoAndStartSearch(bool UpdateMapToo){//setting the start and end node "positions"
		if (UpdateMapToo == true) {
			UpdateNodeMap ();
		}

		_CurrentPosition [0, 0] = _CenterPos [0, 0];
		_CurrentPosition [0, 1] = _CenterPos [0, 1];

		_Xpos = _XDimention + (_WhereToGo [0, 0] - _CurrentPosition [0, 0]);
		_Ypos = _YDimention + (_CurrentPosition [0, 1] - _WhereToGo [0, 1]);

		if (_Xpos < 0) {
			_Xpos = 0;
		} else if (_Xpos > _XDimention * 2) {
			_Xpos = _XDimention * 2;
		}

		if (_Ypos < 0) {
			_Ypos = 0;
		} else if (_Ypos > _YDimention * 2) {
			_Ypos = _YDimention * 2;
		}
	
		_StartNode [0] = _NodeMap [_YDimention, _XDimention];

		if (_NodeMap [Mathf.RoundToInt (_Ypos), Mathf.RoundToInt (_Xpos)].GetCollision () == 100) {
			int x = Mathf.RoundToInt (_Xpos);
			int y = Mathf.RoundToInt (_Ypos);
			int x1 = 0;
			int x2 = 0;
			int y1 = 0;
			int y2 = 0;
			int incrementing = 1;
			_EndNode [0] = null;

			while (_EndNode [0] == null) {//TODO improve this to not search through all nodes but rather all in a straight line and on a 45degree angle
		//		Debug.Log ("here");
				//MyNodeMap [6, 0];//this is 6 down from the top, and 0 from the left (if the gizmo is turned on in creaturonebehaviour)
				//MyNodeMap [6, 12];//this is 6 down from the top, and 12 from the left (if the gizmo is turned on in creaturonebehaviour)

				//MyNodeMap [0, 6];//this is 0 down from the top, and 6 from the left (if the gizmo is turned on in creaturonebehaviour)
				//MyNodeMap [12, 6];//this is 12 down from the top, and 6 from the left (if the gizmo is turned on in creaturonebehaviour)
		//		Debug.Log(x + " | " + y + " | " + WhereToGo [0, 0] + " | " + WhereToGo [0, 1]);
			/*	if (x == XDimention + 1 || y == YDimention + 1) {
					Debug.Log ("same");

				} else {
					Debug.Log ("not");
				
				}*/

		/*		if (x > XDimention && y > YDimention) {
					if (x - incrementing >= 0 && y - incrementing >= 0) {
						if (MyNodeMap [x, y - incrementing].GetCollision () != 100) {
							EndNode [0] = MyNodeMap [x, y - incrementing];
						} else if (MyNodeMap [x - incrementing, y].GetCollision () != 100) {
							EndNode [0] = MyNodeMap [x - incrementing, y];
						} else if (MyNodeMap [x - incrementing, y - incrementing].GetCollision () != 100) {
							EndNode [0] = MyNodeMap [x - incrementing, y - incrementing];
						}
					} else {
						EndNode [0] = MyNodeMap [XDimention, YDimention];
					}
				} else if (x > XDimention && y < YDimention) {
					if (x - incrementing >= 0 && y + incrementing <= YDimention * 2) {
						if (MyNodeMap [x, y + incrementing].GetCollision () != 100) {
							EndNode [0] = MyNodeMap [x, y + incrementing];
						} else if (MyNodeMap [x - incrementing, y].GetCollision () != 100) {
							EndNode [0] = MyNodeMap [x - incrementing, y];
						} else if (MyNodeMap [x - incrementing, y + incrementing].GetCollision () != 100) {
							EndNode [0] = MyNodeMap [x - incrementing, y + incrementing];
						}
					} else {
						EndNode [0] = MyNodeMap [XDimention, YDimention];
					}
				} else if (x < XDimention && y < YDimention) {
					if (x + incrementing <= XDimention * 2 && y + incrementing <= YDimention * 2) {
						if (MyNodeMap [x, y + incrementing].GetCollision () != 100) {
							EndNode [0] = MyNodeMap [x, y + incrementing];
						} else if (MyNodeMap [x + incrementing, y].GetCollision () != 100) {
							EndNode [0] = MyNodeMap [x + incrementing, y];
						} else if (MyNodeMap [x + incrementing, y + incrementing].GetCollision () != 100) {
							EndNode [0] = MyNodeMap [x + incrementing, y + incrementing];
						}
					} else {
						EndNode [0] = MyNodeMap [XDimention, YDimention];
					}
				} else if (x < XDimention && y > YDimention) {
					if (x + incrementing <= XDimention * 2 && y - incrementing >= 0) {
						if (MyNodeMap [x, y - incrementing].GetCollision () != 100) {
							EndNode [0] = MyNodeMap [x, y - incrementing];
						} else if (MyNodeMap [x + incrementing, y].GetCollision () != 100) {
							EndNode [0] = MyNodeMap [x + incrementing, y];
						} else if (MyNodeMap [x + incrementing, y - incrementing].GetCollision () != 100) {
							EndNode [0] = MyNodeMap [x + incrementing, y - incrementing];
						}
					} else {
						EndNode [0] = MyNodeMap [XDimention, YDimention];
					}
				} else {
					EndNode [0] = MyNodeMap [XDimention, YDimention];
				}
				if(EndNode[0] != null)
					Debug.Log (EndNode[0].GetID()[0,0] + " | " + EndNode[0].GetID()[0,1] );*/
			
				if (x + incrementing <= _XDimention * 2) {
					x1 = x + incrementing;
				} else {
					x1 = x;
				}

				if (x - incrementing >= 0) {
					x2 = x - incrementing;
				} else {
					x2 = x;
				}

				if (y + incrementing <= _YDimention * 2) {
					y1 = y + incrementing;
				} else {
					y1 = y;
				}

				if (y - incrementing >= 0) {
					y2 = y - incrementing;
				} else {
					y2 = y;
				}

				for (int i = x2; i < x1; i++) {
					if (_NodeMap [y, i].GetCollision () != 100) {
						if (_NodeMap [_YDimention, _XDimention] != _NodeMap [y, i]) {
							_EndNode [0] = _NodeMap [y, i];
							continue;
						}
					}
				}
				for (int j = y2; j < y1; j++) {
					if (_NodeMap [j, x].GetCollision () != 100) {
						if (_NodeMap [_YDimention, _XDimention] != _NodeMap [j, x]) {
							_EndNode [0] = _NodeMap [j, x];
							continue;
						}
					}
				}
				incrementing++;
			}
		} else {
			_EndNode [0] = _NodeMap [Mathf.RoundToInt (_Ypos), Mathf.RoundToInt (_Xpos)];
		}
		_AStar.CreatePath ();

		return;
	}

	public void AddGameobjectsWithinTrigger(GameObject NewObject){
		_WallColliders.Add (NewObject.GetComponent<BoxCollider2D>());
	}

	public void AddEnemyPositions(GameObject pos){
		_EnemyColliders.Add (pos.GetComponent<BoxCollider2D> ());
	}

	public void RemoveEnemyPositions(GameObject pos){
		_EnemyColliders.Remove (pos.GetComponent<BoxCollider2D> ());
	}

	public int GetListCount(){
		return _WallColliders.Count;
	}

	public void RemoveGameobjectsWithinTrigger(GameObject OldObject){
		_WallColliders.Remove (OldObject.GetComponent<BoxCollider2D>());
	}
	
	public void UpdateNodeMap(){//reseting the collisionid and setting the new once
		for (int i = 0; i < _XDimention * 2 + 1; i++) {//chenges all node collisionid back to 0
			for (int j = 0; j < _YDimention * 2 + 1; j++) {
				_NodeMap [i, j].MapCollision = 0;
			}
		}
		_ListLength = _EnemyColliders.Count;
		for (int k = 0; k < _ListLength; k++) {//setting the collisionID for the enemys
			_ColliderBounds = _EnemyColliders [k].bounds;

			_UpperRightCorner = _ColliderBounds.max;
			_LowerLeftCorner = _ColliderBounds.min;

			_LeftPoint = (int)(_LowerLeftCorner.x - (_CenterPos [0, 0] - (_XDimention + 0.5f)));
			if (_LeftPoint < 0)
				_LeftPoint = 0;

			_RightPoint = (_XDimention * 2) - (int)((_CenterPos [0, 0] + (_XDimention + 0.5f)) - _UpperRightCorner.x);
			if (_RightPoint > _XDimention * 2)
				_RightPoint = _XDimention * 2;

			_HighestPoint = (int)((_CenterPos [0, 1] + (_YDimention + 0.5f)) - _UpperRightCorner.y);
			if (_HighestPoint < 0)
				_HighestPoint = 0;

			_LowestPoint = (_YDimention * 2) - (int)(_LowerLeftCorner.y - (_CenterPos [0, 1] - (_YDimention + 0.5f)));
			if (_LowestPoint > _YDimention * 2)
				_LowestPoint = _YDimention * 2;

			for (int i = _HighestPoint; i <= _LowestPoint; i++) {//changing the nodes inside the coordinates i found to collisionID
				for (int j = _LeftPoint; j <= _RightPoint; j++) {
					_NodeMap [i, j].MapCollision = 2;
				}
			}
		}

		_ListLength = _WallColliders.Count;
		for (int k = 0; k < _ListLength; k++) {//setting the collisionID for the walls
			_ColliderBounds = _WallColliders [k].bounds;

			_UpperRightCorner = _ColliderBounds.max;
			_LowerLeftCorner = _ColliderBounds.min;

			_LeftPoint = (int)(_LowerLeftCorner.x - (_CenterPos [0, 0] - (_XDimention + 0.5f)));
			if (_LeftPoint < 0)
				_LeftPoint = 0;

			_RightPoint = (_XDimention * 2) - (int)((_CenterPos [0, 0] + (_XDimention + 0.5f)) - _UpperRightCorner.x);
			if (_RightPoint > _XDimention * 2)
				_RightPoint = _XDimention * 2;

			_HighestPoint = (int)((_CenterPos [0, 1] + (_YDimention + 0.5f)) - _UpperRightCorner.y);
			if (_HighestPoint < 0)
				_HighestPoint = 0;

			_LowestPoint = (_YDimention * 2) - (int)(_LowerLeftCorner.y - (_CenterPos [0, 1] - (_YDimention + 0.5f)));
			if (_LowestPoint > _YDimention * 2)
				_LowestPoint = _YDimention * 2;

			for (int i = _HighestPoint; i <= _LowestPoint; i++) {//changing the nodes inside the coordinates i found to collisionID
				for (int j = _LeftPoint; j <= _RightPoint; j++) {
					_NodeMap [i, j].MapCollision = 1;
				}
			}
		}
	}

	public Nodes[,] GetNodemap() {
		return _NodeMap;
	}
}
