using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheAnimator : MonoBehaviour {

	public Animator MyAnimator;

	///<summary>
	///[0] == Stop, [1] == AnimatorStage, [2] == Shoot, [3] == LockDirection
	/// </summary>
	public int[] AnimatorVariables = new int[5] {//TODO Going To Add This To The GameManager, Depending On How Expensive This Is
		Animator.StringToHash ("Stop"),
		Animator.StringToHash ("AnimatorStage"),
		Animator.StringToHash ("Shoot"),
		Animator.StringToHash ("Rotating"),
		Animator.StringToHash ("StopExitCheck")
	};

	void SetShooting(int boolValue){
		if (boolValue == 0) {
			MyAnimator.SetBool (AnimatorVariables [2], false);
		} else {
			MyAnimator.SetBool(AnimatorVariables[2], true);
		}
	}

	void SetRotating(int boolValue){
		if (boolValue == 0) {
			MyAnimator.SetBool (AnimatorVariables [3], false);
		} else {
			MyAnimator.SetBool(AnimatorVariables[3], true);
		}
	}

	void SetStop(int boolValue){
		if (boolValue == 0) {
			MyAnimator.SetBool (AnimatorVariables [0], false);
		} else {
			MyAnimator.SetBool(AnimatorVariables[0], true);
		}
	}

	void SetAnimatorStage(int Stage){
		MyAnimator.SetFloat (AnimatorVariables [1], Stage);
	}

	void SetStopExitCheck(int boolValue){
		if (boolValue == 0) {
			MyAnimator.SetBool (AnimatorVariables [4], false);
		} else {
			MyAnimator.SetBool(AnimatorVariables[4], true);
		}
	}

}
