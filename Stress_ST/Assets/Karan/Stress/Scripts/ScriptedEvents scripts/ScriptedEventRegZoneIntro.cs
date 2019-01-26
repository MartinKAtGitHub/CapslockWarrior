using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedEventRegZoneIntro : ScriptedEvent
{
    // Start is called before the first frame update
    void Start()
    {
        TriggerScriptedEvent(); //Maybe let Gamemanager decide when the game starts. Attach to LevelStart Evenet 
    }

    public override bool ScriptedEventEnd { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public override IEnumerator ScriptedEventScene()
    {
        throw new System.NotImplementedException();
    }

    protected override void SetInitalRefs()
    {
        throw new System.NotImplementedException();
    }


}
