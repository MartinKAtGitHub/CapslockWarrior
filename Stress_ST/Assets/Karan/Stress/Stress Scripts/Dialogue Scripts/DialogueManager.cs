using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour { // maybe make this static/ singleton

	public Text NameText;
	public Text DialogueText;
	public Animator DialogueBoxAnimator;

	public bool isDialogueEnd;

	private Queue<string> Sentences;

	void Start()
	{
		Sentences = new Queue<string>();
		//isDialogueEnd = false; // dose not create null error
	}

	public void StartDialogue(Dialogue dialogue)
	{
		isDialogueEnd = false;
		DialogueBoxAnimator.SetBool("IsDialogueOpen",true);

		Debug.Log("Start dialouge With = " + dialogue.CharacterName);
		NameText.text = dialogue.CharacterName;
		Sentences.Clear();

		foreach(string sentence in dialogue.Sentences)
		{
			Sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if(Sentences.Count == 0)
		{

			EndDialogue();

			return;
		}

		string sentence = Sentences.Dequeue();

		// TODO Generate new speach bubble, And move the other one up

		StopAllCoroutines();// <-- incase a player clicks next before text is done animating. Other words it clears before starting again
		StartCoroutine(TypeWriterEffect(sentence));
	}

	public void EndDialogue()
	{
		DialogueBoxAnimator.SetBool("IsDialogueOpen",false);
		isDialogueEnd = true;
		Debug.Log("End Dialogue");

	}

	IEnumerator TypeWriterEffect(string sentence)
	{
		DialogueText.text = "";

		foreach(char letter in sentence.ToCharArray())
		{
			DialogueText.text += letter;
			// play typing sound
			yield return null;
		}
	}

}
