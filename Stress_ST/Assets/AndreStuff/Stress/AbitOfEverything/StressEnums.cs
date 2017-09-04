using UnityEngine;
using System.Collections;

public class StressEnums {
	
	public enum BulletStyle {AttachToSender = 0, AttachToTarget = 1, PlaceOnGroundAtVector = 2}
	public enum BulletCast {RektCast = 0, CircleCast = 1, LineCast = 2}

	public enum BulletPlacementBehaviour {AttackPosition = 0, FollowRotation = 1, FollowWalkingVector = 2}
	public enum BulletRotationBehaviour {NoRotation = 0, CopyPlayersRotation = 1, AttackPositionRotation = 2 , SenderTargetVectorRotation = 3}



	public enum EnemyType {Ranged, Meele, Fast, Tank};//the state define what the object does when attacking(mainly)

	public enum NodeSizes{	One = 1, Half = 2, OneForth = 4, OneEigth = 8	}

}
