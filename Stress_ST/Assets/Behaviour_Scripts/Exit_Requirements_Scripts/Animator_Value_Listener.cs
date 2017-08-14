using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Value_Listener : Default_ExitRequirement_Behaviour {

	public float AnimatorStageToListenTo = 0;
	Animator MyAnimator;
	int[] _AnimatorVariables;

	public override void SetMethod (Object_Behaviour myTransform, Transform targetTransform, int[] AnimatorValues){
		MyAnimator = myTransform.MyAnimator;
		_AnimatorVariables = AnimatorValues;
	}

	public override bool GetBool (int index){
		if (index == 0) {
			if (MyAnimator.GetFloat (_AnimatorVariables [2]) == AnimatorStageToListenTo) {
				return true;
			} else {
				return base.GetBool (index);
			}
		} else {
			return false;
		}
	
	}
}
