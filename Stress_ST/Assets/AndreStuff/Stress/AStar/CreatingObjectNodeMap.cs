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
	int _XYDimentions = 15;//x2+1
	float _NodeSize = 1;
	int _HalfDimention;

	int	_RightPoint = 0;//used to calculate how far the colliding objects collider is inside my collider
	int	_LeftPoint = 0;
	int	_HighestPoint = 0;
	int	_LowestPoint = 0;

	float[] _PathNodeIDStorer;//storing this for later use

	float _Xpos = 0;
	float _Ypos = 0;
	int _ListLength = 0; //used to store the length of the list of the colliders
	int x = 0;
	int y = 0;

	public CreatingObjectNodeMap (float XYDimentions, StressEnums.NodeSizes NodeSize, float[] pathnodeid){
		_NodeSize = 1 / (float)NodeSize;

		_XYDimentions =  Mathf.FloorToInt(XYDimentions / _NodeSize * 2) + 1; 
		_HalfDimention = Mathf.FloorToInt(XYDimentions);

		_PathNodeIDStorer = pathnodeid;
		_NodeMap = new Nodes[_XYDimentions, _XYDimentions];
		_AStar = new AStarPathFinding_Nodes(_XYDimentions * _XYDimentions);
	}

	public void SetCenterPos(float[,] pos){
		_CenterPos = pos;
	}

	public void SetTargetPos(float[,] target){
		_WhereToGo = target; 
	}

	public List<BoxCollider2D> GetEnemyColliders(){
		return _EnemyColliders;
	}

	public void CreateNodeMap(){//creating the node map and adding neighbours to the nodes

		if (_NodeSize == 0.5) {
			_PathNodeIDStorer [0] = _PathNodeIDStorer [0] * 0.5f;
			_PathNodeIDStorer [2] = _PathNodeIDStorer [2] * 0.5f;
		} else if (_NodeSize == 0.25) {
			_PathNodeIDStorer [0] = _PathNodeIDStorer [0] * 0.25f;
			_PathNodeIDStorer [2] = _PathNodeIDStorer [2] * 0.25f;
		} else if (_NodeSize == 0.125) {
			_PathNodeIDStorer [0] = _PathNodeIDStorer [0] * 0.125f;
			_PathNodeIDStorer [2] = _PathNodeIDStorer [2] * 0.125f;
		}
			
		for (int x = 0; x < _XYDimentions; x++) {
			for (int y = 0; y < _XYDimentions; y++) {
				_NodeMap [y, x] = new Nodes (new float[,]{ {(x * _NodeSize) - (_HalfDimention),	(_HalfDimention) - (y * _NodeSize)} }, 0, _PathNodeIDStorer);
			}
		}

		for (int i = 0; i < _XYDimentions; i++) {
			for (int j = 0; j < _XYDimentions; j++) {
				for (int k = -1; k < 2; k++) {
					for (int h = -1; h < 2; h++) {
						if ((i + k >= 0 && j + h >= 0) && ((i + k < _XYDimentions) && j + h < _XYDimentions) && !(k == 0 && h == 0)) {
							_NodeMap [i, j].NeighbourNodes [1 + k, 1 + h] = _NodeMap [i + k, j + h];
						}
					}
				}
			}
		}
		_StartNode [0] = _NodeMap [(_XYDimentions - 1) / 2, (_XYDimentions - 1) / 2];
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

		if (_WhereToGo [0, 0] < _CurrentPosition [0, 0] - _HalfDimention) {//if target is on the left side of the collider == index 0
			x = 0;
		} else if (_WhereToGo [0, 0] > _CurrentPosition [0, 0] + _HalfDimention) {//if target is on the right side of the collider == index -> last
			x = _XYDimentions - 1;
		} else {//if target is inside the collider calculate where 
			_Xpos = (_WhereToGo [0, 0] - (_CurrentPosition [0, 0] - _HalfDimention)) / _NodeSize;

			if (_Xpos % 1 > 0.5f) {
				x = Mathf.CeilToInt (_Xpos);
			} else {
				x = Mathf.FloorToInt (_Xpos);
			}
		}
	
		if (_WhereToGo [0, 1] < _CurrentPosition [0, 1] - _HalfDimention) {//same just with y
			y = _XYDimentions - 1;
		} else if (_WhereToGo [0, 1] > _CurrentPosition [0, 1] + _HalfDimention) {
			y = 0;
		} else {
			_Ypos = ((_CurrentPosition [0, 1] + _HalfDimention) - _WhereToGo [0, 1]) / _NodeSize;

			if (_Ypos % 1 > 0.5f) {
				y = Mathf.CeilToInt (_Ypos);
			} else {
				y = Mathf.FloorToInt (_Ypos);
			}
		}

		if (_NodeMap [y, x].GetCollision () == 100) {
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
			
				if (x + incrementing <= _XYDimentions - 1) {//TODO improve this  //if im outside the array index sett 0 or last index
					x1 = x + incrementing;
				} else {
					x1 = x;
				}

				if (x - incrementing >= 0) {
					x2 = x - incrementing;
				} else {
					x2 = x;
				}

				if (y + incrementing <= _XYDimentions - 1) {
					y1 = y + incrementing;
				} else {
					y1 = y;
				}

				if (y - incrementing >= 0) {
					y2 = y - incrementing;
				} else {
					y2 = y;
				}

				for (int i = x2; i < x1; i++) {//going through all nodes from the previous xy position found before the while() and going around that point until i hit a free spot
					for (int j = y2; j < y1; j++) {
					if (_NodeMap [j,i].GetCollision () != 100) {
						_EndNode [0] = _NodeMap [j,i];
							break;
						}
					}
				}

		
				incrementing++;
				
			//	_EndNode [0] = _NodeMap [Mathf.RoundToInt (0), Mathf.RoundToInt ((_XYDimentions - 1) / 2)];
				if (x1 == _XYDimentions && x2 == 0 && y1 == _XYDimentions && y2 == 0 && _EndNode [0] == null) {
					_EndNode [0] = _StartNode [0];
					return;
				//		Debug.Log ("IM SRRY MASTER.... BUT THERE WAS A DEAD END");
				} else {
				//	Debug.Log ("inside zer loop");
				}
			}
		} else {
			_EndNode [0] = _NodeMap [y, x];
		}

		_AStar.CreatePath ();

		return;
	}

	public void AddWalls(GameObject NewObject){
		_WallColliders.Add (NewObject.GetComponent<BoxCollider2D>());
	}

	public void AddEnemyPositions(GameObject pos){
		_EnemyColliders.Add (pos.GetComponent<BoxCollider2D> ());
	}

	public void RemoveEnemyPositions(GameObject pos){
		_EnemyColliders.Remove (pos.GetComponent<BoxCollider2D> ());
	}

	public void RemoveMyselfFromOthers(GameObject myself){
	//	for (int j = 0; j < _EnemyColliders.Count; j++) {
	//		_EnemyColliders [j].GetComponent<DefaultBehaviour> ().RemoveEnemyWithTrigger (myself);
	//	}
	}


	public int GetListCount(){
		return _WallColliders.Count;
	}

	public void RemoveWalls(GameObject OldObject){
		_WallColliders.Remove (OldObject.GetComponent<BoxCollider2D>());
	}
	
	public void UpdateNodeMap(){//reseting the collisionid and setting the new once
		for (int i = 0; i < _XYDimentions; i++) {//chenges all node collisionid back to 0
			for (int j = 0; j <_XYDimentions; j++) {
				_NodeMap [i, j].MapCollision = 0;
			}
		}
		_ListLength = _EnemyColliders.Count;
		for (int k = 0; k < _ListLength; k++) {//setting the collisionID for the enemys
			if (_EnemyColliders [k] != null) {
			
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
		}



		_ListLength = _WallColliders.Count;
		for (int k = 0; k < _ListLength; k++) {//setting the collisionID for the walls
			_ColliderBounds = _WallColliders [k].bounds;



			_UpperRightCorner = _ColliderBounds.max;
			_LowerLeftCorner = _ColliderBounds.min;

	
		if (_LowerLeftCorner.x < _CenterPos [0, 0] - _HalfDimention) {//same just with y
			_LeftPoint = 0;
		} else if (_LowerLeftCorner.x > _CenterPos [0, 0] + _HalfDimention) {
			_LeftPoint = _XYDimentions - 1;
		} else {
			_Xpos = (_LowerLeftCorner.x - (_CenterPos [0, 0] - _HalfDimention)) / _NodeSize;
			
			if (_Xpos % 1 > 0.5f) {
				_LeftPoint = Mathf.CeilToInt (_Xpos);
			} else {
				_LeftPoint = Mathf.FloorToInt (_Xpos);
			}
		}

		if (_LowerLeftCorner.y > _CenterPos [0, 1] + _HalfDimention) {//same just with y
			_LowestPoint = 0;
		} else if (_LowerLeftCorner.y < _CenterPos [0, 1] - _HalfDimention) {
			_LowestPoint = _XYDimentions - 1;
		} else {
			_Ypos = ((_CenterPos [0, 1] + _HalfDimention) - _LowerLeftCorner.y) / _NodeSize;

			if (_Ypos % 1 > 0.5f) {
				_LowestPoint = Mathf.CeilToInt (_Ypos);
			} else {
				_LowestPoint = Mathf.FloorToInt (_Ypos);
			}
		}

		if (_UpperRightCorner.x < _CenterPos [0, 0] - _HalfDimention) {//same just with y
			_RightPoint = 0;
		} else if (_UpperRightCorner.x > _CenterPos [0, 0] + _HalfDimention) {
			_RightPoint = _XYDimentions - 1;
		} else {
			_Xpos = ( _UpperRightCorner.x - (_CenterPos [0, 0] - _HalfDimention)) / _NodeSize;

			if (_Xpos % 1 > 0.5f) {
				_RightPoint = Mathf.CeilToInt (_Xpos);
			} else {
				_RightPoint = Mathf.FloorToInt (_Xpos);
			}
		}

		if (_UpperRightCorner.y > _CenterPos [0, 1] + _HalfDimention) {//same just with y
			_HighestPoint = 0;
		} else if (_UpperRightCorner.y < _CenterPos [0, 1] - _HalfDimention) {
			_HighestPoint = _XYDimentions - 1;
		} else {
			_Ypos = ((_CenterPos [0, 1] + _HalfDimention) - _UpperRightCorner.y) / _NodeSize;

			if (_Ypos % 1 > 0.5f) {
				_HighestPoint = Mathf.CeilToInt (_Ypos);
			} else {
				_HighestPoint = Mathf.FloorToInt (_Ypos);
			}
		}







		/*	_LeftPoint = (int)(_LowerLeftCorner.x - (_CenterPos [0, 0] - (_XDimention + 0.5f)));
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
				_LowestPoint = _YDimention * 2;*/

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
/*
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

	public List<BoxCollider2D> GetEnemyColliders(){
		return _EnemyColliders;
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
/*
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

public void AddWalls(GameObject NewObject){
	_WallColliders.Add (NewObject.GetComponent<BoxCollider2D>());
}

public void AddEnemyPositions(GameObject pos){
	_EnemyColliders.Add (pos.GetComponent<BoxCollider2D> ());
}

public void RemoveEnemyPositions(GameObject pos){
	_EnemyColliders.Remove (pos.GetComponent<BoxCollider2D> ());
}

public void RemoveMyselfFromOthers(GameObject myself){
	//	for (int j = 0; j < _EnemyColliders.Count; j++) {
	//		_EnemyColliders [j].GetComponent<DefaultBehaviour> ().RemoveEnemyWithTrigger (myself);
	//	}
}


public int GetListCount(){
	return _WallColliders.Count;
}

public void RemoveWalls(GameObject OldObject){
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
		if (_EnemyColliders [k] != null) {

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

*/