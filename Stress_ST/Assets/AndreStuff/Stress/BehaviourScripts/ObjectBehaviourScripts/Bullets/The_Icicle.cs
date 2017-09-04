using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Icicle : The_Default_Bullet_WithMovement {


	void FixedUpdate () {
		Destroy (gameObject);//TODO remove
		
		if (RotateToTargetInUpdate) {
			Rotations ();
		}

		Movement ();

		AnimatorStage ();

	}

	void OnCollisionEnter2D(Collision2D coll){//What Happens When The Object Collides
		if (IgnoreOnTrigger2D == false) {
			if (coll.gameObject.layer == 13) {
				//What happends when colliding with a wall
			} else if (coll.gameObject.layer == 12) {
				//What happends when colliding with an undestuctrable wall
			} else if (coll.gameObject.layer == 14) {
				//What happends when colliding with another spell
			} else if (coll.gameObject.layer == 8) {
				//What happends when colliding with an enemy
			} else if (coll.gameObject.layer == 15) {
				//What happends when colliding with a friend
			}

			if (coll.gameObject != _ImTheShooter) {//Check If The Shooter Hit The This Object
			//	if(coll.gameObject.layer == )
				Destroy (gameObject);
			} /*else {
				Debug.Log ("Hit Parent");
				//		Debug.Log ("Colli " + coll.gameObject.name + " | " + coll.otherCollider.name);//FeetCollider
			}*/
		}
	}

}
