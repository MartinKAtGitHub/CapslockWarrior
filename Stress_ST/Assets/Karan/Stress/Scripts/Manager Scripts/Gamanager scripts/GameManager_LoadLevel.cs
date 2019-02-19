using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager_LoadLevel : MonoBehaviour
{
    public int SceneIndex;

	public void LoadeNewLevel() 
	{
        // Mayabe add a on scene loade EVENT and close everything i dont need
        // SceneChange.Invoke()

		SceneManager.LoadSceneAsync(SceneIndex);
	}
}
