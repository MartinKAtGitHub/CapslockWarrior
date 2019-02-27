using UnityEngine;

public class NPCData : MonoBehaviour
{
    [Tooltip("The ID of this NPC, This ID will be used to connect the sentence to the NPC")]
    [SerializeField] private int speakerID;

    [Tooltip("Choose which position to render the DialogueBox from. Custome is a GameObject you can create and asigne or It will use the NPC position with a Offset")]
    [SerializeField] private bool useCustomeDialogueBoxPosition;
    [Tooltip("The gameobject which will determin the custome position of dialogueBox")]
    [SerializeField] private Transform dialogueBoxPositionTransform;
    [Tooltip("When render from the NPC postion we need a offset or else the box will be inside of the NPC")]
    [SerializeField] private Vector2 dialogueBoxOffset = new Vector2(0, 1f);
    [SerializeField] private Animator animator;

    /// <summary>
    /// The object you put in here will be the Marker/Position the Dialog box will move to. So position the obj where you want the dialogue box to go
    /// </summary>
    public Vector3 DialogueBoxPositionTransform
    {
        //    get => dialogueBoxPositionTransform;

        get
        {
            if (useCustomeDialogueBoxPosition)
            {
                return dialogueBoxPositionTransform.position;
            }
            else
            {
                return transform.position + (Vector3)dialogueBoxOffset;
            }
        }
    }
    /// <summary>
    /// ANimator that controlles the mouth and body animation of the NPC 
    /// </summary>
    public Animator Animator { get => animator; }

    public int SpeakerID { get => speakerID; }

    private void Awake()
    {
        if(animator == null)
        {
            Debug.Log("animator NULL searching children(" + name + ")");
            animator = GetComponentInChildren<Animator>();
            if(animator == null)
            {
                Debug.LogError("NPC(" + name + ") Cant find a animator" );
                gameObject.SetActive(false);
                return;
            }
        }
    }
}
