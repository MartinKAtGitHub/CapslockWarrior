using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MovingCreatures : DefaultBehaviour {

	public BoxCollider2D FeetPlacements;//Tells The AI Where The Object Is And Where To Now Walk
	public BoxCollider2D WalkingColliders;//Used For The AI To Calculate The Node Path Towards An Object

	public override void SetAiRoom(Wall_ID room){//just called once, and that is when spawning an object
		base.SetAiRoom(room);
		ObjectBehaviour._CreateThePath.SetStartRoom (room.Connectors);
	}

	public override void SetNeighbourGroup(List<RoomConnectorCreating> neighbours){//When The Object Enters A New Room This Updates The Nodes
		NeighbourGroups = neighbours;
		ObjectBehaviour._CreateThePath.SetStartRoom (neighbours);
	}

	public override void SetTarget(GameObject target){
		base.SetTarget(target);
		ObjectBehaviour._CreateThePath.SetEndRoomAndNode (_TheTarget[0].NeighbourGroups, _TheTarget[0].MyNode);
	}

	#region What to do when colliding with objects  

	public void AddStaticObject(BoxCollider2D collidingwithobject){
		ObjectBehaviour._PersonalNodeMap.AddWalls (collidingwithobject);
	}

	public void RemoveStaticObjects(BoxCollider2D collidingwithobject){
		ObjectBehaviour._PersonalNodeMap.RemoveWalls (collidingwithobject);
	}

	public void AddEnemy (BoxCollider2D collidingwithobject){
		ObjectBehaviour._PersonalNodeMap.AddEnemyPositions (collidingwithobject);
	}

	public void RemoveEnemy (BoxCollider2D collidingwithobject){
		ObjectBehaviour._PersonalNodeMap.RemoveEnemyPositions (collidingwithobject);
	}

	public void RemoveMyselfFromOthers(){
		List<BoxCollider2D> enemyinside = ObjectBehaviour._PersonalNodeMap.GetEnemyColliders ();

		for (int i = 0; i < enemyinside.Count; i++) {
			if(enemyinside [i] != null)
				enemyinside [i].GetComponent<MovingCreatures> ().RemoveEnemy (FeetPlacements);
		}
	}

	#endregion

}
