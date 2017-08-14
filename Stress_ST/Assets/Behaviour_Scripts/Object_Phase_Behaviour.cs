using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Object_Phase_Behaviour {

	public bool CheckAttackBehaviour = false;

	public Behaviour_Default[] Attack;
	public Behaviour_Default[] Move;

	public Exit_Reuqirements_Group[] ExitRequirementGroup;

}
  