using UnityEngine;
using System.Collections;

public class ShowZerGruid : MonoBehaviour {

	public float individualSize;
	public int mapSize;

	public bool ShowGizmos = false;
	float size;

	void Start(){
		size = individualSize;
	}

	void OnDrawGizmos(){

		if (ShowGizmos == true) {
		
			if (size != individualSize) {
				ShowGizmos = false;
				size = individualSize;
			} else {
			/*	for (float x = 0; x < mapSize; x+= individualSize) {
					for (float y = 0; y <  mapSize; y+= individualSize) {
						Gizmos.color = Color.white;
						Gizmos.DrawCube (new Vector3 ((x - (mapSize / 2)) + (individualSize / 2),(y - (mapSize / 2)) + (individualSize / 2),0 ), new Vector3 (individualSize - 0.01f, individualSize - 0.01f, individualSize));
					}
				}*/

				for (float x = 0; x < mapSize; x+= individualSize) {
					for (float y = 0; y <  mapSize; y+= individualSize) {
						Gizmos.color = Color.green;
						Gizmos.DrawCube (new Vector3 (0 ,(y - (mapSize / 2)),0 ), new Vector3 (mapSize, 0.01f, individualSize));
						Gizmos.DrawCube (new Vector3 ((x - (mapSize / 2)) ,0 ,0 ), new Vector3 (0.01f, mapSize, individualSize));
					}
				}
				Gizmos.DrawCube (new Vector3 (0 ,(mapSize / 2),0 ), new Vector3 (mapSize, 0.01f, individualSize));
				Gizmos.DrawCube (new Vector3 ((mapSize / 2) ,0 ,0 ), new Vector3 (0.01f, mapSize, individualSize));


			}
		}

		/*	if (ShowGizmos) {
			size = 1 / (float)NodeSizess / 2;
			Nodes[,] mynodes = _PersonalNodeMap.GetNodemap ();



			for (int x = 0; x < Mathf.FloorToInt((transform.FindChild ("WalkingCollider").GetComponent<BoxCollider2D> ().size.x / 2) / (1 / (float)NodeSizess) * 2) + 1; x++) {
				for (int y = 0; y <  Mathf.FloorToInt((transform.FindChild ("WalkingCollider").GetComponent<BoxCollider2D> ().size.x / 2) / (1 / (float)NodeSizess) * 2) + 1; y++) {
					if (mynodes [x, y].GetCollision () == PathfindingNodeID[0]) {
						Gizmos.color = Color.black;
					} else if (mynodes [x, y].GetCollision () == PathfindingNodeID[1]) {
						Gizmos.color = Color.blue;
					} else {
						Gizmos.color = Color.yellow;

					}
					Gizmos.DrawCube (new Vector3 ((mynodes [x, y].GetID () [0, 0]) + WalkColliderPoint.position.x, (mynodes [x, y].GetID () [0, 1]) + WalkColliderPoint.position.y, 0), new Vector3 (size, size, size));
				}
			}
			Gizmos.color = Color.white;
			Gizmos.DrawCube (new Vector3 ((mynodes [0, 0].GetID () [0, 0])  + WalkColliderPoint.position.x, (mynodes [0, 0].GetID () [0, 1]) + WalkColliderPoint.position.y, 0), new Vector3 (size, size, size));
			Gizmos.color = Color.yellow;
			Gizmos.DrawCube (new Vector3 ((mynodes [0, 1].GetID () [0, 0]) + WalkColliderPoint.position.x, (mynodes [0, 1].GetID () [0, 1])+ WalkColliderPoint.position.y, 0), new Vector3 (size, size, size));




			Nodes[] mynodess = _PersonalNodeMap.GetNodeList();
			int[] count = _PersonalNodeMap.GetNodeindex ();
			for(int sas = count[0]; sas < mynodess.Length; sas++){
				Gizmos.color = Color.red;
				Gizmos.DrawCube (new Vector3 (mynodess[sas].GetID()[0,0] + WalkColliderPoint.position.x, mynodess[sas].GetID()[0,1] + WalkColliderPoint.position.y, 0), new Vector3 (size, size, size));
			}

			mynodes = _PersonalNodeMap.GetNodemap ();
		}
	}*/
	}
}

