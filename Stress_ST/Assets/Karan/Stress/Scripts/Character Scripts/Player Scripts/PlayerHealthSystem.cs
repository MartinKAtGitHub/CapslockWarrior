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

    [SerializeField] private float healthBarChangeDuration;

    public bool OnHealthChange { get; private set; }

    private float startTimer; // used if we want correct lerp calc
                              //private float InitialtHP;
    private float onDmgBackgroundHPBarValue;
    // public float startPoint;

    private float lerpCounter;
    // int dmgTaken;


    //private void Awake()
    //{
    //    GetCharacterComponant();
    //}

    // If i need to override somthing player spesific do it here. Dont make checks to see what system is beeing used just override the call

    private void Start()
    {
        SetHPBackgroundToMaxHP();
        SetHPBarsToMax();

        InvokeRepeating("RegenerationSystem", 1f, 1f);
    }

    private void Update()
    {

        if (OnHealthChange)
        {
            ChangeBackgroundHPBar();
        }
        //Debug.Log(Mathf.Lerp(1f, TEST, Time.deltaTime * 0.1f));
    }

    protected override void OnCharacterDeath()
    {
        Debug.Log("Trigger on PLAYER DEATH EVENT");
    }

    public override void TakeDamage(int dmg)
    {
        startTimer = Time.time;
        OnHealthChange = true;
        onDmgBackgroundHPBarValue = healthBar_Background_slider.value;

        lerpCounter = 0;

        Character.Stats.Health -= dmg;
        ChangeForegroundHPBar();

        if (Character.Stats.Health <= 0)
        {
            OnCharacterDeath();
        }
    }

    private void ChangeForegroundHPBar() // Insatnt
    {
        var currentHPPer = CalculateHealthPercentage();
        healthBar_Foreground_slider.value = currentHPPer;
    }

    private void ChangeBackgroundHPBar() // over time
    {
        // var lerpHPValue = (Time.time - startTimer) / healthBarChangeDuration; // This increases the lerp value properly
        lerpCounter += Time.deltaTime * healthBarChangeDuration; // This increases ler value incorrect but creates a smooth end
        healthBar_Background_slider.value = Mathf.Lerp(onDmgBackgroundHPBarValue, healthBar_Foreground_slider.value, lerpCounter);

        if (healthBar_Background_slider.value <= healthBar_Foreground_slider.value)
        {
            OnHealthChange = false;
        }
    }

    private IEnumerator ChangeBackgroundHPBar_IE() // This is somthing needs to be updated to be used if we everant to ReGain HP (Bloodborn style) with attacks
    {
        var startTime = Time.time;
        //healthBar_Foreground_slider.value = CalculateHealthPercentage();
        var InitialtHP = healthBar_Background_slider.value;
        var lerpHPValue = 0f;
        var currentHPPrecent = CalculateHealthPercentage();

        while (lerpHPValue < currentHPPrecent)
        {
            // lerpValue = Mathf.Lerp(healthBar_Background_slider.value, currentHPPrecent, Time.deltaTime * HPBarChangeRate);

            //   healthBar_Background_slider.value = lerpValue;
            //   Debug.Log("Lval = " + lerpValue + "HP p = " + currentHPPrecent);


            // lerpHPValue += Time.deltaTime * 0.1f;
            //healthBar_Background_slider.value = Mathf.Lerp(InitialtHP, currentHPPrecent, lerpHPValue);

            lerpHPValue = (Time.time - startTime) / healthBarChangeDuration;
            healthBar_Background_slider.value = Mathf.Lerp(InitialtHP, healthBar_Foreground_slider.value, lerpHPValue);

            // Debug.Log("LerpValu = " + lerpHPValue + " HP PERCENT = " + currentHPPrecent + " BOOL = " + (lerpHPValue <= currentHPPrecent));
            Debug.LogFormat("Lerp({0}, {1}, {2})", InitialtHP, currentHPPrecent, lerpHPValue);
            //yield return new WaitForEndOfFrame();

            // Debug.Log("STACK " + currentHPPrecent);
            yield return null;
        }
        healthBar_Background_slider.value = healthBar_Foreground_slider.value;
        yield return null;
    }

    float CalculateHealthPercentage()
    {
        // Debug.Log("PERCENTAGE == " + Character.Stats.Health / Character.Stats.BaseHealth);
        return Character.Stats.Health / Character.Stats.BaseHealth; // I think the Slider comp Clamps teh value so we cant go below 0
    }

    protected override void GetCharacterComponant()
    {
        Character = GetComponent<Player>();
    }

    protected void SetHPBarsToMax()
    {
        healthBar_Foreground_slider.value = CalculateHealthPercentage();
        healthBar_Background_slider.value = CalculateHealthPercentage();
    }

    private void SetHPBackgroundToMaxHP()
    {
        healthBackground.sizeDelta = new Vector2(Character.Stats.BaseHealth, healthBackground.sizeDelta.y);
    }


    private void RegenerationSystem()
    {
        Character.Stats.Health += HealthRegenAmount;
    }
}
