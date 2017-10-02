using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_Health : The_Default_Transition_Info {

	public bool HideOnExit = true;
	public bool DissableTypeChecking = true;
	public ObjectWords _TheObject;


	public override void OnEnter(){
		_TheObject.MyWord.ShowHealth (DissableTypeChecking);
	}

	public override void OnExit(){
		if (HideOnExit == true) {
			_TheObject.MyWord.ShowHealth (DissableTypeChecking);
		}
	}

	public  override void OnReset(){

	}

}