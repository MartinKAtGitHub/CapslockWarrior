using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialogueTimelineAsset : PlayableAsset
{

    public ExposedReference<DialogueSystem> dialogueSystem;
   // public FullConversationData FullConversationData;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<DialogueTimlineBehaviour>.Create(graph);

        var dialogueTimlineBehaviour = playable.GetBehaviour();
        dialogueTimlineBehaviour.dialogueSystem = dialogueSystem.Resolve(graph.GetResolver());

        return playable;
    }
    
}
