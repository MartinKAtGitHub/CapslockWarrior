﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Everything that has to do with input from the player
/// </summary>
public class PlayerInputManager : MonoBehaviour
{
    //Maybe use dictionary --> this will allow us to loop trhough and check for double keys
    public Dictionary<string, KeyCode> ActiveKeys = new Dictionary<string, KeyCode>();

    [Header("Movement Keys")]
    [SerializeField] KeyCode moveLeft;
    [SerializeField] KeyCode moveRight;
    [SerializeField] KeyCode moveUp;
    [SerializeField] KeyCode moveDown;

    [SerializeField] KeyCode altMoveLeft;
    [SerializeField] KeyCode altMoveRight;
    [SerializeField] KeyCode altMoveUp;
    [SerializeField] KeyCode altMoveDown;

    [Space(10)]
    [Header("Ability Keys")]
    [SerializeField] KeyCode ability1;
    [SerializeField] KeyCode ability2;
    [SerializeField] KeyCode ability3;
    [SerializeField] KeyCode ability4;

    [SerializeField] KeyCode altAbility1;
    [SerializeField] KeyCode altAbility2;
    [SerializeField] KeyCode altAbility3;
    [SerializeField] KeyCode altAbility4;


    [Header("Other keys")]
    [SerializeField] KeyCode reload;
    [SerializeField] KeyCode interact;

    public event Action OnAbilityKey1Down; 
    public event Action OnAbilityKey2Down; 
    public event Action OnAbilityKey3Down; 
    public event Action OnAbilityKey4Down;


    /// <summary>
    /// Holds a value from -1 to 1 which translates to moveing left/right or up/down or 0 standing still
    /// </summary>
    private Vector2 movementInputValues;
    public Vector2 MovementInputValues { get => movementInputValues; } //c#6 ???????????

    void Start()
    {
        ActiveKeys.Add(nameof(moveLeft), moveLeft);
        // now list them all down here LUL
    }

    // What happens if i press buttons when Loading if this goes GameManager
    void Update()
    {
        // TODO ad relaode mode an limit inputs when in this mode

        HorizontalMovementInputs();
        VerticalMovemetnInputs();

        abilityInputs(ability1, altAbility1, OnAbilityKey1Down);
    }

    //TODO double check to see if If else() dosent interfare with inputs
    private void HorizontalMovementInputs()
    {
        if (Input.GetKey(moveLeft) || Input.GetKey(altMoveLeft))
        {
            movementInputValues.x = -1f;
        }
        else if (Input.GetKey(moveRight) || Input.GetKey(altMoveRight))
        {
            movementInputValues.x = 1f;
        }
        else
        {
            movementInputValues.x = 0f;
        }
    }

    private void VerticalMovemetnInputs()
    {
        if (Input.GetKey(moveDown) || Input.GetKey(altMoveDown))
        {
            movementInputValues.y = -1f;
        }
        else if (Input.GetKey(moveUp) || Input.GetKey(altMoveUp))
        {
            movementInputValues.y = 1f;
        }
        else
        {
            movementInputValues.y = 0f;
        }
    }

    private void abilityInputs(KeyCode abilityKey, KeyCode altAbilityKey, Action action)
    {
        if(Input.GetKeyDown(abilityKey) || Input.GetKeyDown(altAbilityKey))
        {
            action?.Invoke();
        }
    }

    /*private bool ReloadMode()
    {
        if()
    }*/

    public void DuplicatedKeyAssignmentCheck() // Maybe do this in its own class in Game manager perhapes. MOve this whole class to game manager
    {
        // Check to see if any inputs have the same keys
        // Like assign all the Active keys into a dictionary and loop, if any key is teh same error out

        // for
            // for
                
            //if(name i == j)
                // continue / skip
               // if(key i == j)
                // Bool duplicate
    }

    public void DefaultSettings()
    {
        // In case the player fucks up he keys he can click this and we will revert all keys back to standard
    }
}