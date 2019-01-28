using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedEventRegZoneIntro : ScriptedEvent
{

    [SerializeField] private Transform introSceneStartPointPlayer;
    
    void Start()
    {
        TriggerScriptedEvent(); //Maybe let Gamemanager decide when the game starts. Attach to LevelStart Evenet 
    }

    public override bool ScriptedEventEnd { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public override IEnumerator ScriptedEventScene()
    {
        player.transform.position = introSceneStartPointPlayer.position;
        var pDir = player.PlayerController.Direction;


        pDir.y = 0.5f;
        player.PlayerController.Direction = pDir;
        // Zoom cam
        yield return new WaitForSeconds(2f);
        pDir.y = 0;
        player.PlayerController.Direction = pDir;

        // Dialogu Loop
            // Message
            // Wait for btn press

        // return player controll

        yield return null;
    }

    protected override void SetInitalRefs()
    {
        throw new System.NotImplementedException();
    }


}
