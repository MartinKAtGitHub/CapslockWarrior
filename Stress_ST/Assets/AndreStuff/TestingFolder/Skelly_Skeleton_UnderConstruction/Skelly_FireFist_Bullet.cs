using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skelly_FireFist_Bullet : The_Default_Bullet {

	public float RotateSpeed = 0;
	public float DistanceFromCenter = 0.1f;

	float _AngleToMove = 0;
	Vector3[] _CurrentDirection;
	Vector3 Saver = Vector3.zero;

	public override void SetMethod (GameManagerTestingWhileWaiting.SpellAttackInfo SpellInfo, The_Object_Behaviour MySender){
		base.SetMethod (SpellInfo, MySender);
		_CurrentDirection = _Shooter.GetMovementVector ();
		Saver = transform.eulerAngles;
	}

	void FixedUpdate(){

			Rotating ();
	
	}

	bool once = false;
	void Rotating(){
		if (once == false) {
			_Shooter = transform.parent.GetComponent<CreatureBehaviour>().ObjectBehaviour;
			_CurrentDirection = transform.parent.GetComponent<CreatureBehaviour>().ObjectBehaviour.GetMovementVector ();
			Saver = transform.eulerAngles;
			once = true;
		}

		_AngleToMove = Vector3.Angle ((Quaternion.Euler (0, 0, Saver.z - 90) * Vector3.right), _CurrentDirection [0]);
		
		if (Vector3.Cross ((Quaternion.Euler (0, 0, Saver.z - 90) * Vector3.right), _CurrentDirection [0]).z < 0) {//Checking Which Side Of The Rotation The Target Rotation Is
			_AngleToMove *= -1;
		}
		
		Saver.z += (_AngleToMove * Time.deltaTime) * RotateSpeed * 2;//Calculating AngleRotation

		transform.eulerAngles = Saver;
		transform.localPosition = (Quaternion.AngleAxis (Saver.z - 90, Vector3.forward) * (Vector3.right * DistanceFromCenter));
		
	}

	void OnTriggerEnter2D(Collider2D Col){
		if (Col.gameObject != _Shooter._MyTransform.gameObject) {
			_Shooter.OnCollision (Col);	
			Debug.Log ("Col " + Col.name);
		}
	}


}

