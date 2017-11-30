using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSpawner : MonoBehaviour {

	public Transform SpawnPosition;
	public List<GameObject> CreaturesToSpawn;
	public int SpawnCreatureIndex = 0;
	public bool SpawnNow = false;
	
	// Update is called once per frame
	void Update () {
		if (SpawnNow == true) {
			SpawnNow = false;
			Instantiate (CreaturesToSpawn [SpawnCreatureIndex], SpawnPosition.position, Quaternion.identity);
		}
	}
}
