using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupDialogue : MonoBehaviour
{

    public string PlayerTAG;
    public NPCDialog[] NPCGroup;
    [Space(25f)]
    public Camera MainCam; // GET THIS FROM GAME MANAGER STATIC;
    public Canvas GroupDialogueCanvas;

    private List<Transform> activTextBoxes;

   // private float offset;

    // Use this for initialization
    void Start()
    {
        activTextBoxes = new List<Transform>();
        GroupDialogueCanvas = transform.GetComponentInChildren<Canvas>();

        for (int i = 0; i < NPCGroup.Length; i++)
        {
            NPCGroup[i].TextObject = Instantiate(NPCGroup[i].TextObject, GroupDialogueCanvas.gameObject.transform);

            NPCGroup[i].StartDialog();

            if (NPCGroup[i].TextObject == null)
            {
                Debug.LogWarning("Cant find Text object on = " + NPCGroup[i].NPCName);
            }
        }

    }


    void Update()
    {
        for (int i = 0; i < NPCGroup.Length; i++)
        {
            var textPos = MainCam.WorldToScreenPoint(NPCGroup[i].transform.position);
            // textPos.y += offset;
            textPos.y += NPCGroup[i].Textoffset;

           // textPos.y = NPCGroup[i].Textoffset;
           //For loop all boxes
            NPCGroup[i].TextObject.transform.position = textPos;
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    OffsetAllText();
        //    Debug.Log("LOLOL");
        //    PlayConversation();
        //}
    }

    public void PlayConversation()
    {
        for (int i = 0; i < NPCGroup.Length; i++)
        {
           // offset += NPCGroup[i].Textoffset;
            NPCGroup[i].DisplayNextSentence(); // I need to lag this out so it stacks
        }
    }


    private void OffsetAllText()
    {
        GroupDialogueCanvas.transform.GetComponentInChildren<Transform>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == PlayerTAG)
        {
            StartCoroutine(PlayDialogue());
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private void CreateTextObjects()
    {
        
    }

    IEnumerator PlayDialogue()
    {
        for (int i = 0; i < NPCGroup.Length; i++)
        {

             yield return null; // WAIT FOR SENTENCTE TO BE READY BEFORE GOING TO NEXT DUDE
        }
    }
}
