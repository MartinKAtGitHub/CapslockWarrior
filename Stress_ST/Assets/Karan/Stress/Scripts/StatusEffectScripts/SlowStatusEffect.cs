using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowStatusEffect : StatusEffect
{

    private static int strongestEffectIdex;
    private static float potancy;


    private float InitialSpeed;

    // private PlayerStatsForTesting playerStatsForTesting;


    public override float Potancy
    {
        get
        {
            //  Debug.Log("Slow POTANCY IS = " + potancy);
            return potancy;
        }

        set
        {
            // Debug.Log("Slow POTANCY SET TO = " + value);
            potancy = value;
        }
    }

    public override int StrongestEffectIndex
    {
        get
        {
            // Debug.Log("Slow INDEX IS = " + strongestEffectIdex);
            return strongestEffectIdex;
        }

        set
        {
            //Debug.Log("Slow INDEX SET TO = " + value);
            strongestEffectIdex = value;
        }
    }

    private void Start()
    {
        //   playerStatsForTesting = Target.GetComponent<PlayerStatsForTesting>(); //TODO What if player Dies While the slow is Starting. NULL ERROR Potential.
        //InitialSpeed = playerStatsForTesting.CurrentSpeed;
    }

    public override void Effect()
    {
        //playerStatsForTesting.CurrentSpeed = InitialSpeed - (InitialSpeed * Power);
    }


    public override void EndEffect()
    {
        // playerStatsForTesting.CurrentSpeed = playerStatsForTesting.MaxSpeed;
    }

    public override void ResetPotancy()
    {
        Potancy = 0;
        //StrongestEffectIdex = 0;
    }
}

