using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Running Straight To The Target. Ignoring Everything And Just Runs
//When The Set Distance Or Time Is Met Then The Behaviour Changes To The Next
public class Move_Straight_To_Target_Without_Pathfinding : The_Default_Movement_Behaviour {

	[Space(10)]
	[Header("Movement Values")]
	[Tooltip("This Is Multiplyed With Movement Speed And Then Added To The MovementVector")]
	public float MovementMultiplyer = 1;
	[Tooltip("Time == true, Distance == false")]
	public bool TimeOrDistance = false;
	[Tooltip("Value To Check, If TimeOrDistance == True Then CheckingValue Is Time, Distance If False")]//Might Make A Enum, But Just Came Up With These Two
	public float CheckingValue = 10;

	float[] _TheTime;
	bool _Attacking = false;

	float TimeStarted = 0;
	float _ValueWhenLastUpdated = 0;

	public override void SetMethod (The_Object_Behaviour myTransform){
		base.SetMethod (myTransform);
		_MyObject = myTransform;
		_MyTransform = myTransform._TheObject;
		_TargetTransform = myTransform._TheTarget;

		_TheTime = _MyObject.GetTheTime ();
	}


	public override void OnEnter (){
		base.OnEnter ();//Rotation

		//Ichigo		_MyObject.MyAnimator.SetFloat (_AnimatorVariables[1], AnimatorStageValueOnEnter);

		if (TheResetState == ResetState.ResetOnEnter) {
			Reset ();
		} else {
			if (TimeOrDistance == true) {
				TimeStarted = _TheTime [0];
			}
		}
	}

	public override void BehaviourUpdate (){
		MovementRotations (); 

		if (TimeOrDistance == false) {
			MoveDirection [0] = _CurrentDirection[0].normalized * MovementMultiplyer * Time.deltaTime *  _MyObject._TheObject.MovementSpeed;//1 == creature standard speed
			_ValueWhenLastUpdated += Vector3.Distance (Vector3.zero, MoveDirection [0]);//Distance Traveled

			if (_Attacking == false) {
				if (_ValueWhenLastUpdated >= CheckingValue) {
					if (TheResetState == ResetState.ResetWhenComplete) {
						Reset ();
					}
					_MyObject.SetMovementBehaviour (WhenCompleteChangeToBehaviourIndex);
				}
			}
		} else {
			MoveDirection [0] = _CurrentDirection[0].normalized * MovementMultiplyer * Time.deltaTime *  _MyObject._TheObject.MovementSpeed;//1 == creature standard speed
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
		if (TimeOrDistance == true) {
			TimeStarted = _TheTime [0];
		}
		_ValueWhenLastUpdated = 0;
	}

	public override bool GetBool (int index){

		if (index == 5) {
			_Attacking = true;
			BehaviourUpdate ();
			_Attacking = false;

			if (_ValueWhenLastUpdated >= CheckingValue) {
				if (TheResetState == ResetState.ResetWhenComplete) {
					Reset ();
				}
				return true;
			}
		}

		return false;
	}

	public override int GetInt (int index){
		if (index == 2) {//5 == Coming From Attack
			return WhenCompleteChangeToBehaviourIndex;
		}else{
			return 0;
		}
	}


}
