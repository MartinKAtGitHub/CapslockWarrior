using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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