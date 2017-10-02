using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsWithMovementAndWords : ObjectWords {
	//TODO TheColliderNeedsToBeDisabledBeforeDestroyed

	public BoxCollider2D FeetPlacements;//Tells The AI Where The Object Is And Where To Now Walk
	public BoxCollider2D WalkingColliders;//Used For The AI To Calculate The Node Path Towards An Object


	public override void SetAiRoom(Wall_ID room){//just called once, and that is when spawning an object
		NeighbourGroups = room.Connectors;
		TheObject._CreateThePath.SetStartRoom (room.Connectors);
	}

	public override void SetNeighbourGroup(List<RoomConnectorCreating> neighbours){//When The Object Enters A New Room This Updates The Nodes
		NeighbourGroups = neighbours;
		TheObject._CreateThePath.SetStartRoom (neighbours);
	}

	public override void SetTarget(GameObject _target){
		base.SetTarget (_target);
		TheObject._CreateThePath.SetEndRoomAndNode (TheObject._TheTarget.NeighbourGroups, TheObject._TheTarget.MyNode);
		TheObject._PersonalNodeMap.SetTargetPos (TheObject._TheTarget.MyPos);
		TheObject.GotPushed = true;
	}

	public override void RemoveMyselfFromOthers (){
		WalkingColliders.enabled = false;
		FeetPlacements.enabled = false;
	}

}
