using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_Health : The_Default_Transition_Info {

	public bool ShowHealthBar = true;

	public override void OnEnter(){
		if (ShowHealthBar == true) {
			Debug.Log ("Showing");
		} else {
			Debug.Log ("Hiding");
		}
	}

	public override void OnExit(){
	}

	public  override void OnReset(){
	}

}
