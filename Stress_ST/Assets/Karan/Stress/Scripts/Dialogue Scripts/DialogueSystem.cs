using System.Collections;
using Cinemachine;
using TMPro;
using UnityEngine;

public abstract class  DialogueSystem : MonoBehaviour
{

    //[SerializeField]
    //protected CinemachineVirtualCamera VRCam_TESTING;
    //private Vector3 temp;

    /// <summary>
    /// The Character that triggers the dialogue
    /// </summary>
    [SerializeField] protected GameObject TargetPlayer;
    /// <summary>
    /// The collider which will trigger the dialogue. Can be drag and droped if child
    /// </summary>
    [SerializeField] protected Collider2D dialogueTrigger;
    /// <summary>
    /// The speed at which the letters are beeing displayed 
    /// </summary>
    [SerializeField] private float typeingSpeed;
    /// <summary>
    /// Time the sentence will stay on screen before next sentence will start
    /// </summary>
    [SerializeField] private float nextMessageSpeed;
    /// <summary>
    /// When centring on a NPC we dont want the box to be inside of NPC so we add a offset (The center is the pivot point located on the edge)
    /// </summary>
    [SerializeField] private Vector2 DialogueBoxOffset;

    [Space(10)]
    /// <summary>
    /// The canavs witch holds the Dialogue box of the currant instance
    /// </summary>
    [SerializeField] protected GameObject DialogueCanvas;
    /// <summary>
    /// Holds the DialogueBox so we can use local space to move DialogueBox around
    /// </summary>
    [SerializeField] protected GameObject DialogueBoxContainer;
    /// <summary>
    /// the Actual bubble witch holds the Text
    /// </summary>
    [SerializeField] protected RectTransform DialogueBox;
    /// <summary>
    /// The text element in the DialogueBox
    /// </summary>
    [SerializeField] protected TextMeshProUGUI dialogueText;

    [Space(10)]
    /// <summary>
    /// The camera the dialogue Box will use to center the Dialogue UI
    /// </summary>
    [SerializeField] private Camera mainCam;
    
    [Space(10)]
    /// <summary>
    /// Assign sentnce and corresponding NPC to the sentence
    /// </summary>
    [SerializeField] protected SentenceData[] sentenceDataArray; // Make into ScriptOBJ
   
    
    
    /// <summary>
    /// Am i currently in a dialogue. Prevents player from running inn and out of trigger starting new dialogue
    /// </summary>
    protected bool isDialogueActiv;
    /// <summary>
    /// Is the main dialogue finished. Used for when we want to start to loop dialogue
    /// </summary>
    protected bool isMainDialogueFinished;


    protected virtual void Start()
    {
        CheckMainCam();
        CheckSentenceDataNull();
    }

    void Update()
    {
        MakeDialogueBoxParentStationaryAboveTarget(); //PERFORMANCE Check to see if in range/indialogue 
    }

    private void MakeDialogueBoxParentStationaryAboveTarget() // If we dont do this the panel will move with the player because its screen UI not in-game
    {
      
       //if(isDialogueActiv)
       // {
            var PnlWorld = mainCam.WorldToScreenPoint(transform.position);
            DialogueBoxContainer.transform.position = new Vector2(PnlWorld.x, PnlWorld.y /*+ DialogueBoxOffsetY*/);

        //}

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
    private void CheckSentenceDataNull()
    {
        if(sentenceDataArray.Length != 0)
        {
            for (int i = 0; i < sentenceDataArray.Length; i++)
            {
                if(sentenceDataArray[i].DialoguePivotCenterPoint == null)
                {
                    Debug.Log(name + " | Missing (NPC) To Sentance");
                    return;
                }
            }
        }
        else
        {
            Debug.LogError( name + " | Dialogue dose not have any Sentence Data to print");
            return;
        }
    }

    /// <summary>
    /// Centers the DialogueBox on the NPC(imagin a speech bubble)
    /// </summary>
    /// <param name="NPC"></param>
    protected void CenterDialogueBoxToNPC(Transform NPC)
    {
        DialogueBox.transform.position = mainCam.WorldToScreenPoint(
            new Vector2(NPC.transform.position.x + DialogueBoxOffset.x , NPC.transform.position.y + DialogueBoxOffset.y));
    }

    /// <summary>
    /// Feed a string into this and it will write the string into the textbox like a Typewriter
    /// </summary>
    /// <param name="sentence"></param>
    /// <returns></returns>
    protected IEnumerator TypeWriterEffect(string sentence)
    {
        foreach (var letters in sentence)
        {
            dialogueText.text += letters;
            yield return new WaitForSeconds(typeingSpeed);
        }
        yield return new WaitForSeconds(nextMessageSpeed);  
    }

    // Maybe add like an automated way so the stuff you need spawns in, like NPC object
    // Eks group dialuge -> add NPC list drag stuff in and spawn it
    
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == TargetPlayer.tag && isDialogueActiv == false && isMainDialogueFinished == false)
        {
            StartCoroutine(StartMainDialogue());
            //cCollider2D.enabled = false;
            Debug.Log("START NPC MAIN Dialogue");
        }
        else if(col.tag == TargetPlayer.tag && isDialogueActiv == false && isMainDialogueFinished == true)
        {
            StartCoroutine(StartLoopDialogue());
            Debug.Log("START NPC LOOP Dialogue");
        }

        // If(Dialogue is over) -> StartLoopDialogue
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == TargetPlayer.tag)
        {
            Debug.Log("EXIT Dialogue Range");
            //playerExitDialogue = true;
            // cCollider2D.enabled = false;
        }
    }


}
