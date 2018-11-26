
using UnityEngine;


[System.Serializable]
public class SentenceData
{
    private string strings = "Sentence";
    public GameObject NPC;

    [TextArea(1, 5)]
    public string Sentence;
}
