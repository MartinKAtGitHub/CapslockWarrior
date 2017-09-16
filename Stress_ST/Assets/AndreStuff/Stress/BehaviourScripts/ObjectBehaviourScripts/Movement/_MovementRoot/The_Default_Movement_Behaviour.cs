using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Default_Movement_Behaviour : The_Default_Behaviour {

	[Tooltip("If You Want The Object To Follow The NodeMap, Then Nothing Is You Answer. If Not The Choose The Option That Suits You Behaviour The Most")]
	public GameManagerTestingWhileWaiting.VectorDirection DirectionBehaviour;


	[Tooltip("When The Behaviour Is Complete, Then It Changes The Behaviour Corresponding To The Set Value")]
	public int WhenCompleteChangeToBehaviourIndex = 0;
	[Tooltip("When This Behaviour Is Entered, The Animaiton Value Is Changed To Change The Animation Clip Playing")]
	public float AnimatorStageValueOnEnter = 0;

	[Header("Rotation Values")]
	[Tooltip("If The Target Is On Left Then It Rotates To Look Left. (Instant)")]
	public bool LookAtTarget = true;
	[Tooltip("If True Then The Objects Rotates To face The Target With The Start Vector Of Vector.Right (1,0,0)")]
	public bool RotateToWalkingDirection = false;
	[Tooltip("If True Then The Object Is Rotating Back To Its Original Position. (1,0,0 If Looking Right. -1,0,0 If Looking Left)")]
	public bool RotateBack = false;
	[Tooltip("The Speed To Rotate")]
	public float RotateSpeed = 1;
	[Space(10)]

	protected DefaultBehaviourPosition _MyTransform;//MyCenterPosition
	protected DefaultBehaviourPosition[] _TargetTransform;//TargetCenterPosition
	protected bool _Turned = true;

	protected Vector3[] MoveDirection;
	protected Vector3[] RotateDirection;
	 
	float _AngleToMove = 0;

	public Vector3[] _CurrentDirection;
	protected Vector3[] _TargetDirection;

	Transform MyRotation;
	protected float[] MovementSpeed;
	protected int[] _AnimatorVariables;


	public override void SetMethod (The_Object_Behaviour myTransform){
		_MyObject = myTransform;
		MovementSpeed = _MyObject._TheObject.MovementSpeed;
		MyRotation = _MyObject._TheObject.GfxObject.transform;
		MoveDirection = _MyObject.GetMovementVector ();
		RotateDirection = _MyObject.GetRotationVector ();

		_CurrentDirection = myTransform.ObjectCurrentVector;
		_TargetDirection = myTransform.ObjectTargetVector;
		_AnimatorVariables = _MyObject.AnimatorVariables;
	}

	public override void OnEnter (){

		if (MyRotation.eulerAngles.y == 180) {
			_Turned = false;
		} else {
			_Turned = true;
		}

		if (DirectionBehaviour == GameManagerTestingWhileWaiting.VectorDirection.LockAtTarget) {
			TargetVectorLockAtTarget ();
		} else if (DirectionBehaviour == GameManagerTestingWhileWaiting.VectorDirection.LockVector) {
			TargetVectorLock ();
		}
	}

	public void MovementRotations(){

			if (DirectionBehaviour == GameManagerTestingWhileWaiting.VectorDirection.StraightToTraget) {
				TargetVectorTargetFollow ();
			}

			RotationCalculation ();
	
		if (_MyObject.MyAnimator.GetBool (_AnimatorVariables [3]) == true) {

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
	}

	void RotationCalculation(){
		_AngleToMove = Vector3.Angle (_CurrentDirection [0], _TargetDirection [0]);

		if (Vector3.Cross (_CurrentDirection [0], _TargetDirection [0]).z < 0) {
			_AngleToMove *= -1;
		}
		_CurrentDirection [0] = Quaternion.AngleAxis ((_AngleToMove * Time.deltaTime) * RotateSpeed, Vector3.forward) * _CurrentDirection [0];
	}

	void TargetVectorTargetFollow(){
		_TargetDirection[0] = (_TargetTransform[0].transform.position - _MyTransform.transform.position).normalized;
	}

	void TargetVectorLockAtTarget(){
		_TargetDirection[0] = (_TargetTransform[0].transform.position - _MyTransform.transform.position).normalized;
	}

	void TargetVectorLock(){
		_TargetDirection [0] = _CurrentDirection [0];
	}

	public void LookAt(){//Rotates The Object To Face The Targeted Object By Rotating The Y'Axis
		
		if (RotateToWalkingDirection == false) {
			if (DirectionBehaviour == GameManagerTestingWhileWaiting.VectorDirection.StraightToTraget) {
				TargetVectorTargetFollow ();
			}

			if (0 > _CurrentDirection [0].x) {
				if (_Turned == true) {
					RotateDirection [0].y = 180;
					_Turned = false;
				}
			} else {
				if (_Turned == false) {
					RotateDirection [0].y = 0;
					_Turned = true;
				}
			}
		} else {//If Im Rotating The Object Then I Need To Check When The Rotation-Z Is More Or Less Then +-90 To Turn Around
			if (0 > _CurrentDirection [0].x) {
				if (_Turned == true) {
					if (MyRotation.eulerAngles.z >= 90 || MyRotation.eulerAngles.z < -90) {
						RotateDirection [0].y = 180;
						_Turned = false;
					}
				} 
			} else {
				if (_Turned == false) {
					if (MyRotation.eulerAngles.z >= 90 || MyRotation.eulerAngles.z < -90) {
						RotateDirection [0].y = 0;
						_Turned = true;
					}
				} 
			}
		}
	}

	public void RotateTowards(){//Rotating Towards The TargetDirection

		_AngleToMove = Vector3.Angle ((Quaternion.Euler (0, RotateDirection [0].y, RotateDirection [0].z) * Vector3.right), _CurrentDirection [0]);

		if (Vector3.Cross ((Quaternion.Euler (0, RotateDirection [0].y, RotateDirection [0].z) * Vector3.right), _CurrentDirection [0]).z < 0) {//Checking Which Side Of The Rotation The Target Rotation Is
			_AngleToMove *= -1;
		}

		if (RotateDirection [0].y == 0) {//When Y Are 180 Then The Object Is Rotated And The Rotation Is Mirrored
			RotateDirection [0].z += (_AngleToMove * Time.deltaTime) * RotateSpeed * 2;//Calculating AngleRotation
		} else {
			RotateDirection [0].z -= (_AngleToMove * Time.deltaTime) * RotateSpeed * 2;//Calculating AngleRotation
		}

	}
	
	public void RotateBackStraight(){

		RotateDirection [0].z += ((0 - RotateDirection [0].z) * Time.deltaTime) * RotateSpeed;//Rotating To [1,0] Or [-1,0] Depending On Y-Axis Rotation

	}

	public override int GetInt (int index){
		if (index == 4)
			return 10;
		if (index == 1) {
			return WhenCompleteChangeToBehaviourIndex;
		} else {
			return base.GetInt (index);
		}
	}

}