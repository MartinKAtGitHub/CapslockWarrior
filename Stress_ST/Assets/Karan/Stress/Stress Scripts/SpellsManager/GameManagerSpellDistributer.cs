using UnityEngine;
using System.Collections;

public class GameManagerSpellDistributer : MonoBehaviour {

	public SpellsController MainHero;

	//delegate void RunLogicAfterSceneLoaded();
	//RunLogicAfterSceneLoaded runLogicAfterSceneLoaded;

	public GameObject SpellOnKeyOne;
	public GameObject SpellOnKeyTwo;
	public GameObject SpellOnKeyThree;
	public GameObject SpellOnKeyFour;


	/// <summary>
	/// Loads spells Data to the MAIN HERO when hero spawns so put this method when you want the hero to spawn and have the new spells selected
	/// </summary>
	public void  LoadDataToHeroOnSceneLoad() // TODO need to make a method for multiplayer heros, maybe just and a string parameter and find them
	{

		MainHero = GameObject.FindGameObjectWithTag("Player1").GetComponent<SpellsController>();

		MainHero.AgumentedSpellGameObjectKeyOne= SpellOnKeyOne;
		MainHero.AgumentedSpellGameObjectKeyTwo = SpellOnKeyTwo;
		MainHero.AgumentedSpellGameObjectKeyThree = SpellOnKeyThree;
		MainHero.AgumentedSpellGameObjectKeyFour = SpellOnKeyFour;

		//FIXME Need a null check to make sure that nothing breakes, just set default spell if something is null
	}
}
