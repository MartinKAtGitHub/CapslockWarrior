using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelManager_Master : MonoBehaviour {

	[SerializeField]private GameObject player;
	[SerializeField]private GameObject Spawner;

	[SerializeField]private GameObject introBackground;
	[SerializeField]private Text introLevelText;
	[SerializeField]private Text introFluffText;

	[SerializeField]private ScriptedEvent LevelScriptedEvent;


	void Awake()
	{
		initializeLevel();
	}

	void Start () 
	{
		//StartCutScene();
	}

	void Update () 
	{
		
	}

	public void StartLevel()
	{
		// StartCutscene
		// SpawnPlayer
	}

	public void StartCutScene()
	{
		StartCoroutine(LevelScriptedEvent.ScriptedEventScene());
	}

	private void initializeLevel()
	{
		GameManager_Master.instance.GetComponent<GameManager_PlayerSpawner>().SpawnPlayer();
		LevelScriptedEvent.SetInitalRefs();
		if(LevelScriptedEvent == null)
		{
			LevelScriptedEvent = GetComponent<ScriptedEvent>();
		}
		Debug.Log("StartingLevel");
	}
}
