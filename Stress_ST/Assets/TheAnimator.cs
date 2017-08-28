﻿using System.Collections;
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
		Animator.StringToHash ("LockDirection"),
		Animator.StringToHash ("StopExitCheck")
	};

}
