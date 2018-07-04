using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TESTGroupDialogue : MonoBehaviour
{

    public GameObject TESTPNL;


    public string PlayerTag;
    public float TypeingEffectSpeed;
    [Space(10)]
    public List<NPCDialogueData> NPCObjects;
    [Space(10)]
    public GameObject TextBoxPrefab;

    [SerializeField]private Transform TextBoxParantCanvas;
    private List<GameObject> TextBoxElementPool;
    private List<GameObject> ActiveTextBoxElement;


    //private List<GameObject> activeTextBoxElements;
    private bool inDialogue;

    private int index;
    private Vector3 WorldTextPos;
    private GameObject textBoxClone;

    [SerializeField] private float TxtBoxOffset = 10;
    [SerializeField] private Camera MainCam;

    void Start()
    {
        
        //activeTextBoxElements = new List<GameObject>();

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
        //CenterTextBoxOverNPC();
        CenterPANEL();
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
            WorldTextPos = MainCam.WorldToScreenPoint(NPCObjects[i].NPCOPosition.position);
            // textPos.y += offset;
            // textPos.y += NPCGroup[i].Textoffset;

            textBoxClone.transform.position = WorldTextPos;

            for (int j = 0; j < NPCObjects[i].activeTextBoxElements.Count; j++)
            {
                // activeTextBoxElements[j].transform.position = new Vector3(textPos.x + activeTextBoxElements[j].transform.position.x, textPos.y + activeTextBoxElements[j].transform.position.y, 0);
                NPCObjects[i].activeTextBoxElements[j].transform.position = new Vector3(WorldTextPos.x, WorldTextPos.y, 0);
            }

            // textPos.y = NPCGroup[i].Textoffset;
            //For loop all boxes
            // NPCObjects[i].TextObject.transform.position = textPos;
        }
    }

    private void CenterPANEL()
    {
           var PnlWorld = MainCam.WorldToScreenPoint(transform.position);
            TESTPNL.transform.position = PnlWorld;
    }

    IEnumerator PlayDialogue()
    {
        var breakCounterFORINFINITLOOPLOL = 0;

        var CurrentOffset = 0f;
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
                    NPCObjects.RemoveAt(i);
                    continue; // This will skip to the next cycle, since we kind of removed this cycle
                }

                CurrentOffset += TxtBoxOffset;

                //for (int j = 0; j < NPCObjects[i].activeTextBoxElements.Count; j++)
                //{
                //    NPCObjects[i].activeTextBoxElements[j].transform.position = new Vector3(NPCObjects[i].activeTextBoxElements[j].transform.position.x, NPCObjects[i].activeTextBoxElements[j].transform.position.y /* + TxtBoxOffset*/, 0);
                //    // Maybe add to Offset in here ONLY, becuse we need this to be in the update
                //}

                //var textBoxClone = Instantiate(TextBoxPrefab, TextBoxParantCanvas);
                //textBoxClone = Instantiate(TextBoxPrefab, TextBoxParantCanvas);
                textBoxClone = Instantiate(TextBoxPrefab, TESTPNL.transform);

                RectTransform boxTrans = textBoxClone.GetComponent<RectTransform>();

                boxTrans.anchoredPosition = new Vector2(60f,12f);

                // textBoxClone.transform.position = new Vector3(textBoxClone.transform.position.x, textBoxClone.transform.position.y + CurrentOffset, 0);

                //textBoxClone.transform.position = new Vector3(WorldTextPos.x, WorldTextPos.y + CurrentOffset, 0);

                var textBoxCloneText = textBoxClone.GetComponentInChildren<Text>(); //PERFORMANCE Look into maybe storing the text object insted of getting every loop


                //var textPos = MainCam.WorldToScreenPoint(NPCObjects[i].NPCOPosition.position);
                // textBoxClone.transform.position = new Vector3(textPos.x , textPos.y, 0);

                

                foreach (var letters in sentence)
                {
                    textBoxCloneText.text += letters;
                    yield return new WaitForSeconds(TypeingEffectSpeed);
                }

                NPCObjects[i].activeTextBoxElements.Add(textBoxClone);

                yield return new WaitForSeconds(0.5f); //UNDONE HARDCODED the time after text

                //for (int j = 0; j < NPCObjects.Count; j++)
                //{
                //    for (int k = 0; k < NPCObjects[k].activeTextBoxElements.Count; k++)
                //    {

                //    }
                //}
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
    public List<GameObject> activeTextBoxElements = new List<GameObject>();
    public bool isDialogEmpty;


    public void InitDialogueData() // PUTS the String array into the Queue
    {
        sentences.Clear();
        foreach (string sentence in DialogueSentences)
        {
            sentences.Enqueue(sentence);
        }
    }
}