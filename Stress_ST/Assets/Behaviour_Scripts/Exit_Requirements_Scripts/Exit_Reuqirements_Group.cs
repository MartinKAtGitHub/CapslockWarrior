using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Exit_Reuqirements_Group {

	public bool CheckAllTheTime = false;
	public int ChangeToPhase = 0;

	public Behaviour_Default[] ExitRequirements;
	public Behaviour_Default[] Transition;

}
