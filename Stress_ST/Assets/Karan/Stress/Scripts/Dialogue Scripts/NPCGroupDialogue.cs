using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NPCGroupDialogue : MonoBehaviour
{

    public string PlayerTag;

    [Space (5)]
    public float TypeingEffectSpeed; // Move To NPC
    public float TimeToNextDialogueBox;

    [Space(5)]
    public GameObject TextBoxParentPrefab;
    public GameObject TextBoxPrefab;
    [SerializeField] private GameObject TextBoxElementsParent;

    [Space(5)]
    [SerializeField] private float padding = 5f;
    [SerializeField] private Transform GroupDialogueCanvas;

    private List<GameObject> ActiveTextBoxElement;

    //private List<GameObject> activeTextBoxElements;
    private bool inDialogue;
    private bool playerInDialgueRange;

    private GameObject textBoxClone;

    [SerializeField] private float TxtBoxOffset = 10;
    [SerializeField] private Camera MainCam;


    [Space(10)]
    public List<NPCDialogueData> NPCObjects;

    void Start()
    {
        ActiveTextBoxElement = new List<GameObject>();
        playerInDialgueRange = false;

        NullCheckTextBoxElementsParent();

        TextBoxElementsParent.SetActive(false);
        GroupDialogueCanvas.gameObject.SetActive(false);

        for (int i = 0; i < NPCObjects.Count; i++)
        {
            NPCObjects[i].InitDialogueData();
        }
    }

    void Update()
    {
        if(playerInDialgueRange)
        {
            CenterTextBoxElementsParent();
        }
    }


    private void CenterTextBoxElementsParent()
    {
        var PnlWorld = MainCam.WorldToScreenPoint(transform.position);

        TextBoxElementsParent.transform.position = new Vector2(PnlWorld.x, PnlWorld.y + 100f);
    }

    private void NullCheckTextBoxElementsParent()
    {
        if(TextBoxElementsParent == null)
        {
            Debug.LogWarning("The TextBoxElementsParent_PNL is NULL spawning Pnl prefab");
            TextBoxElementsParent = Instantiate(TextBoxParentPrefab, GroupDialogueCanvas.transform);

        }
    }


    //private void LOL()
    //{

    //    int[] NPC = new int[3];

    //    List<NPCDialogueData> NPCs = new List<NPCDialogueData>();

    //    List<string> NPCSenteces = new List<string>();
 
    //    // 0 Hello

    //    // 1 hi

    //    // 0 Whats you name

    //    // 1 BOB




    //    for (int i = 0; i < NPC.Length; i++)
    //    {
    //           // NPCs[NPCSenteces[i].Substring(0,1)]
            
    //    }

    //}




    IEnumerator PlayDialogue()
    {
        var oldPrefHight = 0f;

        GroupDialogueCanvas.gameObject.SetActive(true);
        TextBoxElementsParent.SetActive(true);
        playerInDialgueRange = true;
       
        // PLAY PNL ANIM
        //WAITFORSEC( anim.lenght )

        while (inDialogue) // maybe use range if the whisper ()
        {
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
                    //RESize pnl here ?
                }

                textBoxClone = Instantiate(TextBoxPrefab, TextBoxElementsParent.transform);
               
                //BUG the flicker of the instantiatet object happens beaucse of yeald return null(waits 1 frame)---> fixed with textBoxClone.SetActive(false); ?
                yield return null; // I need to wait for 1 frame before i can get all the values need, Its like Start() awake() kind of psroblem 
                RectTransform boxTrans = textBoxClone.GetComponent<RectTransform>();


                
                if (NPCObjects[i].NPCPosition.localPosition.x <= 0) // <---- this will be done in the NPC script so we can choose witch side the message will be shown
                {
                    boxTrans.anchorMin = new Vector2(0f, 0f);
                    boxTrans.anchorMax = new Vector2(0f, 0f);

                    boxTrans.anchoredPosition = new Vector2(padding + boxTrans.sizeDelta.x / 2, padding + 0f);
                }
                else
                {
                    boxTrans.anchorMin = new Vector2(1f, 0f);
                    boxTrans.anchorMax = new Vector2(1f, 0f);
                    boxTrans.anchoredPosition = new Vector2(-1*(padding + boxTrans.sizeDelta.x / 2), padding + 0f);
                }

                var textBoxCloneText = textBoxClone.GetComponentInChildren<Text>(); //PERFORMANCE Look into maybe storing the text object insted of getting every loop

                //textBoxClone.SetActive(true);

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
                        oldPrefHight = textBoxCloneText.preferredHeight;
                    }
                    
                    yield return new WaitForSeconds(TypeingEffectSpeed);
                }
                ActiveTextBoxElement.Add(textBoxClone);
                yield return new WaitForSeconds(TimeToNextDialogueBox); //UNDONE HARDCODED the time after text
            }
        }
        //yield return null; //DO I NEED TO RETURN THIS ??
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == PlayerTag)
        {
            inDialogue = true;
            StartCoroutine(PlayDialogue());
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}

[System.Serializable]
public class NPCDialogueData
{
    public Transform NPCPosition;
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