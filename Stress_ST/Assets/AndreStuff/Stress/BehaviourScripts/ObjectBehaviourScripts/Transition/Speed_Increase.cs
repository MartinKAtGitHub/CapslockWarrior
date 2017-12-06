using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed_Increase : The_Default_Transition_Info {

	public ObjectStats _TheObject;
	public float MovementIncrease = 0;
	public bool RemoveOnExit = false;

	public override void OnEnter(){
		_TheObject.MovementSpeedChange (MovementIncrease);

	}

	public override void OnExit(){
		if (RemoveOnExit == true) {
			_TheObject.MovementSpeedChange (-MovementIncrease);
		}
		
	}

	public  override void OnReset(){

	}


}
