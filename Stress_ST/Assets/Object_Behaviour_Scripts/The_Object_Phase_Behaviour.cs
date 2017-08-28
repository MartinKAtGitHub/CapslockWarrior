using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class The_Object_Phase_Behaviour {

	[Tooltip("The CoolDown On When This Phase Can Be Entered")]
//	public float ColdownTimer = 0;
	public The_Default_Transition_Info[] PhaseChangeInfo;

	[Space(10)]
	[Tooltip("Object Behaviour, If You Want To Run Straight To The Target Then Drag And Drop The Script To Here To Get The Behaviour Running")]
	public The_Default_Behaviour[] Behaviours;
	[Tooltip("Set Of Exit Requirements If You Want More Then One Requirement, 'If Distance < 10 && Distance > 3' -> Do This. This Makes It So That You Can Do Muliple Checks")]
	public The_ExitGroups_Behaviour[] ExitGroups;
	
}
