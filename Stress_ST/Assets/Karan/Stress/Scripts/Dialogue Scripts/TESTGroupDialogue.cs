using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TESTGroupDialogue : MonoBehaviour
{

    public GameObject TESTPNL;


    public string PlayerTag;
    public float TypeingEffectSpeed;
    [Space(10)]
    public List<NPCDialogueData> NPCObjects;
    [Space(10)]
    public GameObject TextBoxPrefab;


    [SerializeField] private float padding = 5f;
    [SerializeField] private Transform TextBoxParantCanvas;

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
        ActiveTextBoxElement = new List<GameObject>();
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




    private void LOL()
    {

        int[] NPC = new int[3];

        List<NPCDialogueData> NPCs = new List<NPCDialogueData>();

        List<string> NPCSenteces = new List<string>();
 
        // 0 Hello

        // 1 hi

        // 0 Whats you name

        // 1 BOB





        for (int i = 0; i < NPC.Length; i++)
        {
               // NPCs[NPCSenteces[i].Substring(0,1)]
            
        }

    }




    IEnumerator PlayDialogue()
    {
        var breakCounterFORINFINITLOOPLOL = 0;
        var oldPrefHight = 0f;

        while (inDialogue) // maybe use range if the whisper ()
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
                //break;
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
                
                for (int j = 0; j < ActiveTextBoxElement.Count; j++)
                {
                    RectTransform temp = ActiveTextBoxElement[j].GetComponent<RectTransform>();
                    temp.anchoredPosition = new Vector2(temp.anchoredPosition.x, temp.anchoredPosition.y + (TxtBoxOffset /*+ temp.rect.height*/)); // TODO Need to increase offset with Box size
                }

                textBoxClone = Instantiate(TextBoxPrefab, TESTPNL.transform);

                yield return null; // I need to wait for 1 frame before i can get all the values need, Its like Start() awake() kind of problem
                RectTransform boxTrans = textBoxClone.GetComponent<RectTransform>();


                // IF(NPCObjects is LEFT then)--------------------
                //If(npc[i])

                boxTrans.anchorMin = new Vector2(0, 0.5f); // <---- this will be done in the NPC script so we can choose witch side the message will be shown
                boxTrans.anchorMax = new Vector2(0, 0.5f); // <---- this will be done in the NPC script so we can choose witch side the message will be shown
                
               
                boxTrans.anchoredPosition = new Vector2(padding + boxTrans.sizeDelta.x/2, 0f);  // boxTrans.anchoredPosition = new Vector2(60f +NPCObjects[i].PIVOT ,12f);       // ADD somthing to make it go BOT LEFT and BOT RIGHT on this line

                //Else

                
                // Flip Ancors and -1 * X-------------------------


                var textBoxCloneText = textBoxClone.GetComponentInChildren<Text>(); //PERFORMANCE Look into maybe storing the text object insted of getting every loop
                // Need to make a clone of the close TURN it OFF simulate the Text and ALLOCATE SPACE BEFORE typing OR WE DO IT ON LINE BREAKS in the for each

                foreach (var letters in sentence)
                {

                    textBoxCloneText.text += letters;
                    if (textBoxCloneText.preferredHeight > oldPrefHight) // TODO HARDCODED value textbox perfferd hight need to be added
                    {
                       

                        for (int j = 0; j < ActiveTextBoxElement.Count; j++)
                        {
                            RectTransform temp = ActiveTextBoxElement[j].GetComponent<RectTransform>();
                            temp.anchoredPosition = new Vector2(temp.anchoredPosition.x, temp.anchoredPosition.y + (textBoxCloneText.preferredHeight - oldPrefHight /*+ temp.rect.height*/)); // TODO Need to increase offset with Box size
                           
                        }
                        Debug.Log("RESIZE BOX PLS");

                        oldPrefHight = textBoxCloneText.preferredHeight;
                        
                    }

                    
                    yield return new WaitForSeconds(TypeingEffectSpeed);
                }

                ActiveTextBoxElement.Add(textBoxClone);
            
                yield return new WaitForSeconds(0.5f); //UNDONE HARDCODED the time after text
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