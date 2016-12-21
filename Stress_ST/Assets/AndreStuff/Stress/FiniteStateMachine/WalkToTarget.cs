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
	List<Nodes> _TheNodePath;
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



	bool once = false;



	public WalkToTarget(CreatingObjectNodeMap personalNodeMap, AStarPathfinding_RoomPaths createThePath, CreatureOneBehaviour myInfo) {//giving copies of info to this class
		Id = "WalkToTargetState";
		PersonalNodeMap = personalNodeMap;
		CreateThePath = createThePath;

		MyInfo = myInfo;
		MyTransform = MyInfo.transform;
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



			if (NeighbourGroups != MyInfo.NeighbourGroups || TargetNeighbourGroups != TargetInfo.NeighbourGroups) {
				NeighbourGroups = MyInfo.NeighbourGroups;
				TargetNeighbourGroups = TargetInfo.NeighbourGroups;

				if (NeighbourGroups == TargetNeighbourGroups) {
					_ThePath.Clear ();
				} else {
					CreateThePath.SetEndRoom (TargetNeighbourGroups);  
					_ThePath = CreateThePath.CreatePath (); 
				}
			}
	
			if (((MyTransform.position.x - TargetInfo.GetMyPosition () [0, 0] <= ChargeTargetDistance) && (MyTransform.position.x - TargetInfo.GetMyPosition () [0, 0] >= -ChargeTargetDistance)) &&
			     ((MyTransform.position.y - TargetInfo.GetMyPosition () [0, 1] <= ChargeTargetDistance) && (MyTransform.position.y - TargetInfo.GetMyPosition () [0, 1] >= -ChargeTargetDistance))) {//instead of using vector2.distance dont know it this is cheaper or not. 

				_Nodeindex = 0;
				PersonalNodeMap.SetTargetPos (TargetInfo.GetMyPosition ());
			
				_TheNodePath = PersonalNodeMap.SetInfoAndStartSearch (true);
			} else if (NeighbourGroups.Count == 1) {
				if (_ThePath.Count < 2) {
					_Nodeindex = 0;
					PersonalNodeMap.SetTargetPos (TargetInfo.GetMyPosition ());
					_TheNodePath = PersonalNodeMap.SetInfoAndStartSearch (true);
				} else {
					_Nodeindex = 0;
					WhichNodeToGOTO (_ThePath [1].GetComponent<RoomConnectorCreating> ().GettheNodes ()); 
					_TheNodePath = PersonalNodeMap.SetInfoAndStartSearch (true);
				}
			} else {
				if (_ThePath.Count == 0) {
					_Nodeindex = 0;
					PersonalNodeMap.SetTargetPos (TargetInfo.GetMyPosition ());
					_TheNodePath = PersonalNodeMap.SetInfoAndStartSearch (true);
				} else {
					_Nodeindex = 0;

					WhichNodeToGOTO (_ThePath.First ().GetComponent<RoomConnectorCreating> ().GettheNodes ());
					_TheNodePath = PersonalNodeMap.SetInfoAndStartSearch (true);
				}
			}

		} else {//if updatethepath havent triggered then this will run, and check if im close to the target. if not continue the given path
			if (((MyTransform.position.x - TargetInfo.GetMyPosition () [0, 0] <= ChargeTargetDistance) && (MyTransform.position.x - TargetInfo.GetMyPosition () [0, 0] >= -ChargeTargetDistance)) &&
			     ((MyTransform.position.y - TargetInfo.GetMyPosition () [0, 1] <= ChargeTargetDistance) && (MyTransform.position.y - TargetInfo.GetMyPosition () [0, 1] >= -ChargeTargetDistance))) {//instead of using vector2.distance dont know it this is cheaper or not. 

				_Nodeindex = 0;
				PersonalNodeMap.SetTargetPos (TargetInfo.GetMyPosition ());
				_TheNodePath = PersonalNodeMap.SetInfoAndStartSearch (false);
			}
		}

	}
	void GoToDestination() {//going through the pathlist and moves the objects
		if (_TheNodePath != null && _TheNodePath.Count > 0 && _TheNodePath.Count > _Nodeindex) {
			
			if ((((MyPreviousePosition[0,0] + _TheNodePath [_Nodeindex].GetID () [0, 0]) - DistanceFromNode) < MyTransform.position.x) && (((MyPreviousePosition[0,0] + _TheNodePath [_Nodeindex].GetID () [0, 0]) + DistanceFromNode) > MyTransform.position.x) && (((MyPreviousePosition[0,1] + _TheNodePath [_Nodeindex].GetID () [0, 1]) - DistanceFromNode) < MyTransform.position.y) && (((MyPreviousePosition[0,1] + _TheNodePath [_Nodeindex].GetID () [0, 1]) + DistanceFromNode) > MyTransform.position.y)) {
				_Nodeindex += 1;
				if (_Nodeindex == SearchAgainIndex || _Nodeindex == _TheNodePath.Count)//every time nodeindex == 1. so if im at the first node search again
					MyInfo.UpdateThePath = true;
			}

			if (_Nodeindex < _TheNodePath.Count) {

				MyTransform.position = Vector3.MoveTowards (MyTransform.position, new Vector3 ((MyPreviousePosition[0,0] + _TheNodePath [_Nodeindex].GetID () [0, 0]), (MyPreviousePosition[0,1] + _TheNodePath [_Nodeindex].GetID () [0, 1]), MyTransform.position.z), Time.smoothDeltaTime * MovementSpeed);
			}
		}
		if (((MyTransform.position.x - TargetInfo.GetMyPosition () [0, 0] <= 1) && (MyTransform.position.x - TargetInfo.GetMyPosition () [0, 0] >= -1)) &&
		    ((MyTransform.position.y - TargetInfo.GetMyPosition () [0, 1] <= 1) && (MyTransform.position.y - TargetInfo.GetMyPosition () [0, 1] >= -1))) {
			once = true;
		}

	}



	void WhichNodeToGOTO(List<Nodes> ListOfNodes){//calculating which node to go to in the roomconnector 
		TheDistance = 1000;
		Counter = 0;
		TargetVector.x = TargetInfo.GetMyPosition () [0, 0];
		TargetVector.y = TargetInfo.GetMyPosition () [0, 1];

		ObjectVector = Vector2.zero;


		foreach (Nodes s in ListOfNodes) {//getting the node closest to the target object
			ObjectVector.x = s.GetID () [0, 0];
			ObjectVector.y = s.GetID () [0, 1];

			Counter++;

			if (Vector2.Distance (TargetVector, ObjectVector) < TheDistance) {
				TheDistance = Vector2.Distance (TargetVector, ObjectVector);
				PlaceHolder [0] = Counter;
			}
		}

		TargetVector = (Vector2)MyTransform.position;
		Counter = 0;
		TheDistance = 1000;

		foreach (Nodes s in ListOfNodes) {//getting the node closest to this object
			ObjectVector.x = s.GetID () [0, 0];
			ObjectVector.y = s.GetID () [0, 1];

			Counter++;

			if (Vector2.Distance (TargetVector, ObjectVector) < TheDistance) {
				TheDistance = Vector2.Distance (TargetVector, ObjectVector);
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
