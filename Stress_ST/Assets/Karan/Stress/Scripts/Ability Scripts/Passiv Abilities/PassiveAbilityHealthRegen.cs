using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/PassiveAbilities/HealthRegenT1")]
public class PassiveAbilityHealthRegen : PassivAbility
{
    /// <summary>
    /// The amount per sec the player will regenerate health
    /// </summary>
    [SerializeField]private float healthRegenAmount;
    public override void ActivatePassivAbility()
    {
        Debug.Log("Activating passiv ability (" + name + ") Power =" + healthRegenAmount);
        AbilityLogic();
    }

    protected override bool AbilityLogic()
    {
        player.PlayerHealthSystem.HealthRegenAmount = healthRegenAmount;
        return true;
    }
}
