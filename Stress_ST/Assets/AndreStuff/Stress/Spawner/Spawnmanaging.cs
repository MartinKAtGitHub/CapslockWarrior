using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawnmanaging : MonoBehaviour {

	public bool StartSpawn = false;

	public GameObject[] SpawnPoints;
	GameObject _SpawnedObject;


	public int SpawnRate;
	int _WaveNumber = 0;	
	int _SpawnSpot = 0;

	public SpawnerWave[] TheWaves;
	MiniWaves[] TheSmallWaves;
	CreatureInfoSpawning[] ObjectsToSpawn;

	// Use this for initialization
	void Start () {
		_WaveNumber = TheWaves.Length;
	}

	// Update is called once per frame
	void Update () {

		if (StartSpawn == true) {
			StartSpawn = false;
			
			StartCoroutine ("SpawnWave");
			
		}
	}

	private IEnumerator SpawnWave()
	{


		for (int i = 0; i < _WaveNumber; i++) {
			TheSmallWaves = TheWaves [i].MiniWaves;//henter wave 1.
			for (int j = 0; j < TheSmallWaves.Length; j++) {
				ObjectsToSpawn = TheSmallWaves [j].Wave;//finner miniwave 1.
				for (int k = 0; k < ObjectsToSpawn.Length; k++) {
					for (int l = 0; l < ObjectsToSpawn [k].SpawnAmount; l++) {
						if (_SpawnSpot >= SpawnPoints.Length) {
							_SpawnSpot = 0;
						}
						_SpawnedObject = Instantiate (ObjectsToSpawn [k].creature.gameObject, SpawnPoints [_SpawnSpot].transform.position, Quaternion.identity, SpawnPoints [_SpawnSpot].transform) as GameObject;
					//	_SpawnedObject.GetComponent<DefaultBehaviour> ().setword(ListOfWords.GetRandomWords (ObjectsToSpawn [k].creature.gameObject.GetComponent<DefaultBehaviour>().Health[0]));
						_SpawnedObject.GetComponent<DefaultBehaviour> ().SetAiRoom (SpawnPoints [_SpawnSpot++].GetComponent<SpawningPointLocation> ().PointPlacement);
					}
				}
				yield return new WaitForSeconds(SpawnRate);
			}
		}


		yield break;
	}
}
//Spawn miniwave after miniwave
//int whichWave = 0;
//int whichSmallWave = 0;

/*	if (whichWave < WaveNumber) {
			TheSmallWaves = TheWaves [whichWave].MiniWaves;//getting the current wave

			if (whichSmallWave < TheSmallWaves.Length) {
				ObjectsToSpawn = TheSmallWaves [whichSmallWave].Wave;//getting the current miniwave

				for (int k = 0; k < ObjectsToSpawn.Length; k++) {//spawning the miniwave
					for (int l = 0; l < ObjectsToSpawn [k].SpawnAmount; l++) {
						if (spawnspot >= SpawnPoints.Length) {
							spawnspot = 0;
						}

						Instantiate (ObjectsToSpawn [k].creature.gameObject, SpawnPoints [spawnspot].transform.position, Quaternion.identity, SpawnPoints [spawnspot++].transform);
					}
				}
				whichSmallWave++;
				if (whichSmallWave == TheSmallWaves.Length) {//if the miniwave amount is bigge then the length of the list, go to the next wave and reset the miniwavecounter
					whichSmallWave = 0;
					whichWave++;
				}
			} 


		}*/