
using UnityEngine;

public abstract class CharacterHealthSystem : MonoBehaviour
{
    private Character character; // DONT REMOVET THIS IT WILL FUCKING CRASH UNITY LUL
    protected Character Character { get => character; set => character = value; }

    /// <summary>
    /// The Regen amount that happens every second
    /// </summary>
    /// 

    public float HealthRegenAmount = 0;
        
    // protected float currentHealth; If we want to see/controll the HP form this script

     protected void Awake() // To easy to forget base.Awake in childe class so just use 1 Awake
     {
        GetCharacterComponant();
     }

    public abstract void TakeDamage(int dmg);
    
    protected abstract void OnCharacterDeath(); //Maybe make Abstarct and let the player/enemy handle its Death
    protected virtual void GetCharacterComponant()
    {
        character = GetComponent<Character>();
        Debug.Log(character.name + " = CHARACTER HEALTH SYSTEM CONNECTED");
    }
   // protected abstract void SetInitialHP();
}

