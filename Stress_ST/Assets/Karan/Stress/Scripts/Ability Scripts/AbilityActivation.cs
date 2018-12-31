using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbilityActivation : ScriptableObject
{
    protected Player player; // This Is the Stats BUT now i need to make a Getcomp to connect the player. If i had a scriptableObject i could just drag it in here and we would be good 
    [SerializeField] protected float manaCost;
    [SerializeField]protected float cooldownTime; // put in Active ?
    
    [SerializeField]protected Image abilityIcon;
    /// <summary>
    /// To crate the Cooldown effect you need somthign to cover the Main ability icon and gradually removed over time
    /// </summary>
    [SerializeField]protected Image abilityIconMask;
    /// <summary>
    /// The text used to display the current cooldown value
    /// </summary>
    [SerializeField]protected Text cooldownNummberText;

    /// <summary>
    /// The target Time.Time when the ability will be ready to be cast. So at the start it will be 0 But when cast The curretn time will be saved and added whith the cooldown time -> TimeWhenAbilityIsReady = Time.time + CooldownTime
    /// </summary>
    protected float TimeWhenAbilityIsReady;// put in Active ?
    /// <summary>
    /// Only purpose of this Timer is to be used for the the CD img effect.
    /// </summary>
    protected float cooldownEffectTimer;// put in Active ?

    public abstract bool Cast();

    public abstract bool IsAbilityOnCooldown(); // What if its a Passiv ability

    public abstract bool CanPayManaCost(); // What if its a Passiv ability

    public abstract void CoolDownImgEffect();

    public void InitializeAbility(Player player)
    {
        this.player = player;

        TimeWhenAbilityIsReady = 0f; // So the ability is ready to go immediately after spawn;
    }

    /// <summary>
    /// Needs an update loop, This methods will allow for Img cd Effect.
    /// </summary>
    
    protected abstract void RestCoolDownImgEffect();

    protected void PayManaCost()
    {
        player.Stats.Mana -= manaCost;
    }
    
}
