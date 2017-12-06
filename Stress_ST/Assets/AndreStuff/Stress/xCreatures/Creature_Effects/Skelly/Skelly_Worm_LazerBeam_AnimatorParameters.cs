using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skelly_Worm_LazerBeam_AnimatorParameters : MonoBehaviour {

	public int[] AnimatorValue = new int[3]{ 
		Animator.StringToHash ("AnimatorStage"),
		Animator.StringToHash ("DoingDmg"),
		Animator.StringToHash ("ShutDown"),
	};


}
