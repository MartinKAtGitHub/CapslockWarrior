using UnityEngine;
using System.Collections;


[System.Serializable]
public class CreatureInfoSpawning{
	
	//	public DefaultBehaviour creature;
	public GameObject creature;
	public float HealthIncrease = 1;
	public float DamageIncrease = 1;
	public float SpeedIncrease = 1;
	public int SpawnAmount = 0;
}
