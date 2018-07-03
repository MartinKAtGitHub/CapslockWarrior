using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TESTGroupDialogue : MonoBehaviour
{

    public string PlayerTag;
    public float TypeingEffectSpeed;
    [Space(10)]
    public List<NPCDialogueData> NPCObjects;
    [Space(10)]
    public GameObject TextBoxPrefab;

    [SerializeField]private Transform TextBoxParantCanvas;
    private List<GameObject> TextBoxElementPool;
    private List<GameObject> ActiveTextBoxElement;


    private List<GameObject> TEXTBOXQUEUE;
    private bool inDialogue;

    private int index;

    [SerializeField] private float TxtBoxOffset = 10;
    [SerializeField] private Camera MainCam;

    void Start()
    {
      
        TEXTBOXQUEUE = new List<GameObject>();

        for (int i = 0; i < NPCObjects.Count; i++)
        {
            NPCObjects[i].InitDialogueData();
        }

        //TextBoxElementPool = new List<GameObject>();
        //index = 0;
        //CreateTextBoxElements();

        //Debug.Log(TextBoxElementPool.Count);

    }


    void Update()
    {

    }

    private void CreateTextBoxElements() 
    {
        for (int i = 0; i < NPCObjects.Count; i++)
        {
            for (int j = 0; j < NPCObjects[i].sentences.Count; j++)
            {
                var txtBox = Instantiate(TextBoxPrefab, TextBoxParantCanvas);
                txtBox.gameObject.SetActive(false);
                TextBoxElementPool.Add(txtBox); // Text Box pool
            }
        }
    }

    public void DisplayNextSentence(Queue<string> Sentences)
    {
        if (Sentences.Count == 0)
        {
            // EndDialogue();
            return;
        }

        string sentence = Sentences.Dequeue();

        //StopAllCoroutines();// <-- incase a player clicks next before text is done animating. Other words it clears before starting again
        //StartCoroutine(TypeWriterEffect(sentence));
    }


    private void CenterTextBoxOverNPC()
    {
        for (int i = 0; i < NPCObjects.Count; i++)
        {
            var textPos = MainCam.WorldToScreenPoint(NPCObjects[i].NPCOPosition.position);
           // textPos.y += offset;
           // textPos.y += NPCGroup[i].Textoffset;

            // textPos.y = NPCGroup[i].Textoffset;
            //For loop all boxes
           // NPCObjects[i].TextObject.transform.position = textPos;
        }
    }

    IEnumerator TypeWriterEffect(string sentence)
    {
        /*
        //DialogueText.text = "";
        TextBoxElements[index].text = string.Empty;

        foreach (char letter in sentence.ToCharArray())
        {
            TextBoxElements[index].text += letter;
            // play typing sound

            yield return new WaitForSeconds(TypeingEffectSpeed);
        }

        index++;*/

        yield return new WaitForSeconds(TypeingEffectSpeed);
    }



    IEnumerator PlayDialogue()
    {
        var breakCounterFORINFINITLOOPLOL = 0;

        while (inDialogue) // maybe use range if the whisper
        {

            if (breakCounterFORINFINITLOOPLOL == 50)
            {
                inDialogue = false;
                Debug.LogError("CANT STOP INFIFIT LOOP OR WE PAST THE INFIRT LOOP CHECK JUST REMOVETHIS");
            }

            if (NPCObjects.Count == 0)
            {
                Debug.Log("NO MORE NPC DIALOG ENDING LOOP");
                inDialogue = false;
            }
            //if (DialogEndCount == NPCObjects.Length)
            //{
            //    inDialogue = false;
            //    Debug.Log("All NPC HAS NO MORE TO SAY");
            //}
   
            for (int i = 0; i < NPCObjects.Count; i++)
            {
                var sentence = string.Empty;

                if (NPCObjects[i].sentences.Count != 0)
                {
                    sentence = NPCObjects[i].sentences.Dequeue();
                    if (sentence == string.Empty)
                    {
                        continue;
                    }
                }
                else
                {
                    // REMOVE THAT NPC FROM LIST ?? maybe
                    // DialogEndCount++;
                    NPCObjects.RemoveAt(i);
                   // Debug.Log(NPCObjects[i].NPCOPosition.gameObject.name + " = is Dont Talking REMOVED");
                    continue;
                }

               

                for (int j = 0; j < TEXTBOXQUEUE.Count; j++)
                {
                    TEXTBOXQUEUE[j].transform.position = new Vector3(TEXTBOXQUEUE[j].transform.position.x, TEXTBOXQUEUE[j].transform.position.y + TxtBoxOffset, 0);
                }

                var textBoxClone = Instantiate(TextBoxPrefab, TextBoxParantCanvas);
                textBoxClone.transform.position = new Vector3(textBoxClone.transform.position.x, textBoxClone.transform.position.y + TxtBoxOffset, 0);
                var textBoxCloneText = textBoxClone.GetComponentInChildren<Text>();// TODO might be a little to much

                foreach (var letters in sentence)
                {
                    textBoxCloneText.text += letters;
                    yield return new WaitForSeconds(TypeingEffectSpeed);
                }

                TEXTBOXQUEUE.Add(textBoxClone);

            }

             breakCounterFORINFINITLOOPLOL++;
        }

        yield return null; //DO I NEED TO RETURN THIS 

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == PlayerTag)
        {
            Debug.Log("This is ");
            inDialogue = true;
            StartCoroutine(PlayDialogue());
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}

[System.Serializable]
public class NPCDialogueData
{
    public Transform NPCOPosition;
    [SerializeField] private string[] DialogueSentences;
    public Queue<string> sentences = new Queue<string>();


    public void InitDialogueData() // PUTS the String array into the Queue
    {
        sentences.Clear();
        foreach (string sentence in DialogueSentences)
        {
            sentences.Enqueue(sentence);
        }
    }
}