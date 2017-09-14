using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_OnCollision : The_Default_Exit_Behaviour {

	bool HitSomething = false;
//	Collider2D TheCol;

	public override void SetMethod (The_Object_Behaviour myTransform){
		myTransform.SetCollisionRequirements (this);
	}

	public override void OnEnter (){
		HitSomething = false;
	}

	public override bool GetBool(int index){
		if (index == 2) {
			return HitSomething;
		} else {
			return base.GetBool (index);
		}
	}

	public override void SetCollision (Collider2D coll){
	//	TheCol = coll;	
		HitSomething = true;
	}

}
