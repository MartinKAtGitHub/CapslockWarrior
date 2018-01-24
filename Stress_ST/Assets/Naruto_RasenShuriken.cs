using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Naruto_RasenShuriken : The_Default_Bullet {

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

	void Start(){

		Target = _Shooter._TheTarget.transform.position;

	}

	void FixedUpdate () {

		if (ChangeToExplotion == false) {

			if (transform.position != Target) {
				transform.position = Vector3.MoveTowards (transform.position, Target, MovementSpeed * Time.deltaTime);
			} else {
				Ball.gameObject.SetActive (false);
				Explotion.gameObject.SetActive (true);
				ChangeToExplotion = true;
				thetime = _Shooter.TheTime [0];
			}
		} else {
			if (thetime + 0.25f < _Shooter.TheTime [0]) {
				if (MyCollider.radius < 1) {
					MyCollider.radius += Time.deltaTime;
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

		if (col.CompareTag ("Player1")) {
			Debug.Log ("Sending dmg " + _Shooter._TheObject.AttackStrength);
			col.gameObject.GetComponent<AbsoluteRoot> ().RecievedDmg (Mathf.FloorToInt (_Shooter._TheObject.AttackStrength));
		}

		if (_Shooter._MyTransform.gameObject != col.gameObject) {
			
		
			if (ChangeToExplotion == false) {
				Ball.gameObject.SetActive (false);
				Explotion.gameObject.SetActive (true);
				ChangeToExplotion = true;
				thetime = _Shooter.TheTime [0];
			}
		}

	}

	void OnCollisionEnter2D(Collision2D col){//objects with rigidbody and box2d ontrigger false
		if(col.gameObject.CompareTag("Player1")){
			Debug.Log ("Sending dmg " + _Shooter._TheObject.AttackStrength);

		col.gameObject.GetComponent<AbsoluteRoot> ().RecievedDmg (Mathf.FloorToInt (_Shooter._TheObject.AttackStrength));
	}
		
		if (_Shooter._MyTransform.gameObject != col.gameObject) {
			if (ChangeToExplotion == false) {
				Ball.gameObject.SetActive (false);
				Explotion.gameObject.SetActive (true);
				ChangeToExplotion = true;
				thetime = _Shooter.TheTime [0];
			}
		}
	}


}