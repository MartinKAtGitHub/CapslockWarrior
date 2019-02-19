using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Connects Abilities to Keyspress events and UI icons
/// </summary>
public class AbilityController : MonoBehaviour
{
    public Sprite ErrorAbilitySprite;

    public AbilityIconUI[] AbilityIcons;

    //public Ability[] abilities;

    private Player player;
    private PlayerInputManager playerInputManager;

    [Space(10)]
    [SerializeField] private Transform abilitySpawnPoint;

    //public bool testboolTRIGGERICONGEN;
    void Start()
    {
        playerInputManager = GameManager.Instance.PlayerInputManager;

        // GET ALL MY ABILITES FROM ORBMENU(GameManger) --here
        // Generate AB UI ICONS

        player = GetComponent<Player>();
        //Get Abilities
        GenerateActiveAbilitesUIICons();

        for (int i = 0; i < playerInputManager.AbilityKeyCodes.Length; i++)
        {
            // playerInputManager.AbilityKeyDownAction[i] += Abilities[i].CastAbility;
            playerInputManager.AbilityKeyDownAction[i] += AbilityIcons[i].CastAbility;
        }
    }

    void Update()
    {
        //if (testboolTRIGGERICONGEN)
        //{
        //    GenerateActiveAbilitesUIICons();
        //    testboolTRIGGERICONGEN = false;
        //}

        for (int i = 0; i < 10/*ActiveAbilitesList.count*/; i++) // The max amount of icons is 10, so i hard coded it but this needs to change
        {
            AbilityIcons[i].AbilityOnIcon?.CoolDownImgEffect();
        }
    }
       
    private void OnDisable()
    {
        for (int i = 0; i < playerInputManager.AbilityKeyCodes.Length; i++)
        {
            playerInputManager.AbilityKeyDownAction[i] -= AbilityIcons[i].CastAbility;
        };
    }

    private void GenerateActiveAbilitesUIICons()
    {
        //PERFORMANCE Ability controller > maybe cahce the call to  GameManager.Instance.OrbSystemMenuManager.abilityKeyDropZones
        for (int i = 0; i <  GameManager.Instance.OrbSystemMenuManager.abilityKeyDropZones.Length; i++)
        {
            var OrbMenuAbility = GameManager.Instance.OrbSystemMenuManager.abilityKeyDropZones[i].orbMenuAbility;
            if (OrbMenuAbility != null)
            {
                // Abilities[i] = OrbMenuAbility.Ability.
                AbilityIcons[i].gameObject.SetActive(true); // Maybe just Intantiante becaouse it will only happen in start()
                AbilityIcons[i].InitializeAbilityIcon(OrbMenuAbility.Ability, player);   
            }
            else
            {
                AbilityIcons[i].gameObject.SetActive(false);
            }
        }
    }
}


