using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GMDontDestroy : MonoBehaviour {


	void Awake()// singleton for the game master.
	{
		DontDestroyOnLoad(this);

		if(FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}

		SceneManager.sceneLoaded += delegate {
			Debug.Log("Level Loaded");
		};
	}
}
