using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Icicle : The_Default_Bullet {

	void FixedUpdate () {

		if (RotateToTargetInUpdate) {
			Rotations ();
		}

		Movement ();

		AnimatorStage ();

	}

	void OnCollisionEnter2D(Collision2D coll){//What Happens When The Object Collides
		if (IgnoreOnTrigger2D == false) {
			if (coll.gameObject != _ImTheShooter) {//Check If The Shooter Hit The This Object
				Debug.Log ("DEAL DMG");
				Destroy (gameObject);
			} else {
				Debug.Log ("Hit Parent");
				//		Debug.Log ("Colli " + coll.gameObject.name + " | " + coll.otherCollider.name);//FeetCollider
			}
		}
	}

}
