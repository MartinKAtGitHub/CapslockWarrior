using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueOverhear : MonoBehaviour
{


    private float TypeingSpeed;

    [SerializeField]
    private SentenceData[] SentenceDatas;
    private Queue<SentenceData> SentenceDataQueue;
    
    private void Start()
    {
        initializeSentenceDataQueue();
    }

    private void initializeSentenceDataQueue()
    {
        SentenceDataQueue = new Queue<SentenceData>();
        //SentenceDataQueue.Clear();

        foreach (SentenceData sentence in SentenceDatas)
        {
            //Debug.Log("TEXT     = " + sentence.DialogueSentences);
            SentenceDataQueue.Enqueue(sentence);
        }
    }

    IEnumerator PlayDialogue()
    {

        for (int i = 0; i < SentenceDatas.Length; i++)
        {
            foreach (var letters in SentenceDatas[i].Sentence)
            {
               // textBoxCloneText.text += letters;
                yield return new WaitForSeconds(TypeingSpeed);
            }
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
