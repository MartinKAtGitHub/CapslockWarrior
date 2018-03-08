using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AbilityController : MonoBehaviour {




	// The finale spell(agumented/default) stored as gameobjects
	public GameObject AbilityObjectKey1;
	public GameObject AbilityObjectKey2;
	public GameObject AbilityObjectKey3;
	public GameObject AbilityObjectKey4;

	[Space (10)]
	[SerializeField]private GameObject DefaultAbility1;
	[SerializeField]private GameObject DefaultAbility2;
	[SerializeField]private GameObject DefaultAbility3;
	[SerializeField]private GameObject DefaultAbility4;


	private float nextReadyTimeKey1;
	private float nextReadyTimeKey2;
	private float nextReadyTimeKey3;
	private float nextReadyTimeKey4;

	private float ability1Cooldown;
	private float ability2Cooldown;
	private float ability3Cooldown;
	private float ability4Cooldown;

	private float coolDownTimeLeftKey1;
	private float coolDownTimeLeftKey2;
	private float coolDownTimeLeftKey3;
	private float coolDownTimeLeftKey4;
	
	private Ability abilityKey1;
	private Ability abilityKey2;
	private Ability abilityKey3;
	private Ability abilityKey4;

	/*[Space (10)]
	public Image SpellIconKey1ImgOverlay;
	public Image SpellIconKey2ImgOverlay;
	public Image SpellIconKey3ImgOverlay;
	public Image SpellIconKey4ImgOverlay;

	[Space (10)]
	public Text SpellIconKey1TextTimer;
	public Text SpellIconKey2TextTimer;
	public Text SpellIconKey3TextTimer;
	public Text SpellIconKey4TextTimer;*/
	
	/*private float timerForSpellOnKeyOne;
	private float timerForSpellOnKeyTwo;
	private float timerForSpellOnKeyThree;
	private float timerForSpellOnKeyFour;*/

	/*private bool spellKeyOneReady;
	private bool spellKeyTwoReady;
	private bool spellKeyThreeReady;
	private bool spellKeyFourReady;*/

	private PlayerManager playerManager;

	private float imageAplhaTimer = 0;

	[Space (10)]
	[SerializeField]private Transform abilitySpawnPoint;


	void Start () 
	{
		playerManager = GetComponent<PlayerManager>();

		//TODO Check save(Player prefs) data, restore preveious Abilitys
		AbilityNullCheck(ref AbilityObjectKey1, DefaultAbility1 ,"Key 1");
		AbilityNullCheck(ref AbilityObjectKey2, DefaultAbility2 ,"Key 2");
		AbilityNullCheck(ref AbilityObjectKey3, DefaultAbility3 ,"Key 3");
		AbilityNullCheck(ref AbilityObjectKey4, DefaultAbility4 ,"Key 4");

		abilityKey1 = AbilityObjectKey1.GetComponent<Ability>(); 
		abilityKey2 = AbilityObjectKey2.GetComponent<Ability>();
		abilityKey3 = AbilityObjectKey3.GetComponent<Ability>();
		abilityKey4 = AbilityObjectKey4.GetComponent<Ability>();

		ability1Cooldown = abilityKey1.BaseCoolDownTimer;
		ability2Cooldown = abilityKey2.BaseCoolDownTimer;
		ability3Cooldown = abilityKey3.BaseCoolDownTimer;
		ability4Cooldown = abilityKey4.BaseCoolDownTimer;


		//Debug.Log("Timers -> k1, k2, k3, k4  =" +"( " + timerForSpellOnKeyOne +", " + timerForSpellOnKeyTwo + ", " + timerForSpellOnKeyThree + ", " + timerForSpellOnKeyFour + " )");

		//////////////// Problemetic code //////////////////
		// TODO find a smarter solution SpellsController
		abilityKey1.PlayerGameObject = this.gameObject;
		abilityKey2.PlayerGameObject = this.gameObject;
		abilityKey3.PlayerGameObject = this.gameObject;
		abilityKey4.PlayerGameObject = this.gameObject;

		//.GetComponent<Transform>(); // Getting type missmatch on spell gameobjects. I dont know why But its is not cousing prob

		abilityKey1.AbilitySpawnPos = abilitySpawnPoint;  // TODO i need this to spawn spells in the rigth places. but might be to heavy
		abilityKey2.AbilitySpawnPos = abilitySpawnPoint;
		abilityKey3.AbilitySpawnPos = abilitySpawnPoint;
		abilityKey4.AbilitySpawnPos = abilitySpawnPoint;

		//////////////// END//////////////////////////////


		// TODO Find a way so this can be used on multiable players, MULTIPLAYER style

	}	
	
	// Update is called once per frame
	void Update () 
	{
		//CheckKeyPress();
		OnAbilityTrigger(KeyCode.Alpha1, KeyCode.Keypad1, "Key 1", ability1Cooldown, ref nextReadyTimeKey1, coolDownTimeLeftKey1);
		OnAbilityTrigger(KeyCode.Alpha2, KeyCode.Keypad2, "Key 2", ability1Cooldown, ref nextReadyTimeKey2, coolDownTimeLeftKey2);
		OnAbilityTrigger(KeyCode.Alpha3, KeyCode.Keypad3, "Key 3", ability1Cooldown, ref nextReadyTimeKey3, coolDownTimeLeftKey3);
		OnAbilityTrigger(KeyCode.Alpha4, KeyCode.Keypad4, "Key 4", ability1Cooldown, ref nextReadyTimeKey4, coolDownTimeLeftKey4);

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


	private void OnAbilityTrigger(KeyCode key, KeyCode altKey, string keyID, float abilityCoolDown, ref float nextReadyTime, float coolDownTimeLeft )
	{
		bool coolDownComplet = (Time.time > nextReadyTime);

		if(coolDownComplet)
		{
			if(Input.GetKeyDown(key) || Input.GetKeyDown(altKey))
			{
				Debug.Log("Casting ability on " + keyID);
				bool isCastSuccsesful = abilityKey1.Cast();

				if(isCastSuccsesful)
				{
					//RestartCD
					AbilityCastSuccsesful(abilityCoolDown, ref nextReadyTime, coolDownTimeLeft);
					Debug.Log("CAST =  Succsesfull");
				}
				else
				{
					//NO CD
					Debug.Log("CAST = NOT succsesfull");
				}

			}
		}else
		{
			CoolDown(coolDownTimeLeft);
		}
	} 
	private void AbilityCastSuccsesful(float abilityCoolDown, ref float nextReadyTime, float coolDownTimeLeft)
	{
		nextReadyTime = abilityCoolDown + Time.time;
		coolDownTimeLeft = abilityCoolDown;

		/*darkMask.enabled = true;
        coolDownTextDisplay.enabled = true;

		abilitySource.clip = ability.abilitySound;
        abilitySource.Play ();
        ability.TriggerAbility ();*/
	}

	private void CoolDown(float coolDownTimeLeft)
	{
		coolDownTimeLeft -= Time.deltaTime; 
//		float roundedCd = Mathf.Round (coolDownTimeLeft);
//      coolDownTextDisplay.text = roundedCd.ToString ();
//      darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration);
	}
	private void AbilityReady()
    {
        /*coolDownTextDisplay.enabled = false;
        darkMask.enabled = false;*/
    }

	
}

