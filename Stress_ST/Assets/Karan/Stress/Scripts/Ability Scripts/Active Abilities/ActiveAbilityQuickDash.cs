using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =("Abilities/ActiveAbilities/QuickDash"))]
public class ActiveAbilityQuickDash : ActiveAbility
{

    [SerializeField]private float dashRange;
    private Vector2 dashDirection;
    [SerializeField]private ParticleSystem dashParticalSystem;

    private Vector2 NoDirection = new Vector2(0, 0);

    public override bool Cast()
    {
        var playerController = player?.PlayerController;
       
        if(playerController.Direction != NoDirection)
        {
            var playerRigidbody2D = player.PlayerRigidbody2D;
            playerRigidbody2D.position += playerController.Direction * dashRange;

            PayManaCost();
            SetNewTimeWhenAbilityIsReadyOnSuccsefulcast();
            RestCoolDownImgEffect();
            IsAbilityOnCD(true, true);

            return true;
        }
        else
        {
            Debug.Log("Can cast Ability = " + nameof(ActiveAbilityQuickDash));
            return false;
        }
    }

}
