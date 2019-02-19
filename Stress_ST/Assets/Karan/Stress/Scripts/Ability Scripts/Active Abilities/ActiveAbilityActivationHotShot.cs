using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAbilityActivationHotShot : ActiveAbility
{

    private float DetectionRange;

    public override bool Cast()
    {
        Debug.Log(" CHECK to see if Enemy is in range --> Insta ( hot shot )");
        return true;
    }

    private void SpawnHotShot()
    {
      //  Instantiate(abilityPrefab);
    }
}
