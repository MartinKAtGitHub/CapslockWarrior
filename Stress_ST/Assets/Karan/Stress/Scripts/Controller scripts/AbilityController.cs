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
	[SerializeField]private GameObject defaultAbility1;
	[SerializeField]private GameObject defaultAbility2;
	[SerializeField]private GameObject defaultAbility3;
	[SerializeField]private GameObject defaultAbility4;

	[Space (10)]
	[SerializeField]private Image ability1Icon;
	[SerializeField]private Image ability2Icon;
	[SerializeField]private Image ability3Icon;
	[SerializeField]private Image ability4Icon;

	[Space (10)]
	[SerializeField]private Image ability1IconMask;
	[SerializeField]private Image ability2IconMask;
	[SerializeField]private Image ability3IconMask;
	[SerializeField]private Image ability4IconMask;

	[Space (10)]
	[SerializeField]private Text Ability1Txt;
	[SerializeField]private Text Ability2Txt;
	[SerializeField]private Text Ability3Txt;
	[SerializeField]private Text Ability4Txt;

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

	private Character player;

  //  private PlayerInputManager playerInputManager;
	//private float imageAplhaTimer = 0;

	[Space (10)]
	[SerializeField]private Transform abilitySpawnPoint;


	void Start () 
	{
		player = GetComponent<Player>();


      //GameManager.Instance.PlayerInputManager.OnAbilityKey1Down += IsAbilityPassiv;

		//TODO Check save(Player prefs) data, restore preveious Abilitys
		AbilityNullCheck(ref AbilityObjectKey1, defaultAbility1 ,"Key 1");
		AbilityNullCheck(ref AbilityObjectKey2, defaultAbility2 ,"Key 2");
		AbilityNullCheck(ref AbilityObjectKey3, defaultAbility3 ,"Key 3");
		AbilityNullCheck(ref AbilityObjectKey4, defaultAbility4 ,"Key 4");

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
		ability1Icon.sprite = abilityKey1.AbilityImageIcon;
		ability2Icon.sprite = abilityKey2.AbilityImageIcon;
		ability3Icon.sprite = abilityKey3.AbilityImageIcon;
		ability4Icon.sprite = abilityKey4.AbilityImageIcon;

		ability1IconMask.sprite = abilityKey1.AbilityImageIcon;
		ability2IconMask.sprite = abilityKey2.AbilityImageIcon;
		ability3IconMask.sprite = abilityKey3.AbilityImageIcon;
		ability4IconMask.sprite = abilityKey4.AbilityImageIcon;

		AbilityReady(ref Ability1Txt, ref ability1IconMask);
		AbilityReady(ref Ability2Txt, ref ability2IconMask);
		AbilityReady(ref Ability3Txt, ref ability3IconMask);
		AbilityReady(ref Ability4Txt, ref ability4IconMask);

	}	
	
	// Update is called once per frame
	void Update () 
	{
		//CheckKeyPress();
		//OnAbilityTrigger(abilityKey1, KeyCode.Alpha1, KeyCode.Keypad1, "Key 1", ability1Cooldown, ref nextReadyTimeKey1, ref coolDownTimeLeftKey1, ref Ability1Txt, ref ability1IconMask);
		//OnAbilityTrigger(abilityKey2, KeyCode.Alpha2, KeyCode.Keypad2, "Key 2", ability2Cooldown, ref nextReadyTimeKey2, ref coolDownTimeLeftKey2, ref Ability2Txt, ref ability2IconMask);
		//OnAbilityTrigger(abilityKey3, KeyCode.Alpha3, KeyCode.Keypad3, "Key 3", ability3Cooldown, ref nextReadyTimeKey3, ref coolDownTimeLeftKey3, ref Ability3Txt, ref ability3IconMask);
		//OnAbilityTrigger(abilityKey4, KeyCode.Alpha4, KeyCode.Keypad4, "Key 4", ability4Cooldown, ref nextReadyTimeKey4, ref coolDownTimeLeftKey4, ref Ability4Txt, ref ability4IconMask);

	}


	/// <summary>
	/// Abilities the null check. Sets a Default spell to the key that has no spell selected
	/// </summary>
	private void AbilityNullCheck(ref GameObject ability, GameObject defaultAbility, string key)
	{
		if(ability == null)
		{
//			Debug.Log("<color=#800080ff> No spell Set on "+ key +" Setting default spells</color>");
			ability = defaultAbility;
		}
	}

	private void OnAbilityTrigger(Ability ability, KeyCode key, KeyCode altKey,
								 string keyID, float abilityCoolDown, ref float nextReadyTime, ref float coolDownTimeLeft,
								 ref Text abilityIconText, ref Image abilityIconMask )
	{
		bool coolDownComplet = (Time.time > nextReadyTime);
	
		if(coolDownComplet)
		{
			AbilityReady(ref abilityIconText, ref abilityIconMask);

			//if(Input.GetKeyDown(key) || Input.GetKeyDown(altKey))
			//{
				if(player.Stats.Mana >= ability.ManaCost)
				{
					Debug.Log("Casting ability on " + keyID);
					bool isCastSuccsesful = ability.Cast();
					
					if(isCastSuccsesful)
					{
						//RestartCD
						AbilityCastSuccsesful(abilityCoolDown, ref nextReadyTime, ref coolDownTimeLeft, ref abilityIconMask, ref abilityIconText);
						player.Stats.Mana -=  ability.ManaCost;
						//Debug.Log("CAST =  Succsesfull");
					}
				}
				else
				{
					Debug.Log(" <color=blue>NO MANA BZZZZZZ MAKE SOUND OR ICON TO INDICATE THIS</color>");
				}
			//}
		}
		else
		{
			CoolDown(ref coolDownTimeLeft, ref abilityIconText, ref abilityIconMask, ref abilityCoolDown);
		}
	} 

	private void AbilityCastSuccsesful(float abilityCoolDown, ref float nextReadyTime, ref float coolDownTimeLeft , ref Image abilityIconMask, ref Text abilityIconText)
	{
		nextReadyTime = abilityCoolDown + Time.time;
		coolDownTimeLeft = abilityCoolDown;

		abilityIconText.enabled = true;
		abilityIconMask.enabled = true;

		//abilitySource.clip = ability.abilitySound;
       // abilitySource.Play ();
        //ability.TriggerAbility ();
	}

	private void CoolDown(ref float coolDownTimeLeft, ref Text abilityIconText, ref Image abilityIconMask, ref float abilityCoolDown )
	{
		coolDownTimeLeft -= Time.deltaTime;
		float roundedCd = Mathf.Round (coolDownTimeLeft);
	    abilityIconText.text = roundedCd.ToString ();
		
		abilityIconMask.fillAmount = (coolDownTimeLeft / abilityCoolDown);
	}

	private void AbilityReady(ref Text abilityIconText, ref Image abilityIconMask )
    {
		abilityIconText.enabled = false;
		abilityIconMask.enabled = false;
    }

    private void IsAbilityPassiv()
    {
        // If(isAbilityPassiv)
        // Start passivAbility
        //Else dont do anythig
      //  OnAbilityTrigger(abilityKey4, KeyCode.Alpha4, KeyCode.Keypad4, "Key 4", ability4Cooldown, ref nextReadyTimeKey4, ref coolDownTimeLeftKey4, ref Ability4Txt, ref ability4IconMask);

        Debug.Log("ABILITY IS PASSIV");
    }


    private void OnDisable()
    {
        // unsub from events
    }
}

