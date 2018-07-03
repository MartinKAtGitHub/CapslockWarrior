using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialog: MonoBehaviour
{
    public GameObject TextObject; // IS A PREFAB
    //public GameObject NPCObject;
    public string NPCName;
    public float Textoffset;
    public float LetterPause;
    public Text DialogTxt;

    [Space(10)]
    public string[] Dialogue;

    private Queue<string> Sentences = new Queue<string>();
    private bool isDialogueEnd;
    
    public void StartDialog()
    {
         isDialogueEnd = false;
        //DialogueBoxAnimator.SetBool("IsDialogueOpen", true);

        Sentences.Clear();
        foreach (string sentence in Dialogue)
        {
            Debug.Log("LUL");
            Sentences.Enqueue(sentence);
        }

        DialogTxt = TextObject.GetComponentInChildren<Text>();
        
    }

    public void DisplayNextSentence()
    {
        if (Sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
       

        string sentence = Sentences.Dequeue();

        StopAllCoroutines();// <-- incase a player clicks next before text is done animating. Other words it clears before starting again
        StartCoroutine(TypeWriterEffect(sentence));
    }

    public void EndDialogue()
    {
        //DialogueBoxAnimator.SetBool("IsDialogueOpen", false);
        isDialogueEnd = true;
        Debug.Log("End Dialogue");

    }

    IEnumerator TypeWriterEffect(string sentence)
    {
        //DialogueText.text = "";
        DialogTxt.text = string.Empty;
        foreach (char letter in sentence.ToCharArray())
        {
            DialogTxt.text += letter;
           // play typing sound

            yield return new WaitForSeconds(LetterPause);
            // index++ ?
        }
    }
}
