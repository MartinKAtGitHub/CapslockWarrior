using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide_Health : The_Default_Transition_Info {

	public bool ShowOnExit = true;
	public bool DissableTypeChecking = true;
	public ObjectWords _TheObject;


	public override void OnEnter(){
		_TheObject.MyWord.HideHealth (DissableTypeChecking);
	}

	public override void OnExit(){
		if (ShowOnExit == true) {
			_TheObject.MyWord.ShowHealth (DissableTypeChecking);
		}
	}

	public  override void OnReset(){

	}

}
