using UnityEngine;
//using System.Collections;

[CreateAssetMenu (menuName = "StatusEffect/Slow")]
public class SlowStatusEffect : StatusEffect // This dosent really need to be a mono DEV can be changed to scripable object
{

    private static int strongestEffectIdex;
    private static float potancy;// Cant have STACTIC because it will also register Enemis getting slowed stacking with their effect
    private Character character;

    private float origianlCharacterSpeed;
   
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

    public override void Effect()
    {
        character.Stats.MovementSpeed = origianlCharacterSpeed - (origianlCharacterSpeed * Power);
    }
    
    public override void EndEffect()
    {
        effectDuration = DurationTime;
        character.Stats.MovementSpeed = character.Stats.BaseMovementSpeed;
    }

    public override void ResetPotancy()
    {
        Potancy = 0;
    }

    public override void InitializeStatusEffect(Character character)
    {
        this.character = character; /*Target.GetComponent<Character>();*/
        origianlCharacterSpeed = character.Stats.BaseMovementSpeed;
        effectDuration = DurationTime;
    }
}

