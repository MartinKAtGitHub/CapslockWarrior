
using UnityEngine;
using UnityEngine.UI;

public class AbilityIconUI : MonoBehaviour
{

    private AbilityActivation abilityOnIcon;

    public Image Icon;
    public Image IconMask;
    public Text CoolDownNumsTxt;

    public AbilityActivation AbilityOnIcon { get => abilityOnIcon; }

    void Start()
    {
        if (Icon == null)
        {
            Icon = GetComponent<Image>();
            Debug.Log("The Ability Icon(image) Is NULL");
        }
        if (IconMask == null)
        {
            IconMask = GetComponentInChildren<Image>();
        }
        if (CoolDownNumsTxt == null)
        {
            CoolDownNumsTxt = GetComponentInChildren<Text>();

        }

    }
    public void InitializeAbilityIcon(AbilityActivation ability,Player player)
    {
        abilityOnIcon = ability;
        abilityOnIcon.InitializeAbility(player, Icon, IconMask, CoolDownNumsTxt);
    }

    private void CastAbilityProcess(AbilityActivation ability)
    {
        if (ability != null)
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
        else
        {
            Debug.LogWarning("No ability to Cast() pls assign ability");
        }
    }

    public void CastAbility()
    {
        CastAbilityProcess(abilityOnIcon);
    }
}
