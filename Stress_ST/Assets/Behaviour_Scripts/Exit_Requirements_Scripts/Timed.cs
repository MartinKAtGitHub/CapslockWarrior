using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timed : Default_ExitRequirement_Behaviour {

	public float TimeToCheck = 10;
	float[] TheTime;
	float TimeChecker = 0;

	public override void SetMethod (Object_Behaviour myTransform, Transform targetTransform, int[] AnimatorValues)
	{
		TheTime = myTransform.GetTheTime ();
	}

	public override void OnEnter ()
	{
		TimeChecker = TheTime [0] + TimeToCheck;
	}

	public override bool GetBool (int index){
		if (TimeChecker >= TheTime [0])
			return true;
		else {
			return base.GetBool (index);
		}
	}

}
