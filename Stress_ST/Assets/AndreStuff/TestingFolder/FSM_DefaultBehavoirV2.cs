using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class FSM_DefaultBehavoirV2 {



	public enum TheMovementVector { FollowNodePathToTarget = 0, GoStraightToTarget = 1, LockMovementDirectionToTarget = 2, LockMovementDirectionToNextNode = 3, LockCurrentVector = 4 }
	//teleport movevalues 0 = distance to move
	//stop movevalues 0 = how long to stand stil, 1 = the index for the time array, 
	//walk movevelues 0 = movementspeed, 1 = movement distance to travel, 2 = the distance traveled 
	//timemove movevalues 0 = time to move, 1 = the index for the time array, 2 = movementspeed
	public enum TheMovementBehaviours { TeleportDistance = 6, Stop = 7, Walk = 8, TimedMovement = 9 }
	public enum TheAttackType {AttackspeedDesideAttack = 10,  CircleCast = 12, RektangleCast = 13, OnCollisionHit = 5, AnimationDecideWhenToAttack = 22 }
	public enum ExitReuirement { Nothing = 15, Collision = 16, Time = 17, DistanceToTargetLessThen = 18, DistanceToTargetMoreThen = 19, RayCastHitWall = 20, RayCastClearPathToTarget = 21 }


	[Tooltip("If You're Going To Attack Then 'True'. If Not Then 'False'. 'False' == Only Movement")]
	public bool AttackOrMove;
	[Tooltip("If The Object Have Some Logic That It Has To Do Then When That Is Done It Can Change State. The 'True'")]
	public bool FinishBehaviourToExit;
	[Tooltip("Vector Index [0] Is Reserved For The MovementVector.\n\nRektCast Uses [1] For Rektangle Size")]
	public Vector3[] Vectors;

	public TheAttackAndMovementBehaviour AttackAndMove;
	public TheMovementBehaviour[] Move;
	[Tooltip("How Many Different Requirement-Goups Do You Want\nA Group Is If You Want To Have An '&&'. Which Means That Both Requirements Must Be Met")]
	public TheExitRequirements[] ExitRequirements;

	[System.Serializable]
	public struct TheAttackAndMovementBehaviour{
		public TheAttackBehaviour[] TheAttack;
		public TheAttackMovement[] TheAttackMovement;

	}

	[System.Serializable]
	public struct SpellAttackInfo{
		public GameObject Spell;
		public float AttackMovement;
		public float AttackDmg;
		public float AttackRange;
		public Vector3[] NeededVectors;
	}

	[System.Serializable]
	public struct TheAttackBehaviour{//The Attack Behaviour Does Not Rotate, Only Movement Does. So If You Want It To Rotate Then Use The Movement To Change The Attack
		[HideInInspector]
		public int AttackBehaviourIndex;//Used To Store The Index To The FunctionPointerIndex 
		public TheAttackType AttackBehaviour;
		public SpellAttackInfo SpellInfo;

		[Tooltip("OnCollisionHit Size 1, [0] = Dmg.\n\nAttackspeed Deside Attack Size 1, [0] = AttackSpeed.\n\nSpellCastOnce Size 1, [0] = AttackSpeed.\n\nAnimationDecidesWhenToAttack Size 0.\n\nOnlyDebugToShow+UsedIfAnimationsHaveBuiltInAttack\nRektCast Size 1, [0] AttackSpeed\nCircleCast Size 2, [0] = AttackSpeed, [1] = Radius")]
		public float[] AttackValues;
		[Tooltip("Currently Used For LineCast CircleCast And RektCast")]
		public LayerMask WhatCanIHit;

		[Tooltip("Only Used For SpellCast..\nThis Is Used To Tell In Which AttackMovement You Can Attack.\n\nIf You Have 4 Movement Behaviours, And Want to Attack In Behaviour 0 And 3. Then You Make The Size [2] And 0 and 3 in [0] and [2]")]
		public int[] MovementIndexToAttack;
	}

	[System.Serializable]
	public struct TheAttackMovement{
		[HideInInspector]
		public int AttackMovementBehaviourIndex; //Used To Store The Index To The FunctionPointerIndex 
		[HideInInspector]
		public int AttackMovementStateIndex;//Used To Store The Index To The FunctionPointerIndex 

		public TheMovementVector AttackMovementBehaviour;
		public TheMovementBehaviours AttackMovementState;

		[Tooltip("Teleport Size 1:\n[0] == Distance To Teleport.\n\nStop Size 1:\n[0] == Stop Time.\n\nWalk Size 3:\n[0] == MovementSpeed.\n[1] == Distance To Walk.\n[2] == Distance Walked.\n\nTimed Movement Size 2:\n[0] == Time To Walk.\n[1] == Movementspeed.")]
		public float[] MovementValues;

		[Tooltip("If True Then You Want To Change The Attack Behaviour. Which Happends The Moment This MovementBehaviour Activates")]
		public bool ChangeAttackIndex;
		public int AttackIndexValue;
		[Tooltip("If True Then You Want To Change The Animation. Which Happends The Moment This MovementBehaviour Activates")]
		public bool ChangeAnimationStage;
		public float AnimationStageValue;
		[Tooltip("If You Want To Rotate While Attacking. The Animator Also Contols This Behaviour, And This Is The Stage Below So The Animator Will Override This")]
		public bool RotateWhileAttacking;
		[Tooltip("If An AttackBehaviour Is Limited To Only Attack Once, Then This Can Override That And Make It Attack Again")]
		public bool ICanAttackAgain;
	}

	[System.Serializable]
	public struct TheMovementBehaviour{
		[HideInInspector]
		public int MovementBehaviourIndex;//Used To Store The Index To The FunctionPointerIndex 
		[HideInInspector]
		public int MovementStateIndex;//Used To Store The Index To The FunctionPointerIndex 

		public TheMovementVector MovementBehaviour;
		public TheMovementBehaviours MovementState;

		[Tooltip("Teleport Size 1:\n[0] == Distance To Teleport.\n\nStop Size 1:\n[0] == Stop Time.\n\nWalk Size 3:\n[0] == MovementSpeed.\n[1] == Distance To Walk.\n[2] == Distance Walked.\n\nTimed Movement Size 2:\n[0] == Time To Walk.\n[1] == Movementspeed.")]
		public float[] MovementValues;
	
		[Tooltip("If True Then You Want To Change That Animation Parameter")]
		public bool ChangeAnimationStage;
		public float AnimationStageValue;
		[Tooltip("Look At Target While Moving. The Animator Can Override This With Its LookDirection Parameter")]
		public bool LookAtTarget;
	}


	[System.Serializable]
	public struct TheExitRequirements {
		[HideInInspector]
		public int[] RequirementIndex;//Used For The FunctionPointer
		[HideInInspector]
		public int[] TimeSavedIndex;//Used To Save The Time In MyTime[TimeSavedIndex] Which Get The Time To Exit
	
		[Tooltip("How Many Different Requirement Do You Want, If More Then 1 Then It Checks If Both Are True, Then It Changes To The Requirement StateIndexValue")]
		public ExitReuirement[] Requirements;

		[Tooltip("Values Need To Align With The Requirements.Length. If Time Is Requirements[4], Then In ValueRequirements[4] Have The Value To Exit")]
		public float[] ValueRequirements;

		[Tooltip("Which Element In The Array Do You Want To Change Too")]
		public int ChangeToBehaviour;

	}
}
