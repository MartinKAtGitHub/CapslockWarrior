using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]private CharacterStats stats; // Current Limit for simplicity(have in inspector) will be to give all characters the same Stats even if they dont use them

    public CharacterStats Stats
    {
        get
        {
            return stats;
        }

        set
        {
            stats = value;
        }
    }

    private void Awake()
    {
        GetStats();
    }
    
    /// <summary>
    /// Gets the stats that are on the spesific gameobject, Implementation will be diffrent on every type of character
    /// as the Stats will not be the same for the PlayerStats as on the BossStats
    /// </summary>
    protected  void GetStats()
    {
        Stats.GetComponent<CharacterStats>();
    }
    protected abstract void OnCharacterDeath();

    //public abstract void TakeDamage();
}
