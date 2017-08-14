using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class On_Collision_Attack : Default_Attack_Behaviour {

	public bool ContinueAfterCollision = false;
	public int HowManyCollision = 1;

	public LayerMask[] WhatCanICollideWith = new LayerMask[1];//this is what the physics2d.linecast cant hit. if it does move closer
	public bool jack = false;
	Collision2D TheCollison;
	Animator MyAnimator;

	float _CollidedSaver = 0;

	public override void SetMethod (Object_Behaviour myTransform, Transform targetTransform, int[] AnimatorValues){
		_AnimatorVariables = AnimatorValues;
		MyAnimator = myTransform.MyAnimator;
	}

	public override void OnSetup (){
		GetComponent<Object_Behaviour> ().SetCollisionBehaviour (this);
	}

	public override void Reset (){
		TheCollison = null;
		_CollidedSaver = 0;
	}

	public override void BehaviourMethod (){
		if (TheCollison != null) {
			if (ContinueAfterCollision == false) {
				//TODO DEAL DMG
				MyAnimator.SetFloat (_AnimatorVariables [2], WhenCompleteChangeAnimatorTo);
			} else {
				if (_CollidedSaver < HowManyCollision) {
					_CollidedSaver++;
					Debug.Log ("HitAnObject");//TODO Recieve DMg
					//TODO Check If OBject Dead If Not COntinue Els Stop
					TheCollison = null;
				} else {
					//TODO DEAL DMG
					MyAnimator.SetFloat (_AnimatorVariables [2], WhenCompleteChangeAnimatorTo);
				}
			}
		}
	}

	public override LayerMask[] GetLayerMask(){
		return WhatCanICollideWith;
	}

	public override void SetCollision (Collision2D coll){
		TheCollison = coll;
	}


}