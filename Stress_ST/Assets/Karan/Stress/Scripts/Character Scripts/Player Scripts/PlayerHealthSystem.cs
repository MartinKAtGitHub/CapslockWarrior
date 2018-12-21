using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : CharacterHealthSystem
{
    /// <summary>
    /// Its the img behind the HP Bar. So everything will be ontop of this element / this is parent OBJ
    /// </summary>
    [SerializeField] private RectTransform healtBackground;
    /// <summary>
    /// The Main Healt bar, will be instantly changed on DMG. Top most element
    /// </summary>
    [SerializeField] private RectTransform healthBar_Foreground;
    /// <summary>
    /// The secondery healthbar, slowly change to the correct value. The middel element
    /// </summary>
    [SerializeField] private RectTransform healthBar_Background;

    Vector2 healthBarsMinMax;
   

    private void Awake()
    {
        Character = GetComponent<Player>();
        SetMaxHealthToHealthBar();


        //healthBarsMinMax = new Vector2(healtBackground.sizeDelta.x - healthBar_Foreground.rect.xMin, healthBar_Foreground.rect.xMax);

        // healthBarsMinMax = healthBar_Foreground.sizeDelta.normalized;
        // Debug.Log((healthBarsMinMax.x * 0.5)  );

        TakeDamage(30);

       
    }

    // If i need to override somthing player spesific do it here. Dont make checks to see what system is beeing used just override the call

    
     

    protected override void OnCharacterDeath()
    {
        Debug.Log("Trigger on PLAYER DEATH EVENT");
    }

    public override void TakeDamage(int dmg)
    {
        Debug.Log("DMG = " + dmg);
        Character.Stats.Health -= dmg;

        Debug.Log("CHAR HP = "+ Character.Stats.Health);

        SetForegroundHPBar(Character.Stats.Health);
    }


    void SetMaxHealthToHealthBar()
    {
        healtBackground.sizeDelta = new Vector2(Character.Stats.BaseHealth, healtBackground.sizeDelta.y);
    }

    
    void SetForegroundHPBar(int currentHealth)
    {
        currentHealth =  Mathf.Clamp(currentHealth, (int)healthBar_Foreground.offsetMax.x /* 0 */, Character.Stats.BaseHealth); //Character.Stats.BaseHealth = healtBackground.sizeDelta.x

        currentHealth *= -1; // Convert

        Debug.Log(currentHealth);

        healthBar_Foreground.sizeDelta = new Vector2(currentHealth , healthBar_Foreground.sizeDelta.y);
    }
}
