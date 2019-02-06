using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public abstract class CutScene : MonoBehaviour
{ 
    protected PlayableDirector playableDirector; 

    private void Awake()
    {
         playableDirector = GetComponent<PlayableDirector>();
        playableDirector.stopped += OnCutSceneEndWithStopped;
    }

    public abstract void TriggerCutScene();
    public abstract void OnCutSceneStart();
    public abstract void OnCutSceneEnd();

    public abstract void OnCutSceneEndWithStopped(PlayableDirector playableDirector);
}
