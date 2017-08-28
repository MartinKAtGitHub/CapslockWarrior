using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MovingCreatures : DefaultBehaviour {

	public FSM_Manager MovementFSM; // Needed for AI
	public AStarPathfinding_RoomPaths _CreateThePath;// Needed for AI
	public CreatingObjectNodeMap _PersonalNodeMap;// Needed for AI

	[HideInInspector] public bool RoomPathUpdate = false;//when this is true the pathfinding will run
	[HideInInspector] public bool AStarSearchUpdate = false;//when this is true the pathfinding will run

	public bool RunObjectBehaviours = true;//if true then the target is using the pathfining

	public float AttackRange = 1;

	public bool[] CanIRanged = new bool[1];//if true then i can shoot
	public GameObject Bullet;//these two are needed for ranged creatures

	public LayerMask LineOfSight;//this is what the physics2d.linecast cant hit. if it does move closer

	public override void SetAiRoom(Wall_ID room){//just called once, and that is when spawning an object
		base.SetAiRoom(room);
		ObjectBehaviour._CreateThePath.SetStartRoom (room.Connectors);
		RoomPathUpdate = true;
	}

	public override void SetNeighbourGroup(List<RoomConnectorCreating> neighbours){//When The Object Enters A New Room This Updates The Nodes
		NeighbourGroups = neighbours;
		ObjectBehaviour._CreateThePath.SetStartRoom (neighbours);
		RoomPathUpdate = true;
	}

	public override void SetTarget(GameObject target){
		base.SetTarget(target);
		ObjectBehaviour._CreateThePath.SetEndRoomAndNode (_TheTarget.NeighbourGroups, _TheTarget.MyNode);
	}

	#region What to do when colliding with objects  

	public void AddStaticObject(GameObject collidingwithobject){
		ObjectBehaviour._PersonalNodeMap.AddWalls (collidingwithobject);
		AStarSearchUpdate = true;
	}

	public void RemoveStaticObjects(GameObject collidingwithobject){
		ObjectBehaviour._PersonalNodeMap.RemoveWalls (collidingwithobject);
		AStarSearchUpdate = true;
	}

	public void AddEnemy (GameObject collidingwithobject){
		ObjectBehaviour._PersonalNodeMap.AddEnemyPositions (collidingwithobject.gameObject);
		AStarSearchUpdate = true;
	}

	public void RemoveEnemy (GameObject collidingwithobject){
		ObjectBehaviour._PersonalNodeMap.RemoveEnemyPositions (collidingwithobject.gameObject);
		AStarSearchUpdate = true;
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
