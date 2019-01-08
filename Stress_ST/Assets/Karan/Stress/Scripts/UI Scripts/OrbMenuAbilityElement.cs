
using UnityEngine;
using UnityEngine.UI;

public class OrbMenuAbilityElement : MonoBehaviour
{

    [SerializeField] private AbilityActivation ability;
    [SerializeField] private Image abilityIconImg;

    public AbilityActivation Ability { get => ability; }

    // Start is called before the first frame update
    void Start()
    {
        abilityIconImg = GetComponent<Image>();

        Debug.Log(ability.UIElement_Icon.sprite.name);

        //abilityIconImg.sprite = ability.UIElement_Icon.sprite;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
