using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AbilityController : MonoBehaviour {




	// The finale spell(agumented/default) stored as gameobjects
	public GameObject AbilityKey1;
	public GameObject AbilityKey2;
	public GameObject AbilityKey3;
	public GameObject AbilityKey4;

	[Space (10)]
	[SerializeField]private GameObject DefaultAbility1;
	[SerializeField]private GameObject DefaultAbility2;
	[SerializeField]private GameObject DefaultAbility3;
	[SerializeField]private GameObject DefaultAbility4;

	[Space (10)]
	public Image SpellIconKey1ImgOverlay;
	public Image SpellIconKey2ImgOverlay;
	public Image SpellIconKey3ImgOverlay;
	public Image SpellIconKey4ImgOverlay;

	[Space (10)]
	public Text SpellIconKey1TextTimer;
	public Text SpellIconKey2TextTimer;
	public Text SpellIconKey3TextTimer;
	public Text SpellIconKey4TextTimer;

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

	Ability SpellsOnKeyOne;
	Ability SpellsOnKeyTwo;
	Ability SpellsOnKeyThree;
	Ability SpellsOnKeyFour;

	private PlayerManager playerManager;

	private float imageAplhaTimer = 0;

	[Space (10)]
	[SerializeField]private Transform abilitySpawnPoint;


	void Start () 
	{
		playerManager = GetComponent<PlayerManager>();

		//TODO Check save(Player prefs) data, restore preveious Abilitys
		AbilityNullCheck(ref AbilityKey1, DefaultAbility1 ,"Key 1");
		AbilityNullCheck(ref AbilityKey2, DefaultAbility2 ,"Key 2");
		AbilityNullCheck(ref AbilityKey3, DefaultAbility3 ,"Key 3");
		AbilityNullCheck(ref AbilityKey4, DefaultAbility4 ,"Key 4");

		SpellsOnKeyOne = AbilityKey1.GetComponent<Ability>(); 
		SpellsOnKeyTwo = AbilityKey2.GetComponent<Ability>();
		SpellsOnKeyThree = AbilityKey3.GetComponent<Ability>();
		SpellsOnKeyFour = AbilityKey4.GetComponent<Ability>();


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

		//.GetComponent<Transform>(); // Getting type missmatch on spell gameobjects. I dont know why But its is not cousing prob

		SpellsOnKeyOne.SpellSpawnPos = abilitySpawnPoint;  // TODO i need this to spawn spells in the rigth places. but might be to heavy
		SpellsOnKeyTwo.SpellSpawnPos = abilitySpawnPoint;
		SpellsOnKeyThree.SpellSpawnPos = abilitySpawnPoint;
		SpellsOnKeyFour.SpellSpawnPos = abilitySpawnPoint;

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

		// TODO Find a way so this can be used on multiable players, MULTIPLAYER style

	}	
	
	// Update is called once per frame
	void Update () 
	{
		//CheckKeyPress();
		OnAbilityTrigger(KeyCode.Alpha1, KeyCode.Keypad1, "Key 1");
		OnAbilityTrigger(KeyCode.Alpha2, KeyCode.Keypad2, "Key 2");
		OnAbilityTrigger(KeyCode.Alpha3, KeyCode.Keypad3, "Key 3");
		OnAbilityTrigger(KeyCode.Alpha4, KeyCode.Keypad4, "Key 4");
	}


	/// <summary>
	/// Abilities the null check. Sets a Default spell to the key that has no spell selected
	/// </summary>
	private void AbilityNullCheck(ref GameObject ability, GameObject defaultAbility, string key)
	{
		if(ability == null)
		{
			Debug.Log("<color=#800080ff> No spell Set on "+ key +" Setting default spells</color>");
			ability = defaultAbility;
		}
	}

	private void AbilityReady()
	{
		
	}

	private void CoolDown()
	{

	}

	private void OnAbilityTrigger(KeyCode key, KeyCode altKey, string keyID)
	{
		
		// if(CD == TRUE)
			// if(Mana)
			if(Input.GetKeyDown(key) || Input.GetKeyDown(altKey))
			{
				Debug.Log("Casting ability on " + keyID);
				bool isCastSuccsesful = SpellsOnKeyOne.Cast();

				if(isCastSuccsesful)
				{
					//RestartCD
				}
				else
				{
					//NO CD
				}

			}
		//Else
			//Count down CD
	} 

	void CheckKeyPress()
	{
		CheckCoolDowns();
		
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			if(spellKeyOneReady == true)
			{
				if(SpellsOnKeyOne.ManaCost <= playerManager.CurrentMana)
				{
					spellKeyOneReady = false;
					if(SpellsOnKeyOne.Cast())
					{
						timerForSpellOnKeyOne = saveTimerKeyOne;
						playerManager.CurrentMana -= SpellsOnKeyOne.ManaCost;	
		
						Debug.Log("Spell on key 1 used -> Current mana is " + playerManager.CurrentMana); 
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
				if(SpellsOnKeyTwo.ManaCost <= playerManager.CurrentMana)
				{
					spellKeyTwoReady = false;
					if(SpellsOnKeyTwo.Cast())
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
				if(SpellsOnKeyThree.ManaCost <= playerManager.CurrentMana)
				{
					spellKeyThreeReady = false;
					if(SpellsOnKeyThree.Cast())
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
				if(SpellsOnKeyFour.ManaCost <= playerManager.CurrentMana)
				{
					spellKeyTwoReady = false;
					if(SpellsOnKeyFour.Cast())
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

	void CheckCoolDowns() // HACK THIS IS BEING DONE EVERY FRAME 
	{
		//CheckForSpellIsUSed();
		NoCheckForSpellIsUsed();

		if(timerForSpellOnKeyOne < 1)
		{
			spellKeyOneReady = true;
			SetAlphaOnText(SpellIconKey1TextTimer,0f);
		}
		else
		{
			DisplayTimerText(SpellIconKey1TextTimer, (int)timerForSpellOnKeyOne);
			//SetAplhaOnFrontImage(SpellIconKey1ImgOverlay, 50);
		}

		if(timerForSpellOnKeyTwo < 1)
		{
			spellKeyTwoReady = true;
			SetAlphaOnText(SpellIconKey2TextTimer, 0f);
		}
		else
		{
			DisplayTimerText(SpellIconKey2TextTimer,(int)timerForSpellOnKeyTwo);
		}

		if(timerForSpellOnKeyThree < 1)
		{
			spellKeyThreeReady = true;
			SetAlphaOnText(SpellIconKey3TextTimer, 0f);
		}
		else
		{
			DisplayTimerText(SpellIconKey3TextTimer, (int)timerForSpellOnKeyThree);
		}

		if(timerForSpellOnKeyFour < 1)
		{
			spellKeyFourReady = true;
			SetAlphaOnText(SpellIconKey4TextTimer, 0f);
		}
		else
		{
			DisplayTimerText(SpellIconKey4TextTimer, (int)timerForSpellOnKeyFour);
		}

	}

	/*void CheckForSpellIsUSed()
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
	}*/

	void NoCheckForSpellIsUsed()
	{
		//TODO THIS COUNTS PAST 0 so it goes into -1 ...> -100
		timerForSpellOnKeyOne -= Time.deltaTime; // maube make this into a method so somthing like . SpellTimers(floate SavedTime) { countdown return true false}
		timerForSpellOnKeyTwo -= Time.deltaTime;
		timerForSpellOnKeyThree -= Time.deltaTime;
		timerForSpellOnKeyFour -= Time.deltaTime;
	}

	void SetAlphaOnText(Text textObject, float alphaValue)
	{
	/*	Color tmpAlpha = textObject.color;
		tmpAlpha.a = alphaValue;
		textObject.color = tmpAlpha;*/
	}

	void SetAplhaOnFrontImage(Image frontImage, float alphaValue) // TODO SPELL ICON ALPHA DOSE NOT WORK FIX NOW PLS
	{

		imageAplhaTimer = timerForSpellOnKeyOne; // this counts down not up
		Color tmpAlpha = frontImage.color;

		Debug.Log("\t\t\t\t\t\t\t\t\t Timer is = " + timerForSpellOnKeyOne);
		if(timerForSpellOnKeyOne == saveTimerKeyOne)
		{
			tmpAlpha.a = 200;
			frontImage.color = tmpAlpha;
			Debug.Log("\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tSET TO 200");

		}
		else if(imageAplhaTimer >= 1f) // this is always true
		{
			imageAplhaTimer = imageAplhaTimer % 1f;

			tmpAlpha.a -= 50;
			frontImage.color = tmpAlpha;
			Debug.Log("\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tWTF IS GOING ON");
		}

		// if(EVERY SEK)
			//Change alpha by (cooldown/200(aplha)) 
	
//		imageAplhaTimer += Time.deltaTime;
//
//		if(imageAplhaTimer >= 1f)
//		{
//			imageAplhaTimer = imageAplhaTimer % 1f;
//			Debug.Log("WTF MODULUS ======== (" + imageAplhaTimer + ")");
//
//			Debug.Log("CHANGIG ALPHA LOLOLOLOLOLOLOLOLOLOL");
//		}
//			tmpAlpha.a = 200f;
//			frontImage.color = tmpAlpha;
//			Debug.Log("REAKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK");
	}

	void DisplayTimerText(Text textObject, int timer)
	{
		int temp = timer;// i know just chill k?
		textObject.text = temp.ToString();
		SetAlphaOnText(textObject,255f);
	}


}

