using System.Collections;
using TMPro;
using UnityEngine;

public abstract class  DialogueSystem : MonoBehaviour
{
    /// <summary>
    /// The Data Needed to determin DialogBox Position, Animator and ID. The order of the List will be the ID of the NPC(who says what)
    /// </summary>
    [SerializeField] protected NPCData[] actors;
    //[SerializeField] protected List<NPCData> actors;// ont be adding in runtime so this is not needed

    [Space(10)]
    /// <summary>
    /// The Conversation beteween NPCs. This is a Scripable object
    /// </summary>
    [SerializeField] protected FullConversationData conversationData;

    [Space(15)]
    
    /// <summary>
    /// The camera the dialogue Box will use to center the Dialogue UI
    /// </summary>
    [SerializeField] private Camera mainCam;

    [Space(10)]
    /// <summary>
    /// The speed at which the letters are beeing displayed 
    /// </summary>
    [SerializeField] protected float typeingSpeed;
    /// <summary>
    /// Time the sentence will stay on screen before next sentence will start
    /// </summary>
    [SerializeField] protected float nextMessageSpeed;
    /// <summary>
    /// The speed the dialogue text will go when you want to skip the dialogue
    /// </summary>
    [SerializeField] protected float skipTypeingSpeed;


    [Space(10)]
    /// <summary>
    /// The canavs witch holds the Dialogue box of the currant instance
    /// </summary>
  //  [SerializeField] protected GameObject dialogueCanvas;
    /// <summary>
    /// Holds the DialogueBox so we can use local space to move DialogueBox around
    /// </summary>
    [SerializeField] protected GameObject dialogueBoxContainer;
    /// <summary>
    /// the Actual bubble witch holds the Text
    /// </summary>
    [SerializeField] protected RectTransform dialogueBox;
    /// <summary>
    /// The text element in the DialogueBox
    /// </summary>
    [SerializeField] protected TextMeshProUGUI dialogueText;


    /// <summary>
    /// Am i currently in a dialogue. Prevents player from running inn and out of trigger starting new dialogue
    /// </summary>
    protected bool isDialogueActiv;
    /// <summary>
    /// Is the main dialogue finished. Used for when we want to start to loop dialogue
    /// </summary>
    protected bool isMainDialogueFinished;
    /// <summary>
    /// If true dialogue is beeing skiped
    /// </summary>
    protected bool skipDialogue;


    protected virtual void Awake()
    {
        FindNPCs();
        CheckConversationHasSpeakersAssigned();
    }

    protected virtual void Start()
    {
        CheckMainCam();
        //CheckSentenceDataNull();
    }

    void Update()
    {
        MakeDialogueBoxParentStationaryAboveTarget(); //PERFORMANCE Check to see if in range/indialogue 
    }

    public virtual void TriggerDialogue()
    {
        StartCoroutine(StartMainDialogue());
    }

    public virtual void TriggerDialogueLoop()
    {
        StartCoroutine(StartLoopDialogue());
    }


    private void MakeDialogueBoxParentStationaryAboveTarget() // If we dont do this the panel will move with the player because its screen UI not in-game
    {
       if(isDialogueActiv)
       {
            var PnlWorld = mainCam.WorldToScreenPoint(transform.position);
            dialogueBoxContainer.transform.position = new Vector2(PnlWorld.x, PnlWorld.y /*+ DialogueBoxOffsetY*/);
       }
    }
    private void CheckMainCam()
    {
        if (mainCam == null)
        {
            // mainCam = GameManager . player . Cam
            mainCam = Camera.main; // This is a direct ref to cam with the MainCamera Tag on
            Debug.LogError("Cant Find Main Cam --> look in Game Manager and set the main Cam");
        }
    }
    private void FindNPCs()
    {
        actors =  GetComponentsInChildren<NPCData>();
        if(actors == null)
        {
            Debug.LogError("NO NPC FOUND, Dialogue system needs NPCs(NPCData) to connect the dialogue to");
        }
    }
    private void CheckConversationHasSpeakersAssigned()
    {
        if(conversationData != null)
        {
            for (int i = 0; i < conversationData.Sentences.Length; i++)
            {
                var counter = 0;

                for (int j = 0; j < actors.Length; j++)
                {
                    if(conversationData.Sentences[i].SpeakerID == actors[j].SpeakerID)
                    {
                        break;
                    }
                    counter++;
                }

                if(counter >= actors.Length)
                {
                    Debug.LogError("Sentences in >(" + name + " )Has SpeakerID (" + conversationData.Sentences[i].SpeakerID + ") But NO NPC has this ID" );
                }
            }
        }else
        { 
            Debug.LogWarning(name +" Has no Conversation Data Assigned");
            //gameObject.SetActive(false);
            //enabled = false;
        }
    }

   
    /// <summary>
    /// Centers the DialogueBox on the NPC(imagin a speech bubble)
    /// </summary>
    /// <param name="dialogueBoxPosition"></param>
    protected void CenterDialogueBoxToNPC(Vector3 dialogueBoxPosition)
    {
        dialogueBox.transform.position = mainCam.WorldToScreenPoint(
            new Vector2(dialogueBoxPosition.x, dialogueBoxPosition.y));
    }

    //WARNING The type worter effect gets skiped
    protected IEnumerator PlayActorTalkingAnims(Animator actorAnimator, string Sentence)
    {
        if (actorAnimator != null)
        {
            var npcAnimator = actorAnimator;

            npcAnimator.SetTrigger("StartTalking");
            npcAnimator.SetTrigger("IsTalking");

            yield return StartCoroutine(TypeWriterEffect(Sentence));

            npcAnimator.SetTrigger("EndTalking"); // I need a check to se if i can continue if the next NPC is the same as this one

        }
        else
        {
            Debug.LogError("Cant find NPC animator, Cant play Talking Anim");
        }
    }


    /// <summary>
    /// Feed a string into this and it will write the string into the textbox like a Typewriter
    /// </summary>
    /// <param name="sentence"></param>
    /// <returns></returns>
    private IEnumerator TypeWriterEffect(string sentence)
    {
        foreach (var letters in sentence)
        {
            dialogueText.text += letters;
            if(!skipDialogue)
            {
                yield return new WaitForSeconds(typeingSpeed);
            }
            else
            {
                yield return new WaitForSeconds(skipTypeingSpeed);
            }
        }
        yield return new WaitForSeconds(nextMessageSpeed);  
    }

    /// <summary>
    /// Use this to Start the dialogue from some place
    /// </summary>
    /// <returns></returns>
    public abstract IEnumerator StartMainDialogue();
    /// <summary>
    /// If you want to some generic stuff to happen after the main Dialogue is finished
    /// </summary>
    /// <returns></returns>
    public abstract IEnumerator StartLoopDialogue();
    /// <summary>
    /// Add this to when you want to end the dialogue. Signaling end of dialogue sequence
    /// </summary>
    public abstract void EndDialouge();


    //TODO Move Trigger Dialogue on Collistion trigger to the overhear script.
    
   

}
