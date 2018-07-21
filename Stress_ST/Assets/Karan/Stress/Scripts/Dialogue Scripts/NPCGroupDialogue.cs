using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NPCGroupDialogue : MonoBehaviour
{

    public string PlayerTag;

    [Space(5)]
    public float TypeingEffectSpeed; // Move To NPC
    public float TimeToNextDialogueBox;

    [Space(5)]
    public GameObject TextBoxParentPrefab;
    public GameObject TextBoxPrefab;
    [SerializeField] private GameObject TextBoxElementsParentPnl;

    [SerializeField] private Animator TextBoxElementsParentAnimator;
    private int pnlCloseTriggerID;

    [Space(5)]
    [SerializeField] private float padding = 5f;
    [SerializeField] private Transform GroupDialogueCanvas;

    private List<GameObject> ActiveTextBoxElement;

    //private List<GameObject> activeTextBoxElements;
    private bool inDialogue;
    private bool playerInDialgueRange;
    private bool playerExitDialogue;

    private GameObject textBoxClone;

    [SerializeField] private float TxtBoxOffset = 10;
    [SerializeField] private Camera MainCam;
    private CircleCollider2D cCollider2D;

    private NPCDialogueData.DialogueData dialogData; // This should be a local variable


    [Space(10)]
    public List<NPCDialogueData> NPCObjects;

    void Start()
    {
        cCollider2D = GetComponent<CircleCollider2D>();

        ActiveTextBoxElement = new List<GameObject>();
        playerInDialgueRange = false;

        pnlCloseTriggerID = Animator.StringToHash("ClosePnl");
        NullCheckTextBoxElementsParent();

        TextBoxElementsParentPnl.SetActive(false);
        GroupDialogueCanvas.gameObject.SetActive(false);

        for (int i = 0; i < NPCObjects.Count; i++)
        {
            NPCObjects[i].InitDialogueData();
        }
    }

    void Update()
    {
        if (playerInDialgueRange)
        {
            CenterTextBoxElementsParent();
        }
    }


    private void CenterTextBoxElementsParent()
    {
        var PnlWorld = MainCam.WorldToScreenPoint(transform.position);

        TextBoxElementsParentPnl.transform.position = new Vector2(PnlWorld.x, PnlWorld.y + 100f);
    }

    private void NullCheckTextBoxElementsParent()
    {
        if (TextBoxElementsParentPnl == null)
        {
            Debug.LogWarning("The TextBoxElementsParent_PNL is NULL spawning Pnl prefab");
            TextBoxElementsParentPnl = Instantiate(TextBoxParentPrefab, GroupDialogueCanvas.transform);

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

    private bool GetPlayerExitDialogue()
    {
        return playerExitDialogue;
    }

    IEnumerator PlayDialogue()
    {
        var oldPrefHight = 0f;
        var BodyAnimTriggerName = string.Empty;
        var MouthAnimTriggerName = string.Empty;

        GroupDialogueCanvas.gameObject.SetActive(true);
        TextBoxElementsParentPnl.SetActive(true);
        playerInDialgueRange = true;
        playerExitDialogue = false;
      

        var animClip = TextBoxElementsParentAnimator.GetCurrentAnimatorClipInfo(0); // TextBoxElementsParentAnimator.GetLayerIndex("Base Layer") <-- this give 0. But i feel its better to use 0 beacuse if we change the name we fucked

        yield return new WaitForSeconds(animClip[0].clip.length);// i cant explain why animClip[0] needs to be 0 or an array. But this is how you accses the curretn active clip

       
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
                    dialogData = NPCObjects[i].sentences.Dequeue();

                    sentence = dialogData.DialogueSentences; // NPCObjects[i].sentences.Dequeue();


                    if(dialogData.AnimationBodyTriggerName != string.Empty) // Not sure if this works or i should use String.Equals
                    {
                        BodyAnimTriggerName = dialogData.AnimationBodyTriggerName;
                        Debug.Log("BODY ANIM NAME SET  = " + BodyAnimTriggerName);
                    }
                    else
                    {
                        Debug.Log("Playing Last anim  = " + BodyAnimTriggerName);
                    }
                    if (dialogData.AnimationMouthTriggerName != string.Empty) // Not sure if this works or i should use String.Equals
                    {
                        MouthAnimTriggerName = dialogData.AnimationMouthTriggerName;
                        Debug.Log("MOUTH ANIM NAME SET  = " + MouthAnimTriggerName);
                    }
                    else
                    {
                        Debug.Log("Playing last Anim  = " + MouthAnimTriggerName);
                    }

                    if (sentence == string.Empty)
                    {
                        continue;
                    }
                }
                else
                {
                    // UNDONE NPCObjects[i].NPCAnimator.SetBool(DEFAULT ANIM/ Final anim  , true) <--- this is needed so when the talking is done they do the final anim
                    NPCObjects.RemoveAt(i);
                    continue; // This will skip to the next cycle, since we kind of removed this cycle
                }

                /*
                    if(dialogData.AnimationTriggerName == NULL)
                        1. Do whaveter i had earlier again
                        2. somhow skip this check or like dont do anything dont 
                    else
                        //NPCObjects[i].NPCAnimator.SetBool(dialogData.AnimationTriggerName, true); // <--- this triggers the animation with that connections
                 */


                //NPCObjects[i].NPCAnimator.SetBool(NPCObjects[i].IsTalkingAnimParameter, true);
                // NPCObjects[i].NPCAnimator.SetTrigger(NPCObjects[i].IsTalkingAnimParameter);
                 NPCObjects[i].NPCAnimator.SetTrigger(MouthAnimTriggerName);
                 NPCObjects[i].NPCAnimator.SetTrigger(BodyAnimTriggerName);


                for (int j = 0; j < ActiveTextBoxElement.Count; j++)
                {
                    RectTransform temp = ActiveTextBoxElement[j].GetComponent<RectTransform>();
                    temp.anchoredPosition = new Vector2(temp.anchoredPosition.x, temp.anchoredPosition.y + (TxtBoxOffset /*+ temp.rect.height*/)); // TODO Need to increase offset with Box size
                    //RESize pnl here ?
                }

                textBoxClone = Instantiate(TextBoxPrefab, TextBoxElementsParentPnl.transform);

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
                    boxTrans.anchoredPosition = new Vector2(-1 * (padding + boxTrans.sizeDelta.x / 2), padding + 0f);
                }

                var textBoxCloneText = textBoxClone.GetComponentInChildren<Text>(); //PERFORMANCE Look into maybe storing the text object insted of getting every loop

                //textBoxClone.SetActive(true);

                oldPrefHight = 0; // RESET for every new word
                foreach (var letters in sentence)
                {
                    textBoxCloneText.text += letters;

                    if (textBoxCloneText.preferredHeight > oldPrefHight)
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
                //NPCObjects[i].NPCAnimator.SetBool(NPCObjects[i].IsTalkingAnimParameter, false); <--- with the ANY state we cant have a bool or it will just keep calling itself
                NPCObjects[i].NPCAnimator.SetTrigger("StopTalk");// UNDONE need to make this flow S
                yield return new WaitForSeconds(TimeToNextDialogueBox); //UNDONE HARDCODED the time after text
            }
        }

        yield return new WaitUntil(GetPlayerExitDialogue);
        TextBoxElementsParentAnimator.SetTrigger(pnlCloseTriggerID);
        yield return new WaitForSeconds(animClip[0].clip.length);
        TextBoxElementsParentPnl.SetActive(false);
        cCollider2D.enabled = false;
       
        //yield return null; //DO I NEED TO RETURN THIS ??
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == PlayerTag && inDialogue == false)
        {
            inDialogue = true;
            StartCoroutine(PlayDialogue());
           //cCollider2D.enabled = false;
            Debug.Log("Starting Group NPC Dialogue");
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == PlayerTag)
        {
            Debug.Log("EXIT Group NPC Dialogue");
            playerExitDialogue = true;
           // cCollider2D.enabled = false;
        }
    }
}

[System.Serializable]
public class NPCDialogueData
{
    public Transform NPCPosition;
    public Animator NPCAnimator;
    public string Trigger1;

    [HideInInspector] public int IsTalkingAnimParameter; // repalced by STRUCTED

    [SerializeField] private DialogueData[] DialogueSentences;
    public Queue<DialogueData> sentences = new Queue<DialogueData>();

    [Serializable]//[SerializableAttribute]
    public struct DialogueData
    {
        public string AnimationBodyTriggerName;
        public string AnimationMouthTriggerName;
        [Space( 15)]
        [TextArea(1,5)]
        public string DialogueSentences;
    }


    public void InitDialogueData() // PUTS the String array into the Queue
    {
        NullCheckAnimator();
        sentences.Clear();
        IsTalkingAnimParameter = Animator.StringToHash(Trigger1);
        foreach (DialogueData sentence in DialogueSentences)
        {
            //Debug.Log("TEXT     = " + sentence.DialogueSentences);
            sentences.Enqueue(sentence);
        }
    }

    private void NullCheckAnimator()
    {
        if (NPCAnimator == null)
        {
            Debug.LogWarning("Cant find NPC ANIMATOR finding it in children");
            NPCAnimator = NPCPosition.gameObject.GetComponentInChildren<Animator>();
        }
    }
}