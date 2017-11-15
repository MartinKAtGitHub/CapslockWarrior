using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghosty_Bullet : The_Default_Bullet {
	
	Vector3 MyShootingDirection;

	public Animator MyAnimator;
	public Rigidbody2D MyRigidbody2D;

	bool _StartMoving = false;
	Vector3 _Direction = Vector3.zero;


	void FixedUpdate () {

		if (_StartMoving == true) {
			transform.position += MyShootingDirection * Time.deltaTime;
		} else {

			MyShootingDirection = _Shooter._TheTarget.transform.position - transform.position;
			_Direction.z = Vector3.Angle (Vector3.right, MyShootingDirection);

			if (MyShootingDirection.y < 0) {
				_Direction.z = _Direction.z * -1;
			}  
			transform.rotation = Quaternion.Euler (_Direction);

			if (MyAnimator.GetBool ("Done") == true) {
				MyShootingDirection = MyShootingDirection.normalized;
				_StartMoving = true;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col){//objects without rigidbody and box2d ontrigger true
		if (_Shooter._MyTransform.gameObject != col.gameObject) {
			if(col.CompareTag("Wall"))
				GameObject.Destroy (transform.gameObject);

			col.gameObject.GetComponent<AbsoluteRoot> ().RecievedDmg (Mathf.FloorToInt (_Shooter._TheObject.AttackStrength));
			GameObject.Destroy (transform.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col){//objects with rigidbody and box2d ontrigger false
		if (_Shooter._MyTransform.gameObject != col.gameObject) {
			col.gameObject.GetComponent<AbsoluteRoot> ().RecievedDmg (Mathf.FloorToInt (_Shooter._TheObject.AttackStrength));
			GameObject.Destroy (transform.gameObject);
		}
	}


}