using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : Behaviour_Default {

	public LayerMask[] WhatCanICollideWith;//this is what the physics2d.linecast cant hit. if it does move closer
	bool _Collided = false;


	public override void OnSetup (){
		GetComponent<Object_Behaviour> ().SetCollisionBehaviour (this);
	}

	public override void Reset (){
		_Collided = false;
	}

	public override void BehaviourMethod (){
		_Collided = true;
	}

	public override bool GetBool (int index){

		if (_Collided == true) {
			return true;
		} else {
			return false;
		}
	}

	public override LayerMask[] GetLayerMask(){
		return WhatCanICollideWith;
	}


}