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


	[Header("Testing V2")]
	[Space(20)]
	public FSM_DefaultBehavoirV2[] CreatureStatesV2;

	public delegate void FunctionPointer();
	public FunctionPointer[] TheFunctionPointer = new FunctionPointer[21];

	int PreviourStateIndex = 0;
	public int StateIndex = 0;
	int AttackIndex = 0;
	int MoveIndex = 0; 
	int RequirementGroupIndex = 0;
	int RequirementIndex = 0;

	bool StateFinished = false;
	bool RequirementReturnTrue = false;

	public Transform target;
	public float []TheTime;//the clock
	public float[] MyTimes;//all time values

	bool CollideToQuit = false;
	bool DidICollide = false;
	bool CollisionOn = false;
	bool AttackBool = false;
	bool MovementBool = false;
	bool FollowNodes = false;
	Animator MyAnimator;
	int AnimatorControllerParameterStop = Animator.StringToHash ("Stop");
	int AnimatorControllerParameterStage = Animator.StringToHash ("AnimatorStage");


	void Start(){//Setting All FunctionPointer Refrences + casting enum value to int
		TheTime = GameObject.FindGameObjectWithTag ("Respawn").GetComponent<ClockTest>().GetTime();
		MyTimes = new float[10];
		MyAnimator = GetComponent<Animator> ();
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
				for (int j = 0; j < CreatureStatesV2 [k].ExitRequirements [i].Requirements.Length; j++) {
					CreatureStatesV2 [k].ExitRequirements [i].RequirementIndex [j] = (int)CreatureStatesV2 [k].ExitRequirements [i].Requirements [j];
					SetExitPointer (CreatureStatesV2 [k].ExitRequirements [i].RequirementIndex[j]);
				}
			}
		}
			
		SetAllTimeVariables ();
		if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {
			if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].ChangeAnimationStage == true) {
				MyAnimator.SetFloat (AnimatorControllerParameterStage, CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].AnimationStageValue);
			}
		} else {
			if (CreatureStatesV2 [StateIndex].Move [MoveIndex].ChangeAnimationStage == true) {
				MyAnimator.SetFloat (AnimatorControllerParameterStage, CreatureStatesV2 [StateIndex].Move [MoveIndex].AnimationStageValue);
			}
		}

	}

	void Update(){

		if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {
			
			TheFunctionPointer [CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackBehaviourIndex] ();//getting the index and uses it to go to the correct method;
			
			TheFunctionPointer [CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].AttackMovementBehaviourIndex] ();//getting the index and uses it to go to the correct method;
			TheFunctionPointer [CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].AttackMovementStateIndex] ();//getting the index and uses it to go to the correct method;
		
			if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].RotateWhileAttacking == true) {
				if (target.transform.position.x - transform.position.x > 0) {
					GetComponent<SpriteRenderer> ().flipX = false;
				} else {
					GetComponent<SpriteRenderer> ().flipX = true;
				}
			}	
		} else {
			
			TheFunctionPointer [CreatureStatesV2 [StateIndex].Move [MoveIndex].MovementBehaviourIndex] ();//getting the index and uses it to go to the correct method;
			TheFunctionPointer [CreatureStatesV2 [StateIndex].Move [MoveIndex].MovementStateIndex] ();//getting the index and uses it to go to the correct method;
		
			if (CreatureStatesV2 [StateIndex].Move[MoveIndex].LookAtTarget == true) {
				if (target.transform.position.x - transform.position.x > 0) {
					GetComponent<SpriteRenderer> ().flipX = false;
				} else {
					GetComponent<SpriteRenderer> ().flipX = true;
				}
			}	
		}




		if (CreatureStatesV2 [StateIndex].FinishBehaviourToExit == true && StateFinished == true) {
			for (RequirementGroupIndex = 0; RequirementGroupIndex < CreatureStatesV2 [StateIndex].ExitRequirements.Length; RequirementGroupIndex++) {//going throught each exit requirement groups
				for (RequirementIndex = 0; RequirementIndex < CreatureStatesV2 [StateIndex].ExitRequirements [RequirementGroupIndex].RequirementIndex.Length; RequirementIndex++) {//going through the group
					TheFunctionPointer [CreatureStatesV2 [StateIndex].ExitRequirements [RequirementGroupIndex].RequirementIndex [RequirementIndex]] ();//getting the index and uses it to go to the correct method;
					if (RequirementReturnTrue == true) {
						RequirementReturnTrue = false;

						if (RequirementIndex == CreatureStatesV2 [StateIndex].ExitRequirements [RequirementGroupIndex].RequirementIndex.Length - 1) {
							ResetAllTimeVariables ();
							PreviourStateIndex = StateIndex;
							StateIndex = CreatureStatesV2 [StateIndex].ExitRequirements [RequirementGroupIndex].ChangeToBehaviour;
							CollideToQuit = false;
							DidICollide = false;
							CollisionOn = false;
							MovementBool = true;
							MoveIndex = 0;
							StateFinished = false;	
							SetAllTimeVariables ();

							if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {
								if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].ChangeAnimationStage == true) {
									MyAnimator.SetFloat (AnimatorControllerParameterStage, CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].AnimationStageValue);
								}
							} else {
								if (CreatureStatesV2 [StateIndex].Move [MoveIndex].ChangeAnimationStage == true) {
									MyAnimator.SetFloat (AnimatorControllerParameterStage, CreatureStatesV2 [StateIndex].Move [MoveIndex].AnimationStageValue);
								}
							}

							return;
						}
					
					} else {
						RequirementIndex = CreatureStatesV2 [StateIndex].ExitRequirements [RequirementGroupIndex].RequirementIndex.Length;
					}
				}
			}
		} else if (CreatureStatesV2 [StateIndex].FinishBehaviourToExit == false) {//checked each frame, so do the most expensive last in a group, or just last.
			for (RequirementGroupIndex = 0; RequirementGroupIndex < CreatureStatesV2 [StateIndex].ExitRequirements.Length; RequirementGroupIndex++) {//going throught each exit requirement groups
				for (RequirementIndex = 0; RequirementIndex < CreatureStatesV2 [StateIndex].ExitRequirements [RequirementGroupIndex].RequirementIndex.Length; RequirementIndex++) {//going through the group
					TheFunctionPointer [CreatureStatesV2 [StateIndex].ExitRequirements [RequirementGroupIndex].RequirementIndex [RequirementIndex]] ();//getting the index and uses it to go to the correct method;
					if (RequirementReturnTrue == true) {
						RequirementReturnTrue = false;

						if (RequirementIndex == CreatureStatesV2 [StateIndex].ExitRequirements [RequirementGroupIndex].RequirementIndex.Length - 1) {
							ResetAllTimeVariables ();
							StateIndex = CreatureStatesV2 [StateIndex].ExitRequirements [RequirementGroupIndex].ChangeToBehaviour;
							MovementBool = true;
							MoveIndex = 0;
							AttackIndex = 0;
							AttackBool = false;
							StateFinished = false;
							CollideToQuit = false;
							DidICollide = false;
							CollisionOn = false;
							SetAllTimeVariables ();

							if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {
								if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].ChangeAnimationStage == true) {
									MyAnimator.SetFloat (AnimatorControllerParameterStage, CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].AnimationStageValue);
								}
							} else {
								if (CreatureStatesV2 [StateIndex].Move [MoveIndex].ChangeAnimationStage == true) {
									MyAnimator.SetFloat (AnimatorControllerParameterStage, CreatureStatesV2 [StateIndex].Move [MoveIndex].AnimationStageValue);
								}
							}

							return;
						}

					} else {
						RequirementIndex = CreatureStatesV2 [StateIndex].ExitRequirements [RequirementGroupIndex].RequirementIndex.Length;
					}
				}
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
				TheFunctionPointer [index] = CastMultipleTimes;
			} else if (index == 11) {
				TheFunctionPointer [index] = CastOnce;
			} else if (index == 12) {
				TheFunctionPointer [index] = CircleCast;
			} else if (index == 13) {
				TheFunctionPointer [index] = RektangleCast;
			} else if (index == 14) {
				TheFunctionPointer [index] = RektangleCast;
			}  else if (index == 5) {
				TheFunctionPointer [index] = OnCollisionDealDamage;
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
				TheFunctionPointer [index] = RayCastHit;
			} else if (index == 21) {
				TheFunctionPointer [index] = RayCastMissed;
			}  else {
				Debug.LogWarning ("Did You Forget Something");
			}

		}
	}

	#endregion

	void OnCollisionEnter2D(Collision2D coll){
		if (CollisionOn == true) {
			DidICollide = true;
			Debug.Log (coll.gameObject.name + " Took " + CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackValues[2] + " Dmg");
		}
		if (CollideToQuit == true) {
			DidICollide = true;
		}
	}

	void OnCollisionStay2D(Collision2D coll){
		if (CollisionOn == true) {
			DidICollide = true;
			Debug.Log (coll.gameObject.name + " Took " + CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackValues[2] + " Dmg");
		}
		if (CollideToQuit == true) {
			DidICollide = true;
		}
	}

	#region Attack
	//**********************************************************
	// The LineCast RektangleCast And CircleCast
	// Is Used For Creatures With Attack Within Their Animation
	//**********************************************************

	#region CastMethods

	public void LineCast(){//if you need a different shape then a circle
	
		if (MyTimes [(int)CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackValues[1]] > TheTime [0]) {
			RaycastHit2D[] SavedCast = Physics2D.LinecastAll (transform.position, transform.position, CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].WhatCanIHit);
			if (SavedCast.Length > 0) {
				for(int i = 0; i < SavedCast.Length; i++)
					Debug.Log (SavedCast[i].transform.name + " Took DMG");
				//	SavedCast [i].gameObject.GetComponent<Script> ().RecieveDmg (Spellinfo);
			}
		}
	
	}

	public void RektangleCast(){//if you need a different shape then a circle

		if (MyTimes [(int)CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackValues[1]] > TheTime [0]) {
			Collider2D[] SavedCast = Physics2D.OverlapAreaAll (transform.position, transform.position, CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].WhatCanIHit);
			if (SavedCast.Length > 0) {
				for(int i = 0; i < SavedCast.Length; i++)
					Debug.Log (SavedCast[i].transform.name + " Took DMG");
				//	SavedCast [i].gameObject.GetComponent<Script> ().RecieveDmg (Spellinfo);
			}
		}

	}

	public void CircleCast(){// if you have an melee that does an aoe 

		if (MyTimes [(int)CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackValues[1]] > TheTime [0]) {
			Collider2D[] SavedCast = Physics2D.OverlapCircleAll (transform.position, CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackValues [0], CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].WhatCanIHit);
			if (SavedCast.Length > 0) {
				for (int i = 0; i < SavedCast.Length; i++) {
					Debug.Log (SavedCast[i].transform.name + " Took DMG");
				//	SavedCast [i].gameObject.GetComponent<Script> ().RecieveDmg (Spellinfo);
				}
			}
		}

	}

	#endregion

	public void OnCollisionDealDamage(){
		if (CollisionOn == false) {
			CollisionOn = true;
		}
	}

	public void CastOnce(){//cast the spell once

		if (AttackBool == false) {
			if (MyTimes [(int)CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackValues[1]] < TheTime [0]) {
				for (int i = 0; i < CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].MovementIndexToAttack.Length; i++) {
					if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].MovementIndexToAttack [i] == MoveIndex) {
						AttackBool = true; 
						Instantiate(CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].SpellInfo.Spell, transform.GetChild(0).position, Quaternion.identity).GetComponent<FSM_BulletBehaviour>().SetDmgModifiers(CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].SpellInfo);
					}
				}
			}
		}

	}
	public void CastMultipleTimes(){//if spell can be cast muliple times

		if (MyTimes [(int)CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackValues [1]] < TheTime [0]) {
			for (int i = 0; i < CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].MovementIndexToAttack.Length; i++) {
				if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].MovementIndexToAttack [i] == MoveIndex) {
					Instantiate (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].SpellInfo.Spell, transform.GetChild (0).position, Quaternion.identity).GetComponent<FSM_BulletBehaviour> ().SetDmgModifiers (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].SpellInfo);
					MyTimes [(int)CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackValues [1]] = CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackValues [0] + TheTime [0];//setting new time
				}
			}
		}

	}

	#endregion

	#region MovementMethods

	public void FollowNodePathToTarget(){
		//update nodemap
		FollowNodes = true;
	}

	public void GoStraightToTarget(){//Movement speed is index 0

		if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {
			CreatureStatesV2 [StateIndex].Vectors [0] = (target.position - transform.position).normalized;
		} else {
			CreatureStatesV2 [StateIndex].Vectors [0] = (target.position - transform.position).normalized;
		}

	}

	public void LockMovementDirectionToTarget(){
	
		if (MovementBool == true) {
			MovementBool = false;
			if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {
				CreatureStatesV2 [StateIndex].Vectors [0] = (target.position - transform.position).normalized;
			} else {
				CreatureStatesV2 [StateIndex].Vectors [0] = (target.position - transform.position).normalized;
			}
		}

	}

	public void LockMovementDirectionToNextNode(){
		//update node map

		if (MovementBool == true) {
			MovementBool = false;
			if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {
				//CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].Vectors [0] = (target - transform.position).normalized;
			} else {
				//CreatureStatesV2 [StateIndex].Move [MoveIndex].Vectors [0] = (target - transform.position).normalized;
			}
		}

	}

	public void LockCurrentVector(){//TODO this is currently impossible
		if (MovementBool == true) {
			MovementBool = false;
			if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {
		//		CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].Vectors [0] = (target.position - transform.position).normalized;
			} else {
				CreatureStatesV2 [StateIndex].Vectors [0] = CreatureStatesV2 [PreviourStateIndex].Vectors [0];
		//		CreatureStatesV2 [StateIndex].Move [MoveIndex].Vectors [0] = (target.position - transform.position).normalized;
			}
		}
	}

/*	public void StandStill(){

		if (CreatureStatesV2 [StateIndex].AttackandmoveOrMove == true) {
			if (MyTimes [(int)CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].MovementValues [0]] > TheTime [0]) {
				ResetTimeMovementVariables ();
				if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement.Length <= MoveIndex + 1) {
					MoveIndex = 0;
					StateFinished = true;
				} else {
					MoveIndex++;
					SetTimeMovementVariables ();
				}
			}
		} else {
			if (MyTimes [(int)CreatureStatesV2 [StateIndex].Move [MoveIndex].MovementValues [0]] > TheTime [0]) {
				ResetTimeMovementVariables ();
				if (CreatureStatesV2 [StateIndex].Move.Length <= MoveIndex + 1) {
					MoveIndex = 0;
					StateFinished = true;
				} else {
					MoveIndex++;
					SetTimeMovementVariables ();
				}
			}
		}

	}*/

	public void TeleportDistance(){

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
					transform.position += CreatureStatesV2 [StateIndex].Vectors [0] * CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].MovementValues [0];
					WhatToDoWhenDone ();
				}
			} else {
				if (MyAnimator.GetBool (AnimatorControllerParameterStop) == false) {
					transform.position += CreatureStatesV2 [StateIndex].Vectors [0] * CreatureStatesV2 [StateIndex].Move [MoveIndex].MovementValues [0];
					WhatToDoWhenDone ();
				}
			}
		}
	}

	public void Stop(){

		if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {
			if (MyTimes [(int)CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].MovementValues [1]] < TheTime [0]) {
				ResetTimeMovementVariables ();
				WhatToDoWhenDone ();
			}
		} else {
			if (MyTimes [(int)CreatureStatesV2 [StateIndex].Move [MoveIndex].MovementValues [1]] < TheTime [0]) {
				ResetTimeMovementVariables ();
				WhatToDoWhenDone ();
			}
		}

	}

	public void Walk(){

		if (FollowNodes == true) {
			//follow them
		} else {
			if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {
				if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].MovementValues [2] > CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].MovementValues [1]) {
					CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].MovementValues [2] = 0;
					WhatToDoWhenDone();
				} else {
					if (MyAnimator.GetBool (AnimatorControllerParameterStop) == false) {
						CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].MovementValues [2] = Vector2.Distance (Vector2.zero, CreatureStatesV2 [StateIndex].Vectors [0] * CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].MovementValues [0] * Time.deltaTime);
						transform.position += CreatureStatesV2 [StateIndex].Vectors [0] * CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].MovementValues [0] * Time.deltaTime;
					}
				}

			} else {
				if (CreatureStatesV2 [StateIndex].Move [MoveIndex].MovementValues [2] > CreatureStatesV2 [StateIndex].Move [MoveIndex].MovementValues [1]) {
					CreatureStatesV2 [StateIndex].Move [MoveIndex].MovementValues [2] = 0;
					WhatToDoWhenDone ();
				} else {
					if (MyAnimator.GetBool (AnimatorControllerParameterStop) == false) {
						CreatureStatesV2 [StateIndex].Move [MoveIndex].MovementValues [2] += Vector2.Distance (Vector2.zero, CreatureStatesV2 [StateIndex].Vectors [0] * CreatureStatesV2 [StateIndex].Move [MoveIndex].MovementValues [0] * Time.deltaTime);
						transform.position += CreatureStatesV2 [StateIndex].Vectors [0] * CreatureStatesV2 [StateIndex].Move [MoveIndex].MovementValues [0] * Time.deltaTime;
					}
				}
			}
		}


	}

	public void TimeMovement(){
		
		if (FollowNodes == true) {
			//follow them
		} else {
			if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {
				if (MyTimes [(int)CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].MovementValues [1]] < TheTime [0]) {//TODO i dont like the cast(int) because this is called once every frame
					ResetTimeMovementVariables ();
					WhatToDoWhenDone();
				} else {
					if (MyAnimator.GetBool (AnimatorControllerParameterStop) == false) {
						transform.position += CreatureStatesV2 [StateIndex].Vectors [0] * CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].MovementValues [2] * Time.deltaTime;
					}
				}

			} else {
				if (MyTimes [(int)CreatureStatesV2 [StateIndex].Move [MoveIndex].MovementValues [1]] < TheTime [0]) {//TODO i dont like the cast(int) because this is called once every frame
					ResetTimeMovementVariables ();
					WhatToDoWhenDone();
				} else {
					if (MyAnimator.GetBool (AnimatorControllerParameterStop) == false) {
						transform.position += CreatureStatesV2 [StateIndex].Vectors [0] * CreatureStatesV2 [StateIndex].Move [MoveIndex].MovementValues [2] * Time.deltaTime;
					}
				}
			}
		}
	}

	void WhatToDoWhenDone(){
		if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {
			if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement.Length <= MoveIndex + 1) {
				MoveIndex = 0;
				StateFinished = true;
			} else {
				MoveIndex++;
				SetTimeMovementVariables ();
			}
			if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].ChangeAnimationStage == true) {//if the exit requirement is met, then i need this to change the animationstate to the correct movement behaviour
				MyAnimator.SetFloat (AnimatorControllerParameterStage, CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].AnimationStageValue);
			}
			if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].ChangeAttackIndex == true) {
				AttackIndex = CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [MoveIndex].AttackIndexValue;
				AttackBool = false;
			}
			DidICollide = false;
			CollisionOn = false;
			MovementBool = true;
		} else {
			if (CreatureStatesV2 [StateIndex].Move.Length <= MoveIndex + 1) {
				MoveIndex = 0;
				StateFinished = true;
			} else {
				MoveIndex++;
				SetTimeMovementVariables ();
			}
			if (CreatureStatesV2 [StateIndex].Move [MoveIndex].ChangeAnimationStage == true) {//if the exit requirement is met, then i need this to change the animationstate to the correct movement behaviour
				MyAnimator.SetFloat (AnimatorControllerParameterStage, CreatureStatesV2 [StateIndex].Move [MoveIndex].AnimationStageValue);
			}
			DidICollide = false;
			CollisionOn = false;
			MovementBool = true;
		}
	}

	#endregion

	#region ExitRequierments

	public void Nothing(){
		RequirementReturnTrue = true;
	}

	public void Collision(){
		if (CollideToQuit == false) {
			CollideToQuit = true;
		}
		if (DidICollide == true) {
			RequirementReturnTrue = true;
		}
	}
		
	#region TheTime

	public void TimeExit(){

		if (MyTimes [(int)CreatureStatesV2 [StateIndex].ExitRequirements [RequirementGroupIndex].ValueRequirements [1]] < TheTime [0]) {//TODO i dont like the cast(int) because this is called once every frame
			RequirementReturnTrue = true;
		}

	}

	void ResetAllTimeVariables(){
		
		for (int i = 0; i < MyTimes.Length; i++) {
			MyTimes [i] = 0;
		}

		for (int i = 0; i < CreatureStatesV2 [StateIndex].ExitRequirements.Length; i++) {//going throught each exit requirement groups
			for (int j = 0; j < CreatureStatesV2 [StateIndex].ExitRequirements [i].RequirementIndex.Length; j++) {//going through the group
				if (CreatureStatesV2 [StateIndex].ExitRequirements [i].RequirementIndex [j] == 17) {
					CreatureStatesV2 [StateIndex].ExitRequirements [i].ValueRequirements [1] = 0;
				}
			}
		}

		if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {

			ResetAttackSpeedVariable ();
			ResetTimeMovementVariables ();

		} else {

			ResetTimeMovementVariables ();

		}

	}

	void ResetTimeMovementVariables(){
		if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {

			for (int i = 0; i < CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement.Length; i++) {//going throught each exit requirement groups
				if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [i].AttackMovementStateIndex == 9) {
					CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [i].MovementValues [1] = 0;
				}
			}

		} else {

			for (int i = 0; i < CreatureStatesV2 [StateIndex].Move.Length; i++) {//going throught each exit requirement groups
				if (CreatureStatesV2 [StateIndex].Move [i].MovementStateIndex == 9) {
					CreatureStatesV2 [StateIndex].Move [i].MovementValues [1] = 0;
				}
			}

		}
	}

	void ResetAttackSpeedVariable(){
		for (int i = 0; i < CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack.Length; i++) {//going throught each exit requirement groups
			CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [i].AttackValues [1] = 0;
		}
	}

	void SetAllTimeVariables(){

		for (int i = 0; i < CreatureStatesV2 [StateIndex].ExitRequirements.Length; i++) {//going throught each exit requirement groups
			for (int j = 0; j < CreatureStatesV2 [StateIndex].ExitRequirements [i].RequirementIndex.Length; j++) {//going through the group
				if (CreatureStatesV2 [StateIndex].ExitRequirements [i].RequirementIndex [j] == 17) {
					for (int k = 0; k < MyTimes.Length; k++) {
						if (MyTimes [k] == 0) {
							CreatureStatesV2 [StateIndex].ExitRequirements [i].ValueRequirements [1] = k;
							MyTimes [k] = CreatureStatesV2 [StateIndex].ExitRequirements [i].ValueRequirements [0] + TheTime [0];
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
		if (CreatureStatesV2 [StateIndex].AttackOrMove == true) {

			for (int i = 0; i < CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement.Length; i++) {//going throught each exit requirement groups
				if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [i].AttackMovementStateIndex == 9 || CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [i].AttackMovementStateIndex == 7) {

					for (int k = 0; k < MyTimes.Length; k++) {
						if (MyTimes [k] == 0) {
							CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [i].MovementValues [1] = k;
							MyTimes [k] = CreatureStatesV2 [StateIndex].AttackAndMove.TheAttackMovement [i].MovementValues [0] + TheTime [0];
							break;
						}
					}

				}
			}

		} else {

			for (int i = 0; i < CreatureStatesV2 [StateIndex].Move.Length; i++) {//going throught each exit requirement groups
				if (CreatureStatesV2 [StateIndex].Move [i].MovementStateIndex == 9 || CreatureStatesV2 [StateIndex].Move [i].MovementStateIndex == 7) {

					for (int k = 0; k < MyTimes.Length; k++) {
						if (MyTimes [k] == 0) {
							CreatureStatesV2 [StateIndex].Move [i].MovementValues [1] = k;
							MyTimes [k] = CreatureStatesV2 [StateIndex].Move [i].MovementValues [0] + TheTime [0];
							break;
						}
					}

				}
			}
		}
	}

	void SetAttackSpeedVariables(){
		for (int k = 0; k < MyTimes.Length; k++) {
			if (MyTimes [k] == 0) {
				if (CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackValues [0] != 0) {
					CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackValues [1] = k;
					MyTimes [k] = CreatureStatesV2 [StateIndex].AttackAndMove.TheAttack [AttackIndex].AttackValues [0] + TheTime [0];
					break;
				} else {
					break;
				}
			}
		}
	}

	#endregion

	public void DistanceToPlayerLessThen(){
		
		if (Vector3.Distance (target.position, transform.position) < CreatureStatesV2 [StateIndex].ExitRequirements [RequirementGroupIndex].ValueRequirements [0]) {
			RequirementReturnTrue = true;
		}
	}

	public void DistanceToPlayerMoreThen(){
		if (Vector3.Distance (target.position, transform.position) > CreatureStatesV2 [StateIndex].ExitRequirements [RequirementGroupIndex].ValueRequirements [0]) {
			RequirementReturnTrue = true;
		}
	}

	public void RayCastHit(){

		if (Physics2D.Linecast (transform.position, target.position, LayerMask.NameToLayer("Wall")).transform != null) {
			RequirementReturnTrue = true;
		}

	}

	public void RayCastMissed(){

		if (Physics2D.Linecast (transform.position, target.position, LayerMask.NameToLayer("Wall")).transform == null) {
			RequirementReturnTrue = true;
		}

	}

	#endregion
}