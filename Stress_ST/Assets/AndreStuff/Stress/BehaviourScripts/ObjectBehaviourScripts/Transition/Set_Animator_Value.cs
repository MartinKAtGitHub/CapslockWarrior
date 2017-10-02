using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Animator_Value : The_Default_Transition_Info {

	public TheAnimator AnimatorVariables;
	public int AnimatorValue = 0;

	public override void OnEnter(){
		AnimatorVariables.MyAnimator.SetInteger (AnimatorVariables.AnimatorVariables[1], AnimatorValue);

	}

	public override void OnExit(){

	}

	public  override void OnReset(){

	}


}
