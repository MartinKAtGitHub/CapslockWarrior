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

	public bool StartPathfinding = false;
	public bool RunPathfinding = true;
	public bool HaveIChangedRoom = false;

	public override void OnDestroyed(){}

	void Awake(){

		myPos [0, 0] = transform.position.x;
		myPos [0, 1] = transform.position.y;	

		_PersonalNodeMap = new CreatingObjectNodeMap(13,13);//if i do this then the constructor is only called once instead of 4 times......
		MyNodePosition[0] = new Nodes (myPos,0);
	}

	void Start(){
		_PersonalNodeMap.CreateNodeMap ();
		_PersonalNodeMap.SetCenterPos (myPos);

		SetTarget (GameObject.FindGameObjectWithTag ("Player1"));
	//	Debug.Log ("Setting GameObject.FindGameObjectWithTag (\"Player1\") Remember to remove this later");

		if (RunPathfinding == true) {

		MovementFSM = new FSM_Manager( new DefaultState[] { new WalkToTarget(_PersonalNodeMap, _CreateThePath, this), new GolemAttackState(this), } );//definerer alle statene for de spesifikke fsm'ene. og den første i dette tilfeller "FindTarget og AttackState" vil bli kalt først/default state
		MovementFSM.Init(this);
		
		}

	}

	bool testing = false;
	void FixedUpdate (){
		testing = true;
		myPos [0, 0] = transform.position.x;
		myPos [0, 1] = transform.position.y;
	}

	void Update(){
		if (RunPathfinding == true && testing == true) {
			testing = false;
		}else if(RunPathfinding == true && testing == false){
			MovementFSM.TheUpdate();//oppdaterer fsm'ene
		}
		
	}

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

	void OnTriggerEnter2D(Collider2D coll) {//sends info to the objects personal nodemap (it also trigger on colliders that doesnt have istrigged toggled)
		if (coll.gameObject.CompareTag(a)) {
			_PersonalNodeMap.AddGameobjectsWithinTrigger (coll.gameObject);
			UpdateThePath = true;
		} else if (coll.gameObject.CompareTag(b)) {
			_PersonalNodeMap.AddEnemyPositions (coll.gameObject);
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
/*		public bool tru = false;

	void OnDrawGizmos(){

		Nodes[,] mynodes = _PersonalNodeMap.GetNodemap ();
		if (tru) {
			for (int x = 0; x < 6 * 2 + 1; x++) {
				for (int y = 0; y < 6 * 2 + 1; y++) {
					if (mynodes [x,y].GetCollision () == 1) {
						Gizmos.color = Color.black;
					}else if (mynodes [x,y].GetCollision () == 100) {
						Gizmos.color = Color.blue;
					} else {
						Gizmos.color = Color.yellow;
					
					}
					Gizmos.DrawCube (new Vector3 ((mynodes [x,y].GetID () [0, 0]),(mynodes [x,y].GetID () [0, 1]), 0), new Vector3 (0.5f, 0.5f, 0.5f));
				}
			}
		Gizmos.color = Color.white;
		Gizmos.DrawCube (new Vector3 ((mynodes [0,0].GetID () [0, 0]),(mynodes [0,0].GetID () [0, 1]), 0), new Vector3 (0.5f, 0.5f, 0.5f));
		Gizmos.color = Color.yellow;
		Gizmos.DrawCube (new Vector3 ((mynodes [0,1].GetID () [0, 0]),(mynodes [0,1].GetID () [0, 1]), 0), new Vector3 (0.5f, 0.5f, 0.5f));


		mynodes = _PersonalNodeMap.GetNodemap ()[6,6].NeighbourNodes;
			for (int x = 0; x < 3; x++) {
				for (int y = 0; y < 3; y++) {
					if (x == 1 && y == 1) {
					} else {
						if (x == 0 && y == 0) {
							Gizmos.color = Color.green;
						} else if (x == 1 && y == 0)
							Gizmos.color = Color.red;
						else
							Gizmos.color = Color.yellow;

						Gizmos.DrawCube (new Vector3 ((mynodes [x, y].GetID () [0, 0]), (mynodes [x, y].GetID () [0, 1]), 0), new Vector3 (0.5f, 0.5f, 0.5f));
					}
				}
			}



		 mynodes = _PersonalNodeMap.GetNodemap ();
		}*/


	//	Debug.Log (mynodes [0,0].GetID () [0, 0] + " | " + mynodes [0,0].GetID () [0, 1]);
	//	Debug.Log (mynodes [0,1].GetID () [0, 0] + " | " + mynodes [0,1].GetID () [0, 1]);
	//	Debug.Log (mynodes [0,2].GetID () [0, 0] + " | " + mynodes [0,2].GetID () [0, 1]);
	//}
}

