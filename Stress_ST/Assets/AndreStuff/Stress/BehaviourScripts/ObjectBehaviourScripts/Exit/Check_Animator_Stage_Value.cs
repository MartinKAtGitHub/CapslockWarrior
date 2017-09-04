using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Animator_Stage_Value : The_Default_Exit_Behaviour {

	public float AnimatorStageToListenTo = 0;
	public Animator MyAnimators;
	int[] _AnimatorVariables;

	public override void SetMethod (The_Object_Behaviour myTransform){
		
		_AnimatorVariables = myTransform.AnimatorVariables;
		MyAnimators = myTransform._TheObject.GfxObject.GetComponent<Animator>();
	
	}

	public override bool GetBool(int index){
		if (index == 2) {
			if (MyAnimators.GetFloat (_AnimatorVariables [1]) == AnimatorStageToListenTo) {
				return true;
			} else {
				return false;
			}
		} else {
			return base.GetBool (index);
		}
	}


}
