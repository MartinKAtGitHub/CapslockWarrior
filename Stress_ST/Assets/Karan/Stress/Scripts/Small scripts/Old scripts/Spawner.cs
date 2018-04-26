using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

// -------------- This enum is just temp so we can get som feel for the spawn rate and stuff ------------
	public enum Difficulty
	{
		EasyMode,
		NormalMode,
		HardMode
	};
	public Difficulty DifficultyMode; 

	public float EasyModeMultiplier = 0.5f;
	public float NormalModeMultiplier = 1f;
	public float HardModeMultiplier = 1.5f;

	[Space (10)]

// --------------------------------------------- END ---------------------------------------------------

//	private IEnumerator Test;

	public GameObject[] SpawnPatterns;

	[Tooltip ("All the enemies you want the spawner to spawn")]
	public GameObject[] EnemyTypes;

	[Header ("Just a test to creat a header take a look")]
	[Tooltip ("Wave timer in Seconds")]
	public float WaveTimer;
	public int  AmountOfWaves;


	private float WaveTimerCountDown; 

	private List<Transform> spawnPositions;

	// Use this for initialization
	void Start () 
	{
		//Test = SpawnEnemyMiniWaves();

		spawnPositions = new List<Transform>();

		for (int i = 0; i < SpawnPatterns.Length; i++) 
		{
			if (SpawnPatterns[i] == null)
			{
				Debug.LogError("SpawnPattern[" + i + "] is NULL" );
				// Do somthing about it here if this becomes an issue. 
			}	
		}

		// can use -> int RoundUp = (int)Math.ceiling(precise); || int Rounded = (int) math.round(preicis, 0); -- this will get you to the neares number to use use in yout for loop
		AmountOfWaves = (int) Mathf.CeilToInt( AmountOfWaves * CalulateDifficultyMultiplier());
		Debug.Log("Amount Of Waves = " + AmountOfWaves);

		WaveTimerCountDown = WaveTimer;

		//DedicateSpawnPatternAndPosition();

	}
	
	// Update is called once per frame
	void Update () 
	{
		WaveTimerCountDown -= Time.deltaTime;

		if(WaveTimerCountDown <= 0 && AmountOfWaves != 0)
		{
			Debug.Log("Spawn wave on the spawn pos");

			//SpawnEnemy();

			StartCoroutine("SpawnEnemyMiniWaves");
			WaveTimerCountDown = WaveTimer;
			 AmountOfWaves -= 1;
		}
	}


	private float CalulateDifficultyMultiplier()
	{
		switch (DifficultyMode) {
		case Difficulty.EasyMode:
			Debug.Log("EasyMode =" + EasyModeMultiplier);
			return EasyModeMultiplier;
			//break;

		case Difficulty.NormalMode:
			Debug.Log("NormalMode =" + NormalModeMultiplier);
			return NormalModeMultiplier;
			//break;

		case Difficulty.HardMode:
			Debug.Log("HardMode =" + HardModeMultiplier);
			return HardModeMultiplier;
			//break;

		default:
			return 1;
			//break;
		}
	}


	private IEnumerator SpawnEnemyMiniWaves()
	{
		DedicateSpawnPatternAndPosition();
		for (int i = 0; i < EnemyTypes.Length; i++)
		{
			for (int j = 0; j < EnemyTypes[i].GetComponent<Enemy>().SpawnAmount; j++) 
			{
				Instantiate(EnemyTypes[i], spawnPositions[Random.Range(0, spawnPositions.Count)].position, Quaternion.identity);
				Debug.LogWarning(EnemyTypes[i].name + " ----->");
				yield return new WaitForSeconds(1.0f);
			}	
		}
		spawnPositions.Clear();
		/*for(int i = 0; i < 5; i++)
		{	
			Debug.LogWarning("NOOOOOOOOOO" + i+i);
			yield return new WaitForSeconds(1f);
		}*/

		yield break;
	}

	private void SpawnEnemy()
	{	
		DedicateSpawnPatternAndPosition();

		for (int i = 0; i < EnemyTypes.Length; i++)
		{
			for (int j = 0; j < EnemyTypes[i].GetComponent<Enemy>().SpawnAmount; j++) 
			{
				Instantiate(EnemyTypes[i], spawnPositions[Random.Range(0, spawnPositions.Count)].position, Quaternion.identity);
			}	
		}

		spawnPositions.Clear();
	}


	private void DedicateSpawnPatternAndPosition()
	{
		GameObject pattern = SpawnPatterns[Random.Range(0,SpawnPatterns.Length)];

		foreach (Transform child in pattern.transform) 
		{
			//Debug.Log(child.gameObject.name);
			spawnPositions.Add(child);	
		}
	}
}
