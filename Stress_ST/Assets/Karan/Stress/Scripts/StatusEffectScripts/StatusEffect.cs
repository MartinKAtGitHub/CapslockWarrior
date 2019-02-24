
using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{ 

    public float Power;
    [Tooltip("Determins how long the Statuseffect will be effecting the target > ReadOnly")]
    [SerializeField] private float durationTime;
    /// <summary>
    /// Determins how long the Statuseffect will be effecting the target BUT can be modified incase resistances or buffs
    /// </summary>
    protected float effectDuration;

    /// <summary>
    /// Determins how long the Statuseffect will be effecting the target > ReadOnly
    /// </summary>
    public float DurationTime { get => durationTime; }


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
        return effectDuration -= Time.deltaTime;
    }

    /// <summary>
    /// Used to Enable the statuseffect. So right before you add the Effect
    /// </summary>
    public abstract void InitializeStatusEffect(Character character);
}
