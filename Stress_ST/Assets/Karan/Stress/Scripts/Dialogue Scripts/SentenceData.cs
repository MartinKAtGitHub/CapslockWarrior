using UnityEngine;

[System.Serializable]
public class SentenceData
{
    private string strings = "Sentence";

   // public GameObject DialoguePivotCenterPoint;

   [SerializeField] private int speakerID; // WARNING dont change the name or you lose it all
    [TextArea(1, 5)]
   [SerializeField]private string sentence; // WARNING dont change the name or you lose it all

    /// <summary>
    /// The ID of the NPC who will say this Sentence
    /// </summary>
    public int SpeakerID { get => speakerID; }
    /// <summary>
    /// The Current Sentence in the conversation
    /// </summary>
    public string Sentence { get => sentence; }
}
