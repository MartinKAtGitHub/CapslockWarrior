using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupDialogue : MonoBehaviour {


    public NPCDialog[] NPCGroup;

	// Use this for initialization
	void Start ()
    {
		
	}
	

    [System.Serializable]
    public struct NPCDialog
    {
        public string[] Dialogue;
    }
}
