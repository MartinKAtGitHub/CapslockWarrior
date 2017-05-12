using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerSinglePlayer : MonoBehaviour {
	

	public GameObject MainHero;
	public GameObject CountDownAnim;

	private GameObject GM;
	private GameObject HeroClone;

	private GameManagerSpellDistributer GMSpellDist;
	private SpellsController spellsController;
	private Animator animController;

	void Start () 
	{
			GM = GameObject.FindGameObjectWithTag("GameManager");

			if(GM != null && MainHero != null)
			{
				GMSpellDist = GM.GetComponent<GameManagerSpellDistributer>();
				spellsController = MainHero.GetComponent<SpellsController>();
			}
			else
			{ 
				Debug.LogError("SpawnPlayerSingleplayer could not find GameManager or Main hero -> GM (" + GMSpellDist + ") HERO (" + MainHero + ")");
			}


	
		SpawnPlayerChar();

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}


	public void StartCountDownAnimGui()
	{
		// so we sett bool to be true the moment level starts i guess -- we might get problems with all the things loading så things will be out of sync
		// we wait for anim to be to play and signal the player spawn to get ready.



	}

	public void SpawnPlayerChar()
	{
		// We send the spells the player choose in Main Map to the acutal hero here.

		// we chnage the prefab
		Instantiate(MainHero,Vector3.zero, Quaternion.identity); // we spawn the defaul hero first. Sould have deafult spells. OR we can send it before we spawn and the prefab wil change.

		spellsController.AgumentedSpellGameObjectKeyOne = GMSpellDist.SpellOnKeyOne;
		spellsController.AgumentedSpellGameObjectKeyTwo = GMSpellDist.SpellOnKeyTwo;
		spellsController.AgumentedSpellGameObjectKeyThree = GMSpellDist.SpellOnKeyThree;
		spellsController.AgumentedSpellGameObjectKeyFour = GMSpellDist.SpellOnKeyFour;

		// we change the clone
		/*HeroClone =	(GameObject)Instantiate(MainHero,Vector3.zero, Quaternion.identity);  
		spellsController = HeroClone.GetComponent<SpellsController>();

		// We send the spells the player choose in Main Map to the acutal hero here.
		spellsController.AgumentedSpellGameObjectKeyOne = GMSpellDist.SpellOnKeyOne;
		spellsController.AgumentedSpellGameObjectKeyTwo = GMSpellDist.SpellOnKeyTwo;
		spellsController.AgumentedSpellGameObjectKeyThree = GMSpellDist.SpellOnKeyThree;
		spellsController.AgumentedSpellGameObjectKeyFour = GMSpellDist.SpellOnKeyFour;
		*/
	}

}
