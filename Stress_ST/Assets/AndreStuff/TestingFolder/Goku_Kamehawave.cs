using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goku_Kamehawave : The_Default_Bullet {

	Vector3 MyShootingDirection;


//	bool _StartMoving = false;
	Vector3 _Direction = Vector3.zero;

	public float TimeToPlay = 1;
	public LayerMask WhatCanIHit;

	Vector3 TargetVector = Vector3.zero;

	public GameObject Point2;
	public GameObject Point3;


	float TimeStarted = 0;




	public override void SetMethod (GameManagerTestingWhileWaiting.SpellAttackInfo SpellInfo, The_Object_Behaviour MySender){
		base.SetMethod (SpellInfo, MySender);

	
		TargetVector = transform.position + _Shooter.ObjectCurrentVector [0];

		_Shooter.MyAnimator.SetBool (_Shooter.MyAnimator.GetComponent<TheAnimator> ().AnimatorVariables [3], false);


	}

	void Start(){

		MyShootingDirection = TargetVector - transform.position;
		_Direction.z = Vector3.Angle (Vector3.right, MyShootingDirection);

		if (MyShootingDirection.y < 0) {
			_Direction.z = _Direction.z * -1.0f;
		}  
		transform.rotation = Quaternion.Euler (_Direction);
		TimeStarted = _Shooter.TheTime [0];

	}

	public static bool PointInTriangle(Vector2 p, Vector2 p0, Vector2 p1, Vector2 p2){//Check If An Object Is Inside The Given Vectors
		
		var s = p0.y * p2.x - p0.x * p2.y + (p2.y - p0.y) * p.x + (p0.x - p2.x) * p.y;
		var t = p0.x * p1.y - p0.y * p1.x + (p0.y - p1.y) * p.x + (p1.x - p0.x) * p.y;

		if ((s < 0) != (t < 0))
			return false;

		var A = -p1.y * p2.x + p0.y * (p2.x - p1.x) + p0.x * (p1.y - p2.y) + p1.x * p2.y;
		if (A < 0.0)
		{
			s = -s;
			t = -t;
			A = -A;
		}
		return s > 0 && t > 0 && (s + t) <= A;
	}

	bool KillMyself = false;

	void FixedUpdate () {

		if (KillMyself == false) {
			if(_Shooter == null)
				Destroy (gameObject);
			
			if (TimeToPlay + TimeStarted <= _Shooter.TheTime [0]) {
				_Shooter.MyAnimator.SetInteger (_Shooter.MyAnimator.GetComponent<TheAnimator> ().AnimatorVariables [1], 2);
				_Shooter.MyAnimator.SetBool (_Shooter.MyAnimator.GetComponent<TheAnimator> ().AnimatorVariables [3], true);
				for (int i = 0; i < 3; i++) {
					transform.GetChild (i).GetComponent<ParticleSystem> ().Stop ();
				}
				KillMyself = true;
			}
		} else {
			if(_Shooter == null)
				Destroy (gameObject);

			if (TimeToPlay + TimeStarted + 1 <= _Shooter.TheTime [0]) {
				Destroy (gameObject);
			}
		
		}

	
		if (PointInTriangle (_Shooter._TheTarget.transform.position, transform.position, (Vector2)(Point2.transform.position), (Vector2)(Point3.transform.position)) == true) {
				_Shooter._TheTarget.GetComponent<AbsoluteRoot> ().RecievedDmg (Mathf.FloorToInt (_Shooter._TheObject.AttackStrength));
		} else {
			//Debug.Log ("MISSED THE PLAYER!!!");
		}

		/*


		if (_StartMoving == true) {
			
			foreach (RaycastHit2D s in Physics2D.CircleCastAll (transform.position, 0.15f, Vector2.zero, 0, WhatCanIHit)) {
				if (s.transform.CompareTag ("Player1")) {
					s.transform.GetComponent<PlayerManager> ().RecievedDmg (Mathf.RoundToInt (1/*_SpellInfo.DamageMultiplyer + _Shooter._TheObject.AttackStrength*//*));
				}

				Destroy (gameObject);
			}

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

		//	if (MyAnimator.GetBool ("Done") == true) {

		//		MyShootingDirection = MyShootingDirection.normalized * DistanceToTravel;
				_StartMoving = true;
		//	}
		}

		_Shooter.MyAnimator.SetInteger (_Shooter.MyAnimator.GetComponent<TheAnimator> ().AnimatorVariables[1], 2);*/

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