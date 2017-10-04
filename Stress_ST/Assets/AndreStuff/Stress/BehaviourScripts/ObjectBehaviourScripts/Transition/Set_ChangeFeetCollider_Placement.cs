using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_ChangeFeetCollider_Placement : The_Default_Transition_Info {

	public Transform FeetCollider;
	public Vector3 UpDistance;

	public override void OnEnter(){
		
		FeetCollider.position -= UpDistance;
	
	}

	public override void OnExit(){
			FeetCollider.position += UpDistance;
	
	}

	public  override void OnReset(){

	}


}
