using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nothing_Behaviour : The_Default_Movement_Behaviour {

	public override void SetMethod (The_Object_Behaviour myTransform)
	{
		base.SetMethod (myTransform);

		_MyObject = myTransform;
		_MyTransform = myTransform._TheObject;

		_TargetTransform = myTransform._TheObject._TheTarget;

	}

	public override void BehaviourUpdate (){

		MovementRotations ();
	}

}