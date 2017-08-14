using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance_Less_Then : Behaviour_Default {

	Transform _MyTransform;
	Transform _TargetTransform;

	public float DistanceLessThen = 0.3f;


	public override void SetMethod (Object_Behaviour myTransform, Transform targetTransform, int[] AnimatorValues){
		_MyTransform = myTransform.transform;
		_TargetTransform = targetTransform;
	}

	public override bool GetBool(int index){
		if (Vector3.Distance (_MyTransform.position, _TargetTransform.position) < DistanceLessThen) {
			return true;
		} else {
			return false;
		}
	}


}