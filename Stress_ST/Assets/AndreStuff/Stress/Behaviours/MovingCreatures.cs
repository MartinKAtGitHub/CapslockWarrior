using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MovingCreatures : DefaultBehaviour {

	public FSM_Manager MovementFSM; // Needed for AI
	public AStarPathfinding_RoomPaths _CreateThePath;// Needed for AI
	public CreatingObjectNodeMap _PersonalNodeMap;// Needed for AI

	[HideInInspector] public bool UpdateThePath = false;//when this is true the pathfinding will run
	public bool RunPathfinding = true;//if true then the target is using the pathfining


	[Tooltip("PathfindingNodeID is the cost to move to the different nodes //// 0 = normal nodes(1) //// 1 = undestructable walls(100) //// 2 = other units(3)")]
	public float[] PathfindingNodeID = new float[3];//when going through the nodemap the this is the value for the different tiles when navigating

	public float AttackRange = 1;

	public bool[] CanIRanged = new bool[1];//if true then i can shoot
	public GameObject Bullet;//these two are needed for ranged creatures

	public LayerMask LineOfSight;//this is what the physics2d.linecast cant hit. if it does move closer



	public override void SetAiRoom(Wall_ID room){//just called once, and that is when spawning an object
		base.SetAiRoom(room);
		_CreateThePath.SetStartRoom (room.Connectors);
	}

	public override void SetNeighbourGroup(List<RoomConnectorCreating> neighbours){
		NeighbourGroups = neighbours;
		_CreateThePath.SetStartRoom (neighbours);
	}

	public override void SetTarget(GameObject target){
		base.SetTarget(target);
		_CreateThePath.SetEndRoomAndNode (DefaultBehaviourTarget.NeighbourGroups, DefaultBehaviourTarget.GetMyNode());
	}

	#region What to do when colliding with objects  
	//TODO improve this so that i only have to call one method for all objects

	public void AddWallWithTrigger(GameObject collidingwithobject){
		_PersonalNodeMap.AddWalls (collidingwithobject);
		UpdateThePath = true;
	}

	public void RemoveWallWithTrigger(GameObject collidingwithobject){
		_PersonalNodeMap.RemoveWalls (collidingwithobject);
		UpdateThePath = true;
	}

	public void AddEnemy (GameObject collidingwithobject){
		if (collidingwithobject != gameObject) {
			_PersonalNodeMap.AddEnemyPositions (collidingwithobject.gameObject);
			UpdateThePath = true;
		}
	}

	public void RemoveEnemy (GameObject collidingwithobject){
		_PersonalNodeMap.RemoveEnemyPositions (collidingwithobject.gameObject);
		UpdateThePath = true;
	}

	public void RemoveMyselfFromOthers(){
		List<BoxCollider2D> enemyinside = _PersonalNodeMap.GetEnemyColliders ();

		for (int i = 0; i < enemyinside.Count; i++) {
			if(enemyinside [i] != null)
				enemyinside [i].GetComponent<MovingCreatures> ().RemoveEnemy (gameObject);
		}
	}

	#endregion

}
