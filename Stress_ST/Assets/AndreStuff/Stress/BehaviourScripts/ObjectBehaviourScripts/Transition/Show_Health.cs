using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_Health : The_Default_Transition_Info {

	public bool HideOnExit = true;
	public bool DissableTypeChecking = true;
	public DefaultBehaviour _TheObject;


	public override void OnEnter(){
		Debug.Log ("HER1");
		_TheObject._WordChecker.ShowHealth (DissableTypeChecking);
	}

	public override void OnExit(){
		if (HideOnExit == true) {
			_TheObject._WordChecker.ShowHealth (DissableTypeChecking);
		}
	}

	public  override void OnReset(){

	}

}