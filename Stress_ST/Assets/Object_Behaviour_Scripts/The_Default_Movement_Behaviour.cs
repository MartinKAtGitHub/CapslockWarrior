using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Default_Movement_Behaviour : The_Default_Behaviour {

	public int WhenCompleteChangeToBehaviourIndex = 0;
	public float AnimatorStageValueOnEnter = 0;

	[Header("Rotation Values")]
	public bool LookAtTarget = true;
	public bool RotateToWalkingDirection = false;
	public bool RotateBack = false;
	public float RotateSpeed = 1;
	[Space(10)]

	protected Transform _MyTransform;
	protected Transform _TargetTransform;
	protected bool _Turned = true;

	protected Vector3[] MoveDirection;
	protected Vector3[] RotateDirection;
	 
	Vector3 _CurrentDirection = Vector3.right;
	float _AngleToMove = 0;

	public override void SetMethod (The_Object_Behaviour myTransform){
		_MyObject = myTransform;
		MoveDirection = _MyObject.GetMovementVector ();
		RotateDirection = _MyObject.GetRotationVector ();
	}

	public override void OnEnter (){
		if (_MyTransform.eulerAngles.y == 180) {
			_Turned = false;
		} else {
			_Turned = true;
		}
		RotateDirection[0] = _MyTransform.eulerAngles;
	}

	public void RotationMethods(){
		if (LookAtTarget == true) {
			LookAt ();
		}

		if (RotateToWalkingDirection == true) {
			RotateTowards ();
		}

		if (RotateBack == true) {
			RotateBackStraight ();
		}
	}


	public void LookAt(){//Rotates The Object To Face The Targeted Object By Rotating The Y'Axis
		if (LookAtTarget == true) {
			if (RotateToWalkingDirection == false) {
				if (_TargetTransform.position.x < _MyTransform.position.x) {
					if (_Turned == true) {
						RotateDirection[0].y = 180;
						_Turned = false;
					//	_MyTransform.eulerAngles = RotateDirection[0];
					}
				} else {
					if (_Turned == false) {
						RotateDirection[0].y = 0;
						_Turned = true;
					//	_MyTransform.eulerAngles = RotateDirection[0];
					}
				}
			} else {//If Im Rotating The Object Then I Need To Check When The Rotation-Z Is More Or Less Then +-90 To Turn Around
				if (_TargetTransform.position.x > _MyTransform.position.x) {
					if (_Turned == false) {
						if (_MyTransform.eulerAngles.z > 90 || _MyTransform.eulerAngles.z < -90) {
							RotateDirection[0].y = 0;
							_Turned = true;
						}
					} 
				} else {
					if (_Turned == true) {
						if (_MyTransform.eulerAngles.z > 90 || _MyTransform.eulerAngles.z < -90) {
							RotateDirection[0].y = 180;
							_Turned = false;
						}
					} 
				}
			}
		}
	}

	public void RotateTowards(){
		if (RotateToWalkingDirection == true) {
			if (_MyTransform.eulerAngles.y == 180) {//If Object Is Rotated 180 Degrees On The Y'axis
				_CurrentDirection = Quaternion.AngleAxis (_MyTransform.eulerAngles.z, Vector3.back) * Vector3.right;//Getting The Looking Direction.
				if (_TargetTransform.position.y - _MyTransform.position.y < 0) {
					_AngleToMove = Vector3.Angle (_CurrentDirection * -1, _TargetTransform.position -_MyTransform.position) * -1;//Getting The Angle From My Looking Direction To The "Targeted" Location
					if (Vector3.Cross (_CurrentDirection * -1, _TargetTransform.position -_MyTransform.position).z < 0) {//Checking If The Target Is On The Other Side Of Me, If True Then I Need To Go Backwards
						_AngleToMove *= -1;
					}
				} else {
					_AngleToMove = Vector3.Angle (_CurrentDirection * -1, _TargetTransform.position -_MyTransform.position);	
					if (Vector3.Cross (_CurrentDirection * -1, _TargetTransform.position -_MyTransform.position).z > 0) {
						_AngleToMove *= -1;
					}
				}
			} else {//Same As Above Just The Oposite
				_CurrentDirection = Quaternion.AngleAxis (_MyTransform.eulerAngles.z, Vector3.forward) * Vector3.right;
				if (_TargetTransform.position.y - _MyTransform.position.y < 0) {
					_AngleToMove = Vector3.Angle (_CurrentDirection, _TargetTransform.position -_MyTransform.position) * -1;
					if (Vector3.Cross (_CurrentDirection, _TargetTransform.position -_MyTransform.position).z > 0) {
						_AngleToMove *= -1;
					}
				} else {
					_AngleToMove = Vector3.Angle (_CurrentDirection, _TargetTransform.position -_MyTransform.position);
					if (Vector3.Cross (_CurrentDirection, _TargetTransform.position -_MyTransform.position).z < 0) {
						_AngleToMove *= -1;
					}
				}
			}

			RotateDirection[0].z += (_AngleToMove * Time.deltaTime) * RotateSpeed;//Adding the New Angle To Move
			if (RotateDirection[0].z < -180) {//Checks If The Rotation Is More Or Less Then 180 -180
				RotateDirection[0].z = 360 + RotateDirection[0].z;
			}
			if (RotateDirection[0].z > 180) {
				RotateDirection[0].z = RotateDirection[0].z - 360;
			}
		}
	}

	public void RotateBackStraight(){//TODO DO THIS, Rotate Back To Start Vector 
	/*	if (RotateToWalkingDirection == true) {
			if (_MyTransform.eulerAngles.y == 180) {//If Object Is Rotated 180 Degrees On The Y'axis
				_CurrentDirection = Quaternion.AngleAxis (_MyTransform.eulerAngles.z, Vector3.back) * Vector3.right;//Getting The Looking Direction.
				if (_TargetTransform.position.y - _MyTransform.position.y < 0) {
					_AngleToMove = Vector3.Angle (_CurrentDirection * -1, _TargetTransform.position -_MyTransform.position) * -1;//Getting The Angle From My Looking Direction To The "Targeted" Location
					if (Vector3.Cross (_CurrentDirection * -1, _TargetTransform.position -_MyTransform.position).z < 0) {//Checking If The Target Is On The Other Side Of Me, If True Then I Need To Go Backwards
						_AngleToMove *= -1;
					}
				} else {
					_AngleToMove = Vector3.Angle (_CurrentDirection * -1, _TargetTransform.position -_MyTransform.position);	
					if (Vector3.Cross (_CurrentDirection * -1, _TargetTransform.position -_MyTransform.position).z > 0) {
						_AngleToMove *= -1;
					}
				}
			} else {//Same As Above Just The Oposite
				_CurrentDirection = Quaternion.AngleAxis (_MyTransform.eulerAngles.z, Vector3.forward) * Vector3.right;
				if (_TargetTransform.position.y - _MyTransform.position.y < 0) {
					_AngleToMove = Vector3.Angle (_CurrentDirection, _TargetTransform.position -_MyTransform.position) * -1;
					if (Vector3.Cross (_CurrentDirection, _TargetTransform.position -_MyTransform.position).z > 0) {
						_AngleToMove *= -1;
					}
				} else {
					_AngleToMove = Vector3.Angle (_CurrentDirection, _TargetTransform.position -_MyTransform.position);
					if (Vector3.Cross (_CurrentDirection, _TargetTransform.position -_MyTransform.position).z < 0) {
						_AngleToMove *= -1;
					}
				}
			}

			RotateDirection[0].z += (_AngleToMove * Time.deltaTime) * RotateSpeed;//Adding the New Angle To Move
			if (RotateDirection[0].z < -180) {//Checks If The Rotation Is More Or Less Then 180 -180
				RotateDirection[0].z = 360 + RotateDirection[0].z;
			}
			if (RotateDirection[0].z > 180) {
				RotateDirection[0].z = RotateDirection[0].z - 360;
			}
		}*/
	}

	public override int GetInt (int index){
		if (index == 1) {
			return WhenCompleteChangeToBehaviourIndex;
		} else {
			return base.GetInt (index);
		}
	}

}