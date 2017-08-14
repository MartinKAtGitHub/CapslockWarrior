using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonPreLaunchTest : MonoBehaviour {
	int _AnimatorControllerParameterStage = Animator.StringToHash ("AnimationStage");
	float _MovementValue = 0;

	public float TheExpotentialValue = 0;//0.7
	public float ExponentialGrowth = 2.1642f;
	float _MovementSaver = 0;

	public Animator MyAnimator;
	public float MaximumMovementValue = 0;//2.5



	public Vector3 MyDirection = Vector3.zero;
	public float RotationGoal;
	public float RotatingSpeed = 0;//125
	Vector3 testing = Vector3.zero;



	public GameObject MyHand;
	public GameObject MyTarget;



	bool turn = false;
	public bool stop = false;

	void Start(){
		if (MyTarget.transform.position.x < transform.position.x) {
				transform.rotation = Quaternion.Euler (0, 180, 0);
		}
		if (MyTarget.transform.position.x > transform.position.x) {
				transform.rotation = Quaternion.Euler (0, 0, 0);
		}
	}


	float counter = -1.5f;


	void Update () {

		if ((counter += Time.deltaTime) >= 2) {
			stop = true;
			counter = 0;
		}


		if (MyAnimator.GetFloat (_AnimatorControllerParameterStage) > 0) {

			if (stop == false) {
				
				if (_MovementValue < MaximumMovementValue) {
					_MovementValue = (Mathf.Pow (_MovementSaver, ExponentialGrowth)) * Time.deltaTime;
					_MovementSaver += TheExpotentialValue;
					if (_MovementValue >= MaximumMovementValue) {
						_MovementValue = MaximumMovementValue;
					}
				}

				if (RotationGoal < 0) {
					if (transform.eulerAngles.z - 360 >= RotationGoal - 10 && transform.eulerAngles.z - 360 <= RotationGoal + 10) {

					} else {
						testing.z = (RotatingSpeed * Time.deltaTime);
						transform.eulerAngles -= testing;
					} 
				} else {
					if (transform.eulerAngles.z >= RotationGoal - 5 && transform.eulerAngles.z <= RotationGoal + 5) {

					} else {
						testing.z = (RotatingSpeed * Time.deltaTime);
						transform.eulerAngles += testing;
					} 
				}


				transform.parent.position += MyDirection * Time.deltaTime * _MovementValue;
			
				if (stop == true) {
					stop = false;
					_MovementSaver = 0;
					MyAnimator.SetFloat (_AnimatorControllerParameterStage, 0);
				}
			} else {
				stop = false;
				_MovementSaver = 0;
				MyAnimator.SetFloat (_AnimatorControllerParameterStage, 0);
			} 

		} else {

			if (MyTarget.transform.position.x < transform.position.x) {
				if (turn == false) {
					turn = true;
					transform.rotation = Quaternion.Euler (0, 180, 0);
				}
			} else {
				if (turn == true) {
					turn = false;
					transform.rotation = Quaternion.Euler (0, 0, 0);
				}
			}

			tesingthi = (MyTarget.transform.position - MyHand.transform.parent.position).normalized;
			MyHand.transform.localPosition = tesingthi * ArmDistance;
			MyDirection = tesingthi;

			if (tesingthi.y < 0) {
				if (transform.eulerAngles.y == 180) {
					RotationGoal = -Vector3.Angle (Vector3.left, tesingthi);
					MyHand.transform.localPosition *= 1.1f;
				} else {
					RotationGoal = -Vector3.Angle (Vector3.right, tesingthi);
				}

				MyHand.transform.rotation = Quaternion.Euler (0, 0, 90 - (Vector3.Angle (Vector3.right, tesingthi)));
			} else {
				if (transform.eulerAngles.y == 180) {
					RotationGoal = Vector3.Angle (Vector3.left, tesingthi);
					MyHand.transform.localPosition *= 1.1f;
				} else {
					RotationGoal = Vector3.Angle (Vector3.right, tesingthi);
				}

				MyHand.transform.rotation = Quaternion.Euler (0, 0, Vector3.Angle (Vector3.right, tesingthi) + 90 );
			}

		}

	}
		
	public float ArmDistance = 1;
//	float MovementSpeed = 0;
//	Vector3 StartVector = Vector3.zero;
	Vector3 tesingthi = Vector3.zero;

}
