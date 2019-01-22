using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class NPCDialogueOverhear : DialogueSystem
{
    private CircleCollider2D cCollider2D;
   
    [SerializeField]
    private GameObject NPCGroupCanvas;
    [SerializeField]
    private GameObject DialogBoxBubblePrefab;
    [SerializeField]
    private GameObject DialogueBoxParent;

    [SerializeField]
    private bool playerInDialgueRange;

    [SerializeField]
    private float PlayerDialogBoxOffsetY;

  
    // [SerializeField]
    // private float DialogueBoxOffsetY;
    
    //private Queue<SentenceData> SentenceDataQueue;
    protected override void Start()
    {
        base.Start();
        CheckPlayerTag();
        CheckDialogBox();
    
       // CheckMainCam();
        cCollider2D = GetComponent<CircleCollider2D>();

        //NPCPlayStartAnim();
        // initializeSentenceDataQueue();
        //StartCoroutine(PlayDialogue());
    }


    //void Update()
    //{
    //    //if (playerInDialgueRange)
    //    //{
    //    //    CenterTextBoxElementsParent(); // So it dosent move around with you
    //    //}
    //}

  
    private void CheckPlayerTag()
    {
        if (TargetPlayer == null)
        {
            Debug.LogError(" No Target PLAYER ");
            // TODO add a way for this NPC dialogue script to get the Player after he is spawned into the scene TargetPlayer = Gamemanager.player
        }
    }

    private void CheckDialogBox()
    {
        if (NPCGroupCanvas == null)
        {
            Debug.LogError("Connect script with NPCGroupCanvas Prefab --> drag and drop connection has been lost");
            //this.enabled = false;
            //gameObject.SetActive(false);
            //return; break;
        }
        else
        {
            if (DialogueBoxParent == null)
            {
                if (DialogBoxBubblePrefab == null)
                {
                    Debug.LogError("DialogueBoxParent & DialogBoxBubblePrefab == NULL |  FIX Gameobject (" + name + ") Turning off NPCDialogueOverhear script");
                    //this.enabled = false;
                    //gameObject.SetActive(false);
                    //return; break;
                }

                DialogueBoxParent = Instantiate(DialogBoxBubblePrefab, NPCGroupCanvas.transform);
                DialogueBoxParent.SetActive(false);
                Debug.LogWarning("DialogueBoxParent = NULL ---> Instantiateing DialogBoxBubblePrefab");
            }
            else
            {
                DialogueBoxParent.SetActive(false);
            }
            if (DialogueBox == null)
            {
                DialogueBox = DialogueBoxParent.GetComponentInChildren<RectTransform>();
                Debug.LogWarning("DialogueBox == NULL Searching..");
                if (DialogueBox == null)
                {
                    Debug.LogError(" DialogueBox == NULL | Cant find DialogBox in DialogueBoxParent Child --> turn of script / OBJ");
                    //this.enabled = false;
                    //gameObject.SetActive(false);
                    //return; break;
                }
                else
                {
                    Debug.Log("Found --> " + DialogueBox.name);
                }

            }
        }
    }


    //private void CheckMainCam()
    //{
    //    if (mainCam == null)
    //    {

    //      Need Gamemnager ref -> mainCam = GameManager.player.Cam
    //        mainCam = Camera.main;
    //        Debug.LogError("Cant Find Main Cam --> look in Game Manager --> Setting MainCam ffrom Camera.main = " + mainCam.gameObject.name);
    //    }
    //}

    //private void CenterTextBoxElementsParent()
    //{
    //    //Debug.Log("Center CAM");
    //    var PnlWorld = mainCam.WorldToScreenPoint(transform.position);
    //    DialogueBoxParent.transform.position = new Vector2(PnlWorld.x, PnlWorld.y /*+ DialogueBoxOffsetY*/);
    //}

    /* private void initializeSentenceDataQueue()
     {
         SentenceDataQueue = new Queue<SentenceData>();
         //SentenceDataQueue.Clear();

         foreach (SentenceData sentence in sentenceDataArray)
         {
             //Debug.Log("TEXT     = " + sentence.DialogueSentences);
             SentenceDataQueue.Enqueue(sentence);
         }
     }*/

    public override IEnumerator StartMainDialogue()
    {
        playerInDialgueRange = true;
        DialogueBoxParent.SetActive(true); // FIXME i think this lags out the game, when player first contacts the trigger (have it ON from the Start lul)

        for (int i = 0; i < sentenceDataArray.Length; i++)
        {
            dialogueText.text = string.Empty;

            yield return null; // wait 1 frame things dosent get Connected properly with rect if e dont wait

            // Center Spech bubble on top of Speaking NPC/obj
            // DialogueBox.transform.position = mainCam.WorldToScreenPoint(new Vector2(sentenceDataArray[i].NPC.transform.position.x, sentenceDataArray[i].NPC.transform.position.y + PlayerDialogBoxOffsetY));
            CenterDialogueBoxToNPC(sentenceDataArray[i].DialoguePivotCenterPoint.transform);

            if (sentenceDataArray[i].DialoguePivotCenterPoint.GetComponentInChildren<Animator>() != null)
            {
                var npcAnimator = sentenceDataArray[i].DialoguePivotCenterPoint.GetComponentInChildren<Animator>();
                npcAnimator.SetTrigger("StartTalking");
                npcAnimator.SetTrigger("IsTalking");
            }
            else
            {
                Debug.LogError("Cant find NPC animator, Cant play Talking Anim");
            }
   
            yield return StartCoroutine(TypeWriterEffect(sentenceDataArray[i].Sentence));

            //if(i == lenght)
            //then end
            //else (previous == current)
            // continue
            //else end
            sentenceDataArray[i].DialoguePivotCenterPoint.GetComponentInChildren<Animator>().SetTrigger("EndTalking"); // I need a check to se if i can continue if the next NPC is the same as this one
        }

        NPCGroupCanvas.SetActive(false);
        //DialogueBoxParent.gameObject.SetActive(false);
        playerInDialgueRange = false;
        this.enabled = false;
        cCollider2D.enabled = false;

        EndDialouge();
    }

    public override IEnumerator StartLoopDialogue()
    {
        Debug.Log("NO LOOPING");
        yield return null;
    }

    public override void EndDialouge()
    {
        isDialogueActiv = false;
        isMainDialogueFinished = true;

        DialogueBoxContainer.SetActive(false); // TODO Add an animation to fade out dialogue text insted of setActiv
        // maybe add an event to signale end 
        Debug.Log("Dialogue End = " + name);

    }

    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.tag == TargetPlayer.tag && playerInDialgueRange == false)
    //    {
    //        StartCoroutine(PlayDialogue());
    //        //cCollider2D.enabled = false;
    //        Debug.Log("Starting Group NPC Dialogue");
    //    }
    //}
    //void OnTriggerExit2D(Collider2D col)
    //{
    //    if (col.tag == TargetPlayer.tag)
    //    {
    //        Debug.Log("EXIT Group NPC Dialogue");
    //        //playerExitDialogue = true;
    //        // cCollider2D.enabled = false;
    //    }
    //}


    //private void lol()
    //{

    //    int[] npc = new int[3];

    //    list<npcdialoguedata> npcs = new list<npcdialoguedata>();

    //    list<string> npcsenteces = new list<string>();

    //    // 0 hello

    //    // 1 hi

    //    // 0 whats you name

    //    // 1 bob




    //    for (int i = 0; i < npc.length; i++)
    //    {
    //           // npcs[npcsenteces[i].substring(0,1)]

    //    }

    //}
}
