using UnityEngine;
using System.Collections;
using System.Collections.Generic;

abstract class EnemySpawnListBlueprint : MonoBehaviour {

	//this is a blueprint that is used to easily change for each level/stage/map/zone/battlefield/arena

	public abstract List<List<KeyValuePair<string, int>>> GetEnemyWaveList ();
	public abstract float GetEnemyStrength ();
}
