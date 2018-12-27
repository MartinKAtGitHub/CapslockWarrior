
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{ 

    public float Power;
    /// <summary>
    /// Holds the Base value of the time it takes for the effect to be over Or how long it lasts
    /// </summary>
    public float BaseActiveTime;
    public GameObject Target; // If we ever want the statuseffect to effect all characters in the scene. we need to get this on hit and not on Start. assigned dynamicly

    public abstract float Potancy { get; set; }
    public abstract int StrongestEffectIndex { get; set; }

    //  public abstract static float Potancy; // Dose not work with abstarct
    public abstract void Effect();
    public abstract void EndEffect();
    public abstract void ResetPotancy();
    public abstract float CountDown(); // Todo Chaneg this to be handeld in here

    /*public void ResetPotancy()
    {
        Potancy = 0;
       // StrongestEffectIdex = 0;
    }*/


    // public abstract float GetPotancy(); // maybe return a STRUCT or Status Class that way we can get other types
    // public abstract void SetPotancy(float value);

}
