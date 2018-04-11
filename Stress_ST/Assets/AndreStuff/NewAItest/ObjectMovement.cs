using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectMovement {

	//For Different Parts To Work Different Variables Is Needed. I Did This To Make It Just 
	[HideInInspector]
	public AnimatorVariables CheckVariables1;
	[HideInInspector]
	public WhatToTarget CheckVariables2;
	[HideInInspector]
	public NodeInfo CheckVariables3;//Info Needed For AStart Search.  PathNodeCost For Movement
	[HideInInspector]
	public ObjectNodeInfo CheckVariables4;//Everything That Has A Node Position Got This. 

	bool ChangedType = false;
	Vector3 MovementVector = Vector3.zero;
	Vector3 RotateDirection = Vector3.right;
	float _AngleToMove = 0;

	int WhatMovementType = 0;//0 - NodeMap Walking, 1 - DirectlyToTarget Walking, 2 - LockVectorToTarget, 3 - LockCurrentVector
	int WhatRotationType = 0;//0 - FlipRotation-MovementVector, 1 - RotateRotation-MovementVector, 2 - FlipRotation-TargetObjectVector, 3 - RotateRotation-TargetObjectVector


	public Vector3 DoMovement(){

		if (WhatMovementType != CheckVariables1.MovementType) {
			ChangedType = true;
			WhatMovementType = CheckVariables1.MovementType;
		}


		if (WhatMovementType == 0) {//0 == With Pathfinding And Walking From Node To Node
			CheckVariables3.MyAStar.StartRunning (CheckVariables4, CheckVariables3, CheckVariables2.MyMovementTarget);
		
			if(CheckVariables3.MyNodePath [2] == null){//If There Is Less Then 2 Nodes Left, Including That StartNode; 
				return Vector3.zero;
			}
			if (CheckVariables3.MyNodePath [1] == null) {
				return Vector3.zero;
			} else {
				MovementVector.x = ((CheckVariables3.MyNodePath [1].PosX - (CheckVariables3.MyNodePath [0].PosX)));
				MovementVector.y = ((CheckVariables3.MyNodePath [1].PosY - (CheckVariables3.MyNodePath [0].PosY)));
			}


			return MovementVector.normalized;
		


		} else if (WhatMovementType == 1) {//Direct Targeting

			MovementVector = (CheckVariables2.MyMovementTarget.transform.position - CheckVariables4.transform.position).normalized;
		
		
			return MovementVector;
					
		
		
		} else if (WhatMovementType == 2) {//Locked Direct Targeting
		
			if (ChangedType == true) {
				ChangedType = false;
			
				MovementVector = (CheckVariables2.MyMovementTarget.transform.position - CheckVariables4.transform.position).normalized;
			

				return MovementVector;
			}


			return MovementVector;
		


		} else {//Locking My Vector;
		

			return MovementVector;
		



		}
	}
		

	public void DoRotation(){

		if (WhatRotationType != CheckVariables1.RotationType) {
			ChangedType = true;
			WhatRotationType = CheckVariables1.RotationType;
		}

		if (WhatRotationType == 0) {//Flip-Movement
			
			if (MovementVector.x > 0) {
				CheckVariables1.transform.eulerAngles = Vector3.zero;//Resets The Objects Rotation
			} else {
				CheckVariables1.transform.eulerAngles = Vector3.up * 180;//Flips The Object
			}



		} else if (WhatRotationType == 1) {//Rotate-Movement
			
			_AngleToMove = Vector3.Angle ((Quaternion.Euler (0, RotateDirection.y, RotateDirection.z) * Vector3.right), MovementVector);

			if (Vector3.Cross ((Quaternion.Euler (0, RotateDirection.y, RotateDirection.z) * Vector3.right), MovementVector).z < 0) {//Checking Which Side Of The Rotation The Target Rotation Is
				_AngleToMove *= -1;
			}

			if (CheckVariables2.MyMovementTarget.transform.position.x - CheckVariables4.transform.position.x > 0) {
				RotateDirection.y = 0;
			} else {
				RotateDirection.y = 180;
			}

			if (RotateDirection.y == 0) {//When Y Are 180 Then The Object Is Rotated And The Rotation Is Mirrored
				RotateDirection.z += (_AngleToMove * Time.deltaTime) * 2 * 2;//Calculating AngleRotation
			} else {
				RotateDirection.z -= (_AngleToMove * Time.deltaTime) * 2 * 2;//Calculating AngleRotation
			}
			CheckVariables1.transform.eulerAngles = RotateDirection;



		} else if (WhatRotationType == 2) {//Flip-TargetObject

			if (CheckVariables2.MyMovementTarget.transform.position.x - CheckVariables4.transform.position.x > 0) {
				CheckVariables1.transform.eulerAngles = Vector3.zero;//Resets The Objects Rotation
			} else {
				CheckVariables1.transform.eulerAngles = Vector3.up * 180;//Flips The Object
			}



		} else if (WhatRotationType == 3) {//Rotate-TargetObject

			_AngleToMove = Vector3.Angle ((Quaternion.Euler (0, RotateDirection.y, RotateDirection.z) * Vector3.right), (CheckVariables2.MyMovementTarget.transform.position - CheckVariables4.transform.position));

			if (Vector3.Cross ((Quaternion.Euler (0, RotateDirection.y, RotateDirection.z) * Vector3.right), (CheckVariables2.MyMovementTarget.transform.position - CheckVariables4.transform.position)).z < 0) {//Checking Which Side Of The Rotation The Target Rotation Is
				_AngleToMove *= -1;
			}

			if (CheckVariables2.MyMovementTarget.transform.position.x - CheckVariables4.transform.position.x > 0) {
				RotateDirection.y = 0;
			} else {
				RotateDirection.y = 180;
			}

			if (RotateDirection.y == 0) {//When Y Are 180 Then The Object Is Rotated And The Rotation Is Mirrored
				RotateDirection.z += (_AngleToMove * Time.deltaTime) * 2 * 2;//Calculating AngleRotation
			} else {
				RotateDirection.z -= (_AngleToMove * Time.deltaTime) * 2 * 2;//Calculating AngleRotation
			}
			CheckVariables1.transform.eulerAngles = RotateDirection;



		} 

	}

}
