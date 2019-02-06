using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
    /// <summary>
    /// Event for when player goes into a Cutscene. This will signal other componants to do so that th eplayer cant do unintended stuff
    /// like moving around in a cutscene
    /// </summary>
    public Action InCutSceneEvent;
    /// <summary>
    /// Event for when player goes Out of Cutscene. This will signal other componants to Release player fro restrictions but also whatever els you want to happen at the end
    /// like moving around in a cutscene
    /// </summary>
    public Action OutCutSceneEvent;

    private AbilityController abilityController;
    private PlayerInputManager playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInputManager>();

        InCutSceneEvent += PlayerInCutScene;

        OutCutSceneEvent += PlayerOutCutScene;
    }

    private void PlayerInCutScene()
    {
        playerInput.ScriptedEventActive = true;
    }
    private void PlayerOutCutScene()
    {
        playerInput.ScriptedEventActive = false;
    }

    private void OnDisable()
    {
        InCutSceneEvent -= PlayerInCutScene;
        OutCutSceneEvent -= PlayerOutCutScene;
    }
}
