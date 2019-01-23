using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCData : MonoBehaviour
{
    [SerializeField] private int speakerID;

    [SerializeField] private Transform dialogueBoxPositionTransform;
    [SerializeField] private Animator animator;

    /// <summary>
    /// The object you put in here will be the Marker/Position the Dialog box will move to. So position the obj where you want the dialogue box to go
    /// </summary>
    public Transform DialogueBoxPositionTransform { get => dialogueBoxPositionTransform; }
    /// <summary>
    /// ANimator that controlles the mouth and body animation of the NPC 
    /// </summary>
    public Animator Animator { get => animator;  }

    public int SpeakerID { get => speakerID;}
}
