using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawnmanaging : MonoBehaviour {

	public GameObject[] SpawnPoints;

	public bool StartSpawn = false;
	public bool NextWave = false;
	public bool SpawnAllAtOnce = false;

	public int SpawnRate;
	int WaveNumber = 0;	
	int whichWave = 0;
	int whichSmallWave = 0;
	int spawnspot = 0;

	public SpawnerWave[] TheWaves;
	MiniWaves[] TheSmallWaves;
	CreatureInfoSpawning[] ObjectsToSpawn;

	// Use this for initialization
	void Start () {
		WaveNumber = TheWaves.Length;

	}

	// Update is called once per frame
	void Update () {

		if (StartSpawn == true) {
		
			if (SpawnAllAtOnce == true || NextWave == true) {
				NextWave = false;
			
				StartCoroutine("SpawnWave");
				/*if (whichWave < WaveNumber) {
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
			}
		}
	}


	private IEnumerator SpawnWave()
	{

		if (whichWave < WaveNumber) {
			TheSmallWaves = TheWaves [whichWave].MiniWaves;//getting the current wave

			if (whichSmallWave < TheSmallWaves.Length) {
				ObjectsToSpawn = TheSmallWaves [whichSmallWave].Wave;//getting the current miniwave

				for (int k = 0; k < ObjectsToSpawn.Length; k++) {//spawning the miniwave
					for (int l = 0; l < ObjectsToSpawn [k].SpawnAmount; l++) {
						if (spawnspot >= SpawnPoints.Length) {
							spawnspot = 0;
						}

						Instantiate (ObjectsToSpawn [k].creature.gameObject, SpawnPoints [spawnspot].transform.position, Quaternion.identity, SpawnPoints [spawnspot++].transform);
						yield return new WaitForSeconds(SpawnRate);
					}
				}
				whichSmallWave++;
				if (whichSmallWave == TheSmallWaves.Length) {//if the miniwave amount is bigge then the length of the list, go to the next wave and reset the miniwavecounter
					whichSmallWave = 0;
					whichWave++;
				}
			} 


		}
		yield break;
	}
}


/*
 		for (int i = 0; i < WaveNumber; i++) {
					TheSmappWaves = TheWaves [i].MiniWaves;//henter wave 1.
					for (int j = 0; j < TheSmappWaves.Length; j++) {
						tes = TheSmappWaves [j].Wave;//finner miniwave 1.
					


						for (int k = 0; k < tes.Length; k++) {
							for (int l = 0; l < tes [k].SpawnAmount; l++) {
								if (spawnspot >= SpawnPoints.Length) {
									spawnspot = 0;
								}

								Instantiate (tes [k].creature.gameObject, SpawnPoints [spawnspot].transform.position, Quaternion.identity, SpawnPoints [spawnspot++].transform);
							}
						}




					}
				}
*/