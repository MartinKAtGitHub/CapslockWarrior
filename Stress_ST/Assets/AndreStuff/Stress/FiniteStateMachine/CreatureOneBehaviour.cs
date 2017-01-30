using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class CreatureOneBehaviour : DefaultBehaviour {

	private FSM_Manager MovementFSM, AttackFSM; // dette er Final State Machinene jeg bruker, en for bevegelse av hjulene/beltene og en for radaren. jeg lagde ikke en for kanonen siden den bare hadde en state

	AStarPathfinding_RoomPaths _CreateThePath = new AStarPathfinding_RoomPaths();
	CreatingObjectNodeMap _PersonalNodeMap;

	Nodes CehckingNOde;

	[Tooltip("PathfindingNodeID is the cost to move to the different nodes //// 0 = normal nodes //// 1 = undestructable walls //// 2 = other units")]
	public int[] PathfindingNodeID = new int[3];


	public bool RunPathfinding = true;

	public override void OnDestroyed(){}

	public EnemyType thetype;

	public float CreatureRange = 1;

	public bool[] CanIRanged = new bool[1];
	public GameObject Bullet;
	LayerMask LineOfSight;

	void Awake(){
		
		myPos [0, 0] = transform.position.x;
		myPos [0, 1] = transform.position.y;	

		_PersonalNodeMap = new CreatingObjectNodeMap(13,13, PathfindingNodeID);//if i do this then the constructor is only called once instead of 4 times......
		MyNodePosition[0] = new Nodes (myPos,0);
		LineOfSight = 1 << LayerMask.NameToLayer ("Walls");

	}

	void Start(){
		GetComponent<Rigidbody2D> ().drag = 0;

		_PersonalNodeMap.CreateNodeMap ();
		_PersonalNodeMap.SetCenterPos (myPos);

		SetTarget (GameObject.FindGameObjectWithTag ("Player1"));
	//	Debug.Log ("Setting GameObject.FindGameObjectWithTag (\"Player1\") Remember to remove this later");

		if (RunPathfinding == true) {
			if (thetype == EnemyType.Rangd) {//makes it possible to add different states to the targets if that is needed
				MovementFSM = new FSM_Manager( new DefaultState[] { new WalkToTarget(_PersonalNodeMap, _CreateThePath, this, CreatureRange, MovementSpeed), new GolemAttackState(this,CanIRanged, CreatureRange),} );//definerer alle statene for de spesifikke fsm'ene. og den første i dette tilfeller "FindTarget og AttackState" vil bli kalt først/default state
				MovementFSM.Init(this);
			}else if(thetype == EnemyType.Melle){
				MovementFSM = new FSM_Manager( new DefaultState[] { new WalkToTarget(_PersonalNodeMap, _CreateThePath, this, CreatureRange, MovementSpeed), new GolemAttackState(this,CanIRanged, CreatureRange),} );//definerer alle statene for de spesifikke fsm'ene. og den første i dette tilfeller "FindTarget og AttackState" vil bli kalt først/default state
				MovementFSM.Init(this);
			}
		}
		Turnoffwithforcestuff = false;
	}

	bool testing = false;
	bool once = false;



	void FixedUpdate (){//this is called at set intevals, and the update is calling the statemachine after the fixedupdate have updated the colliders
		testing = true;
		myPos [0, 0] = transform.position.x;
		myPos [0, 1] = transform.position.y;
		if (RunPathfinding == true) {
			if (Turnoffwithforcestuff == true) {
				if (GetComponent<Rigidbody2D> ().velocity.magnitude < 0.01f) {
					Turnoffwithforcestuff = false;
					GetComponent<Rigidbody2D> ().drag = 0;
					MovementFSM.TheUpdate ();//oppdaterer fsm'ene
				}else{
					GetComponent<Rigidbody2D> ().drag = 10;
				}
			} else {
				MovementFSM.TheUpdate ();//oppdaterer fsm'ene
			}
		}

		if (once == false) {
			once = true;
		}

	}
	/*void Update(){
		if (RunPathfinding == true && testing == true) {
			testing = false;
		}else if(RunPathfinding == true && testing == false){
			if (Turnoffwithforcestuff == true) {
				if(GetComponent<Rigidbody2D>().velocity.magnitude < 0.01f)
				{
					Turnoffwithforcestuff = false;
				}
			} else {
				Debug.Log ("HERERERERERERE");
			}
		}
	}
*/
	public override void SetTarget(GameObject target){//if you want to change target, use this, TODO make it so that its possible to only send the node
		_GoAfter = target;
		UpdateTargetRoomAndNode ();
	} 

	void UpdateTargetRoomAndNode(){//sending the endpoints to the A*
		_CreateThePath.SetEndRoomAndNode (_GoAfter.GetComponent<DefaultBehaviour>().NeighbourGroups, _GoAfter.GetComponent<DefaultBehaviour>().GetMyNode());
	}

	#region Collision functions
	public override void SetNeighbourGroup(List<RoomConnectorCreating> neighbours){
		NeighbourGroups = neighbours;
		_CreateThePath.SetStartRoomAndNode (neighbours, MyNodePosition);

	}
	const String a = "Wall";
	const String b = "Enemy"; 
	const String c = "Spells"; 

	void OnTriggerEnter2D(Collider2D coll){//sends info to the objects personal nodemap (it also trigger on colliders that doesnt have istrigged toggled)
		if (coll.gameObject.CompareTag (a)) {
			_PersonalNodeMap.AddGameobjectsWithinTrigger (coll.gameObject);
			UpdateThePath = true;
		} else if (coll.gameObject.CompareTag (b)) {
			_PersonalNodeMap.AddEnemyPositions (coll.gameObject);
		} 
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.CompareTag (c)) {
			Debug.Log ("Collided  not trigger ");
		}
	}

	void OnTriggerExit2D(Collider2D coll) {//sends info to the objects personal nodemap (it also trigger on colliders that doesnt have istrigged toggled)

		if (coll.gameObject.CompareTag(a)) {
			_PersonalNodeMap.RemoveGameobjectsWithinTrigger (coll.gameObject);
			UpdateThePath = true;
		} else if (coll.gameObject.CompareTag(b)) {
			_PersonalNodeMap.RemoveEnemyPositions (coll.gameObject);
		}


	}

	#endregion

	public override void AttackTarget(){
		if (thetype == EnemyType.Rangd) {
			(Instantiate (Bullet, new Vector3 (transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject).GetComponent<BulletStart> ().SetParent (transform.GetChild(0).gameObject);
		} else if(thetype == EnemyType.Melle){
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

