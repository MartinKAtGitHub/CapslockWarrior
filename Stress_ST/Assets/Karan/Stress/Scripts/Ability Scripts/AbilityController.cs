using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Connects Abilities to Keyspress events and UI icons
/// </summary>
public class AbilityController : MonoBehaviour
{
    [Tooltip("If you want to Debug Abilities make this true And drag and drop Abilites Scriptable object intro the arry")]
    public bool DebugAbilities;

    [Tooltip("If you want to Debug Abilities make this true And drag and drop Abilites Scriptable object intro the arry")]
    [SerializeField] private Ability[] abilities;
    [SerializeField] private AbilityIconUI[] abilityIcons; // Manual Drag n Drop


    private Player player;
    private PlayerInputManager playerInputManager;

    [Space(10)]
    [SerializeField] private Transform abilitySpawnPoint;

    void Start()
    {
        playerInputManager = GameManager.Instance.PlayerInputManager;
        abilityIcons = GetComponentsInChildren<AbilityIconUI>(true); // Find inactive objects = true    
        player = GetComponent<Player>();
     
        InitializeAbilieties();

       // for (int i = 0; i < playerInputManager.AbilityKeyCodes.Length; i++) //TODO create a message for the Keys that dont have a ABility on them but are active
        for (int i = 0; i < abilities.Length; i++)
        {
            if (abilities[i] is ActiveAbility)
            {
                var activeAbility = abilities[i] as ActiveAbility;
                playerInputManager.AbilityKeyDownAction[i] += activeAbility.CastAbility;
            }
            else
            {
                var passivAbility = abilities[i] as PassivAbility;
                passivAbility.ActivatePassivAbility();
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            abilities[i].CoolDownImgEffect();
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            var activeAbility = abilities[i] as ActiveAbility;
            playerInputManager.AbilityKeyDownAction[i] -= activeAbility.CastAbility;
        }
    }

    /// <summary>
    /// Connects the Selecetd abilties from the OrbMenu to the player UI ICONS and INPUTKEYS
    /// </summary>
    private void InitializeAbilieties()
    {
        var abilitieCount = GameManager.Instance.OrbSystemMenuManager.abilityKeyDropZones.Length; // All the abilites you have selected in th OrbMenu

        if (DebugAbilities)
        {
            for (int i = 0; i < abilities.Length; i++)
            {
                abilities[i].InitializeAbility(player, abilityIcons[i].Icon, abilityIcons[i].IconMask, abilityIcons[i].CoolDownNumsTxt);
                abilityIcons[i].gameObject.SetActive(true); // Maybe just Intantiante becaouse it will only happen in start()
            }
        }
        else
        {
            abilities = new Ability[abilitieCount];
        }

        for (int i = 0; i < abilitieCount; i++)
        {
            var OrbMenuAbility = GameManager.Instance.OrbSystemMenuManager.abilityKeyDropZones[i].orbMenuAbility;

            if (OrbMenuAbility != null)
            {
                abilities[i] = OrbMenuAbility.Ability;
                abilities[i].InitializeAbility(player, abilityIcons[i].Icon, abilityIcons[i].IconMask, abilityIcons[i].CoolDownNumsTxt);

                abilityIcons[i].gameObject.SetActive(true); // Maybe just Intantiante becaouse it will only happen in start()
                //abilityIcons[i].InitializeAbilityIcon(OrbMenuAbility.Ability, player);   
            }
            else
            {
                abilityIcons[i].gameObject.SetActive(false);
            }
        }
    }
}


