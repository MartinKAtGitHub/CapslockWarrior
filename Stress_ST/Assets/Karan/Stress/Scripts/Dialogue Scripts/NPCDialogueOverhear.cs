using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class NPCDialogueOverhear : MonoBehaviour
{
    private CircleCollider2D cCollider2D;

    [SerializeField]
    private GameObject TargetPlayer;
    [SerializeField]
    private float typeingSpeed;
    [SerializeField]
    private float nextMessageSpeed;

    [SerializeField]
    private GameObject NPCGroupCanvas;
    [SerializeField]
    private GameObject DialogBoxBubblePrefab;
    [SerializeField]
    private GameObject DialogueBoxParent;
    [SerializeField]
    private RectTransform DialogueBox;
    [SerializeField]
    private TextMeshProUGUI dialogueText;
    [SerializeField]
    private bool playerInDialgueRange;
    [SerializeField]
    private Camera mainCam;
    [SerializeField]
    private float PlayerDialogBoxOffsetY;
   // [SerializeField]
   // private float DialogueBoxOffsetY;


    [SerializeField]
    private SentenceData[] SentenceDatas;

    //private Queue<SentenceData> SentenceDataQueue;
    private void Start()
    {
      
        CheckPlayerTag();
        CheckDialogBox();
        CheckNPCs();
        CheckMainCam();
        cCollider2D = GetComponent<CircleCollider2D>();

        //NPCPlayStartAnim();
        // initializeSentenceDataQueue();
        //StartCoroutine(PlayDialogue());
    }


    void Update()
    {
        if (playerInDialgueRange)
        {
            CenterTextBoxElementsParent(); // So it dosent move around with you
        }
    }

    private void NPCPlayStartAnim()
    {
        throw new NotImplementedException();
    }

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

    private void CheckNPCs()
    {
        for (int i = 0; i < SentenceDatas.Length; i++)
        {
            if(SentenceDatas[i].NPC == null)
            {
                Debug.LogError("Missing NPC on Sentence | Remeber to Drag NPC(gameobject) on all Sentences");
                this.enabled = false;
                gameObject.SetActive(false);
                // i can also make this.gameobject the parent if we dont want to turn off
                return;
            }
        }
    }

    private void CheckMainCam()
    {
        if(mainCam == null)
        {

            // mainCam = GameManager . player . Cam
            Debug.LogError("Cant Find Main Cam --> look in Game Manager");
        }
    }

    private void CenterTextBoxElementsParent()
    {
        //Debug.Log("Center CAM");
        var PnlWorld = mainCam.WorldToScreenPoint(transform.position);
        DialogueBoxParent.transform.position = new Vector2(PnlWorld.x, PnlWorld.y /*+ DialogueBoxOffsetY*/);
    }

    /* private void initializeSentenceDataQueue()
     {
         SentenceDataQueue = new Queue<SentenceData>();
         //SentenceDataQueue.Clear();

         foreach (SentenceData sentence in SentenceDatas)
         {
             //Debug.Log("TEXT     = " + sentence.DialogueSentences);
             SentenceDataQueue.Enqueue(sentence);
         }
     }*/

    IEnumerator PlayDialogue()
    {
        playerInDialgueRange = true;
        DialogueBoxParent.SetActive(true); // FIXME i think this lags out the game, when player first contacts the trigger (have it ON from the Start lul)
      

        for (int i = 0; i < SentenceDatas.Length; i++)
        {
            dialogueText.text = string.Empty;

            yield return null; // wait 1 frame beacuse rect transform is BrokeBack
            
            // Center Dbox ontop of NPC
            DialogueBox.transform.position = mainCam.WorldToScreenPoint(new Vector2(SentenceDatas[i].NPC.transform.position.x, SentenceDatas[i].NPC.transform.position.y + PlayerDialogBoxOffsetY));

            

            if(SentenceDatas[i].NPC.GetComponentInChildren<Animator>() != null)
            {
                var npcAnimator = SentenceDatas[i].NPC.GetComponentInChildren<Animator>();
                npcAnimator.SetTrigger("StartTalking");
                npcAnimator.SetTrigger("IsTalking");
            }else
            {
                Debug.LogWarning("Cant find NPC animator");
            }

            
            // Transition --> transition anim(has exit time) --> main Anim

            foreach (var letters in SentenceDatas[i].Sentence)
            {
                dialogueText.text += letters;
                yield return new WaitForSeconds(typeingSpeed);
            }
            yield return new WaitForSeconds(nextMessageSpeed);
            
            //if(i == lenght)
                //then end
            //else (previous == current)
                // continue
            //else end
            SentenceDatas[i].NPC.GetComponentInChildren<Animator>().SetTrigger("EndTalking"); // I need a check to se if i can continue if the next NPC is the same as this one
        }

        NPCGroupCanvas.SetActive(false);
        //DialogueBoxParent.gameObject.SetActive(false);
        playerInDialgueRange = false;
        this.enabled = false;
        cCollider2D.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == TargetPlayer.tag && playerInDialgueRange == false)
        {
            StartCoroutine(PlayDialogue());
            //cCollider2D.enabled = false;
            Debug.Log("Starting Group NPC Dialogue");
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == TargetPlayer.tag)
        {
            Debug.Log("EXIT Group NPC Dialogue");
            //playerExitDialogue = true;
            // cCollider2D.enabled = false;
        }
    }


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
