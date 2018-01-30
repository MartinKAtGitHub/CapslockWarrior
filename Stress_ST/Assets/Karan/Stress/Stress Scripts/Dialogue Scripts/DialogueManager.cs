using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text NameText;
	public Text DialogueText;

	private Queue<string> Sentences;

	void Start()
	{
		Sentences = new Queue<string>();
	}

	public void StartDialogue(Dialogue dialogue)
	{
		//Starting new dialouge Add animation start here

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
		//Debug.Log(sentence);
		//DialogueText.text = sentence;
		StopAllCoroutines();// <-- incase a player clicks next before text is done animating. Other words it clears before starting again
		StartCoroutine(TypeWriterEffect(sentence));
	}

	void EndDialogue()
	{
		// close the dialogue box here anim
		Debug.Log("End Dialogue");
	}

	IEnumerator TypeWriterEffect(string sentence)
	{
		DialogueText.text = "";

		foreach(char letter in sentence.ToCharArray())
		{
		DialogueText.text += letter;
			yield return null;
		}
	}

}
