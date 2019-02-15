using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneDialogue : DialogueSystem
{
    [SerializeField] private bool displayNextSentence;
    [SerializeField] private bool readyForNextSentence;

    [SerializeField] private PlayableDirector playableDirector;
    
    protected override void Start()
    {
        base.Start();

        nextMessageSpeed = 0;
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

    public override IEnumerator StartLoopDialogue()
    {
        Debug.Log("NO LOOPING Dialogue " + name);
        yield return null;
    }

    public override IEnumerator StartMainDialogue()
    {
        // playableDirector.Pause();
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);

        isDialogueActiv = true;
        dialogueBoxContainer.SetActive(true); // FIXME i think this lags out the game, when player first contacts the trigger (have it ON from the Start lul)

        for (int i = 0; i < conversationData.Sentences.Length; i++)
        {
            dialogueText.text = string.Empty;
            skipDialogue = false;

            yield return null; // wait 1 frame things dosent get Connected properly with rect if e dont wait

            for (int j = 0; j < actors.Length; j++)
            {
                if (conversationData.Sentences[i].SpeakerID == actors[j].SpeakerID)
                {
                    CenterDialogueBoxToNPC(actors[j].DialogueBoxPositionTransform);

                    yield return StartCoroutine(PlayActorTalkingAnims(actors[j].Animator, conversationData.Sentences[i].Sentence));
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

                    readyForNextSentence = true;

                    yield return new WaitUntil(() => displayNextSentence == true);

                    displayNextSentence = false;
                    readyForNextSentence = false;
                }
            }
        }
        //  playableDirector.Resume();
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);

        EndDialouge();

    }



    private void NextDialogueFlag()
    {
        Debug.Log("CLICKING ENETER");

        if(readyForNextSentence)
        {
            displayNextSentence = !displayNextSentence;
            Debug.Log("BOOL IS = " + displayNextSentence);

         

        }
        else
        {
            skipDialogue = true;
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.PlayerInputManager.OnActionKeyDown -= NextDialogueFlag;
    }
    private void OnEnable()
    {
        GameManager.Instance.PlayerInputManager.OnActionKeyDown += NextDialogueFlag;
    }
}
