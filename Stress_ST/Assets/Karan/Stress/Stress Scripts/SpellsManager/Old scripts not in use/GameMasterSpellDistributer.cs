using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMasterSpellDistributer : MonoBehaviour {

	public PC MainHero;

	delegate void RunLogicAfterSceneLoaded();
	RunLogicAfterSceneLoaded runLogicAfterSceneLoaded;

	public GameObject SpellOnKeyOne;
	public GameObject SpellOnKeyTwo;
	public GameObject SpellOnKeyThree;
	public GameObject SpellOnKeyFour;


	// Use this for initialization
	void Start () 
	{
		Debug.Log("This has started ---->");
		
		if(MainHero == null) // TODO this also need to fire off everytime he spawns
		{
			//MainHero = GameObject.FindGameObjectWithTag("MainHero").GetComponent<PC>();
		}
		else
		{
			Debug.Log("Found the main hero");
		}

		//TODO The Timeing needs to be prefect is the Hero dose not exist the info will not be sendt.
		// Maybe we can put this logic after he Spawns on in th lvl and then call LoadeDate

		// this works but its weird
		/*SceneManager.sceneLoaded += delegate {
			SceneLoaded();
			LoadDataToHeroOnSceneLoad();
		};
		*/


		/*		
		runLogicAfterSceneLoaded += LoadDataToHeroOnSceneLoad;
		SceneManager.sceneLoaded += runLogicAfterSceneLoaded();
		*/
	}
	
	// Update is called once per frame
	void Update () 
	{
		//SceneManager.sceneLoaded += SceneLoaded;

	}

	public void  LoadDataToHeroOnSceneLoad()
	{
		/*if(MainHero == null)
		{
			MainHero = GameObject.FindGameObjectWithTag("MainHero").GetComponent<PC>();
		}*/
		
		//TODO WHEN NEW SCENE STARTS SEND DATA TO HERO the fuck do i do that :OOOOOO
		// Scenemanager.sceneLoaded
		//MainHero.GetComponent<PC>().
		MainHero = GameObject.FindGameObjectWithTag("MainHero").GetComponent<PC>();


		MainHero.SpellOnKeyOne = SpellOnKeyOne;
		MainHero.SpellOnKeyTwo = SpellOnKeyTwo;
		MainHero.SpellOnKeyThree = SpellOnKeyThree;
		MainHero.SpellOnKeyFour = SpellOnKeyFour;

		//FIXME Need a null check to make sure that nothing breakes, just set default spell if something is null
	}

	void SceneLoaded()
	{
		Debug.Log("THE SCENE IS LOADED");
	}



}
