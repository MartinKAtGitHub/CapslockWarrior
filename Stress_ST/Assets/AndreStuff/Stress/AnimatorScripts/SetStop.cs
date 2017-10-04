﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStop : StateMachineBehaviour {

	public bool StopValue = true;
	public bool OnEnterOrExit = true;

	// OnStateEnter is called before OnStateEnter is called on any state inside this state machine
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		if (OnEnterOrExit == true) {
				animator.SetBool ("Stop", StopValue);
		}

	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		if (OnEnterOrExit == false) {
				animator.SetBool ("Stop", StopValue);
		}

	}


}