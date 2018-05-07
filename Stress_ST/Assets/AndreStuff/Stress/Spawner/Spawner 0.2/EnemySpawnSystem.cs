using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour {

	public float TimeToNextWave = 10f;
	public Transform [] SpawnLocation;
	//public EnemyWave ew;
	//public EnemyGroupSetup eg;
	public EnemyWave [] EnemyWaves;

	void Start()
	{
		init();
		//StartCoroutine(StartSpawner());
	}

	private void init()
	{
		Debug.Log("INIT Spawner.........");
		for (int i = 0; i < EnemyWaves.Length; i++) 
		{
			EnemyWaves[i].InitWave();
		}

		LevelManager_Master.instance.EnableSpawnSystem.AddListener(StartSpawner);
	}


	public void StartSpawner()
	{
		StartCoroutine(SpawnWaves());
	}

	public IEnumerator SpawnWaves()
	{
		for (int i = 0; i < EnemyWaves.Length; i++) 
		{
			Debug.Log("Spawning Wave " + (i + 1) );
			//StartCoroutine(EnemyWaves[i].SpawnCurrentWave(SpawnLocation[0]));
			//EnemyWaves[i].SpawnCurrentWaveINSTA(SpawnLocation[0]);
			StartCoroutine(EnemyWaves[i].SpawnCurrentWaveRandomEnemyType(SpawnLocation[Random.Range(0, SpawnLocation.Length)]));// TODO SpawnLocation[Random.Range(0, SpawnLocation.Length) maybe change to make more randome

			yield return new WaitForSeconds(TimeToNextWave);
		}
	}

	//TODO -------------- Scripteable objects, we might need to splitt the classes into own files. The Objects are beeing corrupted and not accsesable and this neste classes might be the problem. Also names need to be the same for file and object maybe


}


