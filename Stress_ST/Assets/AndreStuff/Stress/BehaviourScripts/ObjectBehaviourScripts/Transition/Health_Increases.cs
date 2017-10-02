using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Health_Increases : The_Default_Transition_Info {

	public float HealthGain = 1;
	public bool PercentageGain = false;
	public bool RemoveOnExit = false;
	public ObjectStats _TheObject;

	public override void OnEnter(){
		if (PercentageGain == true) {
			_TheObject.HealthWordChange (Mathf.FloorToInt(_TheObject.HealthWords + (_TheObject.HealthWords * HealthGain)));
		} else {
			_TheObject.HealthWordChange (Mathf.FloorToInt(HealthGain));
		}
	}

	public override void OnExit(){
		if (RemoveOnExit == true) {
			if (PercentageGain == true) {
				_TheObject.HealthWordChange (- Mathf.FloorToInt (_TheObject.HealthWords + _TheObject.HealthWords * HealthGain));
			} else {
				_TheObject.HealthWordChange (- Mathf.FloorToInt (HealthGain));
			}
		}
	}

	public  override void OnReset(){
	}

}
