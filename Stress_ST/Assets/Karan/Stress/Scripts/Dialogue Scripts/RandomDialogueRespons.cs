using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDialogueRespons : DialogueSystem
{
    
    public override IEnumerator StartLoopDialogue()
    {
        Debug.Log("Looping Dialogue");
       yield return StartCoroutine(StartMainDialogue());
    }

    public override IEnumerator StartMainDialogue()
    {
        Debug.Log("Start Main Dialogue");
 
        //dialogueTrigger.enabled = false;
        isDialogueActiv = true;
        DialogueBoxContainer.SetActive(true);

        dialogueText.text = string.Empty;
        yield return null;

        AnchorDialogueBoxToNPC(sentenceDataArray[0].NPC.transform);
        // ANIM START ?

         yield return StartCoroutine( TypeWriterEffect(sentenceDataArray[Random.Range(0, sentenceDataArray.Length)].Sentence) );
        // Anim END ?
        
       EndDialouge();
    }


    public override void EndDialouge()
    {
        isDialogueActiv = false;
        isMainDialogueFinished = true;
        
        DialogueBoxContainer.SetActive(false); // TODO Add an animation to fade out dialogue text insted of setActiv
        // maybe add an event to signale end 
        Debug.Log("Dialogue End");
    }
 

}
