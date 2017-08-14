using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Object_Straight_To_Target : Default_Movement_Behaviour { 

	public bool ChangeAttackIndex = false;
	public int AttackIndexChange = 0;

	public float MovementMultiplyer = 1;
	[Tooltip("Time == true, Distance == false")]
	public bool TimeOrDistance = false;
	[Tooltip("Value To Check, If TimeOrDistance == True Then CheckingValue Is Time, Distance If False")]//Might Make A Enum, But Just Came Up With These Two
	public float CheckingValue = 10;
	public int AnimatorStageValue = 0;

	float _TimeDistanceChecker;
	float[] _TheTime;
	int[] _AnimatorVariables;

	public override void SetMethod (Object_Behaviour myTransform, Transform targetTransform, int[] AnimatorValues){
		_MyObject = myTransform;
		_MyTransform = myTransform.transform;
		_TargetTransform = targetTransform;
		_AnimatorVariables = AnimatorValues;
		_TheTime = _MyObject.GetTheTime ();
	}

	public override void OnEnter (){
		base.OnEnter ();
		_MyObject.SetAttackBehaviour (AttackIndexChange);
		_MyObject.MyAnimator.SetFloat (_AnimatorVariables[1], AnimatorStageValue);
		if (TimeOrDistance == true) {
			_TimeDistanceChecker = _TheTime [0] + CheckingValue;
		}
	}

	public override void BehaviourMethod (){
		 
		LookAt ();
		RotateTowards ();

		if (TimeOrDistance == false) {
			MoveDirection[0] = (_TargetTransform.position - _MyTransform.position).normalized * Time.deltaTime * (_MyObject.MovementSpeed * MovementMultiplyer);//1 == creature standard speed
			_TimeDistanceChecker += Vector3.Distance (Vector3.zero, MoveDirection[0]);

			_MyTransform.position += MoveDirection[0];

			if (CheckingValue <= _TimeDistanceChecker) {
				_MyObject.SetMovementBehaviour (WhenCompleteChangeToMovementIndex);
			}
		} else {
			MoveDirection[0] = (_TargetTransform.position - _MyTransform.position).normalized * Time.deltaTime * (_MyObject.MovementSpeed * MovementMultiplyer);//1 == creature standard speed

			if (_TimeDistanceChecker < _TheTime [0]) {
				_MyObject.SetMovementBehaviour (WhenCompleteChangeToMovementIndex);
			}
		}

	}

	public override void Reset (){
		_TimeDistanceChecker = 0;
	}

	public override bool GetBool (int index){
		if (index == 0) {
			return ChangeAttackIndex;
		} else {
			return base.GetBool (index);
		}
	}

	public override int GetInt (int index){
		return AttackIndexChange;
	}


}
