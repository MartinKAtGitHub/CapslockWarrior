using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AbilityController : MonoBehaviour {



	//Kit class -->Ability 1,2,3

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

		//////////////// Problemetic code //////////////////

		// TODO find a smarter solution SpellsController -> maybe singelton Gamemanger
		abilityKey1.PlayerGameObject = this.gameObject;
		abilityKey2.PlayerGameObject = this.gameObject;
		abilityKey3.PlayerGameObject = this.gameObject;
		abilityKey4.PlayerGameObject = this.gameObject;

		abilityKey1.AbilitySpawnPos = abilitySpawnPoint;
		abilityKey2.AbilitySpawnPos = abilitySpawnPoint;
		abilityKey3.AbilitySpawnPos = abilitySpawnPoint;
		abilityKey4.AbilitySpawnPos = abilitySpawnPoint;

		//////////////// END //////////////////////////////


	}	
	
	// Update is called once per frame
	void Update () 
	{
		//CheckKeyPress();
		OnAbilityTrigger(abilityKey1, KeyCode.Alpha1, KeyCode.Keypad1, "Key 1", ability1Cooldown, ref nextReadyTimeKey1, coolDownTimeLeftKey1);
		OnAbilityTrigger(abilityKey2, KeyCode.Alpha2, KeyCode.Keypad2, "Key 2", ability2Cooldown, ref nextReadyTimeKey2, coolDownTimeLeftKey2);
		OnAbilityTrigger(abilityKey3, KeyCode.Alpha3, KeyCode.Keypad3, "Key 3", ability3Cooldown, ref nextReadyTimeKey3, coolDownTimeLeftKey3);
		OnAbilityTrigger(abilityKey4, KeyCode.Alpha4, KeyCode.Keypad4, "Key 4", ability4Cooldown, ref nextReadyTimeKey4, coolDownTimeLeftKey4);

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


	private void OnAbilityTrigger(Ability ability, KeyCode key, KeyCode altKey, string keyID, float abilityCoolDown, ref float nextReadyTime, float coolDownTimeLeft )
	{
		bool coolDownComplet = (Time.time > nextReadyTime);

		if(coolDownComplet)
		{
			if(Input.GetKeyDown(key) || Input.GetKeyDown(altKey))
			{
				Debug.Log("Casting ability on " + keyID);
				bool isCastSuccsesful = ability.Cast();

				if(isCastSuccsesful)
				{
					//RestartCD
					AbilityCastSuccsesful(abilityCoolDown, ref nextReadyTime, coolDownTimeLeft);
					//Debug.Log("CAST =  Succsesfull");
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
		//Debug.Log("Missing CD Ability Icon");
//		float roundedCd = Mathf.Round (coolDownTimeLeft);
//      coolDownTextDisplay.text = roundedCd.ToString ();
//      darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration);
	}
	private void AbilityReady()
    {
        /*coolDownTextDisplay.enabled = false;
        darkMask.enabled = false;*/
    }

    private void IsAbilityPassiv()
    {
    	// If(isAbilityPassiv)
    		// Start passivAbility
    		//Else dont do anythig
    }
	
}

