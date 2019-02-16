
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour // Change to scriptable obj no need to make this MONO
{ 

    public GameObject Target; // If we ever want the statuseffect to effect all characters in the scene. we need to get this on hit and not on Start. assigned dynamicly
    public float Power;
    
    [Tooltip("Determins how long the Statuseffect will be effecting the target > ReadOnly")]
    [SerializeField] private float baseActiveTime;
    /// <summary>
    /// Determins how long the Statuseffect will be effecting the target BUT can be modified incase resistances or buffs
    /// </summary>
    protected float activeTime;

    /// <summary>
    /// Determins how long the Statuseffect will be effecting the target > ReadOnly
    /// </summary>
    public float BaseActiveTime { get => baseActiveTime; }


    public abstract float Potancy { get; set; }
    public abstract int StrongestEffectIndex { get; set; }


    //  public abstract static float Potancy; // Dose not work with abstarct
    public abstract void Effect();
    public abstract void EndEffect();
    public abstract void ResetPotancy();
   
    /// <summary>
    /// Counts Down the Timer for how long the Status effect will be active
    /// </summary>
    /// <returns></returns>
    public float ActiveCountDown()
    {
        return activeTime -= Time.deltaTime;
    }
    
}
