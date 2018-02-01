using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

public Dialogue Dialogue;

public void TriggerDialouge()
{
	FindObjectOfType<DialogueManager>().StartDialogue(Dialogue); // TODO change to singelton(Gamemanager) so we dont need to FIND anything
	// set Seapking IMG = true
}

}
