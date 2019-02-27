using System.Collections;
using UnityEngine;


public class NPCDialogueOverhear : DialogueSystem
{


    [Space(15)]
    [Tooltip("The Character that triggers the dialogue")]
    /// <summary>
    /// The Character that triggers the dialogue
    /// </summary>
    [SerializeField] private GameObject playerTrigger;

    protected override void Awake()
    {
        base.Awake();
        CheckPlayer();
    }
    protected override void Start()
    {
        base.Start();
        // CheckDialogBox();
    }


    //private void CheckDialogBox()
    //{
    //    if (NPCGroupCanvas == null)
    //    {
    //        Debug.LogError("Connect script with NPCGroupCanvas Prefab --> drag and drop connection has been lost");
    //        //this.enabled = false;
    //        //gameObject.SetActive(false);
    //        //return; break;
    //    }
    //    else
    //    {
    //        if (DialogueBoxParent == null)
    //        {
    //            if (DialogBoxBubblePrefab == null)
    //            {
    //                Debug.LogError("DialogueBoxParent & DialogBoxBubblePrefab == NULL |  FIX Gameobject (" + name + ") Turning off NPCDialogueOverhear script");
    //                //this.enabled = false;
    //                //gameObject.SetActive(false);
    //                //return; break;
    //            }

    //            DialogueBoxParent = Instantiate(DialogBoxBubblePrefab, NPCGroupCanvas.transform);
    //            DialogueBoxParent.SetActive(false);
    //            Debug.LogWarning("DialogueBoxParent = NULL ---> Instantiateing DialogBoxBubblePrefab");
    //        }
    //        else
    //        {
    //            DialogueBoxParent.SetActive(false);
    //        }
    //        if (DialogueBox == null)
    //        {
    //            DialogueBox = DialogueBoxParent.GetComponentInChildren<RectTransform>();
    //            Debug.LogWarning("DialogueBox == NULL Searching..");
    //            if (DialogueBox == null)
    //            {
    //                Debug.LogError(" DialogueBox == NULL | Cant find DialogBox in DialogueBoxParent Child --> turn of script / OBJ");
    //                //this.enabled = false;
    //                //gameObject.SetActive(false);
    //                //return; break;
    //            }
    //            else
    //            {
    //                Debug.Log("Found --> " + DialogueBox.name);
    //            }

    //        }
    //    }
    //}

    public override IEnumerator StartMainDialogue()
    {
        isDialogueActiv = true;
        dialogueBoxContainer.SetActive(true); // FIXME i think this lags out the game, when player first contacts the trigger (have it ON from the Start lul)

        for (int i = 0; i < conversationData.Sentences.Length; i++)
        {
            dialogueText.text = string.Empty;
            yield return null; // wait 1 frame things dosent get Connected properly with rect if e dont wait

            for (int j = 0; j < actors.Length; j++)
            {
                if (conversationData.Sentences[i].SpeakerID == actors[j].SpeakerID)
                {
                    CenterDialogueBoxToNPC(actors[j].DialogueBoxPositionTransform);
                   yield return StartCoroutine ( PlayActorTalkingAnims(actors[j].Animator, conversationData.Sentences[i].Sentence));
                    //if (actors[j].Animator != null)
                    //{
                    //    var npcAnimator = actors[j].Animator;

                    //    npcAnimator.SetTrigger("StartTalking");
                    //    npcAnimator.SetTrigger("IsTalking");

                    //    yield return StartCoroutine(TypeWriterEffect(conversationData.Sentences[i].Sentence));

                    //    actors[j].Animator.SetTrigger("EndTalking"); // I need a check to se if i can continue if the next NPC is the same as this one

                    //}
                    //else
                    //{
                    //    Debug.LogError("Cant find NPC animator, Cant play Talking Anim");
                    //}
                }
            }
        }
        EndDialouge();
    }

    public override IEnumerator StartLoopDialogue()
    {
        Debug.Log("NO LOOPING");
        yield return null;
    }

    public override void EndDialouge()
    {
        //  dialogueCanvas.SetActive(false);
        this.enabled = false;
        isDialogueActiv = false;
        isMainDialogueFinished = true;
        dialogueBoxContainer.SetActive(false); // TODO Add an animation to fade out dialogue text insted of setActiv
        // maybe add an event to signale end 
        Debug.Log("Dialogue End = " + name);

    }

    // --------------------------------------- CRATE PARENT Class (Contect Trigger dialogue) -----------------------------------
    private void CheckPlayer()
    {
        if (playerTrigger == null)
        {
            Debug.LogError(name + " Dose not have a Player To trigger Dialogue");
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == playerTrigger.tag && isDialogueActiv == false && isMainDialogueFinished == false)
        {
            TriggerDialogue();
            //cCollider2D.enabled = false;
            Debug.Log("START NPC MAIN Dialogue");
        }
        else if (col.tag == playerTrigger.tag && isDialogueActiv == false && isMainDialogueFinished == true)
        {
            TriggerDialogueLoop();
            Debug.Log("START NPC LOOP Dialogue");
        }

        // If(Dialogue is over) -> StartLoopDialogue
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == playerTrigger.tag)
        {
            Debug.Log("EXIT Dialogue Range");
            //playerExitDialogue = true;
            // cCollider2D.enabled = false;
        }
    }
    // ---------------------------------------  -----------------------------------



}
