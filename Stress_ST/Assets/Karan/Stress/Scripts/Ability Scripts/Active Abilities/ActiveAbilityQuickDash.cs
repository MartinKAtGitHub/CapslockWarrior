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

    protected override bool AbilityLogic() // TODO QUICKDASH --> Dash over/thorugh walls
    {
        var playerController = player?.PlayerController;
       
        if(playerController.Direction != NoDirection)
        {
            var playerRigidbody2D = player.PlayerRigidbody2D;
            playerRigidbody2D.position += playerController.Direction * dashRange;
            
            return true;
        }
        else
        {
            Debug.Log(name + " Failed = Player standing still or wall is blocking");
            return false;
        }
    }
}
