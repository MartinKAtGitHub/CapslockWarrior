using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Done_AnimatorParameter : MonoBehaviour {

	public Animator MyAnimator;

	///<summary>
	///[0] == Done
	/// </summary>
	public int[] AnimatorVariables = new int[1] {//TODO Going To Add This To The GameManager, Depending On How Expensive This Is
		Animator.StringToHash ("Done"),
	};


	void SetDone(int boolValue){
		if (boolValue == 0) {
			MyAnimator.SetBool (AnimatorVariables [0], false);
		} else {
			MyAnimator.SetBool(AnimatorVariables[0], true);
		}
	}

}