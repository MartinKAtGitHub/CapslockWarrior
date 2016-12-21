using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemyAttacher : MonoBehaviour {

	[SerializeField]
	List<MonoBehaviour> _EasyEnemies = new List<MonoBehaviour>();
	[SerializeField]
	List<MonoBehaviour> _MediumEnemies = new List<MonoBehaviour>();
	[SerializeField]
	List<MonoBehaviour> _HardEnemies = new List<MonoBehaviour>();

	[SerializeField]
	List<MonoBehaviour> _MiniBossEnemies = new List<MonoBehaviour>();
	[SerializeField]
	List<MonoBehaviour> _BossEnemies = new List<MonoBehaviour>();

	[SerializeField]
	MonoBehaviour EnemyWaveList;


	public MonoBehaviour GetEasyEnemies(){
		return _EasyEnemies[Random.Range (0,_EasyEnemies.Count)];
	}

	public MonoBehaviour GetMediumEnemies(){
		return _MediumEnemies[Random.Range (0,_MediumEnemies.Count)];
	}

	public MonoBehaviour GetHardEnemies(){
		return _HardEnemies[Random.Range (0,_HardEnemies.Count)];
	}

	public MonoBehaviour GetMiniBossEnemies(){
		return _MiniBossEnemies[Random.Range (0,_MiniBossEnemies.Count)];
	}

	public MonoBehaviour GetBossEnemies(){
		return _BossEnemies[Random.Range (0,_BossEnemies.Count)];
	}

	public List<KeyValuePair<string, int>> GetEnemyWave(int waveNumber){
		if((EnemyWaveList as EnemySpawnListBlueprint).GetEnemyWaveList ().Count > waveNumber)
			return ((EnemyWaveList as EnemySpawnListBlueprint).GetEnemyWaveList () [waveNumber]);
		return null;
	}
}
