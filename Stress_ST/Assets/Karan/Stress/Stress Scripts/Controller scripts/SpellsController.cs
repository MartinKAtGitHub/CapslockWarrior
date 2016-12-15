using UnityEngine;
using System.Collections;

public class SpellsController : MonoBehaviour {


	Spells SpellsOnKeyOne, SpellsOnKeyTwo, SpellsOnKeyThree, SpellsOnKeyFour;

	// The finale spell(agumented/default) stored as gameobjects
	public GameObject AgumentedSpellGameObjectKeyOne;
	public GameObject AgumentedSpellGameObjectKeyTwo;
	public GameObject AgumentedSpellGameObjectKeyThree;
	public GameObject AgumentedSpellGameObjectKeyFour;

	private float timerForSpellOnKeyOne;
	private float timerForSpellOnKeyTwo;
	private float timerForSpellOnKeyThree;
	private float timerForSpellOnKeyFour;

	private float saveTimerKeyOne;
	private float saveTimerKeyTwo;
	private float saveTimerKeyThree;
	private float saveTimerKeyFour;

	private bool spellKeyOneReady;
	private bool spellKeyTwoReady;
	private bool spellKeyThreeReady;
	private bool spellKeyFourReady;


	// Use this for initialization
	void Start () 
	{
		SpellsOnKeyOne = AgumentedSpellGameObjectKeyOne.GetComponent<Spells>(); 
		SpellsOnKeyTwo = AgumentedSpellGameObjectKeyTwo.GetComponent<Spells>();
		SpellsOnKeyThree = AgumentedSpellGameObjectKeyThree.GetComponent<Spells>();
		SpellsOnKeyFour = AgumentedSpellGameObjectKeyFour.GetComponent<Spells>();


		timerForSpellOnKeyOne = SpellsOnKeyOne.CoolDownTimer;
		timerForSpellOnKeyTwo = SpellsOnKeyTwo.CoolDownTimer;
		timerForSpellOnKeyThree = SpellsOnKeyThree.CoolDownTimer;
		timerForSpellOnKeyFour = SpellsOnKeyFour.CoolDownTimer;

		saveTimerKeyOne = timerForSpellOnKeyOne;
		saveTimerKeyTwo = timerForSpellOnKeyTwo;
		saveTimerKeyThree = timerForSpellOnKeyThree;
		saveTimerKeyFour = timerForSpellOnKeyFour;

		Debug.Log("Timers -> k1, k2, k3, k4  =" +"( " + timerForSpellOnKeyOne +", " + timerForSpellOnKeyTwo + ", " + timerForSpellOnKeyThree + ", " + timerForSpellOnKeyFour + " )");

		//////////////// Problemetic code //////////////////
		SpellsOnKeyOne.ProjectileSpawn = transform.FindChild("ProjectileSpawn"); // TODO i need this to spawn spells in the rigth places. but might be to heavy
		if(SpellsOnKeyOne.ProjectileSpawn == null)
		{
			Debug.Log("Trans Is NULL");
		}
		//////////////// END//////////////////////////////

		// now here we need to make a test to see what augment the player choose
		//a = new SplittFire();

		/*
			foreach spell on this key select the on that the player selected
		*/

		// TODO Make a default spell if no spell is slected by player.
		// TODO Find a way so this can be used on multiable players, MULTIPLAYER style

	}
	
	// Update is called once per frame
	void Update () 
	{
		CheckCoolDowns();
		
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			if(spellKeyOneReady == true)
			{
				SpellsOnKeyOne.Cast();
				spellKeyOneReady = false;
				timerForSpellOnKeyOne = saveTimerKeyOne;
				//Debug.Log("Timerk1 -> " + timerForSpellOnKeyOne);
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			if(spellKeyTwoReady == true)
			{
				SpellsOnKeyTwo.Cast();
				spellKeyTwoReady = false;
				timerForSpellOnKeyTwo = saveTimerKeyTwo;
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			if(spellKeyThreeReady == true)
			{
				SpellsOnKeyThree.Cast();
				spellKeyThreeReady = false;
				timerForSpellOnKeyThree = saveTimerKeyThree;
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha4))
		{
			if(spellKeyFourReady == true)
			{
				SpellsOnKeyFour.Cast();
				spellKeyTwoReady = false;
				saveTimerKeyFour = saveTimerKeyFour;
			}
		}
	}


	void CheckCoolDowns()
	{
		timerForSpellOnKeyOne -= Time.deltaTime;
		timerForSpellOnKeyTwo -= Time.deltaTime;
		timerForSpellOnKeyThree -= Time.deltaTime;
		timerForSpellOnKeyFour -= Time.deltaTime;

		if(timerForSpellOnKeyOne <= 0)
		{
			spellKeyOneReady = true;
			//Debug.Log("Spells ready k1");
		}
		if(timerForSpellOnKeyTwo <=0)
		{
			spellKeyTwoReady = true;
		}
		if(timerForSpellOnKeyThree <= 0)
		{
			spellKeyThreeReady = true;
		}
		if(timerForSpellOnKeyFour <= 0)
		{
			spellKeyFourReady = true;
		}
				
	}
}

