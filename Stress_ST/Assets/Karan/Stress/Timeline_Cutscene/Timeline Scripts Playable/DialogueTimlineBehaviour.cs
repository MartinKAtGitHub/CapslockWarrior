using UnityEngine;
using UnityEngine.Playables;

public class DialogueTimlineBehaviour : PlayableBehaviour
{
    public DialogueSystem dialogueSystem = null;

    public bool trigger = true;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if(dialogueSystem != null)
        {
            if (Application.isPlaying && trigger)// if(trigger)
            {
                dialogueSystem.TriggerDialogue();
                Debug.Log("TIMELINE TRIGGER DIALOGUE");
                trigger = false;
            }
        }
    }

    public override void OnGraphStart(Playable playable)
    {
        Debug.Log("TIMELINE START ");
    }
}
