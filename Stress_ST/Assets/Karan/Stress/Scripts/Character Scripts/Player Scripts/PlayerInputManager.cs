using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Everything that has to do with input from the player
/// </summary>
public class PlayerInputManager : MonoBehaviour
{
    //Maybe use dictionary --> this will allow us to loop trhough and check for double keys
    public Dictionary<string, KeyCode> ActiveKeys;

    [Header("Movement Keys")]
    public KeyCode MoveLeft;
    public KeyCode MoveRight;
    public KeyCode MoveUp;
    public KeyCode MoveDown;

    public KeyCode AltMoveLeft;
    public KeyCode AltMoveRight;
    public KeyCode AltMoveUp;
    public KeyCode AltMoveDown;

    [Space(10)]
    [Header("Movement Keys")]
    public KeyCode Ability1;
    public KeyCode Ability2;
    public KeyCode Ability3;
    public KeyCode Ability4;

    [Header("Other keys")]
    public KeyCode Reload;
    public KeyCode Interact;

    /// <summary>
    /// Holds a value from -1 to 1 which translates to moveing left/right or up/down or 0 standing still
    /// </summary>
    public Vector2 MovementInputValues;


    void Start()
    {
        Debug.Log(nameof(MoveLeft));

    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovementInputs();
        VerticalMovemetnInputs();
    }

    //TODO double check to see if If else() dosent interfare with inputs
    private void HorizontalMovementInputs()
    {
        if (Input.GetKey(MoveLeft) || Input.GetKey(AltMoveLeft))
        {
            MovementInputValues.x = -1f;
        }
        else if (Input.GetKey(MoveRight) || Input.GetKey(AltMoveRight))
        {
            MovementInputValues.x = 1f;
        }
        else
        {
            MovementInputValues.x = 0;
        }
    }

    private void VerticalMovemetnInputs()
    {
        if (Input.GetKey(MoveDown) || Input.GetKey(AltMoveDown))
        {
            MovementInputValues.y = -1f;
        }
        else if (Input.GetKey(MoveUp) || Input.GetKey(AltMoveDown))
        {
            MovementInputValues.y = 1f;
        }
        else
        {
            MovementInputValues.y = 0;
        }
    }

    private void AbilityInputs(KeyCode abilityKey, KeyCode altAbilityKey)
    {
        if(Input.GetKey(abilityKey) || Input.GetKey(altAbilityKey))
        {
            // Pass Event and trigger it here
        }
    }

    public void DuplicatedKeyAssignmentCheck()
    {
        // Check to see if any inputs have the same keys
        // Like assign all the Active keys into a dictionary and loop, if any key is teh same error out
    }
}
