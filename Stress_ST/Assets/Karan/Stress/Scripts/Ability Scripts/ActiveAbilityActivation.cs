using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveAbilityActivation : AbilityActivation
{

    /// <summary>
    /// The prefab that holds the main logic for the Ability
    /// </summary>
    [SerializeField] protected Ability abilityPrefab;
    
    public override bool IsAbilityOnCooldown() // What if its a Passiv ability
    {
        if (Time.time > TimeWhenAbilityIsReady)
        {
            TimeWhenAbilityIsReady = cooldownTime + Time.time;
            RestCoolDownImgEffect();

            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool CanPayManaCost() // What if its a Passiv ability
    {
        if (player.Stats.Mana >= manaCost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    /// <summary>
    /// Needs an update loop, This methods will allow for Img cd Effect.
    /// </summary>
    public override void CoolDownImgEffect()
    {
        if (cooldownEffectTimer > 0)
        {
            cooldownEffectTimer -= Time.deltaTime;
            float roundedCd = Mathf.Round(cooldownEffectTimer);
            cooldownNummberText.text = roundedCd.ToString();

            abilityIconMask.fillAmount = (cooldownEffectTimer / cooldownTime);
        }
        else
        {
            return;
        }
    }

    protected override void RestCoolDownImgEffect()
    {
        cooldownEffectTimer = cooldownTime;
    }

   // public abstract bool Cast();
    
}
