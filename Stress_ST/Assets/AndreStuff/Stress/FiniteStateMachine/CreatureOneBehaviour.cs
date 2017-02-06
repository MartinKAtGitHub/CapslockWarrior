using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class CreatureOneBehaviour : DefaultBehaviour {

	private FSM_Manager MovementFSM, AttackFSM; // dette er Final State Machinene jeg bruker, en for bevegelse av hjulene/beltene og en for radaren. jeg lagde ikke en for kanonen siden den bare hadde en state

	AStarPathfinding_RoomPaths _CreateThePath;
	CreatingObjectNodeMap _PersonalNodeMap;

	[Tooltip("PathfindingNodeID is the cost to move to the different nodes //// 0 = normal nodes //// 1 = undestructable walls //// 2 = other units")]
	public int[] PathfindingNodeID = new int[3];

	public float CreatureRange = 1;
	public bool[] CanIRanged = new bool[1];

	public GameObject Bullet;
//	LayerMask LineOfSight;//TODO give to other states insted of finding it on each object

	const string EnemyString = "Enemy"; 

	void Awake(){
		
		myPos [0, 0] = transform.position.x;
		myPos [0, 1] = transform.position.y;	

		_CreateThePath = new AStarPathfinding_RoomPaths (54);//TODO make 54 and 13 gamemanagervariables so that i get them and put them here
		_PersonalNodeMap = new CreatingObjectNodeMap(7,7, PathfindingNodeID);//if i do this then the constructor is only called once instead of 4 times......
		MyNodePosition[0] = new Nodes (myPos,0);
	//	LineOfSight = 1 << LayerMask.NameToLayer ("Walls");
	}

	void Start(){
		_PersonalNodeMap.CreateNodeMap ();
		_PersonalNodeMap.SetCenterPos (myPos);

		SetTarget (GameObject.FindGameObjectWithTag ("Player1"));

		if (RunPathfinding == true) {
			if (thetype == EnemyType.Ranged) {//makes it possible to add different states to the targets if that is needed
				MovementFSM = new FSM_Manager( new DefaultState[] { new WalkToTarget(_PersonalNodeMap, _CreateThePath, this, CreatureRange, MovementSpeed), new GolemAttackState(this,CanIRanged, CreatureRange),} );//definerer alle statene for de spesifikke fsm'ene. og den første i dette tilfeller "FindTarget og AttackState" vil bli kalt først/default state
				MovementFSM.Init(this);
			}else if(thetype == EnemyType.Meele){
				MovementFSM = new FSM_Manager( new DefaultState[] { new WalkToTarget(_PersonalNodeMap, _CreateThePath, this, CreatureRange, MovementSpeed), new GolemAttackState(this,CanIRanged, CreatureRange),} );//definerer alle statene for de spesifikke fsm'ene. og den første i dette tilfeller "FindTarget og AttackState" vil bli kalt først/default state
				MovementFSM.Init(this);
			}
		}
	}



	void FixedUpdate (){//this is called at set intevals, and the update is calling the statemachine after the fixedupdate have updated the colliders
		myPos [0, 0] = transform.position.x;
		myPos [0, 1] = transform.position.y;

		if (RunPathfinding == true) {
			if (StopMoveLogic == true) {
				if (GetComponent<Rigidbody2D> ().velocity.magnitude < 0.01f) {
					StopMoveLogic = false;
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

	void UpdateTargetRoomAndNode(){//sending the endpoints to the A*
		_CreateThePath.SetEndRoomAndNode (_GoAfter.GetComponent<DefaultBehaviour>().NeighbourGroups, _GoAfter.GetComponent<DefaultBehaviour>().MyNodePosition);
	}

	public override void SetTarget(GameObject target){//if you want to change target, use this, TODO make it so that its possible to only send the node
		_GoAfter = target;
		UpdateTargetRoomAndNode ();
	} 

	#region Collision functions
	public override void SetNeighbourGroup(List<RoomConnectorCreating> neighbours){
		NeighbourGroups = neighbours;
		_CreateThePath.SetStartRoomAndNode (neighbours, MyNodePosition);
	}


	public override void AddWallWithTrigger(GameObject collidingwithobject){
		_PersonalNodeMap.AddGameobjectsWithinTrigger (collidingwithobject);
		UpdateThePath = true;
	}

	public override void RemoveWallWithTrigger(GameObject collidingwithobject){
		_PersonalNodeMap.AddGameobjectsWithinTrigger (collidingwithobject);
		UpdateThePath = true;
	}

	public override void AddEnemyWithTrigger (GameObject collidingwithobject){
		_PersonalNodeMap.AddEnemyPositions (collidingwithobject.gameObject);
		UpdateThePath = true;
	}

	public override void RemoveEnemyWithTrigger (GameObject collidingwithobject){
		_PersonalNodeMap.RemoveEnemyPositions (collidingwithobject.gameObject);
		UpdateThePath = true;
	}

	public override void RemoveFromOthers(){
		_PersonalNodeMap.RemoveMyselfFromOthers (gameObject);
	}

	#endregion

	public override void AttackTarget(){
		if (thetype == EnemyType.Ranged) {
			if(CanIRanged[0] == true)
				(Instantiate (Bullet, new Vector3 (transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject).GetComponent<BulletStart> ().SetParent (transform.gameObject, _GoAfter);
		} else if(thetype == EnemyType.Meele){
			_GoAfter.GetComponent<DefaultBehaviour> ().RecievedDmg ();
		}
	}

	public override void RecievedDmg(){
	//	Debug.Log ("I Got Hit :" + name);
	}

	/*public bool ShowGizmos = false;
	void OnDrawGizmos(){

		if (ShowGizmos) {
			Nodes[,] mynodes = _PersonalNodeMap.GetNodemap ();
			for (int x = 0; x < 6 * 2 + 1; x++) {
				for (int y = 0; y < 6 * 2 + 1; y++) {
					if (mynodes [x, y].GetCollision () == 1) {
						Gizmos.color = Color.black;
					} else if (mynodes [x, y].GetCollision () == 100) {
						Gizmos.color = Color.blue;
					} else {
						Gizmos.color = Color.yellow;

					}
					Gizmos.DrawCube (new Vector3 ((mynodes [x, y].GetID () [0, 0]) + 20, (mynodes [x, y].GetID () [0, 1]) + 20, 0), new Vector3 (0.5f, 0.5f, 0.5f));
				}
			}
			Gizmos.color = Color.white;
			Gizmos.DrawCube (new Vector3 ((mynodes [0, 0].GetID () [0, 0])  + 20, (mynodes [0, 0].GetID () [0, 1]) + 20, 0), new Vector3 (0.5f, 0.5f, 0.5f));
			Gizmos.color = Color.yellow;
			Gizmos.DrawCube (new Vector3 ((mynodes [0, 1].GetID () [0, 0]) + 20, (mynodes [0, 1].GetID () [0, 1]) + 20, 0), new Vector3 (0.5f, 0.5f, 0.5f));




			Nodes[] mynodess = _PersonalNodeMap.GetNodeList();
			int[] count = _PersonalNodeMap.GetNodeindex ();
			for(int sas = count[0]; sas < mynodess.Length; sas++){
				Gizmos.color = Color.red;
				Gizmos.DrawCube (new Vector3 (mynodess[sas].GetID()[0,0] + 20, mynodess[sas].GetID()[0,1] + 20, 0), new Vector3 (0.5f, 0.5f, 0.5f));
			}

			mynodes = _PersonalNodeMap.GetNodemap ();
		}
	}*/
}

