using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : CharacterHealthSystem
{
    /// <summary>
    /// Its the img behind the HP Bar. So everything will be ontop of this element / this is parent OBJ
    /// </summary>
    [SerializeField] private RectTransform healthBackground;
    /// <summary>
    /// The Main Healt bar, will be instantly changed on DMG. Top most element
    /// </summary>
    [SerializeField] private Slider healthBar_Foreground_slider;
    /// <summary>
    /// The secondery healthbar, slowly change to the correct value. The middel element
    /// </summary>
    [SerializeField] private Slider healthBar_Background_slider;

    [SerializeField]private float HPBarChangeRate;

    [Range(0,1)]
    public float TEST;
    Vector2 healthBarsMinMax;

    float t;
   // int dmgTaken;
    private void Awake()
    {
        GetCharacterComponant();
    }

    // If i need to override somthing player spesific do it here. Dont make checks to see what system is beeing used just override the call

    private void Start()
    {
     //   SetHealthBarToMaxHP();
      //  SetInitialHP();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            // TakeDamage(10);
           // healthBar_Background_slider.value = Mathf.Lerp(1f, 0.1f, Time.deltaTime *5f);
        }

        //Debug.Log(Mathf.Lerp(1f, TEST, Time.deltaTime * 0.1f));
         t += Time.deltaTime *0.1f;

        healthBar_Background_slider.value = Mathf.Lerp(1f, TEST , t);

    }





    protected override void OnCharacterDeath()
    {
        Debug.Log("Trigger on PLAYER DEATH EVENT");
    }

    public override void TakeDamage(int dmg)
    {
        Character.Stats.Health -= dmg;
        ChangeForegroundHPBar();
        StartCoroutine(ChangeBackgroundHPBar());

        if (Character.Stats.Health <= 0)
        {
            OnCharacterDeath();
        }

       
    }



    private void ChangeForegroundHPBar() // Insatnt
    {
        var test = CalculateHealthPercentage();
        Debug.Log("TEST = " + test);
        healthBar_Foreground_slider.value = test;
    }

    private IEnumerator ChangeBackgroundHPBar()// over time
    {
        //healthBar_Foreground_slider.value = CalculateHealthPercentage();
      
        var lerpValue = 0f;
        var currentHPPrecent = CalculateHealthPercentage();

       Debug.Log("LerpValu = " + lerpValue + "HP PERCENT = " + currentHPPrecent + "BOOL = " + (currentHPPrecent >= lerpValue));

       /* while (currentHPPrecent >= lerpValue )
        {
            lerpValue = Mathf.Lerp(healthBar_Background_slider.value, currentHPPrecent, Time.deltaTime * HPBarChangeRate);
            healthBar_Background_slider.value = lerpValue;
            Debug.Log("Lval = " + lerpValue + "HP p = " + currentHPPrecent);
        }*/


        yield return null;
    }

    float CalculateHealthPercentage()
    {
        return Character.Stats.Health / Character.Stats.BaseHealth; // I think the Slider comp Clamps teh value so we cant go below 0
    }
    
    protected override void GetCharacterComponant()
    {
        Character = GetComponent<Player>();
    }

    protected override void SetInitialHP()
    {
        healthBar_Foreground_slider.value = CalculateHealthPercentage();
        healthBar_Background_slider.value = CalculateHealthPercentage();
    }

    private void SetHealthBarToMaxHP()
    {
        healthBackground.sizeDelta = new Vector2(Character.Stats.BaseHealth, healthBackground.sizeDelta.y);
            
    }
}
