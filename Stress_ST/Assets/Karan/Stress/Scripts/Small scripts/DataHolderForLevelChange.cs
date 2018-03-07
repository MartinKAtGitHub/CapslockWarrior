using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolderForLevelChange : MonoBehaviour {

	public string LevelName;
	public GameObject MenuUI;

	private MenuManager menuManger;

	void Start()
	{
		MenuUI = GameObject.FindGameObjectWithTag("Menu");
		menuManger = MenuUI.GetComponent<MenuManager>();
	}


	public void sendData()
	{
		// maybe instantiate LoadOutGUI
		menuManger.LevelName = LevelName;
	}
}
