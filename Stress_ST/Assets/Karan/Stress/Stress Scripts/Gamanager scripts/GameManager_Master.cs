using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Master : MonoBehaviour {

	/// <summary>
	/// The nummber of keys the player has from beating the main boss.  
	/// </summary>
	public int BossKeys; 

	public static GameManager_Master instance = null;


	void Awake()
	{
		SingeltonCheck();
		DontDestroyOnLoad(gameObject); // This keeps the GM alive in all scenes;
	}




	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	private void SingeltonCheck()
	{
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}
	}


	// Need events listed down here
}
