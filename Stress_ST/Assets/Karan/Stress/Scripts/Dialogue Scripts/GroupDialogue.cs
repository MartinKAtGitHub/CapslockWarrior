using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupDialogue : MonoBehaviour {


    public NPCDialog[] NPCGroup;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < NPCGroup.Length; i++)
        {
            NPCGroup[i].PlayDialog();
        }
    }


     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayConversation();
        }
    }
    public void PlayConversation()
    {
        for (int i = 0; i < NPCGroup.Length; i++)
        {
            NPCGroup[i].DisplayNextSentence(); // I need to lag this out so it stacks
        }
    }

    [System.Serializable]
    public class NPCDialog
    {
        public string[] Dialogue;

        private Queue<string> Sentences = new Queue<string>();
        private bool isDialogueEnd;

        public void PlayDialog()
        {
           
           // isDialogueEnd = false;
            //DialogueBoxAnimator.SetBool("IsDialogueOpen", true);

            //Debug.Log("Start dialouge With = " + dialogue.CharacterName);
            //NameText.text = dialogue.CharacterName;
            Sentences.Clear();

            foreach (string sentence in Dialogue)
            {
                Sentences.Enqueue(sentence);
            }

            DisplayNextSentence();

        }


        public void DisplayNextSentence()
        {
            if (Sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = Sentences.Dequeue();
            Debug.Log(sentence);

            // TODO Generate new speach bubble, And move the other one up

            //StopAllCoroutines();// <-- incase a player clicks next before text is done animating. Other words it clears before starting again
           // StartCoroutine(TypeWriterEffect(sentence));
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

            foreach (char letter in sentence.ToCharArray())
            {
              //  DialogueText.text += letter;
                // play typing sound
                yield return null;
            }
        }
    }

}
