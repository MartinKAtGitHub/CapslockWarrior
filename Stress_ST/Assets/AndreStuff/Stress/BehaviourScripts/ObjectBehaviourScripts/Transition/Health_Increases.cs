using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Health_Increases : The_Default_Transition_Info {

	public float HealthGain = 1;
	public bool PercentageGain = false;
	public bool RemoveOnExit = false;
	public DefaultBehaviour _TheObject;

	public override void OnEnter(){
		if (PercentageGain == true) {
			_TheObject._WordChecker.HealthIncrease(Mathf.FloorToInt(_TheObject.Health [0] * HealthGain));
		} else {
			_TheObject._WordChecker.HealthIncrease(Mathf.FloorToInt(HealthGain));
		}
	}

	public override void OnExit(){
		if (RemoveOnExit == false) {
			if (PercentageGain == true) {
				_TheObject._WordChecker.HealthDecrease (Mathf.FloorToInt (_TheObject.Health [0] * HealthGain));
			} else {
				_TheObject._WordChecker.HealthDecrease (Mathf.FloorToInt (HealthGain));
			}
		}
	}

	public  override void OnReset(){
	}

}
