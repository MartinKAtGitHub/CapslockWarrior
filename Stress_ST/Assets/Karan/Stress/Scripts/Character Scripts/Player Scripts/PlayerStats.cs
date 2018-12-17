using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class PlayerStats : CharacterStats
{
    [Space(10)]

    [Header("Player Stats")]
    /// <summary>
    /// The Characters Base/Max Mana. Base/Max Mana Determins how much Mana the character starts with or goes back to. Think of it as a limit or range 
    /// </summary>
    [SerializeField]private int baseMana;
    /// <summary>
    /// The current Mana of the Character. Mana is the variabel which will be in constant change from Slows or boosts. 
    /// </summary>
    [SerializeField]private int mana;

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

    private void Awake()
    {
        InitStats();
    }
   

    protected override void InitStats() // Needs to be called
    {
        base.InitStats();

        Mana = BaseMana;
    }
}
