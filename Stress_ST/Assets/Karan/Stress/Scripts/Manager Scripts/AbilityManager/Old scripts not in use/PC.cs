using UnityEngine;
using System.Collections;

public class PC : MonoBehaviour {

	Ability SpellsOnKeyOne, SpellsOnKeyTwo, SpellsOnKeyThree, SpellsOnKeyFour;

	public GameObject SpellOnKeyOne;
	public GameObject SpellOnKeyTwo;
	public GameObject SpellOnKeyThree;
	public GameObject SpellOnKeyFour;

	enum SpellKeys
	{
		Key1,
		key2,
		key3,
		key4,
	};

	// Use this for initialization
	void Start () 
	{

		SpellsOnKeyOne = SpellOnKeyOne.GetComponent<Ability>(); // SINCE 

		if(SpellsOnKeyOne == null)
		{
			Debug.Log("NO GAMEOBJ ON SPELL K1");
		}
		// now here we need to make a test to see what augment the player choose
		//a = new SplittFire();

		/*
			
			foreach spell on this key select the on that the player selected



		*/
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			SpellsOnKeyOne.Cast();
			// 
		}
		if(Input.GetKeyDown(KeyCode.Alpha2))
		{

		}
		if(Input.GetKeyDown(KeyCode.Alpha3))
		{

		}
		if(Input.GetKeyDown(KeyCode.Alpha4))
		{

		}


	}
}
