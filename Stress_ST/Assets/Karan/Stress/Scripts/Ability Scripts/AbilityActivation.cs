
using UnityEngine;
using UnityEngine.UI;

public abstract class AbilityActivation : ScriptableObject // TODO change the name to Ability
{
    
    protected Player player; // This Is the Stats BUT now i need to make a Getcomp to connect the player. If i had a scriptableObject i could just drag it in here and we would be good 

    [SerializeField] protected Sprite AbilityIcon;
    [SerializeField] protected float manaCost;
    [SerializeField] protected float cooldownTime; // put in Active ?
    

    protected Image UIElement_Icon;
    /// <summary>
    /// To crate the Cooldown effect you need somthign to cover the Main ability icon and gradually removed over time
    /// </summary>
    protected Image UIElement_IconMask;
    /// <summary>
    /// The text used to display the current cooldown value
    /// </summary>
    [SerializeField]protected Text UIElement_cooldownNumText;

   
    /// <summary>
    /// Only purpose of this Timer is to be used for the the CD img effect.
    /// </summary>
    protected float cooldownEffectTimer;// put in Active ?

    //DISCRIPTION() Somthing to tell the user what this ability is aboutMaybe take a Discr SO


    public abstract bool Cast();

    public abstract bool IsAbilityOnCooldown(); // What if its a Passiv ability --> overider to do nothing

    public abstract bool CanPayManaCost(); // What if its a Passiv ability --> overider to do nothing

    public abstract void CoolDownImgEffect(); // What if its a Passiv ability --> overider to do nothing

    public virtual void InitializeAbility(Player player, Image uIElement_Icon, Image uIElement_IconMask, Text uIElement_cooldownNumText)
    {
        this.player = player;
        UIElement_Icon = uIElement_Icon;
        UIElement_IconMask = uIElement_IconMask;
        UIElement_cooldownNumText = uIElement_cooldownNumText;

        SetSpriteToAbilityUIElements();

        Debug.Log("INIT Base Ability");
    }

    /// <summary>
    /// Needs an update loop, This methods will allow for Img cd Effect.
    /// </summary>
    
    protected abstract void RestCoolDownImgEffect();

    protected void PayManaCost()
    {
        player.Stats.Mana -= manaCost;
    }
    
    private void SetSpriteToAbilityUIElements()
    {
        UIElement_Icon.sprite = AbilityIcon;
        UIElement_IconMask.sprite = AbilityIcon;
    }
}
