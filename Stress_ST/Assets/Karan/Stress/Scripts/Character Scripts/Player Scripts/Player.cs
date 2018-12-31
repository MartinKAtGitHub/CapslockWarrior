using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Character
{

    PlayerHealthSystem playerHealthSystem;
    PlayerController playerController;
    AbilityController abilityController;
    Rigidbody2D playerRigidbody2D;



    private void Awake()
    {
        playerHealthSystem = GetComponent<PlayerHealthSystem>();
        playerController = GetComponent<PlayerController>();
        abilityController = GetComponent<AbilityController>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    public override CharacterStats Stats
    {
        get
        {
            //Debug.Log("Getting player Stats");
            return stats;
        }

        set
        {
          //  Debug.Log("Set player Stats");
            stats = value;
        }
    }

    public PlayerHealthSystem PlayerHealthSystem { get => playerHealthSystem;  }
    public PlayerController PlayerController { get => playerController;  }
    public AbilityController AbilityController { get => abilityController;  }
    public Rigidbody2D PlayerRigidbody2D { get => playerRigidbody2D; }
}
