﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nothing_Behaviour : The_Default_Movement_Behaviour {

	int[] _AnimatorVariables;

	public override void SetMethod (The_Object_Behaviour myTransform){

		base.SetMethod (myTransform);

		_MyObject = myTransform;
		_MyTransform = myTransform._TheObject;

		_TargetTransform = myTransform._TheObject._TheTarget;
		_AnimatorVariables = _MyObject.AnimatorVariables;

	}

	public override void OnEnter (){
		MoveDirection [0] = Vector3.zero;
		_MyObject.MyAnimator.SetFloat (_AnimatorVariables[1], AnimatorStageValueOnEnter);
	}

	public override void BehaviourUpdate (){
		MovementRotations ();
	}

}