using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class LevelManager_Master : MonoBehaviour {

	public Transform PlayerSpawnPosition;


	//[SerializeField]private GameObject player;
	//[SerializeField]private EnemySpawnSystem Spawner;
	//[SerializeField]private GameObject introBackground;
	//[SerializeField]private GameObject introPnl;
	//[SerializeField]private Text introLevelText;
	//[SerializeField]private Text introFluffText;
	//[SerializeField]private ScriptedEvent LevelScriptedEvent;

	//TODO ask what is best To connect events in the inspector or through code
	[HideInInspector]public UnityEvent OnIntroEventEnd;
	[HideInInspector]public UnityEvent StartScriptedEvent;
	[HideInInspector]public UnityEvent EnableSpawnSystem;

	//public Animator introAnimator;

	public static LevelManager_Master instance = null;

	void Awake() // Called even if the GM is disabeld
	{
		SingeltonCheck();	
		OnIntroEventEnd.AddListener(StartSpawner);
	}

	void Start () // calling logic at start of game, only activ if activ
	{
		StartLevel();
	}

	// Dont think we need singelton check for this, Its is NOT DontDestroyOnLoad
	private void SingeltonCheck() // TODO check if LevelManager_master script Singelton is correctly
	{
		if(instance == null)
		{
			Debug.Log("LM is NULL");
			instance = this;
		}
		else if(instance != this)
		{
			Debug.LogError("LM Duplicate, destroying ");
			Destroy(gameObject);
		}
	}


	#region LevelManager OLD

	public void StartSpawner()
	{
		//OnIntroEventEnd.RemoveListener(StartSpawner);
		//EnableSpawnSystem.RemoveAllListeners();
		EnableSpawnSystem.Invoke();
	}

	#region LevelManager_ScriptedEventsManager
		// TODO make a new script to handle all the ScriptedEvents
	public void StartLevel()
	{
		Debug.Log("Starting Level....");
		GameManager_Master.instance.GetComponent<GameManager_PlayerSpawner>().SpawnPlayer(PlayerSpawnPosition.position);
		StartScriptedEvent.Invoke(); // TODO make sure Cutscene works on all Resolution, spawnpoint out of can view
		StartScriptedEvent.RemoveAllListeners();
	}


	#endregion

	void OnDisable() // PERFORMANCE do we need LevelManger events RemoveAllListeners() OnDisble 
	{
		Debug.Log("Level Man Disabled");
		OnIntroEventEnd.RemoveAllListeners();
		StartScriptedEvent.RemoveAllListeners();
		EnableSpawnSystem.RemoveAllListeners();
	}

	#endregion
}