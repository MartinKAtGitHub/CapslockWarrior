using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreateThenShoot : BulletBehaviour {

	public bool FollowWhenCreating = true;
	public bool StartMoving = false;

	Transform TheTarget;
	ShootingAfterAnimation ShootingAnimation;
	private Vector3 _direction = Vector3.zero;

	// Use this for initialization
	void Start () {
		MyRigidbody2D = GetComponent<Rigidbody2D> ();
		ShootingAnimation = GetComponent<Animator> ().GetBehaviour<ShootingAfterAnimation> ();
		ShootingAnimation.ShootingAnimationFinished = false;

	}
	void FixedUpdate () {
		
		if (StartMoving == true) {
			MyRigidbody2D.velocity = _MyShootingDirection * BulletSpeed;
		} else {
			if (FollowWhenCreating == true) {

				_MyShootingDirection = TheTarget.position - transform.position;
				_direction.z = Vector3.Angle (Vector3.right, _MyShootingDirection);

				if (_MyShootingDirection.y < 0) {
					_direction.z = _direction.z * -1;
				}  
				transform.rotation = Quaternion.Euler (_direction);

				if (ShootingAnimation.ShootingAnimationFinished == true) {
					_MyShootingDirection = _MyShootingDirection.normalized;
					StartMoving = true;
				}
			}
		}
	}


	public override void SetObjectDirection(GameObject sender, Transform target){
		TheTarget = target;
		ImTheShooter = sender;

	/*	ImTheShooter = sender;
		_MyShootingDirection = (target - transform.position).normalized;

		if (target.y < transform.position.y) {//this desides which way im rotating
			/*unity -> obsolete so change this TODO*/
	/*		transform.RotateAround (new Vector3 (0, 0, 1), Mathf.Deg2Rad * (Vector3.Angle (Vector3.right, _MyShootingDirection) * -1));//vec3.ang returns a deg value so changing it to rad
		} else {
			/*unity -> obsolete so change this TODO*/
		/*	transform.RotateAround (new Vector3 (0, 0, 1), Mathf.Deg2Rad * (Vector3.Angle (Vector3.right, _MyShootingDirection) * 1));
		}*/
	}


}
