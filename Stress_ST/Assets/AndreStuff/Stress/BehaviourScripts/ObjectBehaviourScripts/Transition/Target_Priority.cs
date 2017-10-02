using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Priority : The_Default_Transition_Info {

	public TargetHierarchy TargetPriority;

	public ObjectStats TheObject;

	public override void OnEnter(){
		TheObject.SetTarget (TargetPriority.GetTarget());
	}

	public override void OnExit(){

	}

	public  override void OnReset(){

	}

}
