using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStopExitCheck : StateMachineBehaviour {

	public bool StopExitCheck = true;
	public bool OnEnterOrExit = true;
	
	// OnStateEnter is called before OnStateEnter is called on any state inside this state machine
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		
		if (OnEnterOrExit == true) {
				animator.SetBool ("StopExitCheck", StopExitCheck);
		}

	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		if (OnEnterOrExit == false) {
				animator.SetBool ("StopExitCheck", StopExitCheck);
		}

	}


}
