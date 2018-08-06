using UnityEngine;
using System.Collections;

public class StressCommonlyUsedInfo {

	public static NodeWalkcostSetter TheSetter = new NodeWalkcostSetter();



	public const int NodesInUnityMeter = 20;
	public const float DistanceBetweenNodes = 0.05f;
	public const int NodesWidth = (int)(NodesInUnityMeter / DistanceBetweenNodes);
//TODO Make This Work. A Map Does Not Have To Be A Perfect Square. TBD A Small Train Kinda Map... 	public const int NodesHeight = (int)(NodesInUnityMeter / DistanceBetweenNodes);
	public const int NodesTotal = NodesWidth * NodesWidth;
	public const int Nodes3D = 10;
	public const int AmountOfDifferentNodes = 10;
	public const int PathCostSize = 3;

	public const float LowestXPos = -1f;//TODO Make This The Bottom Left Of The Map. X Cord
	public const float LowestYPos = -0.25f;//TODO Make This The Bottom Left Of The Map. Y Cord


	public enum BulletStyle {AttachToSender = 0, AttachToTarget = 1, PlaceOnGroundAtVector = 2}
	public enum BulletCast {RektCast = 0, CircleCast = 1, LineCast = 2}

	public enum BulletPlacementBehaviour {AttackPosition = 0, FollowRotation = 1, FollowWalkingVector = 2}
	public enum BulletRotationBehaviour {NoRotation = 0, CopyPlayersRotation = 1, AttackPositionRotation = 2 , SenderTargetVectorRotation = 3}

	public enum WordDifficulty {Easy = 0, EasyPluss = 1, Normal = 2, NormalPluss = 3, Hard = 4, HardPluss = 5, Insane = 6, Random = 7 }

	public enum EnemyType {Ranged, Meele, Fast, Tank};//the state define what the object does when attacking(mainly)

	public enum NodeSizes{	One = 1, Half = 2, OneForth = 4, OneEigth = 8	}

	[System.Serializable]
	public struct TheAbility{

		[Tooltip("This Is The Criteria Which The Spell Needs To Be Used/Initialized")]
		public SpellRoot SpellInfo;
		[Tooltip("The Spell Refrence. Just In Case Of An Unexpected Overlap Of Abilities")]
		public The_Default_Bullet SpellRef;
		[Tooltip("The Cooldown Of The Spell")]
		public float SpellCD;
		[Tooltip("Where Does The Spell Spawn From The Center Of The Object And Looking Vector.Right")]
		public Vector3 SpawnPosition;
		[Tooltip("How Much Energy Does The Ability Need")]
		public int AbilityCost;
		[Tooltip("If You Need Info About The Different Variables In The Array, Go To The Spell You Want To Add These Values To And Look At The Very Top Of The Script For More Info")]
		public float[] SpellVariables;
		[HideInInspector]
		public float SpellCurrentCD;

	}

	[System.Serializable]
	public struct TransitionGrouped{

		public TheAbility[] AllAbilities;

	}

}


[System.Serializable]
public struct TheAbility{

	public SpellRoot SpellInfo;
	public The_Default_Bullet SpellRef;
	public float SpellCD;
	public Vector3 SpawnPosition;
	public int AbilityCost;
	public float[] SpellVariables;
	public float SpellCurrentCD;

}