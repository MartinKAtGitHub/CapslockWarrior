using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Animator_Stage_Value : The_Default_Exit_Behaviour {

	public int AnimatorStageToListenTo = 0;
	public TheAnimator AnimatorVariables;

	public override void SetMethod (The_Object_Behaviour myTransform){
	
	}

	public override bool GetBool(int index){
	
		if (index == 2) {
			if (AnimatorVariables.MyAnimator.GetInteger (AnimatorVariables.AnimatorVariables[1]) == AnimatorStageToListenTo) {
				return true;
			} else {
				return false;
			}
		} else {
			return base.GetBool (index);
		}
	}


}
