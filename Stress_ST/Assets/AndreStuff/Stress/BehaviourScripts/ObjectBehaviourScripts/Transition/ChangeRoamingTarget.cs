using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoamingTarget : The_Default_Transition_Info {

/*	public bool ShowOnExit = true;
	public bool DissableTypeChecking = true;*/
	public DefaultBehaviour _TheObject;
	public GameObject roaming;

	public override void OnEnter(){
	//	_TheObject._WordChecker.HideHealth (DissableTypeChecking);
		Debug.Log(Random.Range(0, roaming.transform.childCount) + " | " + roaming.transform.childCount);
		_TheObject.SetTarget (roaming.transform.GetChild(Random.Range(0, roaming.transform.childCount)).gameObject);
	}

	public override void OnExit(){
	/*	if (ShowOnExit == true) {
			_TheObject._WordChecker.HideHealth (DissableTypeChecking);
		}*/
	}

	public  override void OnReset(){

	}

}
