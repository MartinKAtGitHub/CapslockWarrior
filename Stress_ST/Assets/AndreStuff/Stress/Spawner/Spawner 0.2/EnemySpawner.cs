using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {



	public float TimeToNextWave = 10f;
	public Transform [] SpawnLocation;
	//public EnemyWave ew;
	//public EnemyGroupSetup eg;
	public EnemyWave [] EnemyWaves;


	void Start()
	{
		StartCoroutine(StartSpawner());
	}


	public IEnumerator StartSpawner()
	{
		for (int i = 0; i < EnemyWaves.Length; i++) 
		{
			Debug.Log("Spawning Wave " + i );
			//StartCoroutine(EnemyWaves[i].SpawnCurrentWave(SpawnLocation[0]));
			EnemyWaves[i].SpawnCurrentWaveINSTA(SpawnLocation[0]);
			yield return new WaitForSeconds(TimeToNextWave);
		}
	}

	[CreateAssetMenu(menuName = "SpawnSystem/EnemyWave")]
	public class EnemyWave : ScriptableObject
	{
		public Vector2 RandomSmallDelaybetweenSpawnsMinMax;
		public EnemyGroupSetup Enemies; // public GameObject[] EnemyTypes;
		
		public IEnumerator SpawnCurrentWave(Transform spawnPosition /*, int amount*/)
		{
			for (int i = 0; i < Enemies.EnemyGrouping.Length; i++) 
			{
				for (int j = 0; j < Enemies.EnemyGrouping[i].Amount; j++) 
				{
					Debug.Log("<color=red>" + Enemies.EnemyGrouping[j].EnemyType.name + "</color>");
				}
				//yield return new WaitForSeconds(Random.Range(RandomSmallDelaybetweenSpawnsMinMax.x , RandomSmallDelaybetweenSpawnsMinMax.y));
				yield return null;
			}	
		}

		public void SpawnCurrentWaveINSTA(Transform spawnPosition /*, int amount*/)
		{
			for (int i = 0; i < Enemies.EnemyGrouping.Length; i++) 
			{
				for (int j = 0; j < Enemies.EnemyGrouping[i].Amount; j++) 
				{
					Debug.Log("<color=red>" + Enemies.EnemyGrouping[i].EnemyType.name + "</color>");
				}

				//yield return new WaitForSeconds(Random.Range(RandomSmallDelaybetweenSpawnsMinMax.x , RandomSmallDelaybetweenSpawnsMinMax.y));
			}	
		}
	}

	[CreateAssetMenu(menuName = "SpawnSystem/EnemyGroup")]
	public class EnemyGroupSetup : ScriptableObject // THIS IS 1 WAVE
	{
		public EnemyTypeSetup[] EnemyGrouping;
	}

	[CreateAssetMenu(menuName = "SpawnSystem/EnemyTypeSetup")]
	public class EnemyTypeSetup : ScriptableObject // The problem is a enemy group to me is, enemys(X,Y,Z) and an Amount(Z,Q,T) togther form a group. But what i have now is X and Z is 1 group
	{
		public GameObject EnemyType;
		public int Amount = 0;
	}


	// Spawn --> wave --> Group --> list (2Trolls, 3Dogs, 1Knight) ---= spawns in that order with small rand time bewtine

	// Spawn --> wave --> Group --> list ( 1Combo(Troll, Dog), 2combo(Knight, Dog, Dog)) ---= spawns in that order with small rand time bewtine <- we want this kinda

}


