using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate_Shooting : The_Default_Transition_Info {

	public TheAnimator _TheObject;
	public bool ShootValue = true;
	public bool OnEnterOrExit = true;

	public override void OnEnter(){
		if (OnEnterOrExit == true)
			_TheObject.MyAnimator.SetBool (_TheObject.AnimatorVariables[2], ShootValue);
	}

	public override void OnExit(){
		if(OnEnterOrExit == false)
			_TheObject.MyAnimator.SetBool (_TheObject.AnimatorVariables[2], ShootValue);
		
	}

	public  override void OnReset(){

	}

}
