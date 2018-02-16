using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_PauseToggle : MonoBehaviour 
{

	private GameManager_Master gameManagerMaster;
	[SerializeField]
	private bool isPaused;

	void OnEnable()
	{
	 	SetInitialRefs();
	 	gameManagerMaster.MenuToggleEvent += TogglePause;

	}

	void OnDisable()
	{
		gameManagerMaster.MenuToggleEvent -= TogglePause;
	}


	void SetInitialRefs()
	{
		gameManagerMaster = GetComponent<GameManager_Master>(); // On the same GameObject
		//gameManagerMaster = GameManager_Master.instance; // I do have a static/ Singolton. Maybe it is wrong to use it here
	}

	/// <summary>
	/// Pause toggle, This only Stops the game, and i call it when i need to Stop the game. By saparating this we can stop time whenever we want. 
	/// </summary>
	void TogglePause()
	{
		if(isPaused)
		{
			Time.timeScale = 1;
			isPaused = false;
		} 
		else 
		{
			Time.timeScale = 0;
			isPaused = true;
		}
	}
}
