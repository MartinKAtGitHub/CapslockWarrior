using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goku_SpearCharge : The_Default_Bullet {

	Vector3 MyShootingDirection;


	bool _StartMoving = false;
	Vector3 _Direction = Vector3.zero;

	public float TimeToPlay = 1;
	public LayerMask WhatCanIHit;

	Vector3 TargetVector = Vector3.zero;


	float TimeStarted = 0;

	public float DistanceToFly = 1;
	Vector3 StartPos = Vector3.zero;


	public override void SetMethod (GameManagerTestingWhileWaiting.SpellAttackInfo SpellInfo, The_Object_Behaviour MySender){
		base.SetMethod (SpellInfo, MySender);

		TargetVector = transform.position + _Shooter.ObjectCurrentVector [0];

		transform.parent = _Shooter._MyTransform.transform;

	}

	void Start(){

		/*	MyShootingDirection = TargetVector - transform.position;
		_Direction.z = Vector3.Angle (Vector3.right, MyShootingDirection);

		if (MyShootingDirection.y < 0) {
			_Direction.z = _Direction.z * -1.0f;
		}  
		transform.rotation = Quaternion.Euler (_Direction);*/
		StartPos = transform.position;
		MyShootingDirection = TargetVector - transform.position;


		if (MyShootingDirection.x < 0) {
			transform.rotation = Quaternion.Euler (Vector3.back * 180);
		}


		TimeStarted = _Shooter.TheTime [0];

	}


	bool KillMyself = false;

	void FixedUpdate () {

		if (KillMyself == false) {

			if (Vector3.Distance (StartPos, transform.position) > DistanceToFly) {

				for (int i = 0; i < 3; i++) {
					transform.GetChild (i).GetComponent<ParticleSystem> ().Stop ();
				}
				_Shooter.MyAnimator.SetInteger (_Shooter.MyAnimator.GetComponent<TheAnimator> ().AnimatorVariables [1], 2);

				KillMyself = true;
				TimeStarted = _Shooter.TheTime [0];
			}

		} else {
		
			if (TimeStarted + 0.25f <= _Shooter.TheTime [0]) {
				Destroy (gameObject);
			}
		
		}

	


	/*	if (PointInTriangle (_Shooter._TheTarget.transform.position, transform.position, (Vector2)(Point2.transform.position), (Vector2)(Point3.transform.position)) == true) {
			Debug.Log ("HIT THE PLAYER!!!");
		} else {
			Debug.Log ("MISSED THE PLAYER!!!");
		}
*/

	}

void OnTriggerEnter2D(Collider2D col){//objects without rigidbody and box2d ontrigger true
	if (_Shooter._MyTransform.gameObject != col.gameObject) {
		if(col.CompareTag("Wall"))
			GameObject.Destroy (transform.gameObject);

			if (col.CompareTag ("Player1")) {
				Debug.Log ("SEDNING DMG");
				col.gameObject.GetComponent<AbsoluteRoot> ().RecievedDmg (Mathf.FloorToInt (_Shooter._TheObject.AttackStrength));
			}
		GameObject.Destroy (transform.gameObject);
	}
		Debug.Log ("HERE");

}

void OnCollisionEnter2D(Collision2D col){//objects with rigidbody and box2d ontrigger false
	if (_Shooter._MyTransform.gameObject != col.gameObject) {

			Debug.Log ("Doing Some ForeceStuff Here");



	//	col.gameObject.GetComponent<AbsoluteRoot> ().RecievedDmg (Mathf.FloorToInt (_Shooter._TheObject.AttackStrength));
	//	GameObject.Destroy (transform.gameObject);
	}


}


}