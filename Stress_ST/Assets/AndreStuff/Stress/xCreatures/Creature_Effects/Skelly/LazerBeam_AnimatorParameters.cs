using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBeam_AnimatorParameters : MonoBehaviour {

	public int[] AnimatorValue = new int[3]{ 
		Animator.StringToHash ("AnimatorStage"),
		Animator.StringToHash ("DoingDmg"),
		Animator.StringToHash ("ShutDown"),
	};


}
