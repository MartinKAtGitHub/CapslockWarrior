using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAbilityActivationHotShot : ActiveAbilityActivation
{

    private float DetectionRange;

    public override void  Cast()
    {
        Debug.Log(" CHECK to see if Enemy is in range --> Insta ( hot shot )");
    }

    private void SpawnHotShot()
    {
        Instantiate(abilityPrefab);
    }
}
