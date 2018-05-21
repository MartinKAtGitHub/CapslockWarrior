using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


