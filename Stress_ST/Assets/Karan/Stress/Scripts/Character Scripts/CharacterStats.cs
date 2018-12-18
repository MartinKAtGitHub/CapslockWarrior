
using UnityEngine;


[System.Serializable]
public class CharacterStats : MonoBehaviour// NOTE -> This can be a Scriptable Object. But it will require some extra knowlage on how to generate a Stats object; 
{
    
    /// <summary>
    /// The Characters Base/Max Health. Base/Max Health Determins how much HP the character starts with or goes back to. Think of it as a limit or range 
    /// </summary>
    [SerializeField] private int baseHealth;
    /// <summary>
    /// The Characters Base/Max Mana. Base/Max Mana Determins how much Mana the character starts with or goes back to. Think of it as a limit or range 
    /// </summary>
    [SerializeField] private int baseMana;
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
    /// The current Mana of the Character. Mana is the variabel which will be in constant change. 
    /// </summary>
    [SerializeField] private int mana;
    /// <summary>
    /// The current movementSpeed of the Character. movementSpeed is the variabel which will be in constant change from Slows or boosts. 
    /// </summary>
    [SerializeField] private float movementSpeed;
    /// <summary>
    /// The current damage of the Character. damage is the variabel which will be in constant change from Buffs or DeBuffs. 
    /// </summary>
    [SerializeField] private int damage;



    /// <summary>
    /// The Characters Base/Max Health. Base/Max Health Determins how much HP the character starts with or goes back to. Think of it as a limit or range 
    /// </summary>
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

    /// <summary>
    /// The current health of the Character. healt is the variabel which will be in constant change from dmg or healing. 
    /// </summary>
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

    /// <summary>
    /// The current movementSpeed of the Character. movementSpeed is the variabel which will be in constant change from Slows or boosts. 
    /// </summary>
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

    /// <summary>
    /// The Characters Base/Max Movespeed. Base/Max Movespeed Determins how much MoveSpeed the character starts with or goes back to. Think of it as a limit or range 
    /// </summary>
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

    /// <summary>
    /// The Characters Base/Max Damage. Base/Max Damage Determins how much Damage the character starts with or goes back to. Think of it as a limit or range 
    /// </summary>
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

    /// <summary>
    /// The current damage of the Character. damage is the variabel which will be in constant change from Buffs or DeBuffs. 
    /// </summary>
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

    /// <summary>
    /// The Characters Base/Max Mana. Base/Max Mana Determins how much Mana the character starts with or goes back to. Think of it as a limit or range 
    /// </summary>
    public int BaseMana
    {
        get
        {
            return baseMana;
        }

        set
        {
            baseMana = value;
        }
    }

    /// <summary>
    /// The current Mana of the Character. Mana is the variabel which will be in constant change. 
    /// </summary>
    public int Mana
    {
        get
        {
            return mana;
        }

        set
        {
            mana = value;
        }
    }

    protected virtual void InitStats()
    {
        Debug.Log("Stats INITN => " + gameObject.name);
        Health = BaseHealth;
        movementSpeed = BaseMovementSpeed;
        Damage = BaseDamage;
        Mana = BaseMana;
    }
    
}
