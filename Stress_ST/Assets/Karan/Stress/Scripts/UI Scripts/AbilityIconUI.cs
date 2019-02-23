
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This should only be assgining Icons BUT its actuly casting the ability and Initializeing them
/// </summary>
public class AbilityIconUI : MonoBehaviour
{

    private Ability abilityOnIcon;

    public Image Icon;
    public Image IconMask;
    public TextMeshProUGUI CoolDownNumsTxt;

    public Ability AbilityOnIcon { get => abilityOnIcon; }

    void Awake()
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
            CoolDownNumsTxt = GetComponentInChildren<TextMeshProUGUI>();

        }
    }
}
