﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Master : MonoBehaviour {

	/// <summary>
	/// The nummber of keys the player has from beating the main boss.  
	/// </summary>
	public int BossKeys; 
	public GameObject PlayerCharacter;
	public static GameManager_Master instance = null;


	public delegate void GameManagerEventHandler();
	public event GameManagerEventHandler MenuToggleEvent;
	public event GameManagerEventHandler PauseEvent;
	public event GameManagerEventHandler RestartLevelEvent;
	public event GameManagerEventHandler GoToMainMenuSceneEvent;
	public event GameManagerEventHandler GameOverEvent;

	public bool IsGameOver;
	public bool IsMenuOn;




	void Awake()
	{
		SingeltonCheck(); // maybe make this GM static ??
		DontDestroyOnLoad(gameObject); // This keeps the GM alive in all scenes;
	}

	private void SingeltonCheck() // TODO check if Gamemanger_master script Singelton is correctly done
	{
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
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
