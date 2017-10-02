using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimatorStage : StateMachineBehaviour {

	public int Value;
	public bool OnEnterOrExit = true;


	// OnStateEnter is called before OnStateEnter is called on any state inside this state machine
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		if (OnEnterOrExit == true) {
			animator.SetInteger ("AnimatorStage", Value);
		}

	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		if (OnEnterOrExit == false) {
			animator.SetInteger ("AnimatorStage", Value);
		}

	}


}
