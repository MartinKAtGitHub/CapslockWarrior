using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Increase : Behaviour_Default {

	Transform _MyTransform;
	Transform _TargetTransform;

	public int HowManyTimesCanITransition = 1;
	int _TimesTransitioned = 0;


	public override void SetMethod (Object_Behaviour myTransform, Transform targetTransform, int[] AnimatorValues){
		_MyTransform = myTransform.transform;
		_TargetTransform = targetTransform;

		_MyTransform.position += Vector3.zero;//TO REMOVE WARNING
		_TargetTransform.position += Vector3.zero;//TO REMOVE WARNING
	}

	public override void BehaviourMethod (){
		if(HowManyTimesCanITransition > _TimesTransitioned){
			Debug.Log ("Health Increase");
			_TimesTransitioned++;
		}
	}


}