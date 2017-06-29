using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CreatureBehaviourUpdate : MonoBehaviour {

	//	[Header("Testing V2")]
	//	[Space(20)]       10 each line 
	//	[Tooltip("The Info")]

	// <summary>
	// method description.
	// </summary>
	// <param name="index">Parameter info.</param>
	// <returns>Returns void.</returns>
	//void SetAttackMovementPointer (int index) {

	public int Health = 0;
	public int Energy = 0;
	public int Ammo = 0;


	int StateIndex = 0;

	public FSM_DefaultBehavoirV2[] CreatureStatesV2;

	public delegate void FunctionPointer();
	public FunctionPointer[] TheFunctionPointer = new FunctionPointer[23];

	int StateSaver = 0;
	int PreviourStateIndex = 0;
	int AttackIndex = 0;
	int MovementIndex = 0; 
	int RequirementGroupIndex = 0;
	int RequirementIndex = 0;
	bool StateFinished = false;
	bool RequirementReturnTrue = false;

	Transform target;
	float []TheTime;//the clock
	public float[] MyTimes;//all time values

	bool CollideToQuit = false;
	bool DidICollide = false;
	bool CollisionOn = false;
	bool FollowNodes = false;
	bool NextStatePlz = false; 

	int AttackTimeIndex = 0;//since there can only be one attackbehaviour active at a single moment, then instead of all having one variable each for a indexsaver then just made it here
	int MovementTimeIndex = 0;//there can be multiple movement states but only one is active at once
	bool MovementStarted = false;

	Animator MyAnimator;
	Vector2 LookLeft = new Vector2 (-2, 2);
	Vector2 LookRight = new Vector2 (2, 2);
	int AnimatorControllerParameterStop = Animator.StringToHash ("Stop");
	int AnimatorControllerParameterStage = Animator.StringToHash ("AnimatorStage");
	int AnimatorControllerParameterShoot = Animator.StringToHash ("Shoot");
	int AnimatorControllerParameterLockDirection = Animator.StringToHash ("LockDirection");

	int AnimatorControllerParameterStop1 = Animator.StringToHash ("Stop");
	int AnimatorControllerParameterStage2 = Animator.StringToHash ("AnimatorStage");
	int AnimatorControllerParameterShoot3 = Animator.StringToHash ("Shoot");
	int AnimatorControllerParameterLockDirection4 = Animator.StringToHash ("LockDirection");

	void Start(){//Setting All FunctionPointer Refrences + casting enum value to int
		TheTime = GameObject.FindGameObjectWithTag ("Respawn").GetComponent<ClockTest>().GetTime();
		MyTimes = new float[10];
		MyAnimator = GetComponent<Animator> ();
		target = GameObject.Find ("Hero v5").transform;

		for (int k = 0; k < CreatureStatesV2.Length; k++) {

			if (CreatureStatesV2 [k].AttackOrMove == true) {
				for (int i = 0; i < CreatureStatesV2 [k].AttackAndMove.TheAttack.Length; i++) {//casting attakbehaviour attack enum to int
					CreatureStatesV2 [k].AttackAndMove.TheAttack [i].AttackBehaviourIndex = (int)CreatureStatesV2 [k].AttackAndMove.TheAttack [i].AttackBehaviour;
					SetAttackPointer (CreatureStatesV2 [k].AttackAndMove.TheAttack [i].AttackBehaviourIndex);
				}
				for (int i = 0; i < CreatureStatesV2 [k].AttackAndMove.TheAttackMovement.Length; i++) {//casting attakbehaviour movement enum to int
					CreatureStatesV2 [k].AttackAndMove.TheAttackMovement [i].AttackMovementBehaviourIndex = (int)CreatureStatesV2 [k].AttackAndMove.TheAttackMovement [i].AttackMovementBehaviour;
					CreatureStatesV2 [k].AttackAndMove.TheAttackMovement [i].AttackMovementStateIndex = (int)CreatureStatesV2 [k].AttackAndMove.TheAttackMovement [i].AttackMovementState;
					SetAttackMovementPointer (CreatureStatesV2 [k].AttackAndMove.TheAttackMovement [i].AttackMovementBehaviourIndex);
					SetAttackMovementPointer (CreatureStatesV2 [k].AttackAndMove.TheAttackMovement [i].AttackMovementStateIndex);
				}
			} else {
				for (int i = 0; i < CreatureStatesV2 [k].Move.Length; i++) {//casting movementbehaviour movement enum to int
					CreatureStatesV2 [k].Move [i].MovementBehaviourIndex = (int)CreatureStatesV2 [k].Move [i].MovementBehaviour;
					CreatureStatesV2 [k].Move [i].MovementStateIndex = (int)CreatureStatesV2 [k].Move [i].MovementState;
					SetAttackMovementPointer (CreatureStatesV2 [k].Move [i].MovementBehaviourIndex);
					SetAttackMovementPointer (CreatureStatesV2 [k].Move [i].MovementStateIndex);
				}
			}

			for (int i = 0; i < CreatureStatesV2 [k].ExitRequirements.Length; i++) {//casting exitbehaviour enum to int. the requirementindex need to be the same lenght or more then the exitrequirement length
				CreatureStatesV2 [k].ExitRequirements [i].RequirementIndex = new int[CreatureStatesV2 [k].ExitRequirements [i].Requirements.Length];
				CreatureStatesV2 [k].ExitRequirements [i].TimeSavedIndex = new int[CreatureStatesV2 [k].ExitRequirements [i].Requirements.Length];
				for (int j = 0; j < CreatureStatesV2 [k].ExitRequirements [i].Requirements.Length; j++) {
					CreatureStatesV2 [k].ExitRequirements [i].RequirementIndex [j] = (int)CreatureStatesV2 [k].ExitRequirements [i].Requirements [j];
					SetExitPointer (CreatureStatesV2 [k].ExitRequirements [i].RequirementIndex[j]);
				}
			}
		}

		SetAllTimeVariables ();
		if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {
			if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].ChangeAnimationStage == true) {
				MyAnimator.SetFloat (AnimatorControllerParameterStage, CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].AnimationStageValue);
			}
		} else {
			if (CreatureStatesV2 [StateIndex].Move [MovementIndex].ChangeAnimationStage == true) {
				MyAnimator.SetFloat (AnimatorControllerParameterStage, CreatureStatesV2 [StateIndex].Move [MovementIndex].AnimationStageValue);
			}
		}
	}

	void Update(){

		if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {
			TheFunctionPointer [CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackBehaviourIndex] ();//Calling The attackmethod, it's the way to attack, buff yourself, cast spell on target....
			TheFunctionPointer [CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].AttackMovementBehaviourIndex] ();//Calling The movementmethod for the movementvector enum.  it's the enum that controlls the vector which the object need to go/look at
			TheFunctionPointer [CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].AttackMovementStateIndex] ();//Calling The movementmethod for the movementbehaviour enum.  it's the enum that controlls the movementbehaviour, walk, stand still, teleport...

			if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].RotateWhileAttacking == true) {
				if (MyAnimator.GetBool (AnimatorControllerParameterLockDirection) == false) {
					if (target.transform.position.x - transform.position.x > 0) {
						transform.localScale = LookRight;
					} else {
						transform.localScale = LookLeft;
					}
				}
			}	
		} else {
			TheFunctionPointer [CreatureStatesV2 [StateIndex].Move [MovementIndex].MovementBehaviourIndex] ();//Calling The movementmethod for the movementvector enum.  it's the enum that controlls the vector which the object need to go/look at
			TheFunctionPointer [CreatureStatesV2 [StateIndex].Move [MovementIndex].MovementStateIndex] ();//Calling The movementmethod for the movementbehaviour enum.  it's the enum that controlls the movementbehaviour, walk, stand still, teleport...

			if (CreatureStatesV2 [StateIndex].Move [MovementIndex].LookAtTarget == true) {
				if (MyAnimator.GetBool (AnimatorControllerParameterLockDirection) == false) {
					if (target.transform.position.x - transform.position.x > 0) {
						transform.localScale = LookRight;
					} else {
						transform.localScale = LookLeft;
					}
				}
			}	
		}

		if (CreatureStatesV2 [StateIndex].FinishBehaviourToExit == true && StateFinished == true) {//If The Behaviours In The State Is Finished This Happends
			PreviourStateIndex = StateIndex;
			RunRequirementIndexSearch ();
			if (PreviourStateIndex == StateIndex) {//Requirements Not Met
				StateSaver = StateIndex;
				ResetState ();//Reset And Start Over In The Save State
			}

		} else if (CreatureStatesV2 [StateIndex].FinishBehaviourToExit == false) {
			PreviourStateIndex = StateIndex;
			RunRequirementIndexSearch ();
			if (PreviourStateIndex == StateIndex && StateFinished == true) {//Requirements Not Met And Behaviours Finished
				StateSaver = StateIndex;
				ResetState ();//Reset And Start Over In The Save State
			}
		}
	
	}

	void RunRequirementIndexSearch(){//Checking The Requirements. If Requirement Is Met, The State Changes. If Not Then It Continue

		for (RequirementGroupIndex = 0; RequirementGroupIndex < CreatureStatesV2 [StateIndex].ExitRequirements.Length; RequirementGroupIndex++) {//going throught each exit requirement groups
			for (RequirementIndex = 0; RequirementIndex < CreatureStatesV2 [StateIndex].ExitRequirements [RequirementGroupIndex].RequirementIndex.Length; RequirementIndex++) {//going through the group

				TheFunctionPointer [CreatureStatesV2 [StateIndex].ExitRequirements [RequirementGroupIndex].RequirementIndex [RequirementIndex]] ();//getting the index and uses it to go to the correct method;
				if (RequirementReturnTrue == true) {
					RequirementReturnTrue = false;
					if (RequirementIndex == CreatureStatesV2 [StateIndex].ExitRequirements [RequirementGroupIndex].RequirementIndex.Length - 1) {
						StateSaver = CreatureStatesV2 [StateIndex].ExitRequirements [RequirementGroupIndex].ChangeToBehaviour;
						ResetState ();
						return;
					}
				} else {
					RequirementIndex = CreatureStatesV2 [StateIndex].ExitRequirements [RequirementGroupIndex].RequirementIndex.Length;
				}
			}
		}

	}

	void ResetState(){//Reseting Objects Used In Current State + Setting New Values

		NextStatePlz = true;
		if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {//Calling The methods again to reset the variables used in them
			TheFunctionPointer [CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackBehaviourIndex] ();
			TheFunctionPointer [CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].AttackMovementBehaviourIndex] ();
			TheFunctionPointer [CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].AttackMovementStateIndex] ();
		}else{
			TheFunctionPointer [CreatureStatesV2 [StateIndex].Move [MovementIndex].MovementBehaviourIndex] ();
			TheFunctionPointer [CreatureStatesV2 [StateIndex].Move [MovementIndex].MovementStateIndex] ();
		}

		for (RequirementGroupIndex = 0; RequirementGroupIndex < CreatureStatesV2 [StateIndex].ExitRequirements.Length; RequirementGroupIndex++) {//going throught each exit requirement groups
			for (RequirementIndex = 0; RequirementIndex < CreatureStatesV2 [StateIndex].ExitRequirements [RequirementGroupIndex].RequirementIndex.Length; RequirementIndex++) {//going through the group
				TheFunctionPointer [CreatureStatesV2 [StateIndex].ExitRequirements [RequirementGroupIndex].RequirementIndex [RequirementIndex]] ();//getting the index and uses it to go to the correct method;
			}
		}
		NextStatePlz = false;
		StateFinished = false;	
		StateIndex = StateSaver;
		MovementIndex = 0;
		AttackIndex = 0;

		SetAllTimeVariables ();

		if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {//Setting Animator Parameter That Desides Which Animation To Run
			if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].ChangeAnimationStage == true) {
				MyAnimator.SetFloat (AnimatorControllerParameterStage, CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].AnimationStageValue);
			}

		} else {
			if (CreatureStatesV2 [StateIndex].Move [MovementIndex].ChangeAnimationStage == true) {
				MyAnimator.SetFloat (AnimatorControllerParameterStage, CreatureStatesV2 [StateIndex].Move [MovementIndex].AnimationStageValue);
			}
		}

	}


	#region SettingFunctionPointerMethods

	public void SetAttackMovementPointer (int index) {

		if (TheFunctionPointer [index] == null) {

			if (index == 0) {
				TheFunctionPointer [index] = FollowNodePathToTarget;
			} else if (index == 1) {
				TheFunctionPointer [index] = GoStraightToTarget;
			} else if (index == 2) {
				TheFunctionPointer [index] = LockMovementDirectionToTarget;
			} else if (index == 3) {
				TheFunctionPointer [index] = LockMovementDirectionToNextNode;
			} else if (index == 4) {
				TheFunctionPointer [index] = LockCurrentVector;
			}  else if (index == 6) {
				TheFunctionPointer [index] = TeleportDistance;
			} else if (index == 7) {
				TheFunctionPointer [index] = Stop;
			} else if (index == 8) {
				TheFunctionPointer [index] = Walk;
			} else if (index == 9) {
				TheFunctionPointer [index] = TimeMovement;
			} else {
				Debug.LogWarning ("Did You Forget Something");
			}
		}

	}

	void SetMovementPointer(int index){//Might Not Be The Same In The Future So Just Made Two Different Methods

		if (TheFunctionPointer [index] == null) {

			if (index == 0) {
				TheFunctionPointer [index] = FollowNodePathToTarget;
			} else if (index == 1) {
				TheFunctionPointer [index] = GoStraightToTarget;
			} else if (index == 2) {
				TheFunctionPointer [index] = LockMovementDirectionToTarget;
			} else if (index == 3) {
				TheFunctionPointer [index] = LockMovementDirectionToNextNode;
			} else if (index == 4) {
				TheFunctionPointer [index] = LockCurrentVector;
			} else if (index == 6) {
				TheFunctionPointer [index] = TeleportDistance;
			} else if (index == 7) {
				TheFunctionPointer [index] = Stop;
			} else if (index == 8) {
				TheFunctionPointer [index] = Walk;
			} else if (index == 9) {
				TheFunctionPointer [index] = TimeMovement;
			} else {
				Debug.LogWarning ("Did You Forget Something");
			}
		}

	}

	void SetAttackPointer(int index){

		if (TheFunctionPointer [index] == null) {

			if (index == 10) {
				TheFunctionPointer [index] = AttackSpeedDesides;
			}  else if (index == 12) {
				TheFunctionPointer [index] = CircleCast;
			} else if (index == 13) {
				TheFunctionPointer [index] = RektangleCast;
			} else if (index == 5) {
				TheFunctionPointer [index] = OnCollisionDealDamage;
			}  else if (index == 22) {
				TheFunctionPointer [index] = AnimationDecideAttack;
			}  else {
				Debug.LogWarning ("Did You Forget Something");
			}
		}

	}

	void SetExitPointer(int index){

		if (TheFunctionPointer [index] == null) {

			if (index == 15) {
				TheFunctionPointer [index] = Nothing;
			} else if (index == 16) {
				TheFunctionPointer [index] = Collision;
			} else if (index == 17) {
				TheFunctionPointer [index] = TimeExit;
			} else if (index == 18) {
				TheFunctionPointer [index] = DistanceToPlayerLessThen;
			} else if (index == 19) {
				TheFunctionPointer [index] = DistanceToPlayerMoreThen;
			} else if (index == 20) {
				TheFunctionPointer [index] = RayCastHitWall;
			} else if (index == 21) {
				TheFunctionPointer [index] = RayCastClearPathToTarget;
			}  else {
				Debug.LogWarning ("Did You Forget Something");
			}
		}

	}

	#endregion

	void OnCollisionEnter2D(Collision2D coll){
		if (CollisionOn == true) {
			DidICollide = true;
			Debug.Log (coll.gameObject.name + " Took " + CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackValues[0] + " Dmg");
		}
		if (CollideToQuit == true) {
			DidICollide = true;
		}
	}

	/*	void OnCollisionStay2D(Collision2D coll){
		if (CollisionOn == true) {
			DidICollide = true;
			Debug.Log (coll.gameObject.name + " Took " + CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackValues[0] + " Dmg");
		}
		if (CollideToQuit == true) {
			DidICollide = true;
		}
	}*/

	#region Attack
	//**********************************************************
	// The LineCast RektangleCast And CircleCast
	// Is Used For Creatures With Attack Within Their Animation
	//**********************************************************

	#region CastMethods

	public void RektangleCast(){//Casting the Rektanglecast On The AttackPositionChild. Then I Take The Vector[1] As The Width And Height. TODO Remove Debug When fully Implemented

		if (NextStatePlz == false) {

			if (MyTimes [AttackTimeIndex] < TheTime [0]) {
				Collider2D[] SavedCast = Physics2D.OverlapAreaAll (transform.GetChild(0).transform.position - CreatureStatesV2 [StateIndex].Vectors[1], transform.GetChild(0).transform.position + CreatureStatesV2 [StateIndex].Vectors[1], CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].WhatCanIHit);
				if (SavedCast.Length > 0) {
					Debug.Log ("A " + (CreatureStatesV2 [StateIndex].Vectors[1].x * 2) + "x" + (CreatureStatesV2 [StateIndex].Vectors[1].y * 2) + " Square Around You " + SavedCast.Length + " Was Hit");
					for (int i = 0; i < SavedCast.Length; i++)
						Debug.Log (SavedCast [i].transform.name + " Took DMG");
					//	SavedCast [i].gameObject.GetComponent<Script> ().RecieveDmg (Spellinfo/attackdmg);
				}
			}
		} else {
			MyTimes [AttackTimeIndex] = 0;
		}

	}

	public void CircleCast(){//Circle Center Is AttackPositionChild. Circle Radius Is AttackValues [1] TODO Remove Debug When fully Implemented

		if (NextStatePlz == false) {

			if (MyTimes [AttackTimeIndex] < TheTime [0]) {
				Collider2D[] SavedCast = Physics2D.OverlapCircleAll (transform.GetChild(0).transform.position, CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackValues [1], CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].WhatCanIHit);
				if (SavedCast.Length > 0) {
					Debug.Log ("A " + CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackValues [1] + " Radius Circle Around You Hit " + SavedCast.Length + " Was Hit");
					for (int i = 0; i < SavedCast.Length; i++) {
						Debug.Log (SavedCast [i].transform.name + " Took DMG");
						//	SavedCast [i].gameObject.GetComponent<Script> ().RecieveDmg (Spellinfo/attackdmg);
					}
				}
			}
		} else {
			MyTimes [AttackTimeIndex] = 0;
		}

	}

	#endregion

	public void AnimationDecideAttack(){
		if (MyAnimator.GetBool (AnimatorControllerParameterShoot) == true) {
			MyAnimator.SetBool (AnimatorControllerParameterShoot, false);
			Instantiate (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].SpellInfo.Spell, transform.GetChild (0).position, Quaternion.identity).GetComponent<FSM_BulletBehaviour> ().SetDmgModifiers (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].SpellInfo,target);
		}
	}

	public void OnCollisionDealDamage(){//Just turning on a boolean that controles if im registering the collision or not

		if (NextStatePlz == false) {
			if (CollisionOn == false) {
				CollisionOn = true;
			}
		} else {
			CollisionOn = false;
		}
	}

	public void AttackSpeedDesides(){//Attacks With Intervals Of Value AttackValues [0] == AttackSpeed

		if (NextStatePlz == false) {
			if (MyTimes [AttackTimeIndex] < TheTime [0]) {
				for (int i = 0; i < CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].MovementIndexToAttack.Length; i++) {
					if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].MovementIndexToAttack [i] == MovementIndex) {
						Instantiate (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].SpellInfo.Spell, transform.GetChild (0).position, Quaternion.identity).GetComponent<FSM_BulletBehaviour> ().SetDmgModifiers (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].SpellInfo,target);
						MyTimes [AttackTimeIndex] = CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackValues [0] + TheTime [0];//setting new time
						break;
					}
				}

			}
		} else {
			MyTimes [AttackTimeIndex] = 0;
		}

	}

	#endregion

	#region MovementMethods

	public void FollowNodePathToTarget(){//NodeMap Have Not Been Implemented TODO
		if (NextStatePlz == false) {
			FollowNodes = true;
		} else {
			FollowNodes = false;
		}
	}

	public void GoStraightToTarget(){//Movement speed is index 0

		if (NextStatePlz == false) {
			if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {
				CreatureStatesV2 [StateIndex].Vectors [0] = (target.position - transform.position).normalized;
			} else {
				CreatureStatesV2 [StateIndex].Vectors [0] = (target.position - transform.position).normalized;
			}
		} else {

		}

	}

	public void LockMovementDirectionToTarget(){

		if (NextStatePlz == false) {
			if (MovementStarted == false) {
				MovementStarted = true;
				if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {
					CreatureStatesV2 [StateIndex].Vectors [0] = (target.position - transform.position).normalized;
				} else {
					CreatureStatesV2 [StateIndex].Vectors [0] = (target.position - transform.position).normalized;
				}
			}
		} else {
			MovementStarted = false;
		}

	}

	public void LockMovementDirectionToNextNode(){//NodeMap Have Not Been Implemented TODO
		//update node map

		if (NextStatePlz == false) {
			if (MovementStarted == false) {
				MovementStarted = true;
				if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {
					//CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].Vectors [0] = (target - transform.position).normalized;
				} else {
					//CreatureStatesV2 [StateIndex].Move [MoveIndex].Vectors [0] = (target - transform.position).normalized;
				}
			}
		} else {
			MovementStarted = false;

		}

	}

	public void LockCurrentVector(){

		if (NextStatePlz == false) {
			if (MovementStarted == false) {
				MovementStarted = true;
				if (MovementIndex == 0) {
					CreatureStatesV2 [StateIndex].Vectors [0] = CreatureStatesV2 [PreviourStateIndex].Vectors [0];
				} else {
					CreatureStatesV2 [StateIndex].Vectors [0] = CreatureStatesV2 [StateIndex].Vectors [0];
				}
			}
		} else {
			MovementStarted = false;
		}
	}

	public void TeleportDistance(){

		if (NextStatePlz == false) {
			if (FollowNodes == true) {
				if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {
					//b	//Node[Mathf.floor(CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].MovementValues [0]).x; 
					//Node[Mathf.floor(CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].MovementValues [0]).y; 

					//a	//Node[Mathf.floor(CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].MovementValues [0] + 1).x; 
					//Node[Mathf.floor(CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].MovementValues [0] + 1).y; 

					//aa	//vector = b - a; * (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].MovementValues [0] - Mathf.floor(CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].MovementValues [0])
					//transform.position += aa; 
				} else {
					//b	//Node[Mathf.floor(CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].MovementValues [0]).x; 
					//Node[Mathf.floor(CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].MovementValues [0]).y; 

					//a	//Node[Mathf.floor(CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].MovementValues [0] + 1).x; 
					//Node[Mathf.floor(CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].MovementValues [0] + 1).y; 

					//aa	//vector = b - a; * (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].MovementValues [0] - Mathf.floor(CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].MovementValues [0])
					//transform.position += aa; 
				}
			} else {
				if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {
					if (MyAnimator.GetBool (AnimatorControllerParameterStop) == false) {
						transform.position += CreatureStatesV2 [StateIndex].Vectors [0] * CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].MovementValues [0];
						WhatToDoWhenDone ();
					}
				} else {
					if (MyAnimator.GetBool (AnimatorControllerParameterStop) == false) {
						transform.position += CreatureStatesV2 [StateIndex].Vectors [0] * CreatureStatesV2 [StateIndex].Move [MovementIndex].MovementValues [0];
						WhatToDoWhenDone ();
					}
				}
			}
		} else {

		}
	}

	public void Stop(){

		if (NextStatePlz == false) {
			if (MyTimes [MovementTimeIndex] < TheTime [0]) {
				WhatToDoWhenDone ();
			}
		} else {
			MyTimes [MovementTimeIndex] = 0;
		}

	}

	public void Walk(){
		if (NextStatePlz == false) {
			if (FollowNodes == true) {
				//follow them
			} else {
				if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {
					if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].MovementValues [2] > CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].MovementValues [1]) {
						WhatToDoWhenDone ();
					} else {
						if (MyAnimator.GetBool (AnimatorControllerParameterStop) == false) {
							CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].MovementValues [2] += Vector2.Distance (Vector2.zero, CreatureStatesV2 [StateIndex].Vectors [0] * CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].MovementValues [0] * Time.deltaTime);
							transform.position += CreatureStatesV2 [StateIndex].Vectors [0] * CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].MovementValues [0] * Time.deltaTime;
						}
					} 
				} else {
					if (CreatureStatesV2 [StateIndex].Move [MovementIndex].MovementValues [2] > CreatureStatesV2 [StateIndex].Move [MovementIndex].MovementValues [1]) {
						WhatToDoWhenDone ();
					} else {
						if (MyAnimator.GetBool (AnimatorControllerParameterStop) == false) {
							CreatureStatesV2 [StateIndex].Move [MovementIndex].MovementValues [2] += Vector2.Distance (Vector2.zero, CreatureStatesV2 [StateIndex].Vectors [0] * CreatureStatesV2 [StateIndex].Move [MovementIndex].MovementValues [0] * Time.deltaTime);
							transform.position += CreatureStatesV2 [StateIndex].Vectors [0] * CreatureStatesV2 [StateIndex].Move [MovementIndex].MovementValues [0] * Time.deltaTime;
						}
					}
				}
			}
		} else {
			if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {
				CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].MovementValues [2] = 0;
			} else {
				CreatureStatesV2 [StateIndex].Move [MovementIndex].MovementValues [2] = 0;
			}
		}

	}

	public void TimeMovement(){

		if (NextStatePlz == false) {
			if (FollowNodes == true) {
				//follow them
			} else {
				if (MyTimes [MovementTimeIndex] < TheTime [0]) {
					WhatToDoWhenDone ();
				} else {
					if (MyAnimator.GetBool (AnimatorControllerParameterStop) == false) {
						if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {
							transform.position += CreatureStatesV2 [StateIndex].Vectors [0] * CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].MovementValues [1] * Time.deltaTime;
						} else {
							transform.position += CreatureStatesV2 [StateIndex].Vectors [0] * CreatureStatesV2 [StateIndex].Move [MovementIndex].MovementValues [1] * Time.deltaTime;
						}
					}
				}
			}
		} else {
			MyTimes [MovementTimeIndex] = 0;
		}

	}

	void WhatToDoWhenDone(){//TODO didnt change this
		if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {
			if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement.Length <= MovementIndex + 1) {//If True Then Im At The Last MovementBehaviour
				StateFinished = true;//This Tells Me That The Behaviour Is Finished And That I Want To Check The Requirements If I Can Change State
			} else {
				NextStatePlz = true;
				TheFunctionPointer [CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].AttackMovementBehaviourIndex] ();
				TheFunctionPointer [CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].AttackMovementStateIndex] ();
				NextStatePlz = false;

				MovementIndex++;
				if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].ChangeAttackIndex == true) {
					NextStatePlz = true;
					TheFunctionPointer [CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackBehaviourIndex] ();//I Dont Want The Timers To Be Bugged So Im Reseting The AttackBehaviour Timers When i Enter A New Movement Behaviour
					NextStatePlz = false;
					AttackIndex = CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].AttackIndexValue;
					SetAttackSpeedVariables ();
				}else if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].ICanAttackAgain == true) {
					NextStatePlz = true;
					TheFunctionPointer [CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackBehaviourIndex] ();//I Dont Want The Timers To Be Bugged So Im Reseting The AttackBehaviour Timers When i Enter A New Movement Behaviour
					NextStatePlz = false;
					SetAttackSpeedVariables ();
				}

				if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].ChangeAnimationStage == true) {//if the exit requirement is met, then i need this to change the animationstate to the correct movement behaviour
					MyAnimator.SetFloat (AnimatorControllerParameterStage, CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].AnimationStageValue);
				}

				SetTimeMovementVariables ();
			}

		} else {
			if (CreatureStatesV2 [StateIndex].Move.Length <= MovementIndex + 1) {
				StateFinished = true;//This Tells Me That The Behaviour Is Finished And That I Want To Check The Requirements If I Can Change State
			} else {
				NextStatePlz = true;
				TheFunctionPointer [CreatureStatesV2 [StateIndex].Move [MovementIndex].MovementBehaviourIndex] ();//Calling The movementmethod for the movementvector enum.  it's the enum that controlls the vector which the object need to go/look at
				TheFunctionPointer [CreatureStatesV2 [StateIndex].Move [MovementIndex].MovementStateIndex] ();//Calling The movementmethod for the movementbehaviour enum.  it's the enum that controlls the movementbehaviour, walk, stand still, teleport...
				NextStatePlz = false;

				MovementIndex++;
				if (CreatureStatesV2 [StateIndex].Move [MovementIndex].ChangeAnimationStage == true) {//if the exit requirement is met, then i need this to change the animationstate to the correct movement behaviour
					MyAnimator.SetFloat (AnimatorControllerParameterStage, CreatureStatesV2 [StateIndex].Move [MovementIndex].AnimationStageValue);
				}
				SetTimeMovementVariables ();
			}
		}

	}

	#endregion

	#region ExitRequierments

	public void Nothing(){
		if (NextStatePlz == false) {
			RequirementReturnTrue = true;
		} else {

		}
	}

	public void Collision(){

		if (NextStatePlz == false) {	
			if (CollideToQuit == false) {
				CollideToQuit = true;
			}
			if (DidICollide == true) {
				RequirementReturnTrue = true;
			}
		} else {
			CollideToQuit = false;
			DidICollide = false;
		}
	}

	#region TheTime

	public void TimeExit(){

		if (NextStatePlz == false) {	
			if (MyTimes [CreatureStatesV2 [StateIndex].ExitRequirements [RequirementGroupIndex].TimeSavedIndex [RequirementIndex]] < TheTime [0]) {
				RequirementReturnTrue = true;
			}
		} else {
			MyTimes [CreatureStatesV2 [StateIndex].ExitRequirements [RequirementGroupIndex].TimeSavedIndex [RequirementIndex]] = 0;
		}

	}

	void SetAllTimeVariables(){

		for (int i = 0; i < CreatureStatesV2 [StateIndex].ExitRequirements.Length; i++) {//going throught each exit requirement groups
			for (int j = 0; j < CreatureStatesV2 [StateIndex].ExitRequirements [i].RequirementIndex.Length; j++) {//going through the group
				if (CreatureStatesV2 [StateIndex].ExitRequirements [i].RequirementIndex [j] == 17) {
					for (int k = 0; k < MyTimes.Length; k++) {
						if (MyTimes [k] == 0) {
							CreatureStatesV2 [StateIndex].ExitRequirements [i].TimeSavedIndex [j] = k;
							MyTimes [k] = CreatureStatesV2 [StateIndex].ExitRequirements [i].ValueRequirements [j] + TheTime [0];
							break;
						}
					}
				}
			}
		}

		if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {
			SetAttackSpeedVariables ();
			SetTimeMovementVariables ();
		} else {
			SetTimeMovementVariables ();
		}

	}

	void SetTimeMovementVariables(){
		if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {//If Time Is Used In Any Way, The Time Value Need To Be In Position [0] in MovementValues

			if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].AttackMovementStateIndex == 9 || CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].AttackMovementStateIndex == 7) {
				for (int k = 0; k < MyTimes.Length; k++) {
					if (MyTimes [k] == 0) {
						MovementTimeIndex = k;
						MyTimes [k] = CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MovementIndex].MovementValues [0] + TheTime [0];
						break;
					}
				}
			}
		} else {
			if (CreatureStatesV2 [StateIndex].Move [MovementIndex].MovementStateIndex == 9 || CreatureStatesV2 [StateIndex].Move [MovementIndex].MovementStateIndex == 7) {
				for (int k = 0; k < MyTimes.Length; k++) {
					if (MyTimes [k] == 0) {
						MovementTimeIndex = k;
						MyTimes [k] = CreatureStatesV2 [StateIndex].Move [MovementIndex].MovementValues [0] + TheTime [0];
						break;
					}
				}
			}
		}

	}

	void SetAttackSpeedVariables(){

		if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackValues.Length > 0) {
			for (int k = 0; k < MyTimes.Length; k++) {
				if (MyTimes [k] == 0) {
					AttackTimeIndex = k;
					MyTimes [k] = CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackValues [0] + TheTime [0];
					break;
				}
			}
		}
	}

	#endregion

	public void DistanceToPlayerLessThen(){

		if (NextStatePlz == false) {	
			if (Vector3.Distance (target.position, transform.position) < CreatureStatesV2 [StateIndex].ExitRequirements [RequirementGroupIndex].ValueRequirements [RequirementIndex]) {
				RequirementReturnTrue = true;
			}
		} else {

		}

	}

	public void DistanceToPlayerMoreThen(){

		if (NextStatePlz == false) {	
			if (Vector3.Distance (target.position, transform.position) > CreatureStatesV2 [StateIndex].ExitRequirements [RequirementGroupIndex].ValueRequirements [RequirementIndex]) {
				RequirementReturnTrue = true;
			}
		} else {

		}

	}

	public void RayCastHitWall(){

		if (NextStatePlz == false) {	
			if (Physics2D.Linecast (transform.position, target.position, LayerMask.NameToLayer ("Walls")).transform != null) {
				RequirementReturnTrue = true;
			} else {
				//		Debug.Log (" Didnt Hit Anything");
			}
		} else {

		}

	}

	public void RayCastClearPathToTarget(){

		if (NextStatePlz == false) {	
			if (Physics2D.Linecast (transform.position, target.position, LayerMask.NameToLayer ("Walls")).transform == null) {
				RequirementReturnTrue = true;
			} else {
				//Debug.Log ("Hit " + Physics2D.Linecast (transform.position, target.position, LayerMask.NameToLayer ("Walls")).transform.name);
			}
		} else {

		}

	}

	#endregion
}
