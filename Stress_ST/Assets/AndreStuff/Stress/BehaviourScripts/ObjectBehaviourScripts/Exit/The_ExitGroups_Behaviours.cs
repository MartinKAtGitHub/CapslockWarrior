using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class The_ExitGroups_Behaviour {

	public int ChangeToPhase = 0;
	public bool CheckAllTheTime = false;
	[Space(10)]
	public The_Default_Behaviour[] ExitRequirements;
	
}
