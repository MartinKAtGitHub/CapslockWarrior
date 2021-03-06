﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager Instance = null;

    public GameObject PlayerPrefab;
	public GameObject PlayerObject;

    public PlayerInputManager PlayerInputManager;
    public OrbSystemMenuManager OrbSystemMenuManager;
    public CutSceneManager CutSceneManager;
    public GameManager_LoadLevel Manager_LoadLevel;



    public delegate void GameManagerEventHandler();
	public event GameManagerEventHandler MenuToggleEvent;
	/*public event GameManagerEventHandler PauseEvent;
	public event GameManagerEventHandler RestartLevelEvent;
	public event GameManagerEventHandler GoToMainMenuSceneEvent;*/
	public event GameManagerEventHandler GameOverEvent;

	public bool IsGameOver;
	public bool IsMenuOn;
    

	void Awake()
	{
		SingeltonCheck();
		DontDestroyOnLoad(gameObject); // This keeps the GM alive in all scenes;

        PlayerInputManager = GetComponent<PlayerInputManager>();
        OrbSystemMenuManager = GetComponent<OrbSystemMenuManager>();
        CutSceneManager = GetComponent<CutSceneManager>();
        Manager_LoadLevel = GetComponent<GameManager_LoadLevel>();
    }

	private void SingeltonCheck() // TODO check if Gamemanger_master script Singelton is correctly done
	{
		if(Instance == null)
		{
			Debug.Log("GM is NULL");
			Instance = this;
		}
		else if(Instance != this)
		{
			Debug.LogWarning("GM Duplicate, destroying in Scene");
			Destroy(gameObject);
		}
	}

#region CallEventsMethods

	public void CallEventMenuToggle()
	{
		if(MenuToggleEvent != null)
		{
			MenuToggleEvent();
		}
	}



#endregion

}
