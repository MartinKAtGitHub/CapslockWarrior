using UnityEngine;
using System.Collections;

public class StressCommonlyUsedInfo {

	public const int UnityMeterToCover = 5;//This Is Nodes To Fill In Unity Meter, This Need To Be An Odd Number. 3-5-7-9.....
	public const float DistanceBetweenNodes = 0.0625f;//== 1 / 0.0625 == 16.    That Means 16 Nodes Each Unity Meter. //IMPORTANT The WalkingCollider On Moving Object Needs To Be - (NodeDistance / 2) xy To Compensate For And Even Number Of Nodes
	public const int NodeDimentions = (int)(UnityMeterToCover / DistanceBetweenNodes); 
	public const int TotalNodes = NodeDimentions * NodeDimentions; 

	public const int PathCostSize = 3;




	public enum BulletStyle {AttachToSender = 0, AttachToTarget = 1, PlaceOnGroundAtVector = 2}
	public enum BulletCast {RektCast = 0, CircleCast = 1, LineCast = 2}

	public enum BulletPlacementBehaviour {AttackPosition = 0, FollowRotation = 1, FollowWalkingVector = 2}
	public enum BulletRotationBehaviour {NoRotation = 0, CopyPlayersRotation = 1, AttackPositionRotation = 2 , SenderTargetVectorRotation = 3}

	public enum WordDifficulty {Easy = 0, EasyPluss = 1, Normal = 2, NormalPluss = 3, Hard = 4, HardPluss = 5, Insane = 6, Random = 7 }

	public enum EnemyType {Ranged, Meele, Fast, Tank};//the state define what the object does when attacking(mainly)

	public enum NodeSizes{	One = 1, Half = 2, OneForth = 4, OneEigth = 8	}

	[System.Serializable]
	public struct TheAbility{

		public float SpellCD;
		[HideInInspector]
		public float SpellCurrentCD;
		public SpellRoot SpellInfo;

	}

	[System.Serializable]
	public struct TransitionGrouped{

		public TheAbility[] AllAbilities;

	}

}
