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
	public enum TheAttackType {SpellCastMultipleTimes = 10, SpellCastOnce = 11, CircleCast = 12, RektangleCast = 13, LineCast = 14, OnCollisionHit = 5 }
	public enum ExitReuirement { Nothing = 15, Collision = 16, Time = 17, DistanceToTargetLessThen = 18, DistanceToTargetMoreThen = 19, RayCastHit = 20, RayCastMissed = 21 }


	[Tooltip("If You're Going To Attack Then 'True'. If Not Then 'False'. 'False' == Only Movement")]
	public bool AttackOrMove;
	public bool FinishBehaviourToExit;
	[Tooltip("Vector size == Currently 1")]
	public Vector3[] Vectors;

	public TheAttackAndMovementBehaviour AttackAndMove;
	public TheMovementBehaviour[] Move;
	[Tooltip("How Many Different Requirement-Goups Do You Want\nA Group Is If You Want To Have An '&&'. Which Means That Both Needs To Be True")]
	public TheExitRequirements[] ExitRequirements;


	public void FixTheIndexes(){
		if (AttackOrMove == true) {
			for(int i = 0; i < AttackAndMove.TheAttack.Length; i++){
				AttackAndMove.TheAttack[i].AttackBehaviourIndex = (int)AttackAndMove.TheAttack[i].AttackBehaviour;
			}
			for(int i = 0; i < AttackAndMove.TheAttackMovement.Length; i++){
				AttackAndMove.TheAttackMovement[i].AttackMovementBehaviourIndex = (int)AttackAndMove.TheAttackMovement[i].AttackMovementBehaviour;
				AttackAndMove.TheAttackMovement[i].AttackMovementStateIndex = (int)AttackAndMove.TheAttackMovement[i].AttackMovementState;
			}
		}else{
			for (int i = 0; i < Move.Length; i++) {
				Move [i].MovementBehaviourIndex = (int)Move [i].MovementBehaviour;
				Move [i].MovementStateIndex = (int)Move [i].MovementState;
			}
		}

		for (int i = 0; i < ExitRequirements.Length; i++) {
			for (int j = 0; j < ExitRequirements[i].Requirements.Length; j++) {
				ExitRequirements[i].RequirementIndex[j] = (int)ExitRequirements[i].Requirements[j];
			}
		}
	}




	[System.Serializable]
	public struct WhenTenteringBehaviour {
		public bool ChangeAnimationStage;
		public float AnimatorStage;

	}

	[System.Serializable]
	public struct TheAttackAndMovementBehaviour{
		public TheAttackBehaviour[] TheAttack;
		[Tooltip("Teleport size 0:\n[0] == distance to move.\n\nStop size 2:\n[0] == how long to stand stil.\n[1] == 0.\n\nWalk size 3:\n[0] == movementspeed.\n[1] == distance to travel.\n[2] == 0.\n\nTimemovement size 3:\n[0] == time to move.\n[1] == 0\n[2] == movementspeed.\n-----------\nVector size 1")]
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
		public int AttackBehaviourIndex; 
		public TheAttackType AttackBehaviour;
		public SpellAttackInfo SpellInfo;

		[Tooltip("Needs To Be Size 2")]
		public float[] AttackValues;
		[Tooltip("Currently Used For LineCast CircleCast And RektCast")]
		public LayerMask WhatCanIHit;

		public int[] MovementIndexToAttack;
	}

	[System.Serializable]
	public struct TheAttackMovement{
		[HideInInspector]
		public int AttackMovementBehaviourIndex; 
		[HideInInspector]
		public int AttackMovementStateIndex;

		public TheMovementVector AttackMovementBehaviour;
		public TheMovementBehaviours AttackMovementState;

		[Tooltip("Teleport size 0:\n[0] == distance to move.\n\nStop size 2:\n[0] == how long to stand stil.\n[1] == 0.\n\nWalk size 3:\n[0] == movementspeed.\n[1] == distance to travel.\n[2] == 0.\n\nTimemovement size 3:\n[0] == time to move.\n[1] == 0\n[2] == movementspeed.")]
		public float[] MovementValues;

		public bool ChangeAttackIndex;
		public int AttackIndexValue;
		public bool ChangeAnimationStage;
		public float AnimationStageValue;
		public bool RotateWhileAttacking;
	}

	[System.Serializable]
	public struct TheMovementBehaviour{
		[HideInInspector]
		public int MovementBehaviourIndex; 
		[HideInInspector]
		public int MovementStateIndex;

		public TheMovementVector MovementBehaviour;
		public TheMovementBehaviours MovementState;

		[Tooltip("Teleport size 0:\n[0] == distance to move.\n\nStop size 2:\n[0] == how long to stand stil.\n[1] == 0.\n\nWalk size 3:\n[0] == movementspeed.\n[1] == distance to travel.\n[2] == 0.\n\nTimemovement size 3:\n[0] == time to move.\n[1] == 0\n[2] == movementspeed.")]
		public float[] MovementValues;
	
		[Tooltip("If True Then You Want To Change That Animation Parameter")]
		public bool ChangeAnimationStage;
		[Tooltip("Animator Parameter Value")]
		public float AnimationStageValue;
		[Tooltip("Look At Target While Moving")]
		public bool LookAtTarget;
	}


	[System.Serializable]
	public struct TheExitRequirements {
		[HideInInspector]
		public int[] RequirementIndex;
	
		[Tooltip("How Many Different Requirement Do You Want, If More Then 1 Then It Checks If Both Are True, Then It Changes To The Requirement StateIndexValue")]
		public ExitReuirement[] Requirements;

		[Tooltip("Requirement Values. Time == size[2], Distance == size[2], RayCast == [1]")]
		public float[] ValueRequirements;

		[Tooltip("Which Element In The Array Do You Want To Change Too")]
		public int ChangeToBehaviour;

	}
}
