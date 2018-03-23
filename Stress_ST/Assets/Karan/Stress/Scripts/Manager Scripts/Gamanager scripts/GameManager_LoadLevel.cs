using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager_LoadLevel : MonoBehaviour 
{
	// So I place this on a button ... Prefab / In scene it works but not sure if this will cause problems down the line
	public void LoadeNewLevel(string levelName) 
	{
		SceneManager.LoadScene(levelName);
	}
}
