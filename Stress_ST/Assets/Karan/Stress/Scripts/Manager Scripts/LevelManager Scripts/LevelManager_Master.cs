using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelManager_Master : MonoBehaviour {

	public Transform PlayerSpawnPosition;

	[SerializeField]private GameObject player;
	[SerializeField]private Spawnmanaging Spawner; // TODO Create new spawner

	[SerializeField]private GameObject introBackground;
	[SerializeField]private Text introLevelText;
	[SerializeField]private Text introFluffText;

	[SerializeField]private ScriptedEvent LevelScriptedEvent;

	void Awake() // Called even if the GM is disabeld
	{
		
	}

	void Start () // calling logic at start of game, only activ if activ
	{
		initializeLevel(); // Set up refs
		//GameManager_Master.instance.GetComponent<GameManager_PlayerSpawner>().SpawnPlayer(PlayerSpawnPosition.position);
		StartLevel();

	}

	public void StartSpawner()
	{
		Debug.Log("Spawner Enabled");
		Spawner.StartSpawn = true;
		LevelScriptedEvent.OnScriptedEventEndEvent -= StartSpawner;
	}




	#region LevelManager_ScriptedEventsManager
		// TODO make a new script to handle all the ScriptedEvents
	public void StartLevel()
	{
		// StartCutscene
	
		LevelScriptedEvent.StartScriptedEvent(); // TODO make sure Cutscene works on all Resolution, spawnpoint out of can view
		LevelScriptedEvent.OnScriptedEventEndEvent += StartSpawner;
		// SpawnPlayer
	}

	private void initializeLevel()
	{
		FindObjectsWithTag();

		GameManager_Master.instance.GetComponent<GameManager_PlayerSpawner>().SpawnPlayer(PlayerSpawnPosition.position);
		if(LevelScriptedEvent == null)
		{
			LevelScriptedEvent = GetComponent<ScriptedEvent>();
		}

		LevelScriptedEvent.SetInitalRefs();

		Debug.Log("initialize Level...");
	}
	#endregion


	private void FindObjectsWithTag()
	{
		// TODO Add stuff you need to find with Tag / findtypeoff.
		// Spawner
		// UI ?
		// 
		Debug.Log("Trying to find stuff with TAG");
	}
}