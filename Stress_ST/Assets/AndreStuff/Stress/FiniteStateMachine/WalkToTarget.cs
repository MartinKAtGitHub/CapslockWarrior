using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WalkToTarget : DefaultState {

	#region Refrenceholders

	DefaultBehaviour TargetInfo;
	DefaultBehaviour MyInfo;

	CreatingObjectNodeMap PersonalNodeMap;
	AStarPathfinding_RoomPaths CreateThePath;

	Transform MyTransform;

	List<RoomConnectorCreating> NeighbourGroups;
	List<RoomConnectorCreating> TargetNeighbourGroups;

	List<RoomConnectorCreating> _ThePath;
	Nodes CehckingNOde;

	float[,] MyPreviousePosition;//each time i run the path search this is updated

	#endregion

	float ChargeTargetDistance = 5.5f;//the distance when this object starts charging towards the target
	float DistanceFromNode = 0.5f;//radius that desides if the player is on the node or not. (if 0.5 then if this object is within 0.5 from the center in any direction xy. _Nodeindex++)
	int SearchAgainIndex = 1;//when this object reaches the node in the path, search again (if 1, when the object is at the second index in _TheNodePath search again)

	float MovementSpeed = 3.5f;//enemy speed
	string ReturnState = "";//used for the statemachine to see which state to switch to

	int _Nodeindex = 0;//index _TheNodePath 
	int Counter = 0;
	float TheDistance = 1000;
	int DistanceGap = 0;
	int[] PlaceHolder = new int[2];
	Vector2 TargetVector = Vector2.zero;
	Vector2 ObjectVector = Vector2.zero;

	Nodes[] _TheNodePath;
	int[] nodesindex;

	RoomConnectorCreating[] _TheRoomPath;
	int[] roomsindex;
	int _Roomindex = 0;

	bool once = false;
	Vector2 movemen = Vector2.zero;

	Rigidbody2D myrigid;

	public WalkToTarget(CreatingObjectNodeMap personalNodeMap, AStarPathfinding_RoomPaths createThePath, CreatureOneBehaviour myInfo) {//giving copies of info to this class
		Id = "WalkToTargetState";
		PersonalNodeMap = personalNodeMap;
		CreateThePath = createThePath;

		MyInfo = myInfo;
		MyTransform = MyInfo.transform;
		_TheNodePath = PersonalNodeMap.GetNodeList ();
		nodesindex = PersonalNodeMap.GetNodeindex ();

		_TheRoomPath = CreateThePath.GetListRef ();
		roomsindex = CreateThePath.GetListindexref ();

		myrigid = myInfo.GetComponent<Rigidbody2D> ();
		//TODO SET MOVEMENT SPEED
	}

	public override string EnterState() {//When it switches to this state this is the first thing thats being called
		once = false;

		if (MyInfo.GetTraget () == null) {//having a failsafe here, so if i dont have a target, ill switch state to something that can search
			return ""; //TODO TODO "TargetSearch";
		}else{
			if (TargetInfo != MyInfo.GetTargetBehaviour ()) {//if this target isnt the same as what im after, change it
				TargetInfo = MyInfo.GetTargetBehaviour ();
			}
			MyPreviousePosition = PersonalNodeMap.GetCenterPos ();
		}

		return "";
	}


	public override string ProcessState() {//this is called every frame
		if (TargetInfo != null) {
			UpdatePaths ();
			GoToDestination ();
		}

		ReturnState = "";//denne er enten null eller den staten som den skal forandres til

		if (once == true) {
			ReturnState = "AttackState";
		}
		return ReturnState;
	}

	public override void ExitState(){//when i want to switch to another state this is called before the enterstate of the next state

	}

	#region Makeing Path and going to nodes

	void UpdatePaths(){//the path search behaviour happens here, what to search when im here or there etc.
		if (MyInfo.UpdateThePath == true) {//this is true if something have entered the boxcollider
			MyInfo.UpdateThePath = false;

			PersonalNodeMap.UpdateNodeMap ();

			if (NeighbourGroups != MyInfo.NeighbourGroups || TargetNeighbourGroups != TargetInfo.NeighbourGroups) {
				NeighbourGroups = MyInfo.NeighbourGroups;
				TargetNeighbourGroups = TargetInfo.NeighbourGroups;

				if (NeighbourGroups == TargetNeighbourGroups) {
					_Roomindex = 0;
				} else {
					CreateThePath.SetEndRoom (TargetNeighbourGroups);  
					CreateThePath.CreatePath (); 
					_Roomindex = roomsindex [0];
				}
			}
	
			if (((MyTransform.position.x - TargetInfo.GetMyPosition () [0, 0] <= ChargeTargetDistance) && (MyTransform.position.x - TargetInfo.GetMyPosition () [0, 0] >= -ChargeTargetDistance)) &&
			     ((MyTransform.position.y - TargetInfo.GetMyPosition () [0, 1] <= ChargeTargetDistance) && (MyTransform.position.y - TargetInfo.GetMyPosition () [0, 1] >= -ChargeTargetDistance))) {//instead of using vector2.distance dont know it this is cheaper or not. 

				PersonalNodeMap.SetTargetPos (TargetInfo.GetMyPosition ());
				PersonalNodeMap.SetInfoAndStartSearch (true);
				_Nodeindex = nodesindex [0];

			} else if (NeighbourGroups.Count == 1) {
				if (_TheRoomPath.Length - _Roomindex <= 1) {
					PersonalNodeMap.SetTargetPos (TargetInfo.GetMyPosition ());
				
					PersonalNodeMap.SetInfoAndStartSearch (true);
					_Nodeindex = nodesindex [0];

				} else {
					WhichNodeToGOTO (_TheRoomPath [_Roomindex + 1].GetComponent<RoomConnectorCreating> ().GettheNodes ()); 
					PersonalNodeMap.SetInfoAndStartSearch (true);
					_Nodeindex = nodesindex [0];

				}
			} else {
				if (_TheRoomPath.Length - _Roomindex == 0) {
					PersonalNodeMap.SetTargetPos (TargetInfo.GetMyPosition ());
					PersonalNodeMap.SetInfoAndStartSearch (true);
					_Nodeindex = nodesindex [0];

				} else {

					WhichNodeToGOTO (_TheRoomPath [_Roomindex].GetComponent<RoomConnectorCreating> ().GettheNodes ());
					PersonalNodeMap.SetInfoAndStartSearch (true);
					_Nodeindex = nodesindex [0];

				}
			}
		} else {//if updatethepath havent triggered then this will run, and check if im close to the target. if not continue the given path
			if (((MyTransform.position.x - TargetInfo.GetMyPosition () [0, 0] <= ChargeTargetDistance) && (MyTransform.position.x - TargetInfo.GetMyPosition () [0, 0] >= -ChargeTargetDistance)) &&
			     ((MyTransform.position.y - TargetInfo.GetMyPosition () [0, 1] <= ChargeTargetDistance) && (MyTransform.position.y - TargetInfo.GetMyPosition () [0, 1] >= -ChargeTargetDistance))) {//instead of using vector2.distance dont know it this is cheaper or not. 

				PersonalNodeMap.SetTargetPos (TargetInfo.GetMyPosition ());
				 PersonalNodeMap.SetInfoAndStartSearch (false);
				_Nodeindex = nodesindex [0];
			}
		}

	}

	void GoToDestination() {//going through the pathlist and moves the objects

		if (((MyTransform.position.x - TargetInfo.GetMyPosition () [0, 0] <= 1) && (MyTransform.position.x - TargetInfo.GetMyPosition () [0, 0] >= -1)) &&
		    ((MyTransform.position.y - TargetInfo.GetMyPosition () [0, 1] <= 1) && (MyTransform.position.y - TargetInfo.GetMyPosition () [0, 1] >= -1))) {
			once = true;
		}
		if (_TheNodePath != null) {
			//	Debug.Log ();
			if (_Nodeindex < _TheNodePath.Length && _TheNodePath[_Nodeindex] != null) {

			if (nodesindex [0] < _Nodeindex) {
					movemen.x = _TheNodePath [_Nodeindex].GetID () [0, 0] - _TheNodePath [_Nodeindex - 1].GetID () [0, 0];
					movemen.y = _TheNodePath [_Nodeindex].GetID () [0, 1] - _TheNodePath [_Nodeindex - 1].GetID () [0, 1];
				}else{
					movemen.x = _TheNodePath [_Nodeindex].GetID () [0, 0];
					movemen.y = _TheNodePath [_Nodeindex].GetID () [0, 1];
				}

				//movement with velocity and position =
				myrigid.velocity = (movemen * MovementSpeed);
				//	MyTransform.position = Vector3.MoveTowards (MyTransform.position, new Vector3 ((MyPreviousePosition [0, 0] + _TheNodePath [_Nodeindex].GetID () [0, 0]), (MyPreviousePosition [0, 1] + _TheNodePath [_Nodeindex].GetID () [0, 1]), MyTransform.position.z), Time.smoothDeltaTime * MovementSpeed);
	
				if ((((MyPreviousePosition [0, 0] + _TheNodePath [_Nodeindex].GetID () [0, 0]) - DistanceFromNode) < MyTransform.position.x) && (((MyPreviousePosition [0, 0] + _TheNodePath [_Nodeindex].GetID () [0, 0]) + DistanceFromNode) > MyTransform.position.x) && (((MyPreviousePosition [0, 1] + _TheNodePath [_Nodeindex].GetID () [0, 1]) - DistanceFromNode) < MyTransform.position.y) && (((MyPreviousePosition [0, 1] + _TheNodePath [_Nodeindex].GetID () [0, 1]) + DistanceFromNode) > MyTransform.position.y)) {
					_Nodeindex += 1;
					if (_Nodeindex >= _TheNodePath.Length) {//every time nodeindex == 1. so if im at the first node search again
						MyInfo.UpdateThePath = true;
						return;
					}
				}

			}
		}
	}


	float a = 0;
	void WhichNodeToGOTO(List<Nodes> ListOfNodes){//calculating which node to go to in the roomconnector 
		TheDistance = 1000;
		Counter = 0;
		TargetVector.x = TargetInfo.GetMyPosition () [0, 0];
		TargetVector.y = TargetInfo.GetMyPosition () [0, 1];

		for (int i = 0; i < ListOfNodes.Count; i++) {
			ObjectVector.x = ListOfNodes [i].GetID () [0, 0];
			ObjectVector.y = ListOfNodes [i].GetID () [0, 1];
		
			Counter++;
			a = Vector2.Distance (TargetVector, ObjectVector);

			if (a < TheDistance) {
				TheDistance = a;
				PlaceHolder [0] = Counter;
			}
		}

		TargetVector.x = MyTransform.position.x;
		TargetVector.y = MyTransform.position.y;

		Counter = 0;
		TheDistance = 1000;

		for (int i = 0; i < ListOfNodes.Count; i++) {
			ObjectVector.x = ListOfNodes [i].GetID () [0, 0];
			ObjectVector.y = ListOfNodes [i].GetID () [0, 1];

			Counter++;
			a = Vector2.Distance (TargetVector, ObjectVector);

			if (a < TheDistance) {
				TheDistance = a;
				PlaceHolder [1] = Counter;
			}
		}

		if (PlaceHolder [0] - 1 >= PlaceHolder [1] - 1) {//if target node index is bigger then this, if the distence to the closest node is big enough then it will switch to the the targets closest node, else the index is 

			DistanceGap = Mathf.FloorToInt ((PlaceHolder [1] - 1) + TheDistance);
			if (DistanceGap > PlaceHolder [0] - 1) {
				DistanceGap = PlaceHolder [0] - 1;
			}

			CehckingNOde = ListOfNodes [DistanceGap];
		} else {

			DistanceGap = Mathf.FloorToInt ((PlaceHolder [1] - 1) - TheDistance);
			if (DistanceGap < PlaceHolder [0] - 1) {
				DistanceGap = PlaceHolder [0] - 1;
			}

			CehckingNOde = ListOfNodes [DistanceGap];
		}

		PersonalNodeMap.SetTargetPos (CehckingNOde.GetID ());
	}
	#endregion


}
