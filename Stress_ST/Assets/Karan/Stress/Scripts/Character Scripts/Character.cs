using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    private CharacterStats stats; // I want to make this "Forced" in childe classes, but not every enemy will have the same Stats 

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
      //  GetStats();
    }
    
    /// <summary>
    /// Gets the stats that are on the spesific gameobject, Implementation will be diffrent on every type of character
    /// as the Stats will not be the same for the PlayerStats as on the BossStats
    /// </summary>
    protected abstract void GetStats();
    protected abstract void OnCharacterDeath();

    //public abstract void TakeDamage();
}
