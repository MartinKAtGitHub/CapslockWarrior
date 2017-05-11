﻿using UnityEngine;
using System.Collections;

public class SpellsController : MonoBehaviour {


	Spells SpellsOnKeyOne, SpellsOnKeyTwo, SpellsOnKeyThree, SpellsOnKeyFour;

	// The finale spell(agumented/default) stored as gameobjects
	public GameObject AgumentedSpellGameObjectKeyOne;
	public GameObject AgumentedSpellGameObjectKeyTwo;
	public GameObject AgumentedSpellGameObjectKeyThree;
	public GameObject AgumentedSpellGameObjectKeyFour;

	[Space (10)]
	public GameObject AgumentedSpellGameObjectKeyOneDefaultSpell;
	public GameObject AgumentedSpellGameObjectKeyTwoDefaultSpell;
	public GameObject AgumentedSpellGameObjectKeyThreeDefaultSpell;
	public GameObject AgumentedSpellGameObjectKeyFourDefaultSpell;

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

	private PlayerManager playerManager;

	// Use this for initialization
	void Start () 
	{
		playerManager = GetComponent<PlayerManager>();

		Debug.Log(" HEALT IS =" + playerManager.HealthPoints);
		Debug.Log("Mana is = " + playerManager.CurrentMana);


		if(AgumentedSpellGameObjectKeyOne == null)
		{
			Debug.LogWarning("No spell Set on Key 1, Setting default spells");
			AgumentedSpellGameObjectKeyOne = AgumentedSpellGameObjectKeyOneDefaultSpell;
		}
		if(AgumentedSpellGameObjectKeyTwo == null)
		{
			Debug.LogWarning("No spell Set on Key 2, Setting default spells");
			AgumentedSpellGameObjectKeyTwo = AgumentedSpellGameObjectKeyTwoDefaultSpell;
		}
		if(AgumentedSpellGameObjectKeyThree == null)
		{
			Debug.LogWarning("No spell Set on Key 3, Setting default spells");
			AgumentedSpellGameObjectKeyThree = AgumentedSpellGameObjectKeyThreeDefaultSpell;
		}
		if(AgumentedSpellGameObjectKeyFour == null)
		{
			Debug.LogWarning("No spell Set on Key 4, Setting default spells");
			AgumentedSpellGameObjectKeyFour = AgumentedSpellGameObjectKeyFourDefaultSpell;
		}

		SpellsOnKeyOne = AgumentedSpellGameObjectKeyOne.GetComponent<Spells>(); 
		SpellsOnKeyTwo = AgumentedSpellGameObjectKeyTwo.GetComponent<Spells>();
		SpellsOnKeyThree = AgumentedSpellGameObjectKeyThree.GetComponent<Spells>();
		SpellsOnKeyFour = AgumentedSpellGameObjectKeyFour.GetComponent<Spells>();


		/*
		timerForSpellOnKeyOne = SpellsOnKeyOne.CoolDownTimer;
		timerForSpellOnKeyTwo = SpellsOnKeyTwo.CoolDownTimer;
		timerForSpellOnKeyThree = SpellsOnKeyThree.CoolDownTimer;
		timerForSpellOnKeyFour = SpellsOnKeyFour.CoolDownTimer;
		*/
		timerForSpellOnKeyOne = 0;
		timerForSpellOnKeyTwo = 0;
		timerForSpellOnKeyThree = 0;
		timerForSpellOnKeyFour = 0;
		/*
		saveTimerKeyOne = timerForSpellOnKeyOne;
		saveTimerKeyTwo = timerForSpellOnKeyTwo;
		saveTimerKeyThree = timerForSpellOnKeyThree;
		saveTimerKeyFour = timerForSpellOnKeyFour;
		*/
		saveTimerKeyOne = SpellsOnKeyOne.CoolDownTimer;
		saveTimerKeyTwo = SpellsOnKeyTwo.CoolDownTimer;
		saveTimerKeyThree = SpellsOnKeyThree.CoolDownTimer;
		saveTimerKeyFour = SpellsOnKeyFour.CoolDownTimer;


		//Debug.Log("Timers -> k1, k2, k3, k4  =" +"( " + timerForSpellOnKeyOne +", " + timerForSpellOnKeyTwo + ", " + timerForSpellOnKeyThree + ", " + timerForSpellOnKeyFour + " )");

		//////////////// Problemetic code //////////////////
		// TODO find a smarter solution SpellsController
		SpellsOnKeyOne.PlayerGameObject = this.gameObject;
		SpellsOnKeyTwo.PlayerGameObject = this.gameObject;
		SpellsOnKeyThree.PlayerGameObject = this.gameObject;
		SpellsOnKeyFour.PlayerGameObject = this.gameObject;

		Transform SpellsSpawn = transform.FindChild("ProjectileSpawn");//.GetComponent<Transform>(); // Getting type missmatch on spell gameobjects. I dont know why But its is not cousing prob
		SpellsOnKeyOne.SpellSpawnPos = SpellsSpawn;  // TODO i need this to spawn spells in the rigth places. but might be to heavy
		SpellsOnKeyTwo.SpellSpawnPos = SpellsSpawn;
		SpellsOnKeyThree.SpellSpawnPos = SpellsSpawn;
		SpellsOnKeyFour.SpellSpawnPos = SpellsSpawn;

		if(SpellsOnKeyOne.SpellSpawnPos == null)
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
		CheckKeyPress();
		//CheckKeyPressSecondVersion();
	
	}

	void CheckKeyPressSecondVersion()
	{
		Debug.Log("TIMER SPELL K1 = " + timerForSpellOnKeyOne);
		NoCheckForSpellIsUsed();
		//if(SpellsOnKeyOne.InGameSpellRef != null)
		//	Debug.Log( SpellsOnKeyOne.InGameSpellRef.name + " <Timerk1 -> " + SpellsOnKeyOne.InGameSpellRef.GetComponent<Spells>().CoolDownTimer);

		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			if(timerForSpellOnKeyOne <= 0)
			{
				SpellsOnKeyOne.Cast();
				spellKeyOneReady = false;
				//timerForSpellOnKeyOne = SpellsOnKeyOne.InGameSpellRef.GetComponent<Spells>().CoolDownTimer; // dont think we can cache this yet. leav it for now
				//timerForSpellOnKeyOne = saveTimerKeyOne;
				//Debug.Log("olololololololol");
			//	Debug.Log( SpellsOnKeyOne.InGameSpellRef.name+ " <Timerk1 -> " + SpellsOnKeyOne.InGameSpellRef.GetComponent<Spells>().CoolDownTimer);
			//	Debug.Log(SpellsOnKeyOne.name + " <NO REF > " +SpellsOnKeyOne.CoolDownTimer);
			//	Debug.Log( SpellsOnKeyOne.InGameSpellRef.name+ " <Timerk1 -> " + SpellsOnKeyOne.InGameSpellRef.GetComponent<Spells>().IsSpellCasted);
			//	Debug.Log(SpellsOnKeyOne.name + " <NO REF > " +SpellsOnKeyOne.IsSpellCasted);
			}
			else
			{
			//	Debug.Log( SpellsOnKeyOne.InGameSpellRef.name+ " <Timerk1 -> " + SpellsOnKeyOne.InGameSpellRef.GetComponent<Spells>().CoolDownTimer);
			//	Debug.Log(SpellsOnKeyOne.name + " <NO REF > " +SpellsOnKeyOne.CoolDownTimer);
			//	Debug.Log( SpellsOnKeyOne.InGameSpellRef.name+ " <Timerk1 -> " + SpellsOnKeyOne.InGameSpellRef.GetComponent<Spells>().IsSpellCasted);
			//	Debug.Log(SpellsOnKeyOne.name + " <NO REF > " +SpellsOnKeyOne.IsSpellCasted);
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			if(timerForSpellOnKeyTwo < 0)
			{
				SpellsOnKeyTwo.Cast();
				spellKeyTwoReady = false;
				timerForSpellOnKeyTwo = saveTimerKeyTwo;
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			if(timerForSpellOnKeyThree < 0)
			{
				SpellsOnKeyThree.Cast();
				spellKeyThreeReady = false;
				timerForSpellOnKeyThree = saveTimerKeyThree;
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha4))
		{
			if(timerForSpellOnKeyFour < 0)
			{
				SpellsOnKeyFour.Cast();
				spellKeyTwoReady = false;
				timerForSpellOnKeyFour = saveTimerKeyFour;
			}
		}
	}

	void CheckKeyPress()
	{
		CheckCoolDowns();
		
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			if(spellKeyOneReady == true)
			{
				if(SpellsOnKeyOne.ManaCost < playerManager.CurrentMana)
				{
					spellKeyOneReady = false;
					if(SpellsOnKeyOne.CastBoolienReturn())
					{
						timerForSpellOnKeyOne = saveTimerKeyOne;
						playerManager.CurrentMana -= SpellsOnKeyOne.ManaCost;	

						Debug.Log("Spell on key 2 used -> Current mana is " + playerManager.CurrentMana); 
						Debug.Log("START TIMER AGAIN");
					}
					else
					{
						Debug.Log("DONT START TIMER AGAIN K1");
					}
				}
				else
				{
					Debug.Log("Cant use spell on Key 1  NO MANA");
				}
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			if(spellKeyTwoReady == true)
			{
				if(SpellsOnKeyTwo.ManaCost < playerManager.CurrentMana)
				{
					spellKeyTwoReady = false;
					if(SpellsOnKeyTwo.CastBoolienReturn())
					{
						timerForSpellOnKeyTwo = saveTimerKeyTwo;
						playerManager.CurrentMana -= SpellsOnKeyTwo.ManaCost;
						Debug.Log("Spell on key 2 used -> Current mana is " + playerManager.CurrentMana); 
					}
					else
					{
						Debug.Log("DONT START TIMER AGAIN K2");
					}
				}
				else
				{
					Debug.Log("Cant use spell on Key 2  NO MANA");
				}
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			if(spellKeyThreeReady == true)
			{
				if(SpellsOnKeyThree.ManaCost < playerManager.CurrentMana)
				{
					spellKeyThreeReady = false;
					if(SpellsOnKeyThree.CastBoolienReturn())
					{
						timerForSpellOnKeyThree = saveTimerKeyThree;
						playerManager.CurrentMana -= SpellsOnKeyThree.ManaCost;
						Debug.Log("Spell on key 3 used -> Current mana is " + playerManager.CurrentMana); 
					}
				}
				else
				{
					Debug.Log("Cant use spell on Key 3  NO MANA");
				}
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha4))
		{
			if(spellKeyFourReady == true)
			{
				if(SpellsOnKeyFour.ManaCost < playerManager.CurrentMana)
				{
					spellKeyTwoReady = false;
					if(SpellsOnKeyFour.CastBoolienReturn())
					{
						timerForSpellOnKeyFour = saveTimerKeyFour;
						playerManager.CurrentMana -= SpellsOnKeyFour.ManaCost;
						Debug.Log("Spell on key 4 used -> Current mana is " + playerManager.CurrentMana); 
					}
				}else
				{
					Debug.Log("Cant use spell on Key 4  NO MANA");
				}
			}
		}
	}

	void CheckCoolDowns()
	{
		//CheckForSpellIsUSed();
		NoCheckForSpellIsUsed();

		if(timerForSpellOnKeyOne <= 0)
		{
			spellKeyOneReady = true;

		}
		else
		{
		//	Debug.LogWarning(" SPELL 1 NOT READY");
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

	void CheckForSpellIsUSed()
	{
		//Debug.Log(SpellsOnKeyOne.gameObject.name);

		if(SpellsOnKeyOne.IsSpellCasted == true && timerForSpellOnKeyOne >= 0)
		{

			timerForSpellOnKeyOne -= Time.deltaTime;
			//Debug.LogWarning(" SPELL 1  IS COOLDOWN -- > " + timerForSpellOnKeyOne);
			//Debug.LogWarning(" SPELL 1  IS COOLDOWN -- > ");
			Debug.LogWarning("Succses" +SpellsOnKeyOne.IsSpellCasted);
		}else
		{
			//Debug.LogWarning(" SPELL 1  NOT COOLDOWN");
		//	Debug.LogWarning( "FAILED " + SpellsOnKeyOne.IsSpellCasted);
		}

		if(SpellsOnKeyTwo.IsSpellCasted == true)
		{
			timerForSpellOnKeyTwo -= Time.deltaTime;
		}
		if(SpellsOnKeyThree.IsSpellCasted == true)
		{
			timerForSpellOnKeyThree -= Time.deltaTime;
		}
		if(SpellsOnKeyFour.IsSpellCasted == true)
		{
			timerForSpellOnKeyFour -= Time.deltaTime;
		}
	}

	void NoCheckForSpellIsUsed()
	{
		timerForSpellOnKeyOne -= Time.deltaTime;
		timerForSpellOnKeyTwo -= Time.deltaTime;
		timerForSpellOnKeyThree -= Time.deltaTime;
		timerForSpellOnKeyFour -= Time.deltaTime;
	}
}

