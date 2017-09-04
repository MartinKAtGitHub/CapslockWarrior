using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Distance_More_Then : The_Default_Exit_Behaviour {

	Transform _MyTransform;
	Transform _TargetTransform;

	public float DistanceMoreThen = 10f;


	public override void SetMethod (The_Object_Behaviour myTransform){
		_MyObject = myTransform;
		_MyTransform = _MyObject._MyTransform;
		_TargetTransform = _MyObject._Target;
	}

	public override bool GetBool(int index){
		if (index == 2) {
			if (Vector3.Distance (_MyTransform.position, _TargetTransform.position) >= DistanceMoreThen) {
				return true;
			} else {
				return false;
			}
		} else {
			return base.GetBool (index);
		}
	}


}
