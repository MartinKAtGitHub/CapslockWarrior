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

		StartPos = transform.position;
		MyShootingDirection = TargetVector - transform.position;

		TimeStarted = _Shooter.TheTime [0];

	}


	bool KillMyself = false;

	void FixedUpdate () {

		if (KillMyself == false) {

			if (Vector3.Distance (StartPos, transform.position) > DistanceToFly) {

				for (int i = 0; i < 3; i++) {
					transform.GetChild (0).GetChild (i).GetComponent<ParticleSystem> ().Stop ();
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

	}

void OnTriggerEnter2D(Collider2D col){//objects without rigidbody and box2d ontrigger true
		if (_Shooter._MyTransform.gameObject != col.gameObject) {
			if (col.CompareTag ("Wall")) {
				_Shooter.MyAnimator.SetInteger (_Shooter.MyAnimator.GetComponent<TheAnimator> ().AnimatorVariables [1], 2);
				GameObject.Destroy (transform.gameObject);
			}

			if (col.CompareTag ("Player1")) {
				Debug.Log ("Doing Some Aditional ForeceStuff Here");
				col.gameObject.GetComponent<AbsoluteRoot> ().RecievedDmg (Mathf.FloorToInt (_Shooter._TheObject.AttackStrength));
			}
		}

	}

}