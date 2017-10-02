using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Distance_Less_Then : The_Default_Exit_Behaviour {

	The_Object_Behaviour _MyTransform;

	public float DistanceLessThen = 0.3f;


	public override void SetMethod (The_Object_Behaviour myTransform){
		_MyTransform = myTransform;
	}

	public override bool GetBool(int index){
		if (index == 2) {
			if (Vector3.Distance (_MyTransform._TheObject.transform.GetChild(2).position, _MyTransform._TheTarget.transform.position) < DistanceLessThen * _MyTransform._TheObject.AttackRange) {
				return true;
			} else {
				return false;
			}
		} else {
			return base.GetBool (index);
		}
	}


}
