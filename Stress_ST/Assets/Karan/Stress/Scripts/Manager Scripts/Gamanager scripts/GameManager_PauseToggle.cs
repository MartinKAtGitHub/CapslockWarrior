using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_PauseToggle : MonoBehaviour 
{

	private GameManager_Master gameManagerMaster;

	[SerializeField] private bool isPaused;

	void OnEnable()
	{
	 	SetInitialRefs();
		gameManagerMaster.MenuToggleEvent += TogglePause; // When the MenuToggleEvent happens TogglePause() also

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
	/// Pause toggle, method used to set time scale set 0. Can be called for other purpose 
	/// </summary>
	void TogglePause()
	{
		if(isPaused)
		{
			Time.timeScale = 1;
			//StartCoroutine(DelayPauseResum(1));// Dose not work with delay
			Debug.Log("Pause False");
			isPaused = false;
		} 
		else 
		{
			Debug.Log("Pause True");
			Time.timeScale = 0;
			isPaused = true;
		}
	}


	IEnumerator DelayPauseResum(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		Time.timeScale = 1;
		Debug.Log("Game Resumed");
		//yield return null;
	}
}
