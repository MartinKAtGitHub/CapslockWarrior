using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Straight_To_Target : The_Default_Movement_Behaviour {

	[Space(10)]
	[Header("Movement Values")]

	public float MovementMultiplyer = 1;
	[Tooltip("Time == true, Distance == false")]
	public bool TimeOrDistance = false;
	[Tooltip("Value To Check, If TimeOrDistance == True Then CheckingValue Is Time, Distance If False")]//Might Make A Enum, But Just Came Up With These Two
	public float CheckingValue = 10;
	[Tooltip("When Entering New Behaviour Change The Animation Stage To The Selected Value")]

	float[] _TheTime;
	int[] _AnimatorVariables;
	bool _Attacking = false;

	float TimeStarted = 0;
	float _ValueWhenLastUpdated = 0;

	public override void SetMethod (The_Object_Behaviour myTransform){
		base.SetMethod (myTransform);

		_MyObject = myTransform;
		_MyTransform = myTransform._MyTransform;
		_TargetTransform = myTransform._Target;
		_TheTime = _MyObject.GetTheTime ();
		_AnimatorVariables = _MyObject.AnimatorVariables;
	}


	public override void OnEnter (){
		base.OnEnter ();//Rotation

		_MyObject.MyAnimator.SetFloat (_AnimatorVariables[1], AnimatorStageValueOnEnter);

		if (TheResetState == ResetState.ResetOnEnter) {
			if (TimeOrDistance == true) {
				TimeStarted = _TheTime [0];
			}
			_ValueWhenLastUpdated = 0;
		} else {
			if (TimeOrDistance == true) {
				TimeStarted = _TheTime [0];
			}
		}
	}

	public override void BehaviourUpdate (){

		RotationMethods ();

		if (TimeOrDistance == false) {
			MoveDirection [0] = (_TargetTransform.position - _MyTransform.position).normalized * Time.deltaTime * (_MyObject.MovementSpeed * MovementMultiplyer);//1 == creature standard speed
			_ValueWhenLastUpdated += Vector3.Distance (Vector3.zero, MoveDirection [0]);//Distance Traveled

			_MyTransform.position += MoveDirection [0];

			if (_Attacking == false) {
				if (_ValueWhenLastUpdated >= CheckingValue) {
					if (TheResetState == ResetState.ResetWhenComplete) {
						Reset ();
					}
					_MyObject.SetMovementBehaviour (WhenCompleteChangeToBehaviourIndex);
				}
			}
		} else {
			MoveDirection [0] = (_TargetTransform.position - _MyTransform.position).normalized * Time.deltaTime * (_MyObject.MovementSpeed * MovementMultiplyer);//1 == creature standard speed
			_ValueWhenLastUpdated = _TheTime [0] - TimeStarted;//Time Spent 

			if (_Attacking == false) {
				if (_ValueWhenLastUpdated >= CheckingValue) {
					if (TheResetState == ResetState.ResetWhenComplete) {
						Reset ();
					}
					_MyObject.SetMovementBehaviour (WhenCompleteChangeToBehaviourIndex);
				}
			}
		}

	}

	public override void Reset (){
		_ValueWhenLastUpdated = 0;
	}

	public override bool GetBool (int index){
		_Attacking = true;
		BehaviourUpdate ();
		_Attacking = false;

		if (_ValueWhenLastUpdated >= CheckingValue) {
			if (TheResetState == ResetState.ResetWhenComplete) {
				Reset ();
			}
			return true;
		} else {
			return base.GetBool (index);
		}
	}

	public override int GetInt (int index){
		if (index == 2) {//5 == Coming From Attack
			return WhenCompleteChangeToBehaviourIndex;
		} else {
			return 0;
		}
	}


}
