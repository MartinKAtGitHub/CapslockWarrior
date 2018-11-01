using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{

    public float Power;
    public float Time;
    public GameObject Target; // need to get th eplayer stts from this. maybe make static

    public abstract float Potancy { get; set; } // this needs to return Stat(class) 
    public abstract int StrongestEffectIndex { get; set; }

    //  public abstract static float Potancy; // Dose not work with abstarct
    public abstract void Effect();
    public abstract void EndEffect();
    public abstract void ResetPotancy();

    /*public void ResetPotancy()
    {
        Potancy = 0;
       // StrongestEffectIdex = 0;
    }*/


    // public abstract float GetPotancy(); // maybe return a STRUCT or Status Class that way we can get other types
    // public abstract void SetPotancy(float value);

}
