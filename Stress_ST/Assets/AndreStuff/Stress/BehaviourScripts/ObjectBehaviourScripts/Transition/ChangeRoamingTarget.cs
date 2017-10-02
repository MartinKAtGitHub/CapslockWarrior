using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoamingTarget : The_Default_Transition_Info {


	public ObjectStats _TheObject;

	public override void OnEnter(){
		_TheObject.SetTarget (GameObject.FindGameObjectWithTag("Walkable").transform.GetChild(Random.Range(0, GameObject.FindGameObjectWithTag("Walkable").transform.childCount)).gameObject);
	
	}

	public override void OnExit(){

	}

	public  override void OnReset(){

	}

}
