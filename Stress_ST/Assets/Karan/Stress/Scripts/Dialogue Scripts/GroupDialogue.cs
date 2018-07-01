using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupDialogue : MonoBehaviour
{


    public NPCDialog[] NPCGroup;
    [Space(25f)]
    public Camera MainCam; // GET THIS FROM GAME MANAGER STATIC;
    public Canvas GroupDialogueCanvas;

   // private float offset;

    // Use this for initialization
    void Start()
    {
        GroupDialogueCanvas = transform.GetComponentInChildren<Canvas>();

        for (int i = 0; i < NPCGroup.Length; i++)
        {
            NPCGroup[i].TextObject = Instantiate(NPCGroup[i].TextObject, GroupDialogueCanvas.gameObject.transform);

            NPCGroup[i].PlayDialog();

            if (NPCGroup[i].TextObject == null)
            {
                Debug.LogWarning("Cant find Text object on = " + NPCGroup[i].NPCName);
            }
        }


    }


    void Update()
    {
        for (int i = 0; i < NPCGroup.Length; i++)
        {
            var textPos = MainCam.WorldToScreenPoint(NPCGroup[i].NPCObject.transform.position);
            // textPos.y += offset;
            textPos.y += NPCGroup[i].Textoffset;
            NPCGroup[i].TextObject.transform.position = textPos;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayConversation();
        }
    }
    public void PlayConversation()
    {
        for (int i = 0; i < NPCGroup.Length; i++)
        {
           // offset += NPCGroup[i].Textoffset;
            NPCGroup[i].DisplayNextSentence(); // I need to lag this out so it stacks
        }
    }

    [System.Serializable]
    public class NPCDialog
    {
        public GameObject TextObject; // IS A PREFAB
        public GameObject NPCObject;
        public string NPCName;
        public float Textoffset;
        [Space(10)]
        public string[] Dialogue;

        private Queue<string> Sentences = new Queue<string>();
        private bool isDialogueEnd;

        public void PlayDialog()
        {
             isDialogueEnd = false;
            //DialogueBoxAnimator.SetBool("IsDialogueOpen", true);

            //Debug.Log("Start dialouge With = " + dialogue.CharacterName);
            //NameText.text = dialogue.CharacterName;
            Sentences.Clear();

            foreach (string sentence in Dialogue)
            {
                Sentences.Enqueue(sentence);
            }

           // DisplayNextSentence();

        }


        public void DisplayNextSentence()
        {
            if (Sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            // TextObject.transform.position =  new Vector3 (TextObject.transform.position.x, TextObject.transform.position.y + Textoffset, 0);
            Textoffset += 20;
            string sentence = Sentences.Dequeue();
            TextObject.GetComponentInChildren<Text>().text = sentence;
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
