using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Straight_To_Target_With_Pathfinding : The_Default_Movement_Behaviour {

	[Space(10)]
	[Header("Movement Values")]
	[Tooltip("This Is Multiplyed With Movement Speed And Then Added To The MovementVector")]
	public float MovementMultiplyer = 1;
	[Tooltip("Time == true, Distance == false")]
	public bool TimeOrDistance = false;
	[Tooltip("Value To Check, If TimeOrDistance == True Then CheckingValue Is Time, Distance If False")]//Might Make A Enum, But Just Came Up With These Two
	public float CheckingValue = 10;

	float[] _TheTime;
	int[] _AnimatorVariables;
	bool _Attacking = false;

	float TimeStarted = 0;
	float _ValueWhenLastUpdated = 0;

	#region PathfindingVariables

	Nodes[] _TheNodePath;//The Path
	int[] _Nodesindex;//Index Saver
	int _Nodeindex = 0;//Iteration Index

	RoomConnectorCreating[] _TheRoomPath;//The Path
	int[] _Roomsindex;//Index Saver
	int _Roomindex = 0;//Iteration Index

	int ReSearch = 1;//How Many Nodes That Can Be Walked Through Befor A New Search Happen

	float[,] _IdHolder;//Holds The Node To Walk To
	float[,] _PreviousNode;//Holds The Node I'm On
	float _TheDistance = 1000;
	float _DistanceHolder = 0;

	RoomConnectorCreating _PreviourRoom;
	RoomConnectorCreating _CurrentRoom;

	List<Nodes> _ListOfNodes;
	List<RoomConnectorCreating> _ObjectNeighbourGroups;
	List<RoomConnectorCreating> _TargetNeighbourGroups;

	Nodes _Closest;

	Vector2 _ObjectStartPosition = Vector2.zero;
	Vector2 _TargetVector = Vector2.zero;
	Vector2 _ObjectVector = Vector2.zero;

	bool[] _GotPushed;
	DefaultBehaviour test;

	[HideInInspector]public int NodesLeft = 0;

	#endregion

	public override void SetMethod (The_Object_Behaviour myTransform){
		base.SetMethod (myTransform);

		_MyObject = myTransform;
		_MyTransform = myTransform._TheObject;

		_TargetTransform = myTransform._TheObject._TheTarget;
		
		_TheTime = _MyObject.GetTheTime ();
		_AnimatorVariables = _MyObject.AnimatorVariables;

		_GotPushed = myTransform.GotPushed;

		_TheNodePath = _MyObject._PersonalNodeMap.GetNodeList ();
		_Nodesindex =  _MyObject._PersonalNodeMap.GetNodeindex ();

		_TheRoomPath =  _MyObject._CreateThePath.GetListRef ();
		_Roomsindex = _MyObject._CreateThePath.GetListindexref ();
	}


	public override void OnEnter (){
		base.OnEnter ();//Rotation

		if (TheResetState == ResetState.ResetOnEnter) {
			Reset ();
		} else {
			if (TimeOrDistance == true) {
				TimeStarted = _TheTime [0];
			}
		}

		_MyObject.MyAnimator.SetFloat (_AnimatorVariables [1], AnimatorStageValueOnEnter);

	}

	public override void BehaviourUpdate (){
		PathCheck ();
		CalculateMovementDirection ();

		MovementRotations ();

		NodesLeft = _Nodeindex - _Nodesindex [0];
			
		if (TimeOrDistance == false) {
			
			if (_Attacking == false) {
				if (_ValueWhenLastUpdated >= CheckingValue) {
					if (TheResetState == ResetState.ResetWhenComplete) {
						Reset ();
					}
					_MyObject.SetMovementBehaviour (WhenCompleteChangeToBehaviourIndex);
				}
			}
		} else {
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
		if (index == 4) {
			return NodesLeft;
		}if (index == 2) {//5 == Coming From Attack
			return WhenCompleteChangeToBehaviourIndex;
		}else{
			return 0;
		}
	}

	#region Makeing Path and going to nodes

	void PathCheck(){//Checking If Something Needs Updating And Setting NodePath

		if (_GotPushed[0] == true) {

			if (_TargetNeighbourGroups != _MyObject._TheObject._TheTarget.NeighbourGroups) {//If Target Changed "Room" Do A Room Path Search
				_TargetNeighbourGroups = _MyObject._TheObject._TheTarget.NeighbourGroups;
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

			if (_TargetNeighbourGroups != _MyObject._TheObject._TheTarget.NeighbourGroups) {//If Target Changed "Room" Do A Room Path Search
				_TargetNeighbourGroups = _MyObject._TheObject._TheTarget.NeighbourGroups;
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
			//	if(_Nodeindex < _TheNodePath.Length - 1)//No Need To Check If Im At The End
					NodePathCheck ();
			}
		}
	
	}

	void NodePathCheck(){
		if (_Roomindex < _TheRoomPath.Length - 1) {//If False Then The Object Is In The Same "Room" As The Target
			RoomNodeToGoTo ();//Setting The Coordinate For The "RoomConnector" And Selects A Node That Can Be Walked To  
			_MyObject._PersonalNodeMap.SetInfoAndStartSearch ();//Node Search
			_Nodeindex = _Nodesindex [0];//Index Refrence For Where I'm Starting From In The List
		} else {
			_MyObject._PersonalNodeMap.SetTargetPos (_MyObject._TheObject._TheTarget.MyPos);//Setting The Target As The Next Point To Go To
			_MyObject._PersonalNodeMap.SetInfoAndStartSearch ();
			_Nodeindex = _Nodesindex [0];//Index Refrence For Where I'm Starting From In The List
		}

		_ObjectStartPosition.x = _MyObject._TheObject.MyPos [0, 0];
		_ObjectStartPosition.y = _MyObject._TheObject.MyPos [0, 1];
		_IdHolder = _TheNodePath [_Nodeindex].GetID ();
	}

	void CalculateMovementDirection() {//Calculating Movement Direction
		
		if (_Nodeindex < _TheNodePath.Length && _ObjectStartPosition.x + _TheNodePath [_Nodeindex].GetID () [0, 0] == _MyObject._TheObject.MyPos [0, 0] && _ObjectStartPosition.y + _TheNodePath [_Nodeindex].GetID () [0, 1] == _MyObject._TheObject.MyPos [0, 1]) {//I'm At The Same Position As The Node I'm Going To
			
			_Nodeindex++;
			_ValueWhenLastUpdated++;

			if (_Nodeindex < _TheNodePath.Length) {
				_PreviousNode = _IdHolder;
				_IdHolder = _TheNodePath [_Nodeindex].GetID ();

				_TargetDirection [0].x = _IdHolder [0, 0] - _PreviousNode [0, 0];
				_TargetDirection [0].y = _IdHolder [0, 1] - _PreviousNode [0, 1];

				MoveDirection [0] = _CurrentDirection[0].normalized * MovementMultiplyer * Time.deltaTime * MovementSpeed[0];
			} 
		
		} else {//If The Object Collides With A Collider Which Gives The Object A Wrong Movement Direction. Then By Doing This The Object Refreshes Itself By Adjusting Itself (Isnt Needed, Buf Doesnt Hurt)
			_TargetDirection [0].x = (_ObjectStartPosition.x + _IdHolder [0, 0]) - _MyObject._TheObject.MyPos [0, 0];
			_TargetDirection [0].y = (_ObjectStartPosition.y + _IdHolder [0, 1]) - _MyObject._TheObject.MyPos [0, 1];
			MoveDirection [0] = _CurrentDirection[0].normalized * MovementMultiplyer * Time.deltaTime * MovementSpeed[0];
		}
	}

	//TODO Improvements (Search Logic)
	//Check More Then One Room Ahead To Make A "Cleaner" Curve, Instead Of Going Straight To Target (maybe draw line and the go to where the point crosses the roomconnector)
	//Check Rotations, If Same Choose Closest Node To Target, Else Take Closest Node To Object
	//Use Pathfinding Node ID To Check The Lowest PathID Cost.
	void RoomNodeToGoTo(){//calculating which node to go to in the roomconnector 
		_ListOfNodes = _CurrentRoom.GettheNodes ();
		_Closest = _ListOfNodes [0];

		if (_CurrentRoom.transform.eulerAngles.z == _PreviourRoom.transform.eulerAngles.z) {
			_TargetVector.x = _MyObject._TheObject._TheTarget.MyPos [0, 0];
			_TargetVector.y = _MyObject._TheObject._TheTarget.MyPos [0, 1];
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
	#endregion

}