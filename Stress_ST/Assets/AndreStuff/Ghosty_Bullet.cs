using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghosty_Bullet : BulletBehaviourDefault {

	[HideInInspector] public Vector3 MyShootingDirection;
	[HideInInspector] public const string Wall = "Wall";
	
	public bool FollowWhenCreating = true;
	public Animator MyAnimator;
	public Rigidbody2D MyRigidbody2D;

	bool _StartMoving = false;
	Vector3 _Direction = Vector3.zero;

	void FixedUpdate () {

		if (_StartMoving == true) {
			MyRigidbody2D.velocity = MyShootingDirection * Speed;
		} else {
			if (FollowWhenCreating == true) {

				MyShootingDirection = TheTarget.position - transform.position;
				_Direction.z = Vector3.Angle (Vector3.right, MyShootingDirection);

				if (MyShootingDirection.y < 0) {
					_Direction.z = _Direction.z * -1;
				}  
				transform.rotation = Quaternion.Euler (_Direction);

				if (MyAnimator.GetBool (AnimatorControllerParameterDone) == true) {
					MyShootingDirection = MyShootingDirection.normalized;
					_StartMoving = true;
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col){//objects without rigidbody and box2d ontrigger true
		if (ImTheShooter != col.gameObject) {
			if (col.gameObject.GetComponent<CreatureBehaviourUpdate> () != null) {
				col.gameObject.GetComponent<CreatureBehaviourUpdate> ().ObjectGotHurt (Damage);
				GameObject.Destroy (transform.gameObject);
			} else {
				GameObject.Destroy (transform.gameObject);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col){//objects with rigidbody and box2d ontrigger false
		if (ImTheShooter != col.gameObject) {
			if (col.gameObject.GetComponent<CreatureBehaviourUpdate> () != null) {
				col.gameObject.GetComponent<CreatureBehaviourUpdate> ().ObjectGotHurt (Damage);
				GameObject.Destroy (transform.gameObject);
			}else {
				GameObject.Destroy (transform.gameObject);
			}
		}
	}
}
