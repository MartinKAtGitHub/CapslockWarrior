
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class OrbSystemAbilityIcon : MonoBehaviour
{

    [SerializeField] private AbilityActivation ability; //This will be a List of ABility + Upgraded
    [SerializeField] private Image abilityIconImg;

    public AbilityActivation Ability { get => ability; }// Return the element Based on how meny Orbs or located on the DropZone

    // Start is called before the first frame update
    void Start()
    {
        abilityIconImg = GetComponent<Image>();
        abilityIconImg.sprite = ability.AbilityIcon;
    }
}
