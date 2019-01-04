using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassivAbilityActivation : AbilityActivation
{

    public override void InitializeAbility(Player player, Image uIElement_Icon, Image uIElement_IconMask, Text uIElement_cooldownNumText)
    {
        base.InitializeAbility(player, uIElement_Icon, uIElement_IconMask, uIElement_cooldownNumText);

        //Maybe have a custome Passiv Activation in here
    }

    public override bool CanPayManaCost()
    {
        throw new System.NotImplementedException();
    }

    public override bool Cast()
    {
        // We dont want anything to happen on cast. 
        throw new System.NotImplementedException();
    }

    public override void CoolDownImgEffect()
    {
        throw new System.NotImplementedException();
    }

    public override bool IsAbilityOnCooldown()
    {
        throw new System.NotImplementedException();
    }

    protected override void RestCoolDownImgEffect()
    {
        throw new System.NotImplementedException();
    }
    
}
