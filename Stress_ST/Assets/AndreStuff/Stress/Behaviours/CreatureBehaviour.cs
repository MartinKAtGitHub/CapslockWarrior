using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class CreatureBehaviour : MovingCreatures {



	void Awake(){
		
		myPos [0, 0] = transform.position.x;
		myPos [0, 1] = transform.position.y;	
		MyNode [0] = new Nodes (myPos, 0);

		_CreateThePath = new AStarPathfinding_RoomPaths (54, MyNode[0]);//TODO make 54 and 13 gamemanagervariables so that i get them and put them here
		_PersonalNodeMap = new CreatingObjectNodeMap(7,7, PathfindingNodeID);//if i do this then the constructor is only called once instead of 4 times......
	}

	void Start(){
		_PersonalNodeMap.CreateNodeMap ();
		_PersonalNodeMap.SetCenterPos (myPos);

		SetTarget (GameObject.FindGameObjectWithTag ("Player1"));

		if (RunPathfinding == true) {
			if (thetype == EnemyType.Ranged) {//makes it possible to add different states to the targets if that is needed
				MovementFSM = new FSM_Manager( new DefaultState[] { new WalkToTarget(_PersonalNodeMap, _CreateThePath, this, AttackRange, MovementSpeed,LineOfSight), new AttackState(this,CanIRanged, AttackRange,LineOfSight),} );//definerer alle statene for de spesifikke fsm'ene. og den første i dette tilfeller "FindTarget og AttackState" vil bli kalt først/default state
				MovementFSM.Init(this);
			}else if(thetype == EnemyType.Meele){
				MovementFSM = new FSM_Manager( new DefaultState[] { new WalkToTarget(_PersonalNodeMap, _CreateThePath, this, AttackRange, MovementSpeed,LineOfSight), new AttackState(this,CanIRanged, AttackRange,LineOfSight),} );//definerer alle statene for de spesifikke fsm'ene. og den første i dette tilfeller "FindTarget og AttackState" vil bli kalt først/default state
				MovementFSM.Init(this);
			}else if(thetype == EnemyType.Fast){
				MovementFSM = new FSM_Manager( new DefaultState[] { new WalkToTarget(_PersonalNodeMap, _CreateThePath, this, AttackRange, MovementSpeed,LineOfSight), new AttackState(this,CanIRanged, AttackRange,LineOfSight),} );//definerer alle statene for de spesifikke fsm'ene. og den første i dette tilfeller "FindTarget og AttackState" vil bli kalt først/default state
				MovementFSM.Init(this);
			}
		}
	}



	void FixedUpdate (){//this is called at set intevals, and the update is calling the statemachine after the fixedupdate have updated the colliders
		myPos [0, 0] = transform.position.x;
		myPos [0, 1] = transform.position.y;

		if (RunPathfinding == true) {
			if (FreezeCharacter == true) {
				if (GetComponent<Rigidbody2D> ().velocity.magnitude < 0.01f) {
					FreezeCharacter = false;
					MovementFSM.TheUpdate ();//oppdaterer fsm'ene
				}
			} else {
				MovementFSM.TheUpdate ();//oppdaterer fsm'ene
			}
		}
	}

	public override void OnDestroyed(){
		Debug.Log ("GYAAAAAAA");
	}

	public override void AttackTarget(){
		if (thetype == EnemyType.Ranged) {
			if(CanIRanged[0] == true)
				(Instantiate (Bullet, new Vector3 (transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject).GetComponent<BulletStart> ().SetParent (transform.gameObject, _GoAfter);
		} else if(thetype == EnemyType.Meele){
			_GoAfter.GetComponent<MovingCreatures> ().RecievedDmg ();
		}
	}

	public override void RecievedDmg(){
	//	Debug.Log ("I Got Hit :" + name);
	}

	public override void ChangeMovementAdd(float a){
		MovementSpeed [0] += a;
	}

}

