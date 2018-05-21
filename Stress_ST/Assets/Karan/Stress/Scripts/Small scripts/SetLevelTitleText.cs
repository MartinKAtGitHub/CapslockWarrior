using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetLevelTitleText : MonoBehaviour {
	private Text titleText;
	private Scene currentScene;

	// Use this for initialization
	void Start () 
	{	currentScene = SceneManager.GetActiveScene();
		titleText = GetComponent<Text>();

		titleText.text = currentScene.name;
	}
}
