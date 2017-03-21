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
		if(AgumentedSpellGameObjectKeyOne == null)
		{
			Debug.LogError("No spell Set on Key 1, pls add a default spell here to avoid null ref");
		}
		if(AgumentedSpellGameObjectKeyTwo == null)
		{
			Debug.LogError("No spell Set on Key 2, pls add a default spell here to avoid null ref");
		}
		if(AgumentedSpellGameObjectKeyThree == null)
		{
			Debug.LogError("No spell Set on Key 3, pls add a default spell here to avoid null ref");
		}
		if(AgumentedSpellGameObjectKeyFour == null)
		{
			Debug.LogError("No spell Set on Key 4, pls add a default spell here to avoid null ref");
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
				spellKeyOneReady = false;
				if(SpellsOnKeyOne.CastBoolienReturn())
				{
					timerForSpellOnKeyOne = saveTimerKeyOne;
					Debug.Log("START TIMER AGAIN");
				}
				else
				{
					Debug.Log("DONT START TIMER AGAIN K1");
				}
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			if(spellKeyTwoReady == true)
			{
				spellKeyTwoReady = false;
				if(SpellsOnKeyTwo.CastBoolienReturn())
				{
					timerForSpellOnKeyTwo = saveTimerKeyTwo;
				}
				else
				{
					Debug.Log("DONT START TIMER AGAIN K2");
				}
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			if(spellKeyThreeReady == true)
			{
				spellKeyThreeReady = false;
				if(SpellsOnKeyThree.CastBoolienReturn())
				{
					timerForSpellOnKeyThree = saveTimerKeyThree;
				}
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha4))
		{
			if(spellKeyFourReady == true)
			{
				spellKeyTwoReady = false;
				if(SpellsOnKeyFour.CastBoolienReturn())
				{
					timerForSpellOnKeyFour = saveTimerKeyFour;
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

