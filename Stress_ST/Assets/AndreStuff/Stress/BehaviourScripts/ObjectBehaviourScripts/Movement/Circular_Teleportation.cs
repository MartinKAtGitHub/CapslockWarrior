using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Designed As A CircularTeleport
public class Circular_Teleportation : The_Default_Movement_Behaviour {

	float[] _TheTime;
	bool _Attacking = false;

	float _TimeStarted = 0;
	float _ValueWhenLastUpdated = 0;

	public int AngleMinimum = 50;
	int currentTeleport = 0;
	Vector3 Direction = Vector3.zero;

	int prevtelp = 0;
	int a = 0;
	public float Distance = 2.5f;

	public override void SetMethod (The_Object_Behaviour myTransform){
		base.SetMethod (myTransform);

		_MyObject = myTransform;
		_MyTransform = myTransform._TheObject;

		_TargetTransform = myTransform._TheTarget;

		_TheTime = _MyObject.GetTheTime ();

	}


	public override void OnEnter (){
		base.OnEnter ();//Rotation

		if (TheResetState == ResetState.ResetOnEnter) {
			Reset ();
		} else {
			_TimeStarted = _TheTime [0];
		}

		MoveDirection [0] = Vector3.zero;

		prevtelp = currentTeleport;

		while (true) {
			currentTeleport = Random.Range (0, 360);

			if (prevtelp < currentTeleport) {
				a = currentTeleport - 360 - prevtelp;
			} else {
				a = prevtelp - 360 - currentTeleport;
			}

			if (a > -50 || a < -310) {
			} else {
				Direction.x = Mathf.Cos (Mathf.Deg2Rad * (float)currentTeleport);
				Direction.y = Mathf.Sin (Mathf.Deg2Rad * (float)currentTeleport);
				break;
			}

		}

		_MyTransform.transform.position = _TargetTransform.transform.position + (Direction.normalized * Distance);
		MovementRotations (); 
	

	}



	public override void BehaviourUpdate (){
		MovementRotations (); 
	}

	public override void Reset (){
		_TimeStarted = _TheTime [0];
		_ValueWhenLastUpdated = 0;

	}

	public override bool GetBool (int index){

		if (index == 5) {//Attack Uses This To Do The Movement. Index 5 Is Reserved For Attack Call To Movement
			MovementRotations ();
			return false;
			_Attacking = true;
			BehaviourUpdate ();
			_Attacking = false;

			return true;
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

	#region Makeing Path and going to nodes

/*	void PathCheck(){//Checking If Something Needs Updating And Setting NodePath
		if (_MyObject.GotPushed == true) {

			if (_TargetNeighbourGroups != _MyObject._TheTarget.NeighbourGroups) {//If Target Changed "Room" Do A Room Path Search
				_TargetNeighbourGroups = _MyObject._TheTarget.NeighbourGroups;
				_ObjectNeighbourGroups = _MyObject._TheObject.NeighbourGroups;
				_MyObject._CreateThePath.SetEndRoom (_TargetNeighbourGroups); 
				_MyObject._CreateThePath.CreatePath ();
				_Roomindex = _Roomsindex [0];//Index Refrence For Where I'm Starting From In The List
				_PreviourRoom = _CurrentRoom;
				_CurrentRoom = _TheRoomPath [_Roomindex];

				NodePathCheck ();
			} else if (_ObjectNeighbourGroups != _MyObject._TheObject.NeighbourGroups) {//If I Changed "Room" Do A Room Path Search
				_ObjectNeighbourGroups = _MyObject._TheObject.NeighbourGroups;
				_MyObject._CreateThePath.CreatePath ();
				_Roomindex = _Roomsindex [0];//Index Refrence For Where I'm Starting From In The List
				_PreviourRoom = _CurrentRoom;
				_CurrentRoom = _TheRoomPath [_Roomindex];

				NodePathCheck ();
			} else {
				NodePathCheck ();
			}

		} else {

			if (_TargetNeighbourGroups != _MyObject._TheTarget.NeighbourGroups) {//If Target Changed "Room" Do A Room Path Search
				_TargetNeighbourGroups = _MyObject._TheTarget.NeighbourGroups;
				_ObjectNeighbourGroups = _MyObject._TheObject.NeighbourGroups;
				_MyObject._CreateThePath.SetEndRoom (_TargetNeighbourGroups); 
				_MyObject._CreateThePath.CreatePath ();
				_Roomindex = _Roomsindex [0];//Index Refrence For Where I'm Starting From In The List

				NodePathCheck ();
			} else if (_ObjectNeighbourGroups != _MyObject._TheObject.NeighbourGroups) {//If I Changed "Room" Do A Room Path Search
				_ObjectNeighbourGroups = _MyObject._TheObject.NeighbourGroups;
				_MyObject._CreateThePath.CreatePath ();
				_Roomindex = _Roomsindex [0];//Index Refrence For Where I'm Starting From In The List

				NodePathCheck ();
			}

			if (_Nodeindex >= _Nodesindex [0] + ReSearch) {//If The Object Have Walked For X Amount Of Nodes Start The Node A* Again
				if(_Nodeindex < _TheNodePath.Length - 1)//No Need To Check If Im At The End
					NodePathCheck ();
			}
		}

	}*/

/*	void NodePathCheck(){
		if (_Roomindex < _TheRoomPath.Length - 1) {//If False Then The Object Is In The Same "Room" As The Target
			RoomNodeToGoTo ();//Setting The Coordinate For The "RoomConnector" And Selects A Node That Can Be Walked To  
			_MyObject._PersonalNodeMap.SetInfoAndStartSearch ();//Node Search
			_Nodeindex = _Nodesindex [0];//Index Refrence For Where I'm Starting From In The List
		} else {
			_MyObject._PersonalNodeMap.SetTargetPos (_MyObject._TheTarget.MyPos);//Setting The Target As The Next Point To Go To
			_MyObject._PersonalNodeMap.SetInfoAndStartSearch ();
			_Nodeindex = _Nodesindex [0];//Index Refrence For Where I'm Starting From In The List
		}

		_ObjectStartPosition.x = _MyObject._TheObject.MyPos [0, 0];
		_ObjectStartPosition.y = _MyObject._TheObject.MyPos [0, 1];
		//	_IdHolder = _TheNodePath [_Nodeindex].GetID ();
	}*/

/*	void CalculateMovementDirection() {//Calculating Movement Direction

		if (_Nodeindex < _TheNodePath.Length && _ObjectStartPosition.x + _TheNodePath [_Nodeindex].GetID () [0, 0] == _MyObject._TheObject.MyPos [0, 0] && _ObjectStartPosition.y + _TheNodePath [_Nodeindex].GetID () [0, 1] == _MyObject._TheObject.MyPos [0, 1]) {//I'm At The Same Position As The Node I'm Going To

			_Nodeindex++;
			_ValueWhenLastUpdated++;*/

			/*if (_Nodeindex < _TheNodePath.Length) {
				_PreviousNode = _IdHolder;
				_IdHolder = _TheNodePath [_Nodeindex].GetID ();

				_TargetDirection [0].x = _IdHolder [0, 0] - _PreviousNode [0, 0] ;
				_TargetDirection [0].y = _IdHolder [0, 1] - _PreviousNode [0, 1];

				MoveDirection [0] = _CurrentDirection[0].normalized * Time.deltaTime * _MyObject.MovementSpeed;
			}*/ 

		/*} else {//If The Object Collides With A Collider Which Gives The Object A Wrong Movement Direction. Then By Doing This The Object Refreshes Itself By Adjusting Itself (Isnt Needed, Buf Doesnt Hurt)
			_TargetDirection [0].x = (_ObjectStartPosition.x + _IdHolder [0, 0]) - _MyObject._TheObject.MyPos [0, 0];
			_TargetDirection [0].y = (_ObjectStartPosition.y + _IdHolder [0, 1]) - _MyObject._TheObject.MyPos [0, 1];
			MoveDirection [0] = _CurrentDirection[0].normalized * Time.deltaTime * _MyObject.MovementSpeed;
		}
	}*/

	//TODO Improvements (Search Logic)
	//Check More Then One Room Ahead To Make A "Cleaner" Curve, Instead Of Going Straight To Target (maybe draw line and the go to where the point crosses the roomconnector)
	//Check Rotations, If Same Choose Closest Node To Target, Else Take Closest Node To Object
	//Use Pathfinding Node ID To Check The Lowest PathID Cost.
/*	void RoomNodeToGoTo(){//calculating which node to go to in the roomconnector 
		_ListOfNodes = _CurrentRoom.GettheNodes ();
		_Closest = _ListOfNodes [0];

		if (_CurrentRoom.transform.eulerAngles.z == _PreviourRoom.transform.eulerAngles.z) {
			_TargetVector.x = _MyObject._TheTarget.MyPos [0, 0];
			_TargetVector.y = _MyObject._TheTarget.MyPos [0, 1];
		} else {
			_TargetVector.x = _MyObject._TheObject.MyPos [0, 0];
			_TargetVector.y = _MyObject._TheObject.MyPos [0, 1];	
		}

		_TheDistance = 100;//Checking Value

		for (int i = 0; i < _ListOfNodes.Count; i++) {

			_ObjectVector.x = _ListOfNodes [i].GetID () [0, 0];
			_ObjectVector.y = _ListOfNodes [i].GetID () [0, 1];

			_DistanceHolder = Vector2.Distance (_TargetVector, _ObjectVector);

			if (_DistanceHolder < _TheDistance) {
				if (_ListOfNodes [i].GetCollision () != 99) {//Wall TODO If Fully Barricaded Find Another Or Destroy
					_TheDistance = _DistanceHolder;
					_Closest = _ListOfNodes [i];
				}
			}
		}

		_MyObject._PersonalNodeMap.SetTargetPos (_Closest.GetID ());
	}
*/
	#endregion

}
/*
public bool RandomTeleport = false;
int currentTeleport = 0;
Vector3 Direction = Vector3.zero;

public float TeleportDelay = 1;
public float thetime = 0;

int prevtelp = 0;
int a = 0;

public override void BehaviourUpdate (){

	if (RandomTeleport == true) {

		if (thetime + TeleportDelay < _TheTime [0]) {
			thetime = _TheTime [0];

			prevtelp = currentTeleport;

			while (true) {

				currentTeleport = Random.Range (0, 360);

				if (prevtelp < currentTeleport) {
					a = currentTeleport - 360 - prevtelp;
				} else {
					a = prevtelp - 360 - currentTeleport;
				}

				if (a > -50 || a < -310) {
				} else {
					Direction.x = Mathf.Cos (Mathf.Deg2Rad * (float)currentTeleport);
					Direction.y = Mathf.Sin (Mathf.Deg2Rad * (float)currentTeleport);
					break;
				}

			}


			_MyTransform.transform.position = _TargetTransform.transform.position + (Direction.normalized * 2.5f);
		} 
	}
*/