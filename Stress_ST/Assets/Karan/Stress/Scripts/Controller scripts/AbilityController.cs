﻿using UnityEngine;
using UnityEngine.UI;

public class AbilityController : MonoBehaviour
{

    public AbilityActivation AbilityOnKey1;
    public AbilityActivation AbilityOnKey2;
    public AbilityActivation AbilityOnKey3;
    public AbilityActivation AbilityOnKey4;

    //Kit class -->Ability 1,2,3
    [Space(10)]
    public GameObject AbilityObjectKey1;
    public GameObject AbilityObjectKey2;
    public GameObject AbilityObjectKey3;
    public GameObject AbilityObjectKey4;

    [Space(10)]
    [SerializeField] private GameObject defaultAbility1;
    [SerializeField] private GameObject defaultAbility2;
    [SerializeField] private GameObject defaultAbility3;
    [SerializeField] private GameObject defaultAbility4;

    #region Ability UI
    [Space(10)]
    [SerializeField] private Image ability1_UI_Icon;
    [SerializeField] private Image ability2_UI_Icon;
    [SerializeField] private Image ability3_UI_Icon;
    [SerializeField] private Image ability4_UI_Icon;

    [Space(10)]
    [SerializeField] private Image ability1_UI_IconMask;
    [SerializeField] private Image ability2_UI_IconMask;
    [SerializeField] private Image ability3_UI_IconMask;
    [SerializeField] private Image ability4_UI_IconMask;

    [Space(10)]
    [SerializeField] private Text Ability1_UI_Txt;
    [SerializeField] private Text Ability2_UI_Txt;
    [SerializeField] private Text Ability3_UI_Txt;
    [SerializeField] private Text Ability4_UI_Txt;

    #endregion

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

    private Player player;

    //  private PlayerInputManager playerInputManager;
    //private float imageAplhaTimer = 0;

    [Space(10)]
    [SerializeField] private Transform abilitySpawnPoint;


    void Start()
    {
        player = GetComponent<Player>();

        AbilityOnKey1.InitializeAbility(player, ability1_UI_Icon, ability1_UI_IconMask, Ability1_UI_Txt);

        // AbilityOnKey2.InitializeAbility(player);
        // AbilityOnKey3.InitializeAbility(player);
        // AbilityOnKey4.InitializeAbility(player);

        GameManager.Instance.PlayerInputManager.OnAbilityKey1Down += OnAbilityKey1Press;
        //GameManager.Instance.PlayerInputManager.OnAbilityKey1Down += IsAbilityPassiv;
        #region Old
        //TODO Check save(Player prefs) data, restore preveious Abilitys
        AbilityNullCheck(ref AbilityObjectKey1, defaultAbility1, "Key 1");
        AbilityNullCheck(ref AbilityObjectKey2, defaultAbility2, "Key 2");
        AbilityNullCheck(ref AbilityObjectKey3, defaultAbility3, "Key 3");
        AbilityNullCheck(ref AbilityObjectKey4, defaultAbility4, "Key 4");

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
        //ability1_UI_Icon.sprite = abilityKey1.AbilityImageIcon;
        //ability2_UI_Icon.sprite = abilityKey2.AbilityImageIcon;
        //ability3_UI_Icon.sprite = abilityKey3.AbilityImageIcon;
        //ability4_UI_Icon.sprite = abilityKey4.AbilityImageIcon;

        //ability1_UI_IconMask.sprite = abilityKey1.AbilityImageIcon;
        //ability2_UI_IconMask.sprite = abilityKey2.AbilityImageIcon;
        //ability3_UI_IconMask.sprite = abilityKey3.AbilityImageIcon;
        //ability4_UI_IconMask.sprite = abilityKey4.AbilityImageIcon;

        SetAbilityUIReadyState(ref Ability1_UI_Txt, ref ability1_UI_IconMask); // Move to Ability
        //AbilityReady(ref Ability2_UI_Txt, ref ability2_UI_IconMask);
        //AbilityReady(ref Ability3_UI_Txt, ref ability3_UI_IconMask);
        //AbilityReady(ref Ability4_UI_Txt, ref ability4_UI_IconMask);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        AbilityOnKey1.CoolDownImgEffect();
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
        if (ability == null)
        {
            //			Debug.Log("<color=#800080ff> No spell Set on "+ key +" Setting default spells</color>");
            ability = defaultAbility;
        }
    }

    private void OnAbilityTrigger(Ability ability, float abilityCoolDown, ref float nextReadyTime, ref float coolDownTimeLeft, ref Text abilityIconText, ref Image abilityIconMask)
    {
        bool coolDownComplet = (Time.time > nextReadyTime);//Scrubed

        if (coolDownComplet)
        {
            SetAbilityUIReadyState(ref abilityIconText, ref abilityIconMask);

            if (player.Stats.Mana >= ability.ManaCost)
            {

                bool isCastSuccsesful = ability.Cast();

                if (isCastSuccsesful)
                {
                    //RestartCD
                    AbilityCastSuccsesful(abilityCoolDown, ref nextReadyTime, ref coolDownTimeLeft, ref abilityIconMask, ref abilityIconText);
                    player.Stats.Mana -= ability.ManaCost;
                    //Debug.Log("CAST =  Succsesfull");
                }
            }
            else
            {
                Debug.Log(" <color=blue>NO MANA BZZZZZZ MAKE SOUND OR ICON TO INDICATE THIS</color>");
            }
        }
        else
        {
            CoolDownImgEffect(ref coolDownTimeLeft, ref abilityIconText, ref abilityIconMask, ref abilityCoolDown);
        }
    }

    private void AbilityCastSuccsesful(float abilityCoolDown, ref float nextReadyTime, ref float coolDownTimeLeft, ref Image abilityIconMask, ref Text abilityIconText)
    {
        nextReadyTime = abilityCoolDown + Time.time;
        coolDownTimeLeft = abilityCoolDown;

        abilityIconText.enabled = true;
        abilityIconMask.enabled = true;

        //abilitySource.clip = ability.abilitySound;
        // abilitySource.Play ();
        //ability.TriggerAbility ();
    }

    private void CoolDownImgEffect(ref float coolDownTimeLeft, ref Text abilityIconText, ref Image abilityIconMask, ref float abilityCoolDown)//Scrubed
    {
        coolDownTimeLeft -= Time.deltaTime;
        float roundedCd = Mathf.Round(coolDownTimeLeft);
        abilityIconText.text = roundedCd.ToString();

        abilityIconMask.fillAmount = (coolDownTimeLeft / abilityCoolDown);
    }

    private void SetAbilityUIReadyState(ref Text abilityIconText, ref Image abilityIconMask)
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
        GameManager.Instance.PlayerInputManager.OnAbilityKey1Down -= OnAbilityKey1Press;
    }

    private void CastAbility(AbilityActivation ability)
    {
        var abilityName = ability.name;
        if (ability.IsAbilityOnCooldown())
        {
            if (ability.CanPayManaCost())
            {
                var castStatus = ability.Cast();

                if (castStatus)
                {
                    Debug.Log(abilityName + " <= Cast Succsesful");
                }
                else
                {
                    Debug.Log(abilityName + " <= Cast Failed");
                }

            }
            else
            {
                Debug.Log(" <color=blue>NO MANA BZZZZZZ MAKE SOUND OR ICON TO INDICATE THIS</color>");
            }
        }
        else
        {
            Debug.Log(abilityName + " <color=darkblue>On CD</color>");
        }

    }

    private void OnAbilityKey1Press() // Mayeb change The Event to handle Parameters
    {
        CastAbility(AbilityOnKey1);
    }
}

