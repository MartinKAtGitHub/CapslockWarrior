using UnityEngine;
using System.Collections;

public class GameManagerSpellDistributer : MonoBehaviour {

	public AbilityController MainHero;

	//delegate void RunLogicAfterSceneLoaded();
	//RunLogicAfterSceneLoaded runLogicAfterSceneLoaded;

	public GameObject SpellOnKeyOne;
	public GameObject SpellOnKeyTwo;
	public GameObject SpellOnKeyThree;
	public GameObject SpellOnKeyFour;


	void Start()
	{
		MainHero = GameManager.Instance.PlayerPrefab.GetComponent<AbilityController>();
	}
	/// <summary>
	/// Loads spells Data to the MAIN HERO when hero spawns so put this method when you want the hero to spawn and have the new spells selected
	/// </summary>
	public void  LoadDataToHeroOnSceneLoad() // TODO need to make a method for multiplayer heros, maybe just and a string parameter and find them
	{

		//MainHero = GameObject.FindGameObjectWithTag("Player1").GetComponent<AbilityController>();

	//	MainHero.AbilityObjectKey1= SpellOnKeyOne;
	//	MainHero.AbilityObjectKey2 = SpellOnKeyTwo;
	//	MainHero.AbilityObjectKey3 = SpellOnKeyThree;
	//	MainHero.AbilityObjectKey4 = SpellOnKeyFour;

		// Note If spell is NULL we set default spell in SpellsController. Maybe do it here ?
	}
}
