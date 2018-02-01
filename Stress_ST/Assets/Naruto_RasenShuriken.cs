using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Naruto_RasenShuriken : The_Default_Bullet {



	bool ChangeToExplotion = false;
	public float MovementSpeed = 1;

	public GameObject Ball;
	public GameObject Explotion;
	Vector3 Target  = Vector3.zero;
	public CircleCollider2D MyCollider;

	public ParticleSystem EmmisionRate;
	float thetime = 0;
	bool Countdown = false;
	public float[] TheTime;
	float AttackStrength = 0;

	void Awake(){
		TheTime = ClockTest.TheTime;
	}

	void Start(){
		Target = _Shooter._TheTarget.transform.position;
		AttackStrength = _Shooter._TheObject.AttackStrength;
	}

	void FixedUpdate () {

		if (ChangeToExplotion == false) {

			if (transform.position != Target) {
				transform.position = Vector3.MoveTowards (transform.position, Target, MovementSpeed * Time.deltaTime);
			} else {
				Ball.gameObject.SetActive (false);
				Explotion.gameObject.SetActive (true);
				ChangeToExplotion = true;
				thetime = TheTime [0];
			}
		} else {
			if (thetime + 0.25f < TheTime [0]) {
				if (Countdown == false) {
					if (MyCollider.radius < 1) {
						MyCollider.radius += Time.deltaTime;
					} else {
						if (EmmisionRate.emission.rateOverTimeMultiplier < 250) {
							Countdown = true;
						}
					}
				} else {
				
					if (MyCollider.radius > 0.1f) {
						MyCollider.radius -= Time.deltaTime;
					} else {
						Destroy (gameObject);
					}

				}
			}
		}

	}

	float DmgTime = 0;

	void OnTriggerEnter2D(Collider2D col){//objects without rigidbody and box2d ontrigger true

		if (ChangeToExplotion == false) {
			if (_Shooter._MyTransform.gameObject != col.gameObject) {
				Ball.gameObject.SetActive (false);
				Explotion.gameObject.SetActive (true);
				ChangeToExplotion = true;
				if(TheTime == null)
					thetime = ClockTest.TheTime[0];
				else
					thetime = TheTime[0];

			}
		}
	}

	void OnTriggerStay2D(Collider2D col){//objects without rigidbody and box2d ontrigger true
		if (DmgTime < TheTime [0]) {
			DmgTime = TheTime [0] + 1;
			if (col.CompareTag ("Player1")) {
					col.gameObject.GetComponent<AbsoluteRoot> ().RecievedDmg (Mathf.FloorToInt (AttackStrength));

			}
		}
	}

}