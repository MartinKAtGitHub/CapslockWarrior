using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected CharacterStats stats; // Current Limit for simplicity(have in inspector) will be to give all characters the same Stats even if they dont use them

    public abstract CharacterStats Stats
    {
        get;
        set;
    }

    private void Awake()
    {
        GetStats();
    }

    /// <summary>
    /// Gets the stats that are on the spesific gameobject, Implementation will be diffrent on every type of character
    /// as the Stats will not be the same for the PlayerStats as on the BossStats
    /// </summary>
    protected void GetStats()
    {
        Debug.Log("Getting Stats");
        Stats.GetComponent<CharacterStats>();
    }
    // protected abstract void OnCharacterDeath();
    //public abstract void TakeDamage();
}
