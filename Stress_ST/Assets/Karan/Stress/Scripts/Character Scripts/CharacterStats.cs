
using UnityEngine;


//[System.Serializable]
public abstract class CharacterStats : MonoBehaviour// NOTE -> This can be a Scriptable Object. But it will require some extra knowlage on how to generate a Stats object; 
{
    /// <summary>
    /// The Characters Base/Max Health. Base/Max Health Determins how much HP the character starts with or goes back to. Think of it as a limit or range 
    /// </summary>
    [SerializeField] private int baseHealth;
    /// <summary>
    /// The Characters Base/Max Movespeed. Base/Max Movespeed Determins how much MoveSpeed the character starts with or goes back to. Think of it as a limit or range 
    /// </summary>
    [SerializeField] private float baseMovementSpeed;
    /// <summary>
    /// The Characters Base/Max Damage. Base/Max Damage Determins how much Damage the character starts with or goes back to. Think of it as a limit or range 
    /// </summary>
    [SerializeField] private int baseDamage;

    [Space(10f)]

    /// <summary>
    /// The current health of the Character. healt is the variabel which will be in constant change from dmg or healing. 
    /// </summary>
    [SerializeField] private int health;
    /// <summary>
    /// The current movementSpeed of the Character. movementSpeed is the variabel which will be in constant change from Slows or boosts. 
    /// </summary>
    [SerializeField] private float movementSpeed;
    /// <summary>
    /// The current damage of the Character. damage is the variabel which will be in constant change from Buffs or DeBuffs. 
    /// </summary>
    [SerializeField] private int damage;
    


    public int BaseHealth
    {
        get
        {
            return baseHealth;
        }

        set
        {
            baseHealth = value;
        }
    }

    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    public float MovementSpeed
    {
        get
        {
            return movementSpeed;
        }

        set
        {
            movementSpeed = value;
        }
    }

    public float BaseMovementSpeed
    {
        get
        {
            return baseMovementSpeed;
        }

        set
        {
            baseMovementSpeed = value;
        }
    }

    public int BaseDamage
    {
        get
        {
            return baseDamage;
        }

        set
        {
            baseDamage = value;
        }
    }

    public int Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage = value;
        }
    }

   
    protected virtual void InitStats()
    {
        Debug.Log("Stats INITN => " + gameObject.name);
        Health = BaseHealth;
        movementSpeed = BaseMovementSpeed;
        Damage = BaseDamage;
    }
    
}
