using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =("Abilities/ActiveAbilities/QuickDash"))]
public class ActiveAbilityQuickDash : ActiveAbilityActivation
{

    private float dashRange;
    private Vector2 dashDirection;
    private ParticleSystem dashParticalSystem;

    private Vector2 NoDirection = new Vector2(0, 0);

    public override void Cast()
    {
        var playerController = player.PlayerController;
        if (playerController!= null)
        {
            if(playerController.Direction != NoDirection) // If the player is not standing still
            {
                var playerRigidbody2D = player.PlayerRigidbody2D;
                playerRigidbody2D.position += playerController.Direction * dashRange;
            }

        }else
        {
            Debug.LogError("Player controller Null");
        }
    }
}
