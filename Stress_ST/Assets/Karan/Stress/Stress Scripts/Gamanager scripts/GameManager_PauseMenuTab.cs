using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_PauseMenuTab : MonoBehaviour {

	private GameManager_Master gM_Master; 
	private GameManager_ToggleMenu gM_ToggleMenu;

	void OnEnable()
	{
	 	SetInitialRefs();

	}

	void OnDisable()
	{

	}



	public void ResumeGame()
	{
		gM_ToggleMenu.ToggleMenu();
	}

	public void RestartLevel()
	{

	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void ConfirmQuitGameToDesktop()
	{
		
	}


	private void SetInitialRefs()
	{	
		gM_ToggleMenu = GetComponent<GameManager_ToggleMenu>();
	}
}
