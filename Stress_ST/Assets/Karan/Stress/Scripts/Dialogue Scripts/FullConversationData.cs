using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConversationObj", menuName = "Conversation Object/ConversationData")]
public class FullConversationData : ScriptableObject
{
    public SentenceData[] Conversation;
}
