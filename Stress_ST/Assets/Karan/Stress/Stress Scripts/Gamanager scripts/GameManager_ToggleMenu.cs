using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_ToggleMenu : MonoBehaviour
{
	private GameManager_Master gameManagerMaster;
	public GameObject menu;

	void OnEnable()
	{
		SetInitialRefs();
		gameManagerMaster.GameOverEvent += ToggleMenu;
	}

	void OnDisable()
	{
		gameManagerMaster.GameOverEvent -= ToggleMenu;
	}

	void Start()
	{
		//ToggleMenu();
	}

	void Update()
	{
		CheckForMenuToggleRequest();
	}


	void ToggleMenu()
	{
		if(menu != null)
		{
			menu.SetActive(!menu.activeSelf);// This need to be changed to Start anim
			gameManagerMaster.IsMenuOn = !gameManagerMaster.IsMenuOn;
			gameManagerMaster.CallEventMenuToggle();
		}
		else
		{
			
			Debug.LogError("Pause GameObject / UI is not Assigned on the script");	
		}
	}
	/// <summary>
	/// Checks to make sure that you can press ESC and open menu / pause the game. you dont want to be able to pause the game if the menu is already open or in "game over"
	/// ----ADD MORE CHECKS HERE IF NEEDED---
	/// </summary>
	void CheckForMenuToggleRequest()
	{
		if(Input.GetKeyUp(KeyCode.Escape) && !gameManagerMaster.IsGameOver)
		{
			ToggleMenu();
		}

	}

	void SetInitialRefs()
	{
		gameManagerMaster = GetComponent<GameManager_Master>();
	}
}
