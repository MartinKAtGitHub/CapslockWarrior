using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class NPCDialogueOverhear : MonoBehaviour
{
    private CircleCollider2D cCollider2D;

    [SerializeField]
    private string playerTag;
    [SerializeField]
    private float typeingSpeed;
    [SerializeField]
    private float nextMessageSpeed;
    [SerializeField]
    private RectTransform textBox_pnl;
    [SerializeField]
    private RectTransform DialogueBox;
    [SerializeField]
    private TextMeshProUGUI dialogueText;
    [SerializeField]
    private bool playerInDialgueRange;
    [SerializeField]
    private Camera mainCam;
    [SerializeField]
    private float dialogBoxOffsetY;

    [SerializeField]
    private SentenceData[] SentenceDatas;

    //private Queue<SentenceData> SentenceDataQueue;



    private void Start()
    {
        checkPlayerTag();
        cCollider2D = GetComponent<CircleCollider2D>();
        // initializeSentenceDataQueue();
        //StartCoroutine(PlayDialogue());
    }


    void Update()
    {
        if (playerInDialgueRange)
        {
            CenterTextBoxElementsParent();
        }
    }

    private void checkPlayerTag()
    {
        if (string.IsNullOrEmpty(playerTag))
        {
            Debug.LogError("Cant Find PlayerTag NPCDialogueOverhear");
            //playerTag = Gamemanager.PlayerTag
        }
    }

    private void CenterTextBoxElementsParent()
    {
        Debug.Log("Center CAM");
        var PnlWorld = mainCam.WorldToScreenPoint(transform.position);
        textBox_pnl.transform.position = new Vector2(PnlWorld.x, PnlWorld.y + 50f);
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

        for (int i = 0; i < SentenceDatas.Length; i++)
        {
            dialogueText.text = string.Empty;

            //DialogueBox.position = mainCam.WorldToScreenPoint(SentenceDatas[i].NPC.transform.position); // kind of workd
            //DialogueBox.transform.localPosition = new Vector2(SentenceDatas[i].NPC.transform.localPosition.x, SentenceDatas[i].NPC.transform.localPosition.y + dialogBoxOffsetY);
            //DialogueBox.transform.InverseTransformPoint(SentenceDatas[i].NPC.transform.position.x, SentenceDatas[i].NPC.transform.position.y, 0);
            yield return null; // wait 1 frame beacuse rect transform is BrokeBack

            DialogueBox.transform.position = mainCam.WorldToScreenPoint(new Vector2(SentenceDatas[i].NPC.transform.position.x, SentenceDatas[i].NPC.transform.position.y + dialogBoxOffsetY));


            foreach (var letters in SentenceDatas[i].Sentence)
            {
                dialogueText.text += letters;
                yield return new WaitForSeconds(typeingSpeed);
            }
            yield return new WaitForSeconds(nextMessageSpeed);
        }
        textBox_pnl.gameObject.SetActive(false);
        playerInDialgueRange = false;


    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == playerTag && playerInDialgueRange == false)
        {
            StartCoroutine(PlayDialogue());
            //cCollider2D.enabled = false;
            Debug.Log("Starting Group NPC Dialogue");
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == playerTag)
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
