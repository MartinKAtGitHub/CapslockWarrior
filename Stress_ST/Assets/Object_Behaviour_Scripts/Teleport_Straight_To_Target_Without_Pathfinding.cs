using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_Straight_To_Target_Without_Pathfinding : The_Default_Movement_Behaviour {

	float[] _TheTime;
	int[] _AnimatorVariables;
	bool _Attacking = false;

	float TimeStarted = 0;
	float _ValueWhenLastUpdated = 0;


	[Space(10)]
	[Header("Movement Values")]
	public Rigidbody2D MyRidig2D;
	public Vector3[] TeleportVector;
	Vector3 StartPosition = Vector3.zero;
	public bool[] TeleportFromInitialPositionOrContinue;//If You Want The Object To Teleport From Center + Position. Or Center + Pos + Pos + Pos. You Can Do Center + Pos + Pos BakcToCenter + Pos + Pos    If You Want
	public float[] TimeBetweenTeleports;
	int TeleportIndex = 0;

	public override void SetMethod (The_Object_Behaviour myTransform){
		base.SetMethod (myTransform);

		_MyObject = myTransform;
		_MyTransform = myTransform._TheObject;

		_TargetTransform = myTransform._TheObject._TheTarget;

		_TheTime = _MyObject.GetTheTime ();
		_AnimatorVariables = _MyObject.AnimatorVariables;

	}


	public override void OnEnter (){
		base.OnEnter ();//Rotation

		if (TheResetState == ResetState.ResetOnEnter) {
			Reset ();
		} else {
			TimeStarted = _TheTime [0];
		}

		MoveDirection [0] = Vector3.zero;
		_MyObject.MyAnimator.SetFloat (_AnimatorVariables [1], AnimatorStageValueOnEnter);
		StartPosition = _MyTransform.transform.position;

	}

	public override void BehaviourUpdate (){
		MovementRotations ();
		_ValueWhenLastUpdated = _TheTime [0] - TimeStarted;

		if (TeleportIndex < TeleportVector.Length) {
			if (_ValueWhenLastUpdated > TimeBetweenTeleports [TeleportIndex]) {
				TimeStarted = _TheTime [0];
				if (TeleportFromInitialPositionOrContinue [TeleportIndex] == true) {//If True, Move Back To StartPosition
					if (Vector3.Cross (Vector3.right, _CurrentDirection [0]).z < 0) {
						MyRidig2D.MovePosition ((StartPosition + (Quaternion.AngleAxis (Vector3.Angle (Vector3.right, _CurrentDirection[0]) * -1, Vector3.forward) * TeleportVector [TeleportIndex])));
					} else {
						MyRidig2D.MovePosition ((StartPosition + (Quaternion.AngleAxis (Vector3.Angle (Vector3.right, _CurrentDirection[0]), Vector3.forward) * TeleportVector [TeleportIndex])));
					}

				} else {//Else Continue From Teleported Position
					if (Vector3.Cross (Vector3.right, _CurrentDirection [0]).z < 0) {
						MyRidig2D.MovePosition ((_MyTransform.transform.position + (Quaternion.AngleAxis (Vector3.Angle (Vector3.right, _CurrentDirection[0]) * -1, Vector3.forward) * TeleportVector [TeleportIndex])));
					} else {
						MyRidig2D.MovePosition ((_MyTransform.transform.position + (Quaternion.AngleAxis (Vector3.Angle (Vector3.right, _CurrentDirection[0]), Vector3.forward) * TeleportVector [TeleportIndex])));
					}
				}
				TeleportIndex++;
			}
		} else {
			if (_Attacking == false) {
				if (TeleportIndex >= TeleportVector.Length) {
					if (TheResetState == ResetState.ResetWhenComplete) {
						Reset ();
					}
					_MyObject.SetMovementBehaviour (WhenCompleteChangeToBehaviourIndex);
				}
			}
		}
	}

	public override void Reset (){
		TimeStarted = _TheTime [0];
		_ValueWhenLastUpdated = 0;
		TeleportIndex = 0;
	}
		
	public override bool GetBool (int index){
		
		if (index == 5) {//Attack Uses This To Do The Movement. Index 5 Is Reserved For Attack Call To Movement
			_Attacking = true;
			BehaviourUpdate ();
			_Attacking = false;

			if (TeleportIndex >= TeleportVector.Length) {
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