using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Naruto_Rasengan : The_Default_Bullet {

	Vector3 MyShootingDirection;

	public Animator MyAnimator;

	bool ChangeToExplotion = false;
	Vector3 _Direction = Vector3.zero;

	public float MovementSpeed = 1;
	public float DistanceToTravel = 5;
	public LayerMask WhatCanIHit;


	public GameObject Ball;
	public GameObject Explotion;
	Vector3 Target  = Vector3.zero;
	public CircleCollider2D MyCollider;

	float thetime = 0;
	public float AoeRadius = 0.5f;
	public float MoveSpeed = 1;

	public Bullet_Done_AnimatorParameter TheAnimator;


	void Start(){

	//	Target = _Shooter._TheTarget.transform.position;
		TheAnimator.MyAnimator.speed = _Shooter.MyAnimator.speed;
	}

	void FixedUpdate () {
		if (_Shooter._CreaturePhase > 8)//Quickfix need to look into this problem
			Destroy (gameObject);
		
		if (TheAnimator == null) {
			_Shooter.MyAnimator.SetInteger (_Shooter.MyAnimator.GetComponent<TheAnimator> ().AnimatorVariables [1], 2);

			Destroy (gameObject);
		}

		if(TheAnimator != null && TheAnimator.MyAnimator.GetBool(TheAnimator.AnimatorVariables[0]) == true){
			TheAnimator.MyAnimator.SetBool (TheAnimator.AnimatorVariables [0], false);
			foreach (RaycastHit2D s in Physics2D.CircleCastAll (transform.position, AoeRadius, Vector2.zero, 0, WhatCanIHit)) {
				if (s.transform.CompareTag ("Player1")) {
					Debug.Log ("SEDNGING DMG");
					s.transform.GetComponent<CreatureRoot> ().TookDmg (1);
				}

			}
		}


	}

	void OnTriggerEnter2D(Collider2D col){//objects without rigidbody and box2d ontrigger true
		/*if (_Shooter._MyTransform.gameObject != col.gameObject) {
			if(col.CompareTag("Wall"))
				GameObject.Destroy (transform.gameObject);

			col.gameObject.GetComponent<AbsoluteRoot> ().RecievedDmg (Mathf.FloorToInt (_Shooter._TheObject.AttackStrength));
			GameObject.Destroy (transform.gameObject);
		}*/


	}

	void OnCollisionEnter2D(Collision2D col){//objects with rigidbody and box2d ontrigger false
		/*	if (_Shooter._MyTransform.gameObject != col.gameObject) {
			col.gameObject.GetComponent<AbsoluteRoot> ().RecievedDmg (Mathf.FloorToInt (_Shooter._TheObject.AttackStrength));
			GameObject.Destroy (transform.gameObject);
		}*/

	}


}