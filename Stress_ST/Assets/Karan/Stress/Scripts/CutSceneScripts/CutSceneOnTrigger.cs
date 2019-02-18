using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneOnTrigger : CutScene
{

    private void Start()
    {
        TriggerCutScene();
    }

    public override void OnCutSceneEnd()// I mayabe need to create a custome Track to place at the end of the Timeline since the Timeline system dose not have Action for timline end
    {
        GameManager.Instance.CutSceneManager.OutCutSceneEvent?.Invoke();
    }

    public override void OnCutSceneEndWithStopped(PlayableDirector playableDirector)
    {
        Debug.Log(playableDirector.name + " HAS STOPPED");
        OnCutSceneEnd();
    }

    public override void OnCutSceneStart()
    {
        GameManager.Instance.CutSceneManager.InCutSceneEvent?.Invoke();
        playableDirector.Play();
    }

    public override void TriggerCutScene()
    {
        OnCutSceneStart();
    }
}
