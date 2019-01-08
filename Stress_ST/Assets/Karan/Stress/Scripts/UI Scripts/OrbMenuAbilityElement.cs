
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class OrbMenuAbilityElement : MonoBehaviour
{

    [SerializeField] private AbilityActivation ability;
    [SerializeField] private Image abilityIconImg;

    public AbilityActivation Ability { get => ability; }

    // Start is called before the first frame update
    void Start()
    {
        abilityIconImg = GetComponent<Image>();

        abilityIconImg.sprite = ability.AbilityIcon;
    }
}
