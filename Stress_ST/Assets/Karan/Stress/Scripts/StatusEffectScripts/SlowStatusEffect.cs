using UnityEngine;
//using System.Collections;

//[CreateAssetMenu (menuName = "StatusEffect/Slow")]
public class SlowStatusEffect : StatusEffect // This dosent really need to be a mono DEV
{

    private static int strongestEffectIdex;
    private static float potancy;
    private PlayerController playerController;

    private float InitialSpeed;
    private float InitialTime;

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
        InitialzeSlow();
    }

    public void InitialzeSlow() // IF script ececution is out of order call this in the what ever ability is trying to slow
    {
        playerController = Target.GetComponent<PlayerController>();
        InitialSpeed = playerController.BaseSpeed;
        InitialTime = BaseTime;
    }


    public override void Effect()
    {
       playerController.CurrentSpeed = InitialSpeed - (InitialSpeed * Power);
    }
    
    public override void EndEffect()
    {
        InitialTime = BaseTime;
        playerController.CurrentSpeed = playerController.BaseSpeed;
    }

    public override void ResetPotancy()
    {
        Potancy = 0;
    }

   
    public override float CountDown()
    {
        return InitialTime -= Time.deltaTime;
    }
}

