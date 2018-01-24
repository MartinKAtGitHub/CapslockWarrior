using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goku_BuddaFist_Bullet : The_Default_Bullet {

	Vector3 MyShootingDirection;

	public Animator MyAnimator;

	bool _StartMoving = false;
	Vector3 _Direction = Vector3.zero;

	public float MovementSpeed = 1;
	public float DistanceToTravel = 5;
	public LayerMask WhatCanIHit;

	void Start(){
		
		MyShootingDirection = _Shooter._TheTarget.transform.position - transform.position;
		_Direction.z = Vector3.Angle (Vector3.right, MyShootingDirection);

		if (MyShootingDirection.y < 0) {
			_Direction.z = _Direction.z * -1.0f;
		}  
		transform.rotation = Quaternion.Euler (_Direction);

	//	if (MyAnimator.GetBool ("Done") == true) {

			MyShootingDirection = MyShootingDirection.normalized * DistanceToTravel;
			_StartMoving = true;
	//	}
	}

	void FixedUpdate () {

		if (_StartMoving == true) {

			transform.position += MyShootingDirection * (MovementSpeed * Time.deltaTime);

		/*	foreach (RaycastHit2D s in Physics2D.CircleCastAll (transform.position, 0.15f, Vector2.zero, 0, WhatCanIHit)) {
				Debug.Log (s.transform.name);
			
				if (s.transform.CompareTag ("Player1")) {
					Debug.Log ("SENDING DMG");
					s.transform.GetComponent<PlayerManager> ().RecievedDmg (Mathf.RoundToInt (1/*_SpellInfo.DamageMultiplyer + _Shooter._TheObject.AttackStrength*//*));
					Destroy (gameObject);
				}
			}*/

			if (Vector3.Distance (transform.position, MyShootingDirection) < 0.1f) {
				Destroy (gameObject);
			}


		} else {//Rotation And Direction
		
			MyShootingDirection = _Shooter._TheTarget.transform.position - transform.position;
			_Direction.z = Vector3.Angle (Vector3.right, MyShootingDirection);

			if (MyShootingDirection.y < 0) {
				_Direction.z = _Direction.z * -1.0f;
			}  
			transform.rotation = Quaternion.Euler (_Direction);

			if (MyAnimator.GetBool ("Done") == true) {

				MyShootingDirection = MyShootingDirection.normalized * DistanceToTravel;
				_StartMoving = true;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col){//objects without rigidbody and box2d ontrigger true
		if (_Shooter._MyTransform.gameObject != col.gameObject) {
			if(col.CompareTag("Wall"))
				GameObject.Destroy (transform.gameObject);

			if (col.gameObject.CompareTag ("Player1")) {
				Debug.Log ("SENDING DMG");
				col.gameObject.GetComponent<AbsoluteRoot> ().RecievedDmg (Mathf.FloorToInt (_Shooter._TheObject.AttackStrength));
			}
			GameObject.Destroy (transform.gameObject);
		}


	}

	void OnCollisionEnter2D(Collision2D col){//objects with rigidbody and box2d ontrigger false
		if (_Shooter._MyTransform.gameObject != col.gameObject) {
			if (col.gameObject.CompareTag ("Player1")) {
				Debug.Log ("SENDING DMG");
				col.gameObject.GetComponent<AbsoluteRoot> ().RecievedDmg (Mathf.FloorToInt (_Shooter._TheObject.AttackStrength));
			}
			GameObject.Destroy (transform.gameObject);
		}


	}


}