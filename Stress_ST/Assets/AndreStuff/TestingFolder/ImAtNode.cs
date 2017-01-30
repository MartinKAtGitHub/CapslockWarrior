using UnityEngine;
using System.Collections;

public class ImAtNode : MonoBehaviour {

/*	Nodes[] NodeImAt = new Nodes[1];

	public bool GoToTarget = false;
	AStarPathFinding_Nodes StarTest = new AStarPathFinding_Nodes (11025);
	ImAtNode Target;

	Nodes[] thePath;
	int[] thePathIndex;

	public void SetNode(Nodes node){
		NodeImAt[0] = node;
	}

	public Nodes[] GetNode(){
		return NodeImAt;
	}

	void Start(){
		GameObject.Find ("NewNodeMap").GetComponent<MakeNodeMap> ().AddToListToRefresh (this);
		Target = GameObject.Find ("New Sprite (1)").GetComponent<ImAtNode> ();
		thePath = StarTest.GetListRef ();
		thePathIndex = StarTest.GetListindexref ();
		StarTest.SetEndStartNode (NodeImAt, Target.NodeImAt);
	}

	bool once = false;
	Vector2 movemen = Vector2.zero;
	int _Nodeindex = 0;
	float MovementSpeed = 10.5f;
	float DistanceFromNode = 0.5f;

	void Update(){
		//	if (GoToTarget == true && NodeImAt != null && Target.NodeImAt !=null  && NodeImAt[0] != null && Target.NodeImAt[0] != null){ //&& once == false) {
		if (once == false)
			once = true;
		else
			StarTest.CreatePath ();
	//		_Nodeindex = thePathIndex [0];

			
		//	once = true;
	//	}

		/*if (GoToTarget == true && once == true) {


			if (((transform.position.x - Target.NodeImAt[0].GetID() [0, 0] <= 1) && (transform.position.x - Target.NodeImAt[0].GetID() [0, 0] >= -1)) &&
				((transform.position.y - Target.NodeImAt[0].GetID() [0, 1] <= 1) && (transform.position.y - Target.NodeImAt[0].GetID() [0, 1] >= -1))) {
				once = true;
			}*/
	

	/*		if (thePathIndex [0] < _Nodeindex) {
				movemen.x = _TheNodePath [_Nodeindex].GetID () [0, 0] - _TheNodePath [_Nodeindex - 1].GetID () [0, 0];
				movemen.y = _TheNodePath [_Nodeindex].GetID () [0, 1] - _TheNodePath [_Nodeindex - 1].GetID () [0, 1];
			} else {
				movemen.x = _TheNodePath [_Nodeindex].GetID () [0, 0];
				movemen.y = _TheNodePath [_Nodeindex].GetID () [0, 1];
			}

			//movement with velocity and position =
			myrigid.velocity = (movemen * MovementSpeed);*/
		/*	transform.position = Vector3.MoveTowards (transform.position, new Vector3 ((thePath [_Nodeindex].GetID () [0, 0]), (thePath [_Nodeindex].GetID () [0, 1]), 0), Time.smoothDeltaTime * MovementSpeed);

			if ((((thePath [_Nodeindex].GetID () [0, 0]) - DistanceFromNode) < transform.position.x) && (((thePath [_Nodeindex].GetID () [0, 0]) + DistanceFromNode) > transform.position.x) && (((thePath [_Nodeindex].GetID () [0, 1]) - DistanceFromNode) < transform.position.y) && (((thePath [_Nodeindex].GetID () [0, 1]) + DistanceFromNode) > transform.position.y)) {
				_Nodeindex += 1;
				if (_Nodeindex >= thePath.Length) {//every time nodeindex == 1. so if im at the first node search again
					GoToTarget = false;
					return;
				}
			}
		}*/
/*	}

	public bool tru = false;

	void OnDrawGizmos(){

		if (tru == true) {
			Nodes[] tes = StarTest.openlist ();
			for (int i = 0; i < tes.Length; i++) {
				if (tes [i] == null) {
					i = tes.Length;
				} else {
					Gizmos.color = Color.blue;
					Gizmos.DrawCube (new Vector3 ((tes [i].GetID () [0, 0]), (tes [i].GetID () [0, 1]), 0), new Vector3 (0.5f, 0.5f, 0.5f));	
				}
			}

			tes = StarTest.closedlist ();
			for (int i = 0; i < tes.Length; i++) {
				if (tes [i] == null) {
					i = tes.Length;
				} else {
					Gizmos.color = Color.black;
					Gizmos.DrawCube (new Vector3 ((tes [i].GetID () [0, 0]), (tes [i].GetID () [0, 1]), 0), new Vector3 (0.5f, 0.5f, 0.5f));	
				}
			}

			tes = StarTest.thepath ();
		for (int i = tes.Length - 1; i >= 0; i--) {
				if (tes [i] == null) {
					i = 0;
				} else {
					Gizmos.color = Color.red;
					Gizmos.DrawCube (new Vector3 ((tes [i].GetID () [0, 0]), (tes [i].GetID () [0, 1]), 0), new Vector3 (0.5f, 0.5f, 0.5f));	
				}
			}
		}
	}*/
}
