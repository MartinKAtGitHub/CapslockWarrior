using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Character
{
    public override CharacterStats Stats
    {
        get
        {
            Debug.Log("Getting player Stats");
            return stats;
        }

        set
        {
            Debug.Log("Set player Stats");
            stats = value;
        }
    }
}
