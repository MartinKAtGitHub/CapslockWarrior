using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_PlayerSpawner : MonoBehaviour 
{

	public void SpawnPlayer(Vector3 spawnPosition) // cehck up on events again 
	{
		// instantiate player
		// set the Ability Data to the spawned player
		// signel the level the player has spawed 
		GameManager.Instance.PlayerObject = Instantiate(GameManager.Instance.PlayerPrefab,
																spawnPosition, Quaternion.identity);
	} 
}
