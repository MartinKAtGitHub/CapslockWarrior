using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class  DialogueSystem : MonoBehaviour
{
    /// <summary>
    /// The Character that triggers the dialogue
    /// </summary>
    [SerializeField] protected GameObject TargetPlayer;
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
    [SerializeField] protected SentenceData[] sentenceDataArray;


    protected bool isDialogueActiv;

    void Start()
    {
        CheckMainCam();
        CheckSentenceDataNull();
    }

    void Update()
    {
        AnchorDialogueBoxContainer();
    }

    private void AnchorDialogueBoxContainer() // If we dont do this the panel will move with the player because its screen UI not in-game
    {
        var PnlWorld = mainCam.WorldToScreenPoint(transform.position);
        DialogueBoxContainer.transform.position = new Vector2(PnlWorld.x, PnlWorld.y /*+ DialogueBoxOffsetY*/);
    }
    private void CheckMainCam()
    {
        if (mainCam == null)
        {

            // mainCam = GameManager . player . Cam
            Debug.LogError("Cant Find Main Cam --> look in Game Manager and set the main Cam");
        }
    }
    private void CheckSentenceDataNull()
    {
        if(sentenceDataArray.Length != 0)
        {
            for (int i = 0; i < sentenceDataArray.Length; i++)
            {
                if(sentenceDataArray[i].NPC == null)
                {
                    Debug.Log(name + " | Missing (NPC) To Sentance");
                    return;
                }

            }
        }
        else
        {
            Debug.LogError( name + " | Dialogue dose not have any Data to print");
            return;
        }
    }

    /// <summary>
    /// Centers the DialogueBox on the NPC(imagin a speech bubble)
    /// </summary>
    /// <param name="NPC"></param>
    protected void AnchorDialogueBoxToNPC(Transform NPC)
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

}
