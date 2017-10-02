using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureWithMovementsAndNoWords : ObjectsWithMovementAndNoWords {

	public TargetHierarchy TargetPriorityClass;

	const int _NewMapCenter = -100;//Previour Center Was 0,0. That Caused Some Problems When The Player Was On A 0 Value. -0.9 == 0. 0.9 = 0. So That Fixed It But That Means That You Cant Go Below -100xy. Change This To Change The Center
	const float _NodeDimentions = 0.08f;//update CreatureBehaviour -> NodeMapCollision -> PlayerManager


	void Awake(){ 

		MyPos [0, 0] = ((FeetPlacements.transform.position.x - _NewMapCenter) / _NodeDimentions) - (((FeetPlacements.transform.position.x - _NewMapCenter) / _NodeDimentions) % 1);//Calculating Object World Position In The Node Map
		MyPos [0, 1] = ((FeetPlacements.transform.position.y - _NewMapCenter) / _NodeDimentions) - (((FeetPlacements.transform.position.y - _NewMapCenter) / _NodeDimentions) % 1);//Calculating Object World Position In The Node Map
		MyNode [0] = new Nodes (MyPos, 0);

		TheObject._CreateThePath = new AStarPathfinding_RoomPaths (GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ClockTest>().RoomPathsCount);//Performance Increase Is To Put This In A Different Script And Let Everyone Use That One Script, Insted Of One For Each Object
		TheObject._PersonalNodeMap = new CreatingObjectNodeMap(FeetPlacements.size, WalkingColliders.size.x, _NodeDimentions, TheObject.PathfindingNodeID, MyNode);
	}

	void Start(){
		TheObject.BehaviourStart ();
		TheObject._PersonalNodeMap.CreateNodeMap ();
		TheObject._PersonalNodeMap.SetTargetPos (TheObject._TheTarget.MyPos);
	}


	void FixedUpdate (){//this is called at set intevals, and the update is calling the statemachine after the fixedupdate have updated the colliders


		MyPos [0, 0] = ((FeetPlacements.transform.position.x - _NewMapCenter) / _NodeDimentions) - (((FeetPlacements.transform.position.x - _NewMapCenter) / _NodeDimentions) % 1);//Calculating Object World Position In The Node Map
		MyPos [0, 1] = ((FeetPlacements.transform.position.y - _NewMapCenter) / _NodeDimentions) - (((FeetPlacements.transform.position.y - _NewMapCenter) / _NodeDimentions) % 1);//Calculating Object World Position In The Node Map


		if (TheObject.FreezeCharacter == true) {
			if (TheObject.MyRididBody.velocity.magnitude < 0.01f) {
				TheObject.GotPushed = true;
				TheObject.FreezeCharacter = false;
			}
		} else {
			TheObject.BehaviourUpdate ();
		}
	}


	public override void OnDestroyed(){//TODO implement deathstuff here, its just a method so call this to cancel the update and gg wp hf
		base.OnDestroyed();

		Instantiate (Resources.Load ("DeadObject") as GameObject, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}


}