using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MakeNodeMap : MonoBehaviour {

	Nodes[,] MyNodeMap = new Nodes[105,105];
	int _HighestPoint = 0;
	int _LeftPoint = 0;
	int _RightPoint = 0;
	int _LowestPoint = 0;

	Vector3 Center;
	Vector2 Dimension;
	List<ImAtNode> ObjectToRefrech = new List<ImAtNode>();

	void Awake(){

		for (int x = 0; x < 105; x++) {
			for (int y = 0; y < 105; y++) {
				MyNodeMap [y, x] = new Nodes (new float[,]{ { x - (transform.position.x + 52.5f ) + 0.5f, y - (52.5f - transform.position.y ) + 0.5f } }, 1);
			}
		}

		for (int i = transform.childCount - 1; i >= 0; i--) {
			GameObject child = transform.GetChild (i).gameObject;

			Dimension = child.GetComponent<RectTransform> ().sizeDelta / 2;
			Center = child.transform.position;

			_LeftPoint = (int)((Center.x - Dimension.x) - (transform.position.x - 52.5f));
			if (_LeftPoint < 0)
				_LeftPoint = 0;


			_RightPoint = (int)((transform.position.x + 52.5f) + (Center.x + Dimension.x));
			if (_RightPoint >= 105)
				_RightPoint = 105 - 1;

			_HighestPoint = (int)((transform.position.y + 52.5f) + (Center.y + Dimension.y));
			if (_HighestPoint >= 105)
				_HighestPoint = 105 - 1;

			_LowestPoint = (int)((Center.y - Dimension.y) - (transform.position.y - 52.5f));
			if (_LowestPoint < 0)
				_LowestPoint = 0;

			for (int h = _LowestPoint; h <= _HighestPoint; h++) {//changing the nodes inside the coordinates i found to collisionID
				for (int j = _LeftPoint; j <= _RightPoint; j++) {
					MyNodeMap [h, j].SetCollision (100);
				}
			}
			Destroy (child);
		}

		for (int i = 0; i < 105; i++) {
			for (int j = 0; j < 105; j++) {
				for (int k = -1; k < 2; k++) {
					for (int h = -1; h < 2; h++) {
						if ((i + k >= 0 && j + h >= 0) && ((i + k < 105) && j + h < 105) && !(k == 0 && h == 0)) {
							if(MyNodeMap [i + k, j + h].GetCollision() != 100)
								MyNodeMap [i,j].SetNeighbors (MyNodeMap [i + k, j + h]);
						}
					}
				}
			}
		}

		for (int i = 0; i < 105; i++) {
			for (int j = 0; j < 105; j++) {
				for (int k = -1; k < 2; k++) {
					for (int h = -1; h < 2; h++) {
						if ((i + k >= 0 && j + h >= 0) && ((i + k < 105) && j + h < 105) && !(k == 0 && h == 0)) {
							if(MyNodeMap [i + k, j + h].GetCollision() != 100)
								MyNodeMap [i, j].NeighbourNodes [1 + k,1 + h] = MyNodeMap [i + k, j + h];
						}
					}
				}
			}
		}

	//	NeighbourNodes
	}

	public void AddToListToRefresh(ImAtNode theObject){
		ObjectToRefrech.Add (theObject);
	}

	public void RemoveToListToRefresh(ImAtNode theObject){
		ObjectToRefrech.Remove (theObject);
	}



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < ObjectToRefrech.Count; i++) {
			ImAtNode theObject = ObjectToRefrech [i];

			int yPos = Mathf.FloorToInt(theObject.gameObject.transform.position.x - transform.position.x + 52.5f);
			int xPos = Mathf.FloorToInt(theObject.gameObject.transform.position.y - transform.position.y + 52.5f);

			theObject.SetNode (MyNodeMap[xPos,yPos]);
		}
	}












/*	public bool tru = false;

	void OnDrawGizmos(){

		if (tru && MyNodeMap != null && MyNodeMap [0,0] != null) {
			for (int x = 0; x < 105; x++) {
				for (int y = 0; y < 105; y++) {
					if (MyNodeMap [y, x].GetCollision () == 1) {
						Gizmos.color = Color.green;
					} else if (MyNodeMap [y, x].GetCollision () == 100) {
						Gizmos.color = Color.red;
					}
					Gizmos.DrawCube (new Vector3 ((MyNodeMap [y, x].GetID () [0, 0]), (MyNodeMap [y, x].GetID () [0, 1]), 0), new Vector3 (0.5f, 0.5f, 0.5f));
				}
			}
		}
		Gizmos.color = Color.black;
		Gizmos.DrawCube (new Vector3 ((MyNodeMap [103,103].GetID () [0, 0]), (MyNodeMap [103,103].GetID () [0, 1]), 0), new Vector3 (0.5f, 0.5f, 0.5f));	

		Gizmos.color = Color.blue;
		Gizmos.DrawCube (new Vector3 ((MyNodeMap [0,0].GetID () [0, 0]), (MyNodeMap [0,0].GetID () [0, 1]), 0), new Vector3 (0.5f, 0.5f, 0.5f));

	}*/
}
