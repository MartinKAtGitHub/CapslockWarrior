using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nothing_Behaviour : The_Default_Movement_Behaviour {


	public override void SetMethod (The_Object_Behaviour myTransform){

		base.SetMethod (myTransform);

		_MyObject = myTransform;
		_MyTransform = myTransform._TheObject;

		_TargetTransform = myTransform._TheTarget;

	}

	public override void OnEnter (){
		MoveDirection [0] = Vector3.zero;
		//Ichigo		_MyObject.MyAnimator.SetFloat (_AnimatorVariables[1], AnimatorStageValueOnEnter);
	}

	public override void BehaviourUpdate (){
		MovementRotations ();
	}

}