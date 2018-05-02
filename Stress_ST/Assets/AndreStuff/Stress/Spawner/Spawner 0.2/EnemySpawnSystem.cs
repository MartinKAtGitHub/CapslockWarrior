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

	[CreateAssetMenu(menuName = "SpawnSystem/EnemyWave")]
	public class EnemyWave : ScriptableObject // This can be merged into the spawn main system array og Groupds insted of waves.
	{
		public Vector2 RandomDelay;
		public EnemyGroupSetup Wave; // public GameObject[] EnemyTypes;
		private List<GameObject> enemyWaveOrder; //TODO change SPAWN LIST it to struct / class we want to spawn the full object in the future not just Gameobject


		public void InitWave()
		{
			EnemyDataTransfer();
			ShuffleList(enemyWaveOrder);
		}

		public void EnemyDataTransfer()
		{
			Debug.Log("EnemyDataTransfer....");
			enemyWaveOrder = new List<GameObject>();

			for (int i = 0; i < Wave.EnemiesInWave.Length; i++) 
			{
				for (int j = 0; j < Wave.EnemiesInWave[i].Amount; j++) 
				{
					enemyWaveOrder.Add(Wave.EnemiesInWave[i].EnemyType);
					//Debug.Log(enemyWaveOrder[i].name);
				}
			}	
		}
		public void ShuffleList (List<GameObject> list)
		{
	 		int listLenght = list.Count;

	 		System.Random ran = new System.Random();

			for (int i = 0; i < listLenght; i++) 
			{
				Swap(list, i, i + ran.Next(listLenght - i)); /*Random.Range(0, (arrayLenght - i))*/
			}
		}
		private void Swap(List<GameObject> list, int a, int b)
		{
			GameObject temp = list[a];
			list[a] = list[b];
			list[b] = temp;
		}

		public IEnumerator SpawnCurrentWave(Transform spawnPosition /*, int amount*/)
		{
			for (int i = 0; i < Wave.EnemiesInWave.Length; i++) 
			{
				for (int j = 0; j < Wave.EnemiesInWave[i].Amount; j++) 
				{
					//Instantiate(EnemyGroup.EnemyData[i].EnemyType, spawnPosition.position, Quaternion.identity);
					Debug.Log("<color=green>" + Wave.EnemiesInWave[i].EnemyType.name + "</color>");// TODO SPAWNER the objects are not the same. i get diffrent message from first object spawn Wave1 then in wave 2

					yield return new WaitForSeconds(Random.Range(RandomDelay.x , RandomDelay.y));// Delay between amount
				}
				yield return new WaitForSeconds(Random.Range(RandomDelay.x , RandomDelay.y));// Delay whole group
				//yield return null;
			}	
		}

		public IEnumerator SpawnCurrentWaveRandomEnemyType(Transform spawnPosition /*, int amount*/)
		{
			for (int i = 0; i < enemyWaveOrder.Count; i++) 
			{
				Instantiate(enemyWaveOrder[i], spawnPosition.position, Quaternion.identity);
				yield return new WaitForSeconds(Random.Range(RandomDelay.x , RandomDelay.y));
			}
		}

		public void SpawnCurrentWaveINSTA(Transform spawnPosition /*, int amount*/)
		{
			for (int i = 0; i < Wave.EnemiesInWave.Length; i++) 
			{
				for (int j = 0; j < Wave.EnemiesInWave[i].Amount; j++) 
				{
					Debug.Log("<color=red>" + Wave.EnemiesInWave[i].EnemyType.name + "</color>"); 
				}
			}	
		}
	}

	[CreateAssetMenu(menuName = "SpawnSystem/EnemyGroup")]
	public class EnemyGroupSetup : ScriptableObject // THIS IS 1 WAVE
	{
		//public EnemyTypeSetup[] EnemyGrouping;
		public EnemySetupStruct[] EnemiesInWave;

		[System.Serializable]
		public struct EnemySetupStruct
		{
			public GameObject EnemyType;
			public int Amount;
		}
	}

	/*[CreateAssetMenu(menuName = "SpawnSystem/EnemyTypeSetup")]
	public class EnemyTypeSetup : ScriptableObject // The problem is a enemy group to me is, enemys(X,Y,Z) and an Amount(Z,Q,T) togther form a group. But what i have now is X and Z is 1 group
	{
		public GameObject EnemyType;
		public int Amount = 0;
	}
	*/


}


