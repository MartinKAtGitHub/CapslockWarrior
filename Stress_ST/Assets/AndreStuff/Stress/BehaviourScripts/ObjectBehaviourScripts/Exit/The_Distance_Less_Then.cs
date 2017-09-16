using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Distance_Less_Then : The_Default_Exit_Behaviour {

	Transform _MyTransform;
	DefaultBehaviourPosition[] _TheTarget;

	public float DistanceLessThen = 0.3f;


	public override void SetMethod (The_Object_Behaviour myTransform){
		_MyTransform = myTransform._MyTransform;
		_TheTarget = myTransform._TheObject._TheTarget;
	}

	public override bool GetBool(int index){
		if (index == 2) {
			
			if (Vector3.Distance (_MyTransform.position, _TheTarget[0].transform.GetChild(2).transform.position) < DistanceLessThen) {
				return true;
			} else {
				return false;
			}
		} else {
			return base.GetBool (index);
		}
	}


}
