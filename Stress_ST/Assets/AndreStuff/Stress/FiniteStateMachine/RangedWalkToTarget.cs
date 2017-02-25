using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RangedWalkToTarget : DefaultState {

	#region Refrenceholders

	DefaultBehaviour TargetInfo;
	CreatureBehaviour MyInfo;

	CreatingObjectNodeMap PersonalNodeMap;
	AStarPathfinding_RoomPaths CreateThePath;

	Transform MyTransform;

	List<RoomConnectorCreating> NeighbourGroups;
	List<RoomConnectorCreating> TargetNeighbourGroups;

	List<RoomConnectorCreating> _ThePath;
	Nodes CehckingNOde;

	float[,] MyPreviousePosition;//each time i run the path search this is updated

	#endregion

	float _DistanceFromNode = 1f;//radius that desides if the player is on the node or not. (if 0.5 then if this object is within 0.5 from the center in any direction xy. _Nodeindex++)
	int _SearchAgainIndex = 1;//when this object reaches the node in the path, search again (if 1, when the object is at the second index in _TheNodePath search again)

	float[] _MovementSpeed;//enemy speed

	int _Nodeindex = 0;//index _TheNodePath 
	int _Counter = 0;
	float _TheDistance = 1000;
	int _DistanceGap = 0;
	int[] _PlaceHolder = new int[2];
	Vector2 _TargetVector = Vector2.zero;
	Vector2 _ObjectVector = Vector2.zero;

	Nodes[] _TheNodePath;
	int[] _Nodesindex;

	RoomConnectorCreating[] _TheRoomPath;
	int[] _Roomsindex;
	int _Roomindex = 0;

	Vector2 _MovementDirection = Vector2.zero;

	Rigidbody2D _MyRigidbody2D;

	Animator _CreatureAnimator;
	Vector3 _RotateCharacter = Vector3.zero;
	Transform GFX;

	Vector2 _GoalPosition = Vector2.zero;
	float[,] _IdHolder, _NextIdHolder;

	float a = 0;

	bool HaveSearched = false; 


	public RangedWalkToTarget(CreatingObjectNodeMap personalNodeMap, AStarPathfinding_RoomPaths createThePath, CreatureBehaviour myInfo, float theRange, float[] movementspeed, LayerMask lineOfSight) {//giving copies of info to this class
		Id = "WalkToTargetState";
		PersonalNodeMap = personalNodeMap;
		CreateThePath = createThePath;

		MyInfo = myInfo;
		MyTransform = MyInfo.WalkColliderPoint.transform;
	
		_TheNodePath = PersonalNodeMap.GetNodeList ();
		_Nodesindex = PersonalNodeMap.GetNodeindex ();

		_TheRoomPath = CreateThePath.GetListRef ();
		_Roomsindex = CreateThePath.GetListindexref ();

		_MyRigidbody2D = myInfo.GetComponent<Rigidbody2D> ();

		GFX = myInfo.transform.FindChild ("GFX");
		_CreatureAnimator = GFX.GetComponent<Animator> ();
		_LineOfSight = lineOfSight;

		_Range = theRange;
		_MovementSpeed = movementspeed;

		_DistanceFromNode = (1 / (float)myInfo.NodeSizess) / 3;
	}

	public override string EnterState() {//When it switches to this state this is the first thing thats being called

		if (MyInfo._GoAfter == null) {//having a failsafe here, so if i dont have a target, ill switch state to something that can search
			return ""; //TODO TODO "TargetSearch";
		}else{
			if (TargetInfo != MyInfo.GetTargetBehaviour ()) {//if this target isnt the same as what im after, change it
				TargetInfo = MyInfo.GetTargetBehaviour ();
			}
			MyPreviousePosition = PersonalNodeMap.GetCenterPos ();
		}
		_CreatureAnimator.SetFloat ("ChangeAnimation", 1);
		_ReturnState = "";
	
		HaveSearched = false;
		MyInfo.UpdateThePath = true;
		return "";
	}


	public override string ProcessState() {//this is called every frame

		_ReturnState = "";//denne er enten null eller den staten som den skal forandres til

		if (TargetInfo != null) {
			UpdatePaths ();
			GoToDestination ();
		}

		return _ReturnState;
	}

	public override void ExitState(){//when i want to switch to another state this is called before the enterstate of the next state

	}

	#region Makeing Path and going to nodes

	void UpdatePaths(){//the path search behaviour happens here, what to search when im here or there etc.
	
		if (MyInfo.UpdateThePath == true) {

			MyInfo.UpdateThePath = false;
			NeighbourGroups = MyInfo.NeighbourGroups;
			TargetNeighbourGroups = TargetInfo.NeighbourGroups;

			if (NeighbourGroups == TargetNeighbourGroups) {//if in the same room go straight to the target
				_Roomindex = _TheRoomPath.Length;

				PersonalNodeMap.SetTargetPos (TargetInfo.myPos);
				PersonalNodeMap.SetInfoAndStartSearch (true);
				_Nodeindex = _Nodesindex [0];

			} else {
				CreateThePath.SetEndRoom (TargetNeighbourGroups);  

				if (CreateThePath.CreatePath () == false) {//if eather of us dont have a room connected yet, go straight to the target
					PersonalNodeMap.SetTargetPos (TargetInfo.myPos);
					PersonalNodeMap.SetInfoAndStartSearch (true);
					_Nodeindex = _Nodesindex [0];
					HaveSearched = true;
					return;
				}

				_Roomindex = _Roomsindex [0];

				if ((_Roomindex) - _TheRoomPath.Length < 0) {//if true go to the pathconnector node closest to the target

					WhichNodeToGOTO (_TheRoomPath [_Roomindex].GetComponent<RoomConnectorCreating> ().GettheNodes ()); 
					PersonalNodeMap.SetInfoAndStartSearch (true);
					_Nodeindex = _Nodesindex [0];

				} else {
					PersonalNodeMap.SetTargetPos (TargetInfo.myPos);
					PersonalNodeMap.SetInfoAndStartSearch (true);
					_Nodeindex = _Nodesindex [0];
				}
			}
			HaveSearched = true;
		}
	}
		
	void GoToDestination() {//going through the pathlist and moves the objects

		if (HaveSearched == true) {//if this is false then the search failed or never happened or an error :D
				
			_GoalPosition.x = MyPreviousePosition [0, 0] + _TheNodePath [_Nodeindex].GetID () [0, 0];
			_GoalPosition.y = MyPreviousePosition [0, 1] + _TheNodePath [_Nodeindex].GetID () [0, 1];

			if (Vector2.Distance (_GoalPosition, (Vector2)MyTransform.position) <= _DistanceFromNode) {//if im inside the node im going to 
				_Nodeindex++;

				_MovementDirection.x = TargetInfo.myPos[0,0];
				_MovementDirection.y = TargetInfo.myPos[0,1];

				if (Vector2.Distance ((Vector2)MyTransform.position, _MovementDirection) <= _Range) {//checking if im withing range of the target 
					if (Physics2D.Linecast ((Vector2)MyTransform.position, _MovementDirection, _LineOfSight).transform == null) {//if im in range do a raycast and see if there is an obsacle in the way, if true then i didnt hit anything
						_ReturnState = "AttackState";
						return;
					} 
				}

				#region Calculating Direction/speed

				if (_Nodeindex < _TheNodePath.Length) {
					_IdHolder = _TheNodePath [_Nodeindex].GetID ();

					if (_Nodesindex [0] < _Nodeindex) {
						_NextIdHolder = _TheNodePath [_Nodeindex - 1].GetID ();

						_MovementDirection.x = _IdHolder [0, 0] - _NextIdHolder [0, 0];
						_MovementDirection.y = _IdHolder [0, 1] - _NextIdHolder [0, 1];
					} else {
						_IdHolder = _TheNodePath [_Nodeindex].GetID ();

						_MovementDirection.x = _IdHolder [0, 0];
						_MovementDirection.y = _IdHolder [0, 1];
					}
					_MyRigidbody2D.velocity = (_MovementDirection * _MovementSpeed [0]);//speed through velocity. currently it can eather go straight or 45degrees.   currently [1,0] || [0,1] || [1,1]
				}

				if (GFX.position.x < _GoalPosition.x - 0.075f) {
					if (GFX.localScale.x > 0) {
						_CreatureAnimator.SetFloat ("ChangeAnimation", 1);
						_RotateCharacter.x = GFX.localScale.x * -1;
						_RotateCharacter.y = GFX.localScale.y;
						_RotateCharacter.z = 0;
						GFX.localScale = _RotateCharacter;
					}
				} else if (GFX.position.x > _GoalPosition.x + 0.075f) {
					if (GFX.localScale.x < 0) {
						_CreatureAnimator.SetFloat ("ChangeAnimation", 1);
						_RotateCharacter.x = GFX.localScale.x * -1;
						_RotateCharacter.y = GFX.localScale.y;
						_RotateCharacter.z = 0;
						GFX.localScale = _RotateCharacter;
					}
				}

				#endregion

				if (_Nodeindex > _SearchAgainIndex) {
					HaveSearched = false;
					MyInfo.UpdateThePath = true;
				}


				if (_Nodeindex >= _TheNodePath.Length) {
					HaveSearched = false;
					MyInfo.UpdateThePath = true;
				}

			} else {
				#region Calculating Direction/speed

				if (_Nodeindex < _TheNodePath.Length) {
					_IdHolder = _TheNodePath [_Nodeindex].GetID ();

					if (_Nodesindex [0] < _Nodeindex) {
						_NextIdHolder = _TheNodePath [_Nodeindex - 1].GetID ();

						_MovementDirection.x = _IdHolder [0, 0] - _NextIdHolder [0, 0];
						_MovementDirection.y = _IdHolder [0, 1] - _NextIdHolder [0, 1];
					} else {
						_IdHolder = _TheNodePath [_Nodeindex].GetID ();

						_MovementDirection.x = _IdHolder [0, 0];
						_MovementDirection.y = _IdHolder [0, 1];
					}
					_MyRigidbody2D.velocity = (_MovementDirection * _MovementSpeed [0]);//speed through velocity. currently it can eather go straight or 45degrees.   currently [1,0] || [0,1] || [1,1]
				}

				if (GFX.position.x < _GoalPosition.x - 0.075f) {
					if (GFX.localScale.x > 0) {
						_CreatureAnimator.SetFloat ("ChangeAnimation", 1);
						_RotateCharacter.x = GFX.localScale.x * -1;
						_RotateCharacter.y = GFX.localScale.y;
						_RotateCharacter.z = 0;
						GFX.localScale = _RotateCharacter;
					}
				} else if (GFX.position.x > _GoalPosition.x + 0.075f) {
					if (GFX.localScale.x < 0) {
						_CreatureAnimator.SetFloat ("ChangeAnimation", 1);
						_RotateCharacter.x = GFX.localScale.x * -1;
						_RotateCharacter.y = GFX.localScale.y;
						_RotateCharacter.z = 0;
						GFX.localScale = _RotateCharacter;
					}
				}

				#endregion
			}

		} else {
			Debug.Log ("Could Not Find What I Needed");
			_MyRigidbody2D.velocity = Vector2.zero;
		}
	}

	void WhichNodeToGOTO(List<Nodes> ListOfNodes){//calculating which node to go to in the roomconnector 
		_TheDistance = 1000;
		_Counter = 0;
		_TargetVector.x = TargetInfo.myPos [0, 0];
		_TargetVector.y = TargetInfo.myPos [0, 1];

		for (int i = 0; i < ListOfNodes.Count; i++) {
			_ObjectVector.x = ListOfNodes [i].GetID () [0, 0];
			_ObjectVector.y = ListOfNodes [i].GetID () [0, 1];

			_Counter++;
			a = Vector2.Distance (_TargetVector, _ObjectVector);

			if (a < _TheDistance) {
				_TheDistance = a;
				_PlaceHolder [0] = _Counter;
			}
		}

		_TargetVector.x = MyTransform.position.x;
		_TargetVector.y = MyTransform.position.y;

		_Counter = 0;
		_TheDistance = 1000;

		for (int i = 0; i < ListOfNodes.Count; i++) {
			_ObjectVector.x = ListOfNodes [i].GetID () [0, 0];
			_ObjectVector.y = ListOfNodes [i].GetID () [0, 1];

			_Counter++;
			a = Vector2.Distance (_TargetVector, _ObjectVector);

			if (a < _TheDistance) {
				_TheDistance = a;
				_PlaceHolder [1] = _Counter;
			}
		}

		if (_PlaceHolder [0] - 1 >= _PlaceHolder [1] - 1) {//if target node index is bigger then this, if the distence to the closest node is big enough then it will switch to the the targets closest node, else the index is 

			_DistanceGap = Mathf.FloorToInt ((_PlaceHolder [1] - 1) + _TheDistance);
			if (_DistanceGap > _PlaceHolder [0] - 1) {
				_DistanceGap = _PlaceHolder [0] - 1;
			}

			CehckingNOde = ListOfNodes [_DistanceGap];
		} else {

			_DistanceGap = Mathf.FloorToInt ((_PlaceHolder [1] - 1) - _TheDistance);
			if (_DistanceGap < _PlaceHolder [0] - 1) {
				_DistanceGap = _PlaceHolder [0] - 1;
			}

			CehckingNOde = ListOfNodes [_DistanceGap];
		}
		PersonalNodeMap.SetTargetPos (CehckingNOde.GetID ());
	}
	#endregion


}
