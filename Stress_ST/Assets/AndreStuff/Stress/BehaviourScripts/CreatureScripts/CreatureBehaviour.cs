using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.UI;

public class CreatureBehaviour : MovingCreatures {

	public TargetHierarchy TargetPriorityClass;

	const int _NewMapCenter = -100;//Previour Center Was 0,0. That Caused Some Problems When The Player Was On A 0 Value. -0.9 == 0. 0.9 = 0. So That Fixed It But That Means That You Cant Go Below -100xy. Change This To Change The Center
	const float _NodeDimentions = 0.08f;//update CreatureBehaviour -> NodeMapCollision -> PlayerManager

	bool _Once = false;
	bool _Twice = true;
	
	public bool ShowGizmos = false;
	float size = 0.04f;
	
	void Awake(){ 
		_WordChecker = new EnemyWordChecker(TextElement, this);
		
		ObjectBehaviour._MyTransform = transform;
		_TheTarget = TargetPriorityClass.GetTarget ();

		MyPos [0, 0] = transform.position.x; 
		MyPos [0, 1] = transform.position.y;	
		MyNode [0] = new Nodes (MyPos, 0);

		ObjectBehaviour._CreateThePath = new AStarPathfinding_RoomPaths (GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ClockTest>().RoomPathsCount);//Performance Increase Is To Put This In A Different Script And Let Everyone Use That One Script, Insted Of One For Each Object
		ObjectBehaviour._PersonalNodeMap = new CreatingObjectNodeMap(FeetPlacements.size, WalkingColliders.size.x, _NodeDimentions, ObjectBehaviour.PathfindingNodeID, MyNode);
	}

	void Start(){
		ObjectBehaviour.BehaviourStart ();
		ObjectBehaviour._PersonalNodeMap.CreateNodeMap ();
		ObjectBehaviour._PersonalNodeMap.SetTargetPos (_TheTarget.MyPos);
	}


	void FixedUpdate (){//this is called at set intevals, and the update is calling the statemachine after the fixedupdate have updated the colliders

		if (_Twice == true) {
			if (_Once == true) {
				_Once = false;
				_Twice = false;
				ObjectBehaviour._PersonalNodeMap.RemoveEnemyPositions (FeetPlacements);//This Objects FeetCollider Is Also Added At Start Which Means We Have To Remove It
			} else {
				_Once = true;
			}
		}

		MyPos [0, 0] = ((transform.position.x - _NewMapCenter) / _NodeDimentions) - (((transform.position.x - _NewMapCenter) / _NodeDimentions) % 1);//Calculating Object World Position In The Node Map
		MyPos [0, 1] = ((transform.position.y - _NewMapCenter) / _NodeDimentions) - (((transform.position.y - _NewMapCenter) / _NodeDimentions) % 1);//Calculating Object World Position In The Node Map

		if (_TheTarget == null) {
			_TheTarget = TargetPriorityClass.GetTarget ();
		}

		if (FreezeCharacter == true) {
			if (MyRididBody.velocity.magnitude < 0.01f) {
				ObjectBehaviour.GotPushed [0] = true;
				FreezeCharacter = false;
			}
		} else {
			ObjectBehaviour.BehaviourUpdate ();
		}
	}

	public override void OnDestroyed(){//TODO implement deathstuff here, its just a method so call this to cancel the update and gg wp hf

		_WordChecker.RemoveEvent ();
		Instantiate (Resources.Load ("DeadObject") as GameObject, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}

	GameObject saved;
	public override void AttackTarget(Transform targetPos){
		
	/*	if (_GoAfter != null) {
			if (CreatureType == StressEnums.EnemyType.Ranged) {
				if (CanIRanged [0] == true) {

					(Instantiate (Bullet, new Vector3 (HitPoint.transform.position.x, HitPoint.transform.position.y, HitPoint.transform.position.z), Quaternion.identity) as GameObject).GetComponent<BulletBehaviour> ().SetObjectDirection (transform.gameObject, targetPos as Transform);;
				}
			} else if (CreatureType == StressEnums.EnemyType.Meele) {
				if (CanIRanged [0] == true) {
					(Instantiate (Bullet, new Vector3 (HitPoint.transform.position.x, HitPoint.transform.position.y, HitPoint.transform.position.z), Quaternion.identity) as GameObject).GetComponent<BulletBehaviour> ().SetObjectDirection (transform.gameObject, targetPos  as Transform);;
				} else {
					DefaultBehaviourTarget.RecievedDmg(2);
				}
			}
		}*/
	}

	public override void SetTarget(GameObject target){
		base.SetTarget(target);
	}

	public override void RecievedDmg(int _damage){
		//OnDestroyed ();
			Debug.Log (name + " Got Hit with: " + _damage + " damage");
	}

	public override void ChangeMovementAdd(float a){
		MovementSpeed [0] += a;
	}

	public override void GotTheKill(int a){
		Debug.Log ("Score " + a);
	}


/*	void OnCollisionEnter2D(Collision2D coll){
		if (coll.otherCollider == gameObject) {//Check If FeetCollider Or BodyCollider Triggered The Collision
			ObjectBehaviour.OnCollisions (coll);
		} else {
	//		Debug.Log ("Colli " + coll.gameObject.name + " | " + coll.otherCollider.name);//FeetCollider
		}
	}*/

	void OnDrawGizmos(){

		if (ShowGizmos) {
			size = _NodeDimentions /1.25f;
			Nodes[,] mynodes = 	ObjectBehaviour._PersonalNodeMap.GetNodemap ();
			for (int x = 0; x < Mathf.FloorToInt((WalkingColliders.size.x) / _NodeDimentions); x++) {
				for (int y = 0; y <  Mathf.FloorToInt((WalkingColliders.size.x) / _NodeDimentions); y++) {
					if (mynodes [x, y].GetCollision () == ObjectBehaviour.PathfindingNodeID[0]) {
						Gizmos.color = Color.black;
					} else if (mynodes [x, y].GetCollision () == ObjectBehaviour.PathfindingNodeID[1]) {
						Gizmos.color = Color.blue;
					} else {
						Gizmos.color = Color.yellow;
					
					} 
					Gizmos.DrawCube (new Vector3 ((((mynodes [x, y].GetID () [0, 0]) + MyPos [0, 0]) * _NodeDimentions) - 100 + (_NodeDimentions / 2), ((mynodes [x, y].GetID () [0, 1]) + MyPos [0, 1]) * _NodeDimentions - 100  + (_NodeDimentions / 2), 0), new Vector3 (size, size, 0));
				}
			}
		/*	Gizmos.color = Color.white;
			Gizmos.DrawCube (new Vector3 ((((mynodes [0,0].GetID () [0, 0]) + myPos [0, 0]) * _NodeDimentions) - 100 + (_NodeDimentions / 2), ((mynodes [0,0].GetID () [0, 1]) + myPos [0, 1]) * _NodeDimentions - 100  + (_NodeDimentions / 2), 0), new Vector3 (size, size, size));
			Gizmos.DrawCube (new Vector3 ((((mynodes [31,31].GetID () [0, 0]) + myPos [0, 0]) * _NodeDimentions) - 100 + (_NodeDimentions / 2), ((mynodes [31,31].GetID () [0, 1]) + myPos [0, 1]) * _NodeDimentions - 100  + (_NodeDimentions / 2), 0), new Vector3 (size, size, size));
			Gizmos.DrawCube (new Vector3 ((((mynodes [30,31].GetID () [0, 0]) + myPos [0, 0]) * _NodeDimentions) - 100 + (_NodeDimentions / 2), ((mynodes [30,31].GetID () [0, 1]) + myPos [0, 1]) * _NodeDimentions - 100  + (_NodeDimentions / 2), 0), new Vector3 (size, size, size));
			*/		

			Nodes[] mynodess = ObjectBehaviour._PersonalNodeMap.GetNodeList();
			int[] count = ObjectBehaviour._PersonalNodeMap.GetNodeindex ();
			for(int sas = count[0]; sas < mynodess.Length; sas++){
				Gizmos.color = Color.red;
				Gizmos.DrawCube (new Vector3 (((mynodess[sas].GetID()[0,0] + MyPos [0, 0]) * _NodeDimentions) - 100 + (_NodeDimentions / 2), ((mynodess[sas].GetID()[0,1] + MyPos [0, 1])  * _NodeDimentions) - 100  + (_NodeDimentions / 2), 0), new Vector3 (size, size, size));
			}

			mynodes = ObjectBehaviour._PersonalNodeMap.GetNodemap ();
		}
	}

}

/*
public class CreatureBehaviour : MovingCreatures {

	public TargetHierarchy TargetPriorityClass;
	List<DefaultBehaviour> WhatToPrioritize;//this might become an List<string> containing name of tags to search after   or simply just List<gameobject>

	public The_Object_Behaviour ObjectBehaviour;

	public StressEnums.NodeSizes NodeSizess = StressEnums.NodeSizes.One;
	public Transform HitPoint;
	public Transform WalkColliderPoint;

	public Text TextElement;
	public string _EnemyHealth;
	EnemyWordChecker test;

	public void setword(string a){
		test._EnemyHealth = a;
		TextElement.text = a;
		_EnemyHealth = a;
	}

	void Awake(){ 
		ObjectBehaviour._MyTransform = transform;
		//TargetPriorityClass = new TargetHierarchy (this);

		TargetPriorityClass.SetTargetHierarchy(this);
		if (test == null) {
			test = new EnemyWordChecker(TextElement,_EnemyHealth, this); 
		}

		if (WalkColliderPoint == null)
			WalkColliderPoint = this.transform;
		myPos [0, 0] = WalkColliderPoint.position.x;
		myPos [0, 1] = WalkColliderPoint.position.y;	
		MyNode [0] = new Nodes (myPos, 0);
		_CreateThePath = new AStarPathfinding_RoomPaths (54, MyNode[0]);//TODO make 54 and 13 gamemanagervariables so that i get them and put them here
		_PersonalNodeMap = new CreatingObjectNodeMap(transform.Find("FeetCollider").GetComponent<BoxCollider2D>().size, transform.Find ("WalkingCollider").GetComponent<BoxCollider2D> ().size.x / 2, NodeSizess, PathfindingNodeID);//if i do this then the constructor is only called once instead of 4 times......
	}

	void Start(){
		_PersonalNodeMap.CreateNodeMap ();
		_PersonalNodeMap.SetCenterPos (myPos);
	
		if (RunPathfinding == true) {
			if (CreatureType == StressEnums.EnemyType.Ranged) {//makes it possible to add different states to the targets if that is needed
				MovementFSM = new FSM_Manager( new DefaultState[] { new RangedWalkToTarget(TargetPriorityClass, _PersonalNodeMap, _CreateThePath, this, AttackRange, MovementSpeed,LineOfSight), new AttackState(TargetPriorityClass, this,CanIRanged, AttackRange,LineOfSight),} );//definerer alle statene for de spesifikke fsm'ene. og den første i dette tilfeller "FindTarget og AttackState" vil bli kalt først/default state
				MovementFSM.Init(this);
			}else if(CreatureType == StressEnums.EnemyType.Meele){
				MovementFSM = new FSM_Manager( new DefaultState[] { new MeeleWalkToTarget(TargetPriorityClass, _PersonalNodeMap, _CreateThePath, this, AttackRange, MovementSpeed,LineOfSight), new AttackState(TargetPriorityClass, this,CanIRanged, AttackRange,LineOfSight),} );//definerer alle statene for de spesifikke fsm'ene. og den første i dette tilfeller "FindTarget og AttackState" vil bli kalt først/default state
				MovementFSM.Init(this);
			}else if(CreatureType == StressEnums.EnemyType.Fast){
				MovementFSM = new FSM_Manager( new DefaultState[] { new MeeleWalkToTarget(TargetPriorityClass, _PersonalNodeMap, _CreateThePath, this, AttackRange, MovementSpeed,LineOfSight), new AttackState(TargetPriorityClass, this,CanIRanged, AttackRange,LineOfSight),} );//definerer alle statene for de spesifikke fsm'ene. og den første i dette tilfeller "FindTarget og AttackState" vil bli kalt først/default state
				MovementFSM.Init(this);
			}
		}
	}

	void FixedUpdate (){//this is called at set intevals, and the update is calling the statemachine after the fixedupdate have updated the colliders

		myPos [0, 0] = WalkColliderPoint.position.x;
		myPos [0, 1] = WalkColliderPoint.position.y;

		if (myPos [0, 0] < 0) {
			if ((myPos [0, 0] % 0.25f) < -0.125f) {
				myPos [0, 0] +=	-(myPos [0, 0] % 0.25f) - 0.25f;
			} else {
				myPos [0, 0] +=	-(myPos [0, 0] % 0.25f);
			}
		} else {
			if ((myPos [0, 0] % 0.25f) < 0.125f) {
				myPos [0, 0] +=	-(myPos [0, 0] % 0.25f);
			} else {
				myPos [0, 0] +=	-(myPos [0, 0] % 0.25f) + 0.25f;
			}
		}

		if (myPos [0, 1] < 0) {
			if ((myPos [0, 1] % 0.25f) < -0.125f) {
				myPos [0, 1] +=	-(myPos [0, 1] % 0.25f) - 0.25f;
			} else {
				myPos [0, 1] +=	-(myPos [0, 1] % 0.25f);
			}
		} else {
			if ((myPos [0, 1] % 0.25f) < 0.125f) {
				myPos [0, 1] +=	-(myPos [0, 1] % 0.25f);
			} else {
				myPos [0, 1] +=	-(myPos [0, 1] % 0.25f) + 0.25f;
			}
		}

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

	public override void OnDestroyed(){//TODO implement deathstuff here, its just a method so call this to cancel the update and gg wp hf

		test.RemoveEvent ();
		Instantiate (Resources.Load ("Andre/Prefabs/Creatures/DeadObject") as GameObject, transform.position, Quaternion.identity);
		Destroy (this.gameObject);
	}

	GameObject saved;
	public override void AttackTarget(Transform targetPos){
		
		if (_GoAfter != null) {
			if (CreatureType == StressEnums.EnemyType.Ranged) {
				if (CanIRanged [0] == true) {

					(Instantiate (Bullet, new Vector3 (HitPoint.transform.position.x, HitPoint.transform.position.y, HitPoint.transform.position.z), Quaternion.identity) as GameObject).GetComponent<BulletBehaviour> ().SetObjectDirection (transform.gameObject, targetPos as Transform);;
				}
			} else if (CreatureType == StressEnums.EnemyType.Meele) {
				if (CanIRanged [0] == true) {
					(Instantiate (Bullet, new Vector3 (HitPoint.transform.position.x, HitPoint.transform.position.y, HitPoint.transform.position.z), Quaternion.identity) as GameObject).GetComponent<BulletBehaviour> ().SetObjectDirection (transform.gameObject, targetPos  as Transform);;
				} else {
					DefaultBehaviourTarget.RecievedDmg(2);
				}
			}
		}
	}

	public override void SetTarget(GameObject target){
		base.SetTarget(target);
	}

	public override void RecievedDmg(int _damage){
		//OnDestroyed ();
		//	Debug.Log (name + " Got Hit with: " + _damage + " damage");
	}

	public override void ChangeMovementAdd(float a){
		MovementSpeed [0] += a;
	}

	public override void GotTheKill(int a){
		Debug.Log ("Score " + a);
	}

	public bool ShowGizmos = false;
	float size;
	void OnDrawGizmos(){
		
		if (ShowGizmos) {
			size = 1 / (float)NodeSizess / 2;
			Nodes[,] mynodes = _PersonalNodeMap.GetNodemap ();



			for (int x = 0; x < Mathf.FloorToInt((transform.Find ("WalkingCollider").GetComponent<BoxCollider2D> ().size.x / 2) / (1 / (float)NodeSizess) * 2) + 1; x++) {
				for (int y = 0; y <  Mathf.FloorToInt((transform.Find ("WalkingCollider").GetComponent<BoxCollider2D> ().size.x / 2) / (1 / (float)NodeSizess) * 2) + 1; y++) {
					if (mynodes [x, y].GetCollision () == PathfindingNodeID[0]) {
						Gizmos.color = Color.black;
					} else if (mynodes [x, y].GetCollision () == PathfindingNodeID[1]) {
						Gizmos.color = Color.blue;
					} else {
						Gizmos.color = Color.yellow;
					
					} 
					Gizmos.DrawCube (new Vector3 ((mynodes [x, y].GetID () [0, 0]) + myPos [0, 0], (mynodes [x, y].GetID () [0, 1]) + myPos [0, 1], 0), new Vector3 (size, size, size));
				}
			}
			Gizmos.color = Color.white;
			Gizmos.DrawCube (new Vector3 ((mynodes [0, 0].GetID () [0, 0])  + myPos [0, 0], (mynodes [0, 0].GetID () [0, 1]) + myPos [0, 1], 0), new Vector3 (size, size, size));
			Gizmos.color = Color.yellow;
			Gizmos.DrawCube (new Vector3 ((mynodes [0, 1].GetID () [0, 0]) + myPos [0, 0], (mynodes [0, 1].GetID () [0, 1])+ myPos [0, 1], 0), new Vector3 (size, size, size));


		

			Nodes[] mynodess = _PersonalNodeMap.GetNodeList();
			int[] count = _PersonalNodeMap.GetNodeindex ();
			for(int sas = count[0]; sas < mynodess.Length; sas++){
				Gizmos.color = Color.red;
				Gizmos.DrawCube (new Vector3 (mynodess[sas].GetID()[0,0] + myPos [0, 0], mynodess[sas].GetID()[0,1] + myPos [0, 1], 0), new Vector3 (size, size, size));
			}

			mynodes = _PersonalNodeMap.GetNodemap ();

			/*for (int x = 0; x < Mathf.FloorToInt((transform.FindChild ("WalkingCollider").GetComponent<BoxCollider2D> ().size.x / 2) / (1 / (float)NodeSizess) * 2) + 1; x++) {
				for (int y = 0; y <  Mathf.FloorToInt((transform.FindChild ("WalkingCollider").GetComponent<BoxCollider2D> ().size.x / 2) / (1 / (float)NodeSizess) * 2) + 1; y++) {
					if (mynodes [x, y].GetCollision () == PathfindingNodeID[0]) {
						Gizmos.color = Color.black;
					} else if (mynodes [x, y].GetCollision () == PathfindingNodeID[1]) {
						Gizmos.color = Color.blue;
					} else {
						Gizmos.color = Color.yellow;

					} 
					Gizmos.DrawCube (new Vector3 ((mynodes [x, y].GetID () [0, 0]) , (mynodes [x, y].GetID () [0, 1])  , 0), new Vector3 (size, size, size));
				}
			}
			Gizmos.color = Color.white;
			Gizmos.DrawCube (new Vector3 ((mynodes [0, 0].GetID () [0, 0]) , (mynodes [0, 0].GetID () [0, 1]) , 0), new Vector3 (size, size, size));
			Gizmos.color = Color.yellow;
			Gizmos.DrawCube (new Vector3 ((mynodes [0, 1].GetID () [0, 0]) , (mynodes [0, 1].GetID () [0, 1]), 0), new Vector3 (size, size, size));




			Nodes[] mynodess = _PersonalNodeMap.GetNodeList();
			int[] count = _PersonalNodeMap.GetNodeindex ();
			for(int sas = count[0]; sas < mynodess.Length; sas++){
				Gizmos.color = Color.red;
				Gizmos.DrawCube (new Vector3 (mynodess[sas].GetID()[0,0] , mynodess[sas].GetID()[0,1] , 0), new Vector3 (size, size, size));
			}

			mynodes = _PersonalNodeMap.GetNodemap ();*/
/*}
}

}


*/