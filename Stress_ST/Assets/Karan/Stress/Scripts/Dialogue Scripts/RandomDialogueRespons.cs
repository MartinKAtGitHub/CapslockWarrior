using System.Collections;
using UnityEngine;

public class RandomDialogueRespons : DialogueSystem
{
    [Space(10)]
    [SerializeField] Animator guardAnimator;
    /// <summary>
    /// Used to crate a short delay before Animation starts or els all anims would be in sync and make it look Robotic
    /// </summary>
    [SerializeField] Vector2 RandomDelay;

    [SerializeField] private GameObject playerTrigger;

    readonly int idlelookOut = Animator.StringToHash("Idle1");
    readonly int idleFlipStick = Animator.StringToHash("Idle2");
    readonly int idleCrossArms = Animator.StringToHash("Idle3");
   
    private new void Start()
    {
        base.Start();
        StartCoroutine(PlayerRandomIdleAnim());
    }

    public override IEnumerator StartLoopDialogue()
    {
        Debug.Log("Looping Dialogue");
       yield return StartCoroutine(StartMainDialogue()); // The guard only has a Loop so we just call the Loop  as the main dialogue
    }

    public override IEnumerator StartMainDialogue()
    {
        isDialogueActiv = true;
        dialogueBoxContainer.SetActive(true);

        dialogueText.text = string.Empty;
        yield return null;

        for (int i = 0; i < actors.Length; i++)// There will most liky only be 1 BUT if we every want a groupe to have Random coversation we can
        {
            if (conversationData.Sentences[i].SpeakerID == actors[i].SpeakerID)
            {
                CenterDialogueBoxToNPC(actors[i].DialogueBoxPositionTransform);
                yield return StartCoroutine(PlayActorTalkingAnims(actors[i].Animator, conversationData.Sentences[Random.Range(0, conversationData.Sentences.Length)].Sentence));
            }
        }

        EndDialouge();
    }


    public override void EndDialouge()
    {
        isDialogueActiv = false;
        isMainDialogueFinished = true;
        
        dialogueBoxContainer.SetActive(false); // TODO Add an animation to fade out dialogue text insted of setActiv
        // maybe add an event to signale end 
        Debug.Log("Dialogue End");
    }
 
    IEnumerator PlayerRandomIdleAnim()
    {
        var randTime = Random.Range(RandomDelay.x, RandomDelay.y); //TODO Get the the 12(sample) frames and choose a random time from that
        yield return new WaitForSeconds(randTime);

        var idleAnimValue = Random.Range(0 , 3); // 3 dosent exist cuz Computers
        //Debug.Log("Guard Anim = " + idleAnimValue);
        switch (idleAnimValue)
        {
            case 0:
                guardAnimator.SetTrigger(idlelookOut);
                break;
            case 1:
                guardAnimator.SetTrigger(idleFlipStick);
                break;
            case 2:
                guardAnimator.SetTrigger(idleCrossArms);
                break;
            default:
                Debug.Log(" PlayerRandomIdleAnim() Switch faild Default set --> no anim ");
                break;
        }
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
        }
        else if (col.tag == playerTrigger.tag && isDialogueActiv == false && isMainDialogueFinished == true)
        {
            TriggerDialogueLoop();
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == playerTrigger.tag)
        {
            //playerExitDialogue = true;
            // cCollider2D.enabled = false;
        }
    }
    // --------------------------------------------------------------------------


}
