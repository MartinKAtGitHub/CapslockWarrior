using UnityEngine;
using UnityEngine.UI;

public class AbilityController : MonoBehaviour
{

    public AbilityActivation AbilityOnKey1;
   
    //public AbilityActivation AbilityOnKey2;
    //public AbilityActivation AbilityOnKey3;
    //public AbilityActivation AbilityOnKey4;

    #region Ability UI CD BOX
    [Space(10)]
    [SerializeField] private Image ability1_UI_Icon;
    //[SerializeField] private Image ability2_UI_Icon;
    //[SerializeField] private Image ability3_UI_Icon;
    //[SerializeField] private Image ability4_UI_Icon;

    [Space(10)]
    [SerializeField] private Image ability1_UI_IconMask;
    //[SerializeField] private Image ability2_UI_IconMask;
    //[SerializeField] private Image ability3_UI_IconMask;
    //[SerializeField] private Image ability4_UI_IconMask;

    [Space(10)]
    [SerializeField] private Text Ability1_UI_Txt;
    //[SerializeField] private Text Ability2_UI_Txt;
    //[SerializeField] private Text Ability3_UI_Txt;
    //[SerializeField] private Text Ability4_UI_Txt;

    #endregion


    private Player player;
    //private PlayerInputManager playerInputManager;
    //private float imageAplhaTimer = 0;
    [Space(10)]
    [SerializeField] private Transform abilitySpawnPoint;
    

    void Start()
    {
        player = GetComponent<Player>();

        InitializeAbility(AbilityOnKey1,player, ability1_UI_Icon, ability1_UI_IconMask, Ability1_UI_Txt);
        //InitializeAbility(AbilityOnKey2,player, ability2_UI_Icon, ability2_UI_IconMask, Ability2_UI_Txt);
        //InitializeAbility(AbilityOnKey3,player, ability3_UI_Icon, ability3_UI_IconMask, Ability3_UI_Txt);
        //InitializeAbility(AbilityOnKey4,player, ability4_UI_Icon, ability4_UI_IconMask, Ability4_UI_Txt);

        // AbilityOnKey2.InitializeAbility(player);
        // AbilityOnKey3.InitializeAbility(player);
        // AbilityOnKey4.InitializeAbility(player);

       
        //GameManager.Instance.PlayerInputManager.OnAbilityKey1Down += IsAbilityPassiv;

    }

    // Update is called once per frame
    void Update()
    {
        AbilityOnKey1.CoolDownImgEffect();
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

    private void InitializeAbility(AbilityActivation ability, Player player, Image uIElement_Icon, Image uIElement_IconMask, Text uIElement_cooldownNumText)
    {
        if(ability != null)
        {
            AbilityOnKey1.InitializeAbility(player, uIElement_Icon, uIElement_IconMask, uIElement_cooldownNumText);
            GameManager.Instance.PlayerInputManager.OnAbilityKey1Down += OnAbilityKey1Press;
        }
    }
    
    private void OnDisable()
    {
        GameManager.Instance.PlayerInputManager.OnAbilityKey1Down -= OnAbilityKey1Press;
    }
}

