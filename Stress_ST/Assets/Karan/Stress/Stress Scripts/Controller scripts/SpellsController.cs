using UnityEngine;
using System.Collections;

public class SpellsController : MonoBehaviour {


	Spells SpellsOnKeyOne, SpellsOnKeyTwo, SpellsOnKeyThree, SpellsOnKeyFour;

	// The finale spell(agumented/default) stored as gameobjects
	public GameObject AgumentedSpellGameObjectKeyOne;
	public GameObject AgumentedSpellGameObjectKeyTwo;
	public GameObject AgumentedSpellGameObjectKeyThree;
	public GameObject AgumentedSpellGameObjectKeyFour;


	// Use this for initialization
	void Start () 
	{

		// Since the agumentet spell is childe of spells this works
		SpellsOnKeyOne = AgumentedSpellGameObjectKeyOne.GetComponent<Spells>(); 

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

