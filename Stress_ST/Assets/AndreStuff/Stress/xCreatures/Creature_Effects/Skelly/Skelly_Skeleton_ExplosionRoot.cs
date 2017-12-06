using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skelly_Skeleton_ExplosionRoot : The_Default_Bullet {

	/*public float RotateSpeed = 0;
	public float DistanceFromCenter = 0.1f;
	public float StartDist = 0.125f;
	public float TimeToFly = 1;
	float StartTime = 0;
	public GameObject OnCollisionExplotion;

	public	float _AngleToMove = 0;
	public	Vector3[] _CurrentDirection;
	public Vector3 Saver = Vector3.zero;*/

	public GameObject Explotions;


	public override void SetMethod (GameManagerTestingWhileWaiting.SpellAttackInfo SpellInfo, The_Object_Behaviour MySender){
		base.SetMethod (SpellInfo, MySender);
		/*_CurrentDirection = _Shooter.ObjectCurrentVector;
		transform.parent = _Shooter._MyTransform.transform;
		Saver = transform.eulerAngles;
		RotateSpeed = RotateSpeed / 100;
		StartTime = _Shooter.TheTime [0];*/

		time = Time.time + 1.5f;
	}

	public int ExplotionsInRadius = 5; //Explotion radius == 0.1f
	public int rowsDone = 0; //Explotion radius == 0.1f

	Vector3 posit = Vector3.zero;
	bool shoot1 = false;
	float time = 2;

	public int DontSpawn = 0;
	float Size = 0.2f;

	void FixedUpdate(){
		if (shoot1 == false) {
			transform.position = _Shooter._TheTarget.transform.position;
		}

		if (Time.time > time) {
			shoot1 = true;

			if (rowsDone < ExplotionsInRadius) {
				if (rowsDone < ExplotionsInRadius - 1) {
					DontSpawn = Random.Range (1, (ExplotionsInRadius - rowsDone) * 2);
				} else {
					DontSpawn = 100; //Then Nothing 100 Is So Big That It Wont Even Happen
				}
				DontSpawn = 100; //Then Nothing 100 Is So Big That It Wont Even Happen


				time = Time.time + 1.5f; 
				posit.x = -(Size * (ExplotionsInRadius - rowsDone)) + (Size / 2);//All The Way Left
				posit.y = (Size * (ExplotionsInRadius - rowsDone)) - (Size / 2);//All The Way Top

				for (int j = 0; j < (ExplotionsInRadius - rowsDone) * 2; j++) {//Spawning Top And Bottom First From Left To RIght

					if (DontSpawn != j) {
						Instantiate (Explotions, transform.position + posit, Quaternion.identity);
						posit.y *= -1;//To The Bottom
						Instantiate (Explotions, transform.position + posit, Quaternion.identity);
						posit.y *= -1;//Back To Top
						posit.x += Size;
					} else {
						posit.x += Size;
					}

				}

				posit.x = (Size * (ExplotionsInRadius - rowsDone)) - (Size / 2);//All The Way Left
				for (int j = 1; j < (ExplotionsInRadius - rowsDone) * 2 - 1; j++) {//Spawning Left And Right 

					if (DontSpawn != j) {
						posit.y -= Size;
						Instantiate (Explotions, transform.position + posit, Quaternion.identity);
						posit.x *= -1;//To The Left
						Instantiate (Explotions, transform.position + posit, Quaternion.identity);
						posit.x *= -1;//Back To Right
					} else {
						posit.y -= Size;
					}
				
				}
				rowsDone++;
				
			} else {
				Destroy (this.gameObject);
			}
			
		}


	


		/*if (_Shooter.TheTime [0] > StartTime + TimeToFly) {
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
		transform.localPosition = (Quaternion.AngleAxis (Saver.z - 90, Vector3.forward) * (Vector3.right * StartDist));*/

	}

/*	void OnTriggerEnter2D(Collider2D Col){
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
	}*/


}

