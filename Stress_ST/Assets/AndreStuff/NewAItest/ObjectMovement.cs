using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectMovement {

	[HideInInspector]
	public CreatureRoot myVariables;

	//For Different Parts To Work Different Variables Is Needed. I Did This To Make It Just 
	[HideInInspector]
	public AnimatorVariables AnimatorInfo;

	[HideInInspector]
	public bool ChangedMovementType = false;
	[HideInInspector]
	public bool ChangedRotationType = false;
	Vector3 MovementVector = Vector3.zero;
	Vector3 RotateDirection = Vector3.right;
	float _AngleToMove = 0;


	public void Setup(CreatureRoot me){

		myVariables = me;
		AnimatorInfo = me.GetAnimatorVariables ();
	
	}

	int _SavedMovement = 0;
	int _SavedRotation = 0;

	public Vector3 DoMovement(){

		if (_SavedMovement != AnimatorInfo.MovementType) {
			ChangedMovementType = true;
			_SavedMovement = AnimatorInfo.MovementType;
		}

		if (AnimatorInfo.MovementType == 0) {//0 == With Pathfinding And Walking From Node To Node
			myVariables.GetNodeInfo().MyAStar.StartRunning (myVariables.GetObjectNodeInfo(), myVariables.GetNodeInfo(), myVariables.GetWhatToTarget().MyMovementTarget);
		
			if(myVariables.GetNodeInfo().MyNodePath [2] == null){//If There Is Less Then 2 Nodes Left, Including That StartNode; 
				return Vector3.zero;
			}
			if (myVariables.GetNodeInfo().MyNodePath [1] == null) {
				return Vector3.zero;
			} else {
				MovementVector.x = ((myVariables.GetNodeInfo().MyNodePath [1].PosX - (myVariables.GetNodeInfo().MyNodePath [0].PosX)));
				MovementVector.y = ((myVariables.GetNodeInfo().MyNodePath [1].PosY - (myVariables.GetNodeInfo().MyNodePath [0].PosY)));
			}

			return MovementVector.normalized;
		


		} else if (AnimatorInfo.MovementType == 1) {//Direct Targeting

			MovementVector = (myVariables.GetWhatToTarget().MyMovementTarget.transform.position - myVariables.transform.position).normalized;
		
		
			return MovementVector;
					
		
		
		} else if (AnimatorInfo.MovementType == 2) {//Locked Direct Targeting
		
			if (ChangedMovementType == true) {
				ChangedMovementType = false;
			
				MovementVector = (myVariables.GetWhatToTarget().MyMovementTarget.transform.position - myVariables.transform.position).normalized;
			

				return MovementVector;
			}


			return MovementVector;
		


		} else {//Locking My Vector;
		

			return MovementVector;
		



		}
	}
		

	public void DoRotation(){

		if (_SavedRotation != AnimatorInfo.RotationType) {
			ChangedRotationType = true;
			_SavedRotation = AnimatorInfo.RotationType;
		}

		if (AnimatorInfo.RotationType == 0) {//Flip-Movement
			
			if (MovementVector.x > 0) {
				AnimatorInfo.transform.eulerAngles = Vector3.zero;//Resets The Objects Rotation
			} else {
				AnimatorInfo.transform.eulerAngles = Vector3.up * 180;//Flips The Object
			}



		} else if (AnimatorInfo.RotationType == 1) {//Rotate-Movement
			
			_AngleToMove = Vector3.Angle ((Quaternion.Euler (0, RotateDirection.y, RotateDirection.z) * Vector3.right), MovementVector);

			if (Vector3.Cross ((Quaternion.Euler (0, RotateDirection.y, RotateDirection.z) * Vector3.right), MovementVector).z < 0) {//Checking Which Side Of The Rotation The Target Rotation Is
				_AngleToMove *= -1;
			}

			if (myVariables.GetWhatToTarget().MyMovementTarget.transform.position.x - myVariables.transform.position.x > 0) {
				RotateDirection.y = 0;
			} else {
				RotateDirection.y = 180;
			}

			if (RotateDirection.y == 0) {//When Y Are 180 Then The Object Is Rotated And The Rotation Is Mirrored
				RotateDirection.z += (_AngleToMove * Time.deltaTime) * 2 * 2;//Calculating AngleRotation
			} else {
				RotateDirection.z -= (_AngleToMove * Time.deltaTime) * 2 * 2;//Calculating AngleRotation
			}
			AnimatorInfo.transform.eulerAngles = RotateDirection;



		} else if (AnimatorInfo.RotationType == 2) {//Flip-TargetObject

			if (myVariables.GetWhatToTarget().MyMovementTarget.transform.position.x - myVariables.transform.position.x > 0) {
				AnimatorInfo.transform.eulerAngles = Vector3.zero;//Resets The Objects Rotation
			} else {
				AnimatorInfo.transform.eulerAngles = Vector3.up * 180;//Flips The Object
			}



		} else if (AnimatorInfo.RotationType == 3) {//Rotate-TargetObject

			_AngleToMove = Vector3.Angle ((Quaternion.Euler (0, RotateDirection.y, RotateDirection.z) * Vector3.right), (myVariables.GetWhatToTarget().MyMovementTarget.transform.position - myVariables.transform.position));

			if (Vector3.Cross ((Quaternion.Euler (0, RotateDirection.y, RotateDirection.z) * Vector3.right), (myVariables.GetWhatToTarget().MyMovementTarget.transform.position - myVariables.transform.position)).z < 0) {//Checking Which Side Of The Rotation The Target Rotation Is
				_AngleToMove *= -1;
			}

			if (myVariables.GetWhatToTarget().MyMovementTarget.transform.position.x - myVariables.transform.position.x > 0) {
				RotateDirection.y = 0;
			} else {
				RotateDirection.y = 180;
			}

			if (RotateDirection.y == 0) {//When Y Are 180 Then The Object Is Rotated And The Rotation Is Mirrored
				RotateDirection.z += (_AngleToMove * Time.deltaTime) * 2 * 2;//Calculating AngleRotation
			} else {
				RotateDirection.z -= (_AngleToMove * Time.deltaTime) * 2 * 2;//Calculating AngleRotation
			}
			AnimatorInfo.transform.eulerAngles = RotateDirection;



		} 

	}

}
