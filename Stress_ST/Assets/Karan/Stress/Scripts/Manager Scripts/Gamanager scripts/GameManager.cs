﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	/// <summary>
	/// The nummber of keys the player has from beating the main boss.  
	/// </summary>
	//public int BossKeys; 
	public GameObject PlayerPrefab;
	public GameObject PlayerObject;

    public PlayerInputManager PlayerInputManager;

	public static GameManager Instance = null;


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
			Debug.LogError("GM Duplicate, destroying ");
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