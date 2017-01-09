using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatingObjectNodeMap {

	AStarPathFinding_Nodes nodesastar = new AStarPathFinding_Nodes(169);

	List<BoxCollider2D> ObjectsWithinNodeMap = new List<BoxCollider2D> ();

	Nodes[] StartNode = new Nodes[1];
	Nodes[] EndNode = new Nodes[1];
	Nodes[,] MyNodeMap;

	Vector3 upperRightCorner = Vector3.zero;
	Vector3 lowerLeftCorner = Vector3.zero;

	float[,] CenterPos;
	float[,] WhereToGo;
	float[,] CurrentPossition = new float[1,2];

	List<BoxCollider> EnemyPositions = new List<BoxCollider>(); 

	int XDimention = 6;//x2+1
	int YDimention = 6;

	float xpos = 0;
	float ypos = 0;

	int	_RightPoint = 0;
	int	_LeftPoint = 0;
	int	_HighestPoint = 0;
	int	_LowestPoint = 0;

	public CreatingObjectNodeMap (int x, int y){
		MyNodeMap = new Nodes[y, x];
	}

	public void SetCenterPos(float[,] pos){
		CenterPos = pos;
	}

	public void SetTargetPos(float[,] target){
		WhereToGo = target; 
	}

	public void CreateNodeMap(){
		for (int x = 0; x < XDimention * 2 + 1; x++) {
			for (int y = 0; y < YDimention * 2 + 1; y++) {
				MyNodeMap [y, x] = new Nodes (new float[,]{ { x - XDimention, YDimention - y } }, 1);
			}
		}

	/*	for (int i = 0; i < XDimention * 2 + 1; i++) {
			for (int j = 0; j < YDimention * 2 + 1; j++) {
				for (int k = -1; k < 2; k++) {
					for (int h = -1; h < 2; h++) {
						if ((i + k >= 0 && j + h >= 0) && ((i + k < XDimention * 2 + 1) && j + h < YDimention * 2 + 1) && !(k == 0 && h == 0)) {
							MyNodeMap [j, i].SetNeighbors (MyNodeMap [j + h, i + k]);
						}
					}
				}
			}
		}*/

		for (int i = 0; i < XDimention * 2 + 1; i++) {
			for (int j = 0; j < YDimention * 2 + 1; j++) {
				for (int k = -1; k < 2; k++) {
					for (int h = -1; h < 2; h++) {
						if ((i + k >= 0 && j + h >= 0) && ((i + k < XDimention * 2 + 1) && j + h < YDimention * 2 + 1) && !(k == 0 && h == 0)) {
								MyNodeMap [i, j].NeighbourNodes [1 + k,1 + h] = MyNodeMap [i + k, j + h];
						}
					}
				}
			}
		}


		nodesastar.SetEndStartNode (StartNode, EndNode);
	}

	public float[,] GetCenterPos(){
		return CurrentPossition;
	}

	public Nodes[] GetNodeList(){
		return nodesastar.GetListRef ();
	}

	public int[] GetNodeindex(){
		return nodesastar.GetListindexref ();
	}

	public void SetInfoAndStartSearch(bool UpdateMapToo){//TODO GET THE NODE LIST OF CONNECTOR NODES THEN ITTERATE THROUGH THEM, might not be the solution 
		if (UpdateMapToo == true) {
			UpdateNodeMap ();
		}

		CurrentPossition [0, 0] = CenterPos [0, 0];
		CurrentPossition [0, 1] = CenterPos [0, 1];

		xpos = XDimention + (WhereToGo [0, 0] - CurrentPossition [0, 0]);
		ypos = YDimention + (CurrentPossition [0, 1] - WhereToGo [0, 1]);

		if (xpos < 0) {
			xpos = 0;
		} else if (xpos > XDimention * 2) {
			xpos = XDimention * 2;
		}

		if (ypos < 0) {
			ypos = 0;
		} else if (ypos > YDimention * 2) {
			ypos = YDimention * 2;
		}
	
		StartNode [0] = MyNodeMap [YDimention, XDimention];

		if (MyNodeMap [Mathf.RoundToInt (ypos), Mathf.RoundToInt (xpos)].GetCollision () == 100) {
			int x = Mathf.RoundToInt (xpos);
			int y = Mathf.RoundToInt (ypos);
			int x1 = 0;
			int x2 = 0;
			int y1 = 0;
			int y2 = 0;
			int incrementing = 1;
			EndNode [0] = null;

			while (EndNode [0] == null) {

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
				if (x + incrementing <= XDimention * 2) {
					x1 = x + incrementing;
				} else {
					x1 = x;
				}

				if (x - incrementing >= 0) {
					x2 = x - incrementing;
				} else {
					x2 = x;
				}

				if (y + incrementing <= YDimention * 2) {
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
					if (MyNodeMap [y, i].GetCollision () != 100) {
						if (MyNodeMap [YDimention, XDimention] != MyNodeMap [y, i]) {
							EndNode [0] = MyNodeMap [y, i];
							continue;
						}
					}
				}
				for (int j = y2; j < y1; j++) {
					if (MyNodeMap [j, x].GetCollision () != 100) {
						if (MyNodeMap [YDimention, XDimention] != MyNodeMap [j, x]) {
							EndNode [0] = MyNodeMap [j, x];
							continue;
						}
					}
				}
				incrementing++;
			}
		} else {
			EndNode [0] = MyNodeMap [Mathf.RoundToInt (ypos), Mathf.RoundToInt (xpos)];
		}
		nodesastar.CreatePath ();

		return;
	}


	/*public List<GameObject> Getpositions(){
		return EnemyPositions;
	}*/

	public void AddGameobjectsWithinTrigger(GameObject NewObject){
		ObjectsWithinNodeMap.Add (NewObject.GetComponent<BoxCollider2D>());
	}

	public void AddEnemyPositions(GameObject pos){
		EnemyPositions.Add (pos.transform.GetChild (0).GetComponent<BoxCollider> ());
	}

	public void RemoveEnemyPositions(GameObject pos){
		EnemyPositions.Remove (pos.transform.GetChild (0).GetComponent<BoxCollider> ());
	}


	public int GetListCount(){
		return ObjectsWithinNodeMap.Count;
	}

	public void RemoveGameobjectsWithinTrigger(GameObject OldObject){
		ObjectsWithinNodeMap.Remove (OldObject.GetComponent<BoxCollider2D>());
	}
	BoxCollider a;
	BoxCollider2D b;

	public void UpdateNodeMap(){
		for (int i = 0; i < XDimention * 2 + 1; i++) {//chenges all node collisionid back to 0
			for (int j = 0; j < YDimention * 2 + 1; j++) {
				MyNodeMap [i, j].SetCollision (1);
			}
		}
	
		for (int k = 0; k < EnemyPositions.Count; k++) {
			a = EnemyPositions [k];

			upperRightCorner = a.bounds.max;
			lowerLeftCorner = a.bounds.min;

			_LeftPoint = (int)(lowerLeftCorner.x - (CenterPos [0, 0] - (XDimention + 0.5f)));
			if (_LeftPoint < 0)
				_LeftPoint = 0;

			_RightPoint = (XDimention * 2) - (int)((CenterPos [0, 0] + (XDimention + 0.5f)) - upperRightCorner.x);
			if (_RightPoint > XDimention * 2)
				_RightPoint = XDimention * 2;

			_HighestPoint = (int)((CenterPos [0, 1] + (YDimention + 0.5f)) - upperRightCorner.y);
			if (_HighestPoint < 0)
				_HighestPoint = 0;

			_LowestPoint = (YDimention * 2) - (int)(lowerLeftCorner.y - (CenterPos [0, 1] - (YDimention + 0.5f)));
			if (_LowestPoint > YDimention * 2)
				_LowestPoint = YDimention * 2;

			for (int i = _HighestPoint; i <= _LowestPoint; i++) {//changing the nodes inside the coordinates i found to collisionID
				for (int j = _LeftPoint; j <= _RightPoint; j++) {
					MyNodeMap [i, j].SetCollision (1);
				}
			}
		}
		for (int k = 0; k < ObjectsWithinNodeMap.Count; k++) {
			b = ObjectsWithinNodeMap [k];

			upperRightCorner = b.bounds.max;
			lowerLeftCorner = b.bounds.min;

			_LeftPoint = (int)(lowerLeftCorner.x - (CenterPos [0, 0] - (XDimention + 0.5f)));
			if (_LeftPoint < 0)
				_LeftPoint = 0;

			_RightPoint = (XDimention * 2) - (int)((CenterPos [0, 0] + (XDimention + 0.5f)) - upperRightCorner.x);
			if (_RightPoint > XDimention * 2)
				_RightPoint = XDimention * 2;

			_HighestPoint = (int)((CenterPos [0, 1] + (YDimention + 0.5f)) - upperRightCorner.y);
			if (_HighestPoint < 0)
				_HighestPoint = 0;

			_LowestPoint = (YDimention * 2) - (int)(lowerLeftCorner.y - (CenterPos [0, 1] - (YDimention + 0.5f)));
			if (_LowestPoint > YDimention * 2)
				_LowestPoint = YDimention * 2;

			for (int i = _HighestPoint; i <= _LowestPoint; i++) {//changing the nodes inside the coordinates i found to collisionID
				for (int j = _LeftPoint; j <= _RightPoint; j++) {
					MyNodeMap [i, j].SetCollision (100);
				}
			}
		}
	}

	public Nodes[,] GetNodemap() {
		return MyNodeMap;
	}
}
