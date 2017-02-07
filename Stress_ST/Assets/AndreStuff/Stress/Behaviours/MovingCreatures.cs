using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MovingCreatures : DefaultBehaviour {

	public FSM_Manager MovementFSM; // Needed for AI
	public AStarPathfinding_RoomPaths _CreateThePath;// Needed for AI
	public CreatingObjectNodeMap _PersonalNodeMap;// Needed for AI

	[HideInInspector] public List<RoomConnectorCreating> NeighbourGroups = new List<RoomConnectorCreating>();//all objects that are moving need to know in which room they are
	[HideInInspector] public GameObject _GoAfter = null;//the target


	[HideInInspector] public bool UpdateThePath = false;//when this is true the pathfinding will run
	public bool RunPathfinding = true;//if true then the target is using the pathfining

	public enum EnemyType {Ranged, Meele, Fast, Tank};//the state define what the object does when attacking(mainly)
	public EnemyType thetype;

	public float[,] myPos = new float[1,2];//used to update the position for the Objects node position 


	[Tooltip("PathfindingNodeID is the cost to move to the different nodes //// 0 = normal nodes //// 1 = undestructable walls //// 2 = other units")]
	public int[] PathfindingNodeID = new int[3];//when going through the nodemap the this is the value for the different tiles when navigating

	public float AttackRange = 1;

	public bool[] CanIRanged = new bool[1];//if true then i can shoot
	public GameObject Bullet;//these two are needed for ranged creatures

	public LayerMask LineOfSight;//this is what the physics2d.linecast cant hit. if it does move closer

	public virtual MovingCreatures GetTargetBehaviour(){
		if (_GoAfter != null)
			return _GoAfter.GetComponent<MovingCreatures>();

		return null;
	}

	public virtual void SetTarget(GameObject target){
		_GoAfter = target;
		Debug.Log (_GoAfter.GetComponent<MovingCreatures>().NeighbourGroups);
		_CreateThePath.SetEndRoomAndNode (_GoAfter.GetComponent<MovingCreatures>().NeighbourGroups, _GoAfter.GetComponent<MovingCreatures>()._CreateThePath.GetPosNode());
	}

	public virtual void SetAiRoom(Wall_ID room){//just called once, and that is when spawning an object
		NeighbourGroups = room.Connectors;
		_CreateThePath.SetStartRoom (room.Connectors);
	}

	public virtual void SetNeighbourGroup(List<RoomConnectorCreating> neighbours){
		NeighbourGroups = neighbours;
		_CreateThePath.SetStartRoom (neighbours);
	}




	public abstract void AddWallWithTrigger (GameObject collidingwithobject);//TODO
	public abstract void RemoveWallWithTrigger (GameObject collidingwithobject);//TODO
	public abstract void AddEnemyWithTrigger (GameObject collidingwithobject);//TODO
	public abstract void RemoveEnemyWithTrigger (GameObject collidingwithobject);//TODO
	public abstract void RemoveFromOthers ();




}
