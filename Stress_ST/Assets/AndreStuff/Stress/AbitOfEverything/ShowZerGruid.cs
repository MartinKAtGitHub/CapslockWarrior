using UnityEngine;
using System.Collections;

public class ShowZerGruid : MonoBehaviour {

	public float individualSize;
	public int mapSize;
	public float LineSize = 0.01f;

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
				Gizmos.color = Color.green;
				for (float x = 0; x < mapSize; x += individualSize) {
					for (float y = 0; y < mapSize; y += individualSize) {
						Gizmos.DrawCube (new Vector3 (0, (y - (mapSize / 2)), 0), new Vector3 (mapSize, LineSize, individualSize));
						Gizmos.DrawCube (new Vector3 ((x - (mapSize / 2)), 0 , 0), new Vector3 (LineSize, mapSize, individualSize));
					}
				}
				Gizmos.DrawCube (new Vector3 (0 , (mapSize / 2) , 0), new Vector3 (mapSize, LineSize, individualSize));
				Gizmos.DrawCube (new Vector3 ((mapSize / 2), 0, 0), new Vector3 (LineSize, mapSize, individualSize));

				/*for (float x = 0; x < mapSize; x += individualSize) {//There are 4 center nodes,
					for (float y = 0; y < mapSize; y += individualSize) {
						Gizmos.DrawCube (new Vector3 (0 - (individualSize / 2), (y - (mapSize / 2))  - (individualSize / 2), 0), new Vector3 (mapSize, LineSize, individualSize));
						Gizmos.DrawCube (new Vector3 ((x - (mapSize / 2))  - (individualSize / 2), 0  - (individualSize / 2), 0), new Vector3 (LineSize, mapSize, individualSize));
					}
				}
				Gizmos.DrawCube (new Vector3 (0  - (individualSize / 2), (mapSize / 2)  - (individualSize / 2), 0), new Vector3 (mapSize, LineSize, individualSize));
				Gizmos.DrawCube (new Vector3 ((mapSize / 2)  - (individualSize / 2), 0  - (individualSize / 2), 0), new Vector3 (LineSize, mapSize, individualSize));*/
			}
		}
	}
}