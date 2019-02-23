
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class OrbSystemAbilityIcon : MonoBehaviour
{

    [SerializeField] private Ability ability; //This will be a List of ABility + Upgraded
    [SerializeField] private Image abilityIconImg;

    public Ability Ability { get => ability; }// Return the element Based on how meny Orbs or located on the DropZone

    // Start is called before the first frame update
    void Start()
    {
        if(ability!= null)
        {
           // abilityIconImg = GetComponentInChildren<Image>(); dosent work
            abilityIconImg.sprite = ability.AbilityIcon;
        }else
        {
            Debug.LogWarning("No ABILITY SET, Cant Find Img for Ability Drag obj");
        }
    }


/*#if UNITY_EDITOR
    private void Update()
    {
        if (ability != null)
        {
            abilityIconImg = GetComponent<Image>();
            abilityIconImg.sprite = ability.AbilityIcon;
        }
        else
        {
            Debug.LogWarning("No ABILITY SET, Cant Find Img for Ability Drag obj");
        }

        Debug.Log("Asdjahdkjashdkjashdkjashdkas");
    }
#endif*/
}
