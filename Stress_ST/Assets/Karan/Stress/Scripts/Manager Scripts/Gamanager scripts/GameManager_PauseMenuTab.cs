using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameManager_PauseMenuTab : MonoBehaviour {

	private GameManager gM_Master; 
	private GameManager_ToggleMenu gM_ToggleMenu;

	public GameObject MenuTab;
	public GameObject OptionsTab;
	public GameObject MainMenu;
	public GameObject OptionsMenu;
	public GameObject ConfirmQuitGameToDesktopPnl;
	public GameObject ConfirmRestartPnl;

	[SerializeField]
	private Color activeTabColor;
	[SerializeField]
	private Color inActiveTabColor;




	void Start()
	{
		SetInitialRefs();
	}

	void OnEnable()
	{
	 	

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
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		// Need to reset The state of the menu to default. Maybe do that in the start of scene
		ConfirmRestartLevel(true);
		ResumeGame();
	}


	public void QuitToDesktopYes()
	{
		Application.Quit();
	}


	public void ConfirmQuitGameToDesktop(bool answer)
	{
		MainMenu.GetComponent<CanvasGroup>().interactable = answer;
		ConfirmQuitGameToDesktopPnl.SetActive(!answer);
	}

	public void ConfirmRestartLevel(bool answer)
	{
		MainMenu.GetComponent<CanvasGroup>().interactable = answer;
		ConfirmRestartPnl.SetActive(!answer);
	}


	private void SetInitialRefs()
	{	
		gM_ToggleMenu = GetComponent<GameManager_ToggleMenu>();

		activeTabColor = MenuTab.GetComponent<Image>().color;
		inActiveTabColor = OptionsTab.GetComponent<Image>().color;

	}

	public void OpenMenuTab()
	{
		MenuTab.transform.SetAsLastSibling();
		MenuTab.GetComponent<Image>().color = activeTabColor;

		OptionsTab.GetComponent<Image>().color = inActiveTabColor;
		OptionsMenu.SetActive(false);
		MainMenu.SetActive(true);
	}
	public void OpenOptionsTab()
	{
		OptionsTab.transform.SetAsLastSibling();
		OptionsTab.GetComponent<Image>().color = activeTabColor;

		MenuTab.GetComponent<Image>().color = inActiveTabColor;
		MainMenu.SetActive(false);
		OptionsMenu.SetActive(true);
	}
}
