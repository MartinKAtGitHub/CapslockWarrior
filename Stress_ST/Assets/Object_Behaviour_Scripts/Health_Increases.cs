using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Health_Increases : The_Default_Transition_Info {

	public float HealthGain = 1;
	public bool PercentageGain = false;

	public override void OnEnter(){
		if (PercentageGain == true) {
			Debug.Log ((HealthGain * 100) + "% increase");
		} else {
			Debug.Log (HealthGain + " increase");
		}
	}

	public override void OnExit(){
	}

	public  override void OnReset(){
	}

}
