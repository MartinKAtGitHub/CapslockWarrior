using UnityEngine;

[System.Serializable]
public class SentenceData
{
    private string strings = "Sentence";
    public GameObject DialoguePivotCenterPoint;

    [TextArea(1, 5)]
    public string Sentence;
}
