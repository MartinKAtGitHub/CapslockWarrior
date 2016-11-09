using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class CaveEnemySpawnList : EnemySpawnListBlueprint {

	//This is the script that have the list of what to spawn in the cave level(currently the test level)

	List<List<KeyValuePair<string, int>>> EnemyWaveList = new List<List<KeyValuePair<string, int>>>();
	List<KeyValuePair<string, int>> WaveObject = new List<KeyValuePair<string, int>>();

	float EnemyStrength = 1.0f;

	public CaveEnemySpawnList(){
		EnemyWaveList = new List<List<KeyValuePair<string, int>>>();
		WaveObject = new List<KeyValuePair<string, int>>();//wave one adds 3 easy enemies to the list and 3 medium

		WaveObject.Add (new KeyValuePair<string, int>("easy",3));
		WaveObject.Add (new KeyValuePair<string, int>("medium",3));

		EnemyWaveList.Add (WaveObject);
		WaveObject = new List<KeyValuePair<string, int>>();//wave two adds 3 medium and 3 easy to the list

		WaveObject.Add (new KeyValuePair<string, int>("medium",3));
		WaveObject.Add (new KeyValuePair<string, int>("easy",3));

		EnemyWaveList.Add (WaveObject);
		WaveObject = new List<KeyValuePair<string, int>>();

		WaveObject.Add (new KeyValuePair<string, int>("medium",3));//wave three adds 3 medium and 3 easy to the list
		WaveObject.Add (new KeyValuePair<string, int>("easy",3));

		EnemyWaveList.Add (WaveObject);
	}

	public override List<List<KeyValuePair<string, int>>> GetEnemyWaveList(){
		return EnemyWaveList;
	}

	public override float GetEnemyStrength(){
		return EnemyStrength;
	}

}
