﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide_Health : The_Default_Transition_Info {

	public bool ShowOnExit = true;
	public bool DissableTypeChecking = true;
	public DefaultBehaviour _TheObject;


	public override void OnEnter(){
		_TheObject._WordChecker.HideHealth (DissableTypeChecking);
	}

	public override void OnExit(){
		if (ShowOnExit == true) {
			_TheObject._WordChecker.HideHealth (DissableTypeChecking);
		}
	}

	public  override void OnReset(){

	}

}
