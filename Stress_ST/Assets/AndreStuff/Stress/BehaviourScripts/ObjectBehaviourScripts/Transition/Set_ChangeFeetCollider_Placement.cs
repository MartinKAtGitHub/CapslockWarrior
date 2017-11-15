using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_ChangeFeetCollider_Placement : The_Default_Transition_Info {

	public Transform FeetCollider;
	public Vector3 UpDistance;
	public bool TurnOffBodyCollider = true;

	public override void OnEnter(){
		
		FeetCollider.position += UpDistance;
	
		if (TurnOffBodyCollider == true) {
			FeetCollider.parent.GetComponent<BoxCollider2D> ().enabled = false;
		}

	}

	public override void OnExit(){

		FeetCollider.position -= UpDistance;
	
		if (TurnOffBodyCollider == true) {
			FeetCollider.parent.GetComponent<BoxCollider2D> ().enabled = true;
		}
	}

	public  override void OnReset(){

	}


}
