﻿using UnityEngine;
using System.Collections;

public class BulletStart : MonoBehaviour {
	float walkingSpeed = 5;
	Vector3 MyShootingDirection;
	float direction = 0;
	Rigidbody2D testing;
	GameObject testin;
	public float bulletspeed;
	void Start () {
		testin = GameObject.FindWithTag ("Player1");
	
		Vector3 TargetPosiiton = testin.transform.position;
		MyShootingDirection = TargetPosiiton - transform.position;

		if (TargetPosiiton.y < transform.position.y) {
			direction = -1;	
			
		} else {
			direction = 1;
		}

		transform.RotateAround (new Vector3 (0, 0, 1), Mathf.Deg2Rad * (Vector3.Angle(Vector3.right, MyShootingDirection) * direction ));

		MyShootingDirection = MyShootingDirection.normalized;
		testing = GetComponent<Rigidbody2D> ();
	}
	GameObject s;
	public void SetParent(GameObject g){
		s = g;
	}

	// Update is called once per frame
	void Update () {
		testing.velocity = MyShootingDirection * bulletspeed;
	//	transform.position = new Vector3((MyShootingDirection.x * Time.smoothDeltaTime * walkingSpeed) + transform.position.x , (MyShootingDirection.y * Time.smoothDeltaTime * walkingSpeed) + transform.position.y , transform.position.z);
	}
	string a = "Wall";
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.CompareTag(a)) {
			Destroy (this.gameObject);
		} else if (coll.gameObject ==  testin) {
			coll.transform.GetComponent<DefaultBehaviour> ().RecievedDmg ();
			Destroy (this.gameObject);
			//this.gameObject.SetActive (false);
		}
	}
}
