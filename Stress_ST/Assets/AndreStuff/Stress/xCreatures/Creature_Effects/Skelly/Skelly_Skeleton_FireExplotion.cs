using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skelly_Skeleton_FireExplotion : The_Default_Bullet {

	public float RotateSpeed = 0;
	public float DistanceFromCenter = 0.1f;
	public float StartDist = 0.125f;
	public float TimeToFly = 1;
	float StartTime = 0;
	public GameObject OnCollisionExplotion;

	public	float _AngleToMove = 0;
	public	Vector3[] _CurrentDirection;
	public Vector3 Saver = Vector3.zero;

	public override void SetMethod (GameManagerTestingWhileWaiting.SpellAttackInfo SpellInfo, The_Object_Behaviour MySender){
		base.SetMethod (SpellInfo, MySender);
		_CurrentDirection = _Shooter.ObjectCurrentVector;
		transform.parent = _Shooter._MyTransform.transform;
		Saver = transform.eulerAngles;
		RotateSpeed = RotateSpeed / 100;
		StartTime = _Shooter.TheTime [0];
	}

	void FixedUpdate(){

		if (_Shooter.TheTime [0] > StartTime + TimeToFly) {
			_Shooter.MyAnimator.SetInteger(_Shooter.AnimatorVariables[1] , 2);
			Destroy (this.gameObject);
		}

		if (StartDist < DistanceFromCenter) {
			StartDist += 0.005f;
			if (StartDist >= DistanceFromCenter) {
				StartDist = DistanceFromCenter;
				RotateSpeed = (RotateSpeed * 100) * 2;
			}
		}

		_AngleToMove = Vector3.Angle ((Quaternion.Euler (0, 0, Saver.z - 90) * Vector3.right), _CurrentDirection [0]);

		if (Vector3.Cross ((Quaternion.Euler (0, 0, Saver.z - 90) * Vector3.right), _CurrentDirection [0]).z < 0) {//Checking Which Side Of The Rotation The Target Rotation Is
			_AngleToMove *= -1;
		}

		Saver.z += (_AngleToMove * Time.deltaTime) * RotateSpeed;//Calculating AngleRotation

		transform.eulerAngles = Saver;
		transform.localPosition = (Quaternion.AngleAxis (Saver.z - 90, Vector3.forward) * (Vector3.right * StartDist));

	}

	void OnTriggerEnter2D(Collider2D Col){
		if (Col.gameObject != _Shooter._MyTransform.gameObject) {

			if (Col.gameObject.layer == 13 || Col.gameObject.layer == 12) {//Hit Walls
				//	if (Col.transform.CompareTag ("Wall")) {
				//		TODO Wall Recieve Dmg, Depending On If It Broke Or Not Stop
				//	} TODO else Hit A Rock Hard Wall
				_Shooter.MyAnimator.SetInteger(_Shooter.AnimatorVariables [1], 1);
				Instantiate (OnCollisionExplotion, transform.position, Quaternion.identity);
				Destroy (this.gameObject);
			} else {
				if (Col.transform.CompareTag ("Player1")) {
					Col.GetComponent<PlayerManager> ().RecievedDmg (1);
					_Shooter.MyAnimator.SetInteger(_Shooter.AnimatorVariables [1], 1);
					Instantiate (OnCollisionExplotion, transform.position, Quaternion.identity);
					Destroy (this.gameObject);
				} else {
					Debug.LogWarning ("The Object Didnt Take Dmg??? " + Col.name);
				}
			}

		}
	}


}

