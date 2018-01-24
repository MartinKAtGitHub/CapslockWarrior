using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleach_Getsuga : The_Default_Bullet {

	Vector3 MyShootingDirection;
	Vector3 _Direction = Vector3.zero;

	Vector3 TargetVector = Vector3.zero;
	bool KillMyself = false;

	public float DisableColliderAt = 2;
	float theTime = 0;
	public GameObject TheCollider;
	public GameObject Particleeffect;
	





	public override void SetMethod (GameManagerTestingWhileWaiting.SpellAttackInfo SpellInfo, The_Object_Behaviour MySender){
		base.SetMethod (SpellInfo, MySender);


		TargetVector = _Shooter.ObjectCurrentVector[0];


	}

	void Start(){

	//	MyShootingDirection = TargetVector - transform.position;
		MyShootingDirection = _Shooter.ObjectTargetVector[0];
		_Direction.z = Vector3.Angle (Vector3.right, MyShootingDirection);

		if (MyShootingDirection.y < 0) {
			_Direction.z = _Direction.z * -1.0f;
		}  
		transform.rotation = Quaternion.Euler (_Direction);
		theTime = _Shooter.TheTime [0];
		Particleeffect.GetComponent<MoveForwardFast> ().starting = true;

	}




	void FixedUpdate () {
		if (_Shooter._CreaturePhase > 3)//Quickfix need to look into this problem
			Destroy (gameObject);
		
		if (KillMyself == false) {
		
			if (theTime + DisableColliderAt < _Shooter.TheTime [0]) {
				KillMyself = true;
				theTime = _Shooter.TheTime [0];
				TheCollider.SetActive (false);
				Particleeffect.GetComponent<ParticleSystem> ().Stop (true);
			}

		} else {
		
			if (theTime + DisableColliderAt < _Shooter.TheTime [0]) {
			
				Destroy (gameObject);
			
			}
		}
	}

void OnTriggerEnter2D(Collider2D col){//objects without rigidbody and box2d ontrigger true
	if (_Shooter._MyTransform.gameObject != col.gameObject) {
		if(col.CompareTag("Wall"))
			GameObject.Destroy (transform.gameObject);

			if (col.CompareTag ("Player1")) {
				Debug.Log ("SENDING DMG");
				col.gameObject.GetComponent<AbsoluteRoot> ().RecievedDmg (Mathf.FloorToInt (_Shooter._TheObject.AttackStrength));
			}
	}


}

void OnCollisionEnter2D(Collision2D col){//objects with rigidbody and box2d ontrigger false
	if (_Shooter._MyTransform.gameObject != col.gameObject) {
			if (col.gameObject.CompareTag ("Player1")) {
				Debug.Log ("SENDING DMG");
				col.gameObject.GetComponent<AbsoluteRoot> ().RecievedDmg (Mathf.FloorToInt (_Shooter._TheObject.AttackStrength));
			}
	}


}


}