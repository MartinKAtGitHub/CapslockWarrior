using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems; // <------ CHECK THIS OUT i have no clue what this is 

public class DynamicListener : MonoBehaviour
{

    /// <summary>
    /// Attaching some exstra listeners to the buttons
    /// 
    /// 1. if Button is destryoed or disabels you sould remove the listners | myselfButton.onClick.RemoveListener(() => actionToMaterial(index)); |
    /// </summary>

    public string objectListeningTag = "Enter GameObject's Tag";
    public bool thisIsListener = false;
    public bool parameter = false;
    public string sendMessage = "Enter GameObject's method name";
    public string messageParameter;


    private Button btn;
    GameObject objectListening;

	// Use this for initialization
	void Start ()
    {
        btn = GetComponent<Button>();
        GetObjectListening();
	}
	

    private void GetObjectListening()
    {
        if(thisIsListener)
        {
            objectListening = gameObject;
            //Debug.Log("this is list");
        }
        else
        {
            // if we are listening to another object
            objectListening = GameObject.FindGameObjectWithTag(objectListeningTag);
        }

        if(objectListening)
        {
            SetListener();
           // Debug.Log("listning");
        }
    }

    private void SetListener()
    {
        if(btn)
        {
            if(!parameter)
            {
                btn.onClick.AddListener(()=> objectListening.SendMessage(sendMessage)); // SendMessage is the method name on the game object eks DoWhatever()

                Debug.Log("no PARA");
            }
            else
            {
                btn.onClick.AddListener(() => objectListening.SendMessage(sendMessage, messageParameter)); // This also sends a paramater with it eks SetNextPage("MenuPage");
                Debug.Log("With Para");
            }
        }
        else
        {
            Debug.LogError("Dynamic Listeners belong on buttons");
           // btn.interactable = false;
        }
    }
}
