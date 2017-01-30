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

	float[] MovementSpeed;//enemy speed
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

	Vector2 movemen = Vector2.zero;
	float range = 10;

	Rigidbody2D myrigid;
	RaycastHit2D[] collisions;
	LayerMask CollisionLayer;

	Animator CreatureAnimator;
	Vector3 RotateCharacter = Vector3.zero;



	Vector2 GoalPosition = Vector2.zero;
	float[,] IdHolder, NextIdHolder;
	bool FirstMovementAfterSearch = false;




	public WalkToTarget(CreatingObjectNodeMap personalNodeMap, AStarPathfinding_RoomPaths createThePath, CreatureOneBehaviour myInfo, float theRange, float[] movementspeed) {//giving copies of info to this class
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
		CollisionLayer = 1 << LayerMask.NameToLayer ("Walls");

		CreatureAnimator = myInfo.GetComponent<Animator> ();
		range = theRange;
		MovementSpeed = movementspeed;
		//TODO SET MOVEMENT SPEED
	}

	public override string EnterState() {//When it switches to this state this is the first thing thats being called

		if (MyInfo.GetTraget () == null) {//having a failsafe here, so if i dont have a target, ill switch state to something that can search
			return ""; //TODO TODO "TargetSearch";
		}else{
			if (TargetInfo != MyInfo.GetTargetBehaviour ()) {//if this target isnt the same as what im after, change it
				TargetInfo = MyInfo.GetTargetBehaviour ();
			}
			MyPreviousePosition = PersonalNodeMap.GetCenterPos ();
		}
		CreatureAnimator.SetFloat ("ChangeAnimation", 1);
		ReturnState = "";
	
		HaveSearched = false;
		NodeMapUpdated = false;
		MyInfo.UpdateThePath = true;
		FirstMovementAfterSearch = false;
		return "";
	}


	public override string ProcessState() {//this is called every frame

		ReturnState = "";//denne er enten null eller den staten som den skal forandres til

		if (TargetInfo != null) {
			UpdatePaths ();
			GoToDestination ();
		}

		return ReturnState;
	}

	public override void ExitState(){//when i want to switch to another state this is called before the enterstate of the next state

	}

	#region Makeing Path and going to nodes

	bool NodeMapUpdated = false;
	bool HaveSearched = false;
	void UpdatePaths(){//the path search behaviour happens here, what to search when im here or there etc.
		//TODO GO OVER THIS 
	
		if (MyInfo.UpdateThePath == true) {

			if (NodeMapUpdated == true) {
				MyInfo.UpdateThePath = false;

				NeighbourGroups = MyInfo.NeighbourGroups;
				TargetNeighbourGroups = TargetInfo.NeighbourGroups;

				if (NeighbourGroups == TargetNeighbourGroups) {//if true then this object and the target is in the same room
					_Roomindex = _TheRoomPath.Length;

					PersonalNodeMap.SetTargetPos (TargetInfo.GetMyPosition ());
					PersonalNodeMap.SetInfoAndStartSearch (true);
					_Nodeindex = nodesindex [0];

				} else {
					CreateThePath.SetEndRoom (TargetNeighbourGroups);  
					CreateThePath.CreatePath (); 
					_Roomindex = roomsindex [0];
					if ((_Roomindex) - _TheRoomPath.Length < 0) {

						if (NeighbourGroups.Count == 1) {//TODO an errer might occur if this object is within a room that only has one connector. (so atleast in the scene, make it so that the room that only have one roomconnector, have neighbour connectors that are higher or lower in the exiting side (if that happends an infinite loop might possibly occur))
							WhichNodeToGOTO (_TheRoomPath [_Roomindex].GetComponent<RoomConnectorCreating> ().GettheNodes ()); 
							PersonalNodeMap.SetInfoAndStartSearch (true);
							_Nodeindex = nodesindex [0];

						} else {
							WhichNodeToGOTO (_TheRoomPath [_Roomindex].GetComponent<RoomConnectorCreating> ().GettheNodes ()); 
							PersonalNodeMap.SetInfoAndStartSearch (true);
							_Nodeindex = nodesindex [0];
						}

					} else {
						PersonalNodeMap.SetTargetPos (TargetInfo.GetMyPosition ());
						PersonalNodeMap.SetInfoAndStartSearch (true);
						_Nodeindex = nodesindex [0];
					}
				}

				HaveSearched = true;
				NodeMapUpdated = false;
				MyInfo.UpdateThePath = false;
				FirstMovementAfterSearch = false;
			} else {
				PersonalNodeMap.UpdateNodeMap ();
				NodeMapUpdated = true;
			}
		}
	
/*		if (MyInfo.UpdateThePath == true) {//this is true if something have entered the boxcollider
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

			//	if (Vector2.Distance ((Vector2)MyTransform.position, TargetInfo.GetMyPositionVector2 ()) <= ChargeTargetDistance) {
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
					WhichNodeToGOTO (_TheRoomPath [_Roomindex].GetComponent<RoomConnectorCreating> ().GettheNodes ()); 
					PersonalNodeMap.SetInfoAndStartSearch (true);
					_Nodeindex = nodesindex [0];

				}
			} else {
				if (_TheRoomPath.Length - _Roomindex == 0) {
					PersonalNodeMap.SetTargetPos (TargetInfo.GetMyPosition ());
					PersonalNodeMap.SetInfoAndStartSearch (true);
					_Nodeindex = nodesindex [0];

				} else {
					PersonalNodeMap.SetTargetPos (TargetInfo.GetMyPosition ());
					PersonalNodeMap.SetInfoAndStartSearch (false);
					_Nodeindex = nodesindex [0];
				}
			}
			SearchAgainIndex = _Nodeindex;
			FirstMovementAfterSearch = false;
		} else {//if updatethepath havent triggered then this will run, and check if im close to the target. if not continue the given path
			//if (Vector2.Distance ((Vector2)MyTransform.position, TargetInfo.GetMyPositionVector2 ()) <= ChargeTargetDistance) {
			if (((MyTransform.position.x - TargetInfo.GetMyPosition () [0, 0] <= ChargeTargetDistance) && (MyTransform.position.x - TargetInfo.GetMyPosition () [0, 0] >= -ChargeTargetDistance)) &&
				((MyTransform.position.y - TargetInfo.GetMyPosition () [0, 1] <= ChargeTargetDistance) && (MyTransform.position.y - TargetInfo.GetMyPosition () [0, 1] >= -ChargeTargetDistance))) {//instead of using vector2.distance dont know it this is cheaper or not. 

				PersonalNodeMap.SetTargetPos (TargetInfo.GetMyPosition ());
				PersonalNodeMap.SetInfoAndStartSearch (false);
				_Nodeindex = nodesindex [0];
			}
			SearchAgainIndex = _Nodeindex;
			FirstMovementAfterSearch = false;
		}*/
	}

	void GoToDestination() {//going through the pathlist and moves the objects

		//if (_Nodeindex < _TheNodePath.Length && _TheNodePath [_Nodeindex] != null) {//if this is false then the search failed or never happened or an error :D
		if (HaveSearched == true) {//if this is false then the search failed or never happened or an error :D
		//	Debug.Log (_Nodeindex + " | " + _TheNodePath.Length);
				
			GoalPosition.x = MyPreviousePosition [0, 0] + _TheNodePath [_Nodeindex].GetID () [0, 0];
			GoalPosition.y = MyPreviousePosition [0, 1] + _TheNodePath [_Nodeindex].GetID () [0, 1];

			if (Vector2.Distance (GoalPosition, (Vector2)MyTransform.position) <= DistanceFromNode) {//if im inside the node im going to 
				_Nodeindex++;

				if (Vector2.Distance ((Vector2)MyTransform.position, TargetInfo.GetMyPositionVector2 ()) <= range) {//checking if im withing range of the target 
					if (Physics2D.Linecast ((Vector2)MyTransform.position, TargetInfo.GetMyPositionVector2 (), CollisionLayer).transform == null) {//if im in range do a raycast and see if there is an obsacle in the way, if true then i didnt hit anything
						ReturnState = "AttackState";
						return;
					} 
				}

				#region Calculating Direction/speed

				if (_Nodeindex < _TheNodePath.Length) {
					IdHolder = _TheNodePath [_Nodeindex].GetID ();

					if (nodesindex [0] < _Nodeindex) {
						NextIdHolder = _TheNodePath [_Nodeindex - 1].GetID ();

						movemen.x = IdHolder [0, 0] - NextIdHolder [0, 0];
						movemen.y = IdHolder [0, 1] - NextIdHolder [0, 1];
					} else {
						IdHolder = _TheNodePath [_Nodeindex].GetID ();

						movemen.x = IdHolder [0, 0];
						movemen.y = IdHolder [0, 1];
					}
					myrigid.velocity = (movemen * MovementSpeed[0]);//speed through velocity. currently it can eather go straight or 45degrees.   currently [1,0] || [0,1] || [1,1]
				}

				if (MyTransform.position.x < GoalPosition.x - 0.075f) {
					if (MyTransform.localScale.x > 0) {
						CreatureAnimator.SetFloat ("ChangeAnimation", 1);
						RotateCharacter.x = MyTransform.localScale.x * -1;
						RotateCharacter.y = MyTransform.localScale.y;
						RotateCharacter.z = 0;
						MyTransform.localScale = RotateCharacter;
					}
				} else if (MyTransform.position.x > GoalPosition.x + 0.075f) {
					if (MyTransform.localScale.x < 0) {
						CreatureAnimator.SetFloat ("ChangeAnimation", 1);
						RotateCharacter.x = MyTransform.localScale.x * -1;
						RotateCharacter.y = MyTransform.localScale.y;
						RotateCharacter.z = 0;
						MyTransform.localScale = RotateCharacter;
					}
				}

				#endregion

				if (_Nodeindex > SearchAgainIndex + 1) {
					HaveSearched = false;
					NodeMapUpdated = false;
					MyInfo.UpdateThePath = true;
					FirstMovementAfterSearch = false;
				}


				if (_Nodeindex >= _TheNodePath.Length) {
					HaveSearched = false;
					NodeMapUpdated = false;
					MyInfo.UpdateThePath = true;
					FirstMovementAfterSearch = false;
				}

			} else {
				if (FirstMovementAfterSearch == false) {

					#region Calculating Direction/speed

					FirstMovementAfterSearch = true;
					IdHolder = _TheNodePath [_Nodeindex].GetID ();

					if (nodesindex [0] < _Nodeindex) {
						NextIdHolder = _TheNodePath [_Nodeindex - 1].GetID ();

						movemen.x = IdHolder [0, 0] - NextIdHolder [0, 0];
						movemen.y = IdHolder [0, 1] - NextIdHolder [0, 1];
					} else {
						IdHolder = _TheNodePath [_Nodeindex].GetID ();

						movemen.x = IdHolder [0, 0];
						movemen.y = IdHolder [0, 1];
					}
					myrigid.velocity = (movemen * MovementSpeed[0]);//speed through velocity. currently it can eather go straight or 45degrees.   currently [1,0] || [0,1] || [1,1]
				}

				if (MyTransform.position.x < GoalPosition.x - 0.075f) {
					if (MyTransform.localScale.x > 0) {
						CreatureAnimator.SetFloat ("ChangeAnimation", 1);
						RotateCharacter.x = MyTransform.localScale.x * -1;
						RotateCharacter.y = MyTransform.localScale.y;
						RotateCharacter.z = 0;
						MyTransform.localScale = RotateCharacter;
					}
				} else if (MyTransform.position.x > GoalPosition.x + 0.075f) {
					if (MyTransform.localScale.x < 0) {
						CreatureAnimator.SetFloat ("ChangeAnimation", 1);
						RotateCharacter.x = MyTransform.localScale.x * -1;
						RotateCharacter.y = MyTransform.localScale.y;
						RotateCharacter.z = 0;
						MyTransform.localScale = RotateCharacter;
					}
				}

				#endregion
			}

		} else {
	//		Debug.Log ("Could Not Find What I Needed");
			myrigid.velocity = Vector2.zero;
		}




	/*	if (_TheNodePath != null) {

			if (_Nodeindex < _TheNodePath.Length) {



				if (nodesindex [0] < _Nodeindex) {
					movemen.x = _TheNodePath [_Nodeindex].GetID () [0, 0] - _TheNodePath [_Nodeindex - 1].GetID () [0, 0];
					movemen.y = _TheNodePath [_Nodeindex].GetID () [0, 1] - _TheNodePath [_Nodeindex - 1].GetID () [0, 1];
				} else {
					movemen.x = _TheNodePath [_Nodeindex].GetID () [0, 0];
					movemen.y = _TheNodePath [_Nodeindex].GetID () [0, 1];
				}

				myrigid.velocity = (movemen * MovementSpeed);


				GoalPosition.x = MyPreviousePosition [0, 0] + _TheNodePath [_Nodeindex].GetID () [0, 0];
				GoalPosition.y = MyPreviousePosition [0, 1] + _TheNodePath [_Nodeindex].GetID () [0, 1];

				if (Vector2.Distance (GoalPosition, MyTransform.position) <= DistanceFromNode) {
			//	if ((((MyPreviousePosition [0, 0] + _TheNodePath [_Nodeindex].GetID () [0, 0]) - DistanceFromNode) < MyTransform.position.x) && (((MyPreviousePosition [0, 0] + _TheNodePath [_Nodeindex].GetID () [0, 0]) + DistanceFromNode) > MyTransform.position.x) && (((MyPreviousePosition [0, 1] + _TheNodePath [_Nodeindex].GetID () [0, 1]) - DistanceFromNode) < MyTransform.position.y) && (((MyPreviousePosition [0, 1] + _TheNodePath [_Nodeindex].GetID () [0, 1]) + DistanceFromNode) > MyTransform.position.y)) {
					if (Vector2.Distance ((Vector2)MyTransform.position, TargetInfo.GetMyPositionVector2 ()) <= range) {//checking if im withing range of the target 
						if (_Nodeindex >= _TheNodePath.Length) {//every time nodeindex == 1. so if im at the first node search again
							MyInfo.UpdateThePath = true;
							return;
						}

						if (Physics2D.Linecast ((Vector2)MyTransform.position, TargetInfo.GetMyPositionVector2 (), LineOfSight).transform != null) {//if im in range do a raycast and see if there is an obsacle in the way
							_Nodeindex += 1;
							if (MyTransform.position.x < MyPreviousePosition [0, 0] + _TheNodePath [_Nodeindex].GetID () [0, 0]) {
								if (MyTransform.localScale.x > 0) {
									RotateCharacter.x = MyTransform.localScale.x * -1;
									RotateCharacter.y = MyTransform.localScale.y;
									RotateCharacter.z = 0;
									MyTransform.localScale = RotateCharacter;
								}
								CreatureAnimator.SetFloat ("ChangeAnimation", 1);
							} else {
								if (MyTransform.localScale.x < 0) {
									RotateCharacter.x = MyTransform.localScale.x * -1;
									RotateCharacter.y = MyTransform.localScale.y;
									RotateCharacter.z = 0;
									MyTransform.localScale = RotateCharacter;
								}
								CreatureAnimator.SetFloat ("ChangeAnimation", 1);
							}
						} else {
							Debug.Log ("CHANGE TO ATTACK STATE");
							ReturnState = "AttackState";
						}
					} else {
						if (MyTransform.position.x < MyPreviousePosition [0, 0] + _TheNodePath [_Nodeindex].GetID () [0, 0] - 0.075f) {
							if (MyTransform.localScale.x > 0) {
								CreatureAnimator.SetFloat ("ChangeAnimation", 1);
								RotateCharacter.x = MyTransform.localScale.x * -1;
								RotateCharacter.y = MyTransform.localScale.y;
								RotateCharacter.z = 0;
								MyTransform.localScale = RotateCharacter;
							}
						} else if (MyTransform.position.x > MyPreviousePosition [0, 0] + _TheNodePath [_Nodeindex].GetID () [0, 0] + 0.075f) {
							if (MyTransform.localScale.x < 0) {
								CreatureAnimator.SetFloat ("ChangeAnimation", 1);
								RotateCharacter.x = MyTransform.localScale.x * -1;
								RotateCharacter.y = MyTransform.localScale.y;
								RotateCharacter.z = 0;
								MyTransform.localScale = RotateCharacter;
							}
						}
						_Nodeindex += 1;
						if (_Nodeindex > SearchAgainIndex)
							MyInfo.UpdateThePath = true;
					}


				} else {
				}

			}
		}*/
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
