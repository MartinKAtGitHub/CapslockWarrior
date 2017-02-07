using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawnmanaging : MonoBehaviour {

	public GameObject[] SpawnPoints;

	public bool StartSpawn = false;
	public bool NextWave = false;
	public bool SpawnAllAtOnce = false;

	int WaveNumber = 0;
	int wavecounter = 0;
	int wavesize = 0;
	int spawnspot = 0;

	CreatureInfoSpawning[] tes;
	public SpawnerWave[] TheWaves;

	// Use this for initialization
	void Start () {
		WaveNumber = TheWaves.Length;
	}
	
	// Update is called once per frame
	void Update () {

		if (StartSpawn == true) {
		
			if (SpawnAllAtOnce == true || NextWave == true) {
				NextWave = false;
				if (wavecounter < WaveNumber) {
					spawnspot = 0;
					tes = TheWaves [wavecounter].Wave;
					wavesize = TheWaves [wavecounter].Wave.Length;

					for (int j = 0; j < wavesize; j++) {

						for (int k = 0; k < TheWaves [wavecounter].Wave [j].SpawnAmount; k++) {


							if (spawnspot >= SpawnPoints.Length) {
								spawnspot = 0;
							}

							Instantiate (tes [j].creature.gameObject, SpawnPoints [spawnspot].transform.position, Quaternion.identity, SpawnPoints [spawnspot++].transform);
						}
					}
		
					wavecounter++;

				}
			}
		}
	}
}
