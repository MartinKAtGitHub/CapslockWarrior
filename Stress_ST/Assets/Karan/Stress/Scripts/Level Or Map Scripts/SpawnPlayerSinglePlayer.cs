using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPlayerSinglePlayer : MonoBehaviour {
	

	public GameObject MainHero;
	public GameObject CountDownAnim;
	public GameObject SpellBarGUI;

	private GameObject MainCanvas;
	private GameObject GM;
	private GameObject HeroClone;

	private GameObject SpellIconKey1;
	private GameObject SpellIconKey2;
	private GameObject SpellIconKey3;
	private GameObject SpellIconKey4;


	private GameManagerSpellDistributer GMSpellDist;
	private AbilityController spellsController;
	private Animator animController;
	private Spawnmanaging Spawner;

	void Start () 
	{

		Spawner = GetComponent<Spawnmanaging>();

		GM = GameObject.FindGameObjectWithTag("GameManager");
		MainCanvas = GameObject.FindGameObjectWithTag("MainCanvas");


		if(GM != null && MainHero != null)
		{
			GMSpellDist = GM.GetComponent<GameManagerSpellDistributer>();
			spellsController = MainHero.GetComponent<AbilityController>();
		}
		else
		{ 
			Debug.LogError("SpawnPlayerSingleplayer could not find GameManager or Main hero -> GM (" + GMSpellDist + ") HERO (" + MainHero + ")");
		}

		SetGUISpellICons();

		StartCountDownAnimGui();
		// spawnanim (1 sek) start anim
 		SpawnPlayerChar();

	}

	public void StartCountDownAnimGui()
	{
		// so we sett bool to be true the moment level starts i guess -- we might get problems with all the things loading så things will be out of sync
		// we wait for anim to be to play and signal the player spawn to get ready.
		CountDownAnim.SetActive(true);

		//CountDownAnimStartScript t =  CountDownAnim.GetComponent<CountDownAnimStartScript>();


	}

	public void SpawnPlayerChar()
	{
		//TODO Catch ERROR / NULL SO THIS DOSENT CRASH
		// We send the spells the player choose in Main Map to the acutal hero here.

		// we chnage the prefab
		Instantiate(MainHero,Vector3.zero, Quaternion.identity); // we spawn the defaul hero first. Sould have deafult spells. OR we can send it before we spawn and the prefab wil change.

		spellsController.AbilityObjectKey1 = GMSpellDist.SpellOnKeyOne;
		spellsController.AbilityObjectKey2 = GMSpellDist.SpellOnKeyTwo;
		spellsController.AbilityObjectKey3 = GMSpellDist.SpellOnKeyThree;
		spellsController.AbilityObjectKey4 = GMSpellDist.SpellOnKeyFour;

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


	public void StartSpawner()
	{
		Spawner.StartSpawn = true;
	}

	private void SetGUISpellICons()
	{
		GameObject spellBar;
		spellBar = (GameObject)Instantiate(SpellBarGUI, MainCanvas.transform);


		SpellIconKey1 = spellBar.transform.GetChild(0).gameObject;
		SpellIconKey2 = spellBar.transform.GetChild(1).gameObject;
		SpellIconKey3 = spellBar.transform.GetChild(2).gameObject;
		SpellIconKey4 = spellBar.transform.GetChild(3).gameObject;

	/*	spellsController.SpellIconKey1ImgOverlay = SpellIconKey1.transform.GetChild(0).gameObject.GetComponent<Image>();
		spellsController.SpellIconKey2ImgOverlay = SpellIconKey2.transform.GetChild(0).gameObject.GetComponent<Image>();
		spellsController.SpellIconKey3ImgOverlay = SpellIconKey3.transform.GetChild(0).gameObject.GetComponent<Image>();
		spellsController.SpellIconKey4ImgOverlay = SpellIconKey4.transform.GetChild(0).gameObject.GetComponent<Image>();

		spellsController.SpellIconKey1TextTimer = SpellIconKey1.transform.GetChild(1).gameObject.GetComponent<Text>();
		spellsController.SpellIconKey2TextTimer = SpellIconKey2.transform.GetChild(1).gameObject.GetComponent<Text>();
		spellsController.SpellIconKey3TextTimer = SpellIconKey3.transform.GetChild(1).gameObject.GetComponent<Text>();
		spellsController.SpellIconKey4TextTimer = SpellIconKey4.transform.GetChild(1).gameObject.GetComponent<Text>();*/


		SpellIconKey1.GetComponent<Image>().sprite = GMSpellDist.SpellOnKeyOne.GetComponent<Ability>().AbilityImageIcon;
		SpellIconKey2.GetComponent<Image>().sprite = GMSpellDist.SpellOnKeyTwo.GetComponent<Ability>().AbilityImageIcon;
		SpellIconKey3.GetComponent<Image>().sprite = GMSpellDist.SpellOnKeyThree.GetComponent<Ability>().AbilityImageIcon;
		SpellIconKey4.GetComponent<Image>().sprite = GMSpellDist.SpellOnKeyFour.GetComponent<Ability>().AbilityImageIcon;



	}
}
