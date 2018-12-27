using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Game manager toggle menu the idea here is that this ONLY opens up the Menu on esc key --> 
/// send a event signal --> which the pause logic is listing to. So when menu is open Do other stuff that is necesery
/// </summary>
public class GameManager_ToggleMenu : MonoBehaviour
{
	private GameManager gameManagerMaster;
	public GameObject menu;
	private Animator pauseAnimator;

	void OnEnable()
	{
		SetInitialRefs();
		gameManagerMaster.GameOverEvent += ToggleMenu; // So when Game over event happens, toggle the menu
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


	public void ToggleMenu()
	{
		if(menu != null)
		{
			//if(!menu.activeSelf)
			menu.SetActive(!menu.activeSelf);

			//PauseWithAnimations(); //TODO SpamPause will break the Logic, paused when menu is not actv

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
		//TODO Add check for in MainMenu(start of game)
		//TODO Add check for in cutscene maybe? 
		if(Input.GetKeyUp(KeyCode.Escape) && !gameManagerMaster.IsGameOver)
		{
			ToggleMenu();
			Debug.Log("I CAN STILL PAUSE IN DIALOG + NO RETURN ANIM");
		}

	}

	void SetInitialRefs()
	{
		gameManagerMaster = GetComponent<GameManager>();
		pauseAnimator = menu.GetComponent<Animator>();
	}

	void PauseWithAnimations()
	{
		bool pauseMenuToggle = !menu.activeSelf;
		// TODO The pause Pnl needs to be turned on and off. To that i turn it on here but off in a animation event
		if(pauseMenuToggle == true)
		{
			menu.SetActive(true);
		}
		else
		{
			pauseAnimator.SetTrigger("Close"); // menu.SetActive(False); in a Anim Event, has its own script
		}
	}
}
