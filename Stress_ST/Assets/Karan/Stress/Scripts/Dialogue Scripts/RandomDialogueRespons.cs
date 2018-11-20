using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDialogueRespons : DialogueSystem
{
    
    

    public override IEnumerator StartLoopDialogue()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator StartMainDialogue()
    {
        isDialogueActiv = true;
        DialogueBoxContainer.SetActive(true);

        dialogueText.text = string.Empty;
        yield return null;

        AnchorDialogueBoxToNPC(sentenceDataArray[0].NPC.transform);
        // ANIM START ?

        StartCoroutine( TypeWriterEffect(sentenceDataArray[Random.Range(0, sentenceDataArray.Length)].Sentence) );
        // Anim END ?


       EndDialouge(); // I NEED TO WAIT FOR TYPE TO FINISH BEFORE I CAN CALL THIS yield return new WaitForSeconds(nextMessageSpeed);  

    }


    public override void EndDialouge()
    {
        isDialogueActiv = false;

        DialogueBoxContainer.SetActive(false);

        Debug.Log("Dialogue End");
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == TargetPlayer.tag && isDialogueActiv == false)
        {
            StartCoroutine(StartMainDialogue());
            //cCollider2D.enabled = false;
            Debug.Log("START NPC Dialogue");
        }
       
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == TargetPlayer.tag)
        {
            Debug.Log("EXIT Dialogue Range");
            //playerExitDialogue = true;
            // cCollider2D.enabled = false;
        }
    }


}
