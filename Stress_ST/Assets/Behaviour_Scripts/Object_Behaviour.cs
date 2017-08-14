using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;


public class Object_Behaviour : MonoBehaviour {

	public Object_Phase_Behaviour[] CreaturePhases;
	public float Health = 5;
	public float Damage = 2;
	public float MovementSpeed = 3;

	int[] _AttackIndex = new int[1];
	int[] _MovementIndex = new int[1];
	int _CreaturePhase = 0;

	Transform _MyTransform;
	Transform _Traget;
	List<Behaviour_Default> _CollisionBehaviours;
	LayerMask[] _LayerMasks;
	public Animator MyAnimator;
	public GameObject AttackPosition;

	Vector3[] MyMovementVector = new Vector3[1];
	Vector3[] MyRotationVector = new Vector3[1];
	public float[] TheTime = new float[1];//TODO Going To Add This To The GameManager, Depending On How Expensive This Is

	///<summary>
	///[0] == Stop, [1] == AnimatorStage, [2] == Shoot, [3] == LockDirection
	/// </summary>
	int[] AnimatorVariables = new int[4] {//TODO Going To Add This To The GameManager, Depending On How Expensive This Is
		Animator.StringToHash ("Stop"),
		Animator.StringToHash ("AnimatorStage"),
		Animator.StringToHash ("Shoot"),
		Animator.StringToHash ("LockDirection")
	};

	void Start(){//Setting All FunctionPointer Refrences + casting enum value to int
		_MyTransform = transform;
		_MyTransform.position += Vector3.zero;
		_Traget = GameObject.Find ("Hero v5").transform;
	
		for (int j = 0; j < CreaturePhases.Length; j++) {
			if (CreaturePhases [j].CheckAttackBehaviour == true) {
				for (int i = 0; i < CreaturePhases [j].Attack.Length; i++) {
					CreaturePhases [j].Attack [i].SetMethod (this, _Traget, AnimatorVariables); 
				}
			}
			for (int i = 0; i < CreaturePhases [j].Move.Length; i++) {
				CreaturePhases [j].Move [i].SetMethod (this, _Traget, AnimatorVariables);
			}
			for (int k = 0; k < CreaturePhases [j].ExitRequirementGroup.Length; k++) {
				for (int i = 0; i < CreaturePhases [j].ExitRequirementGroup [k].ExitRequirements.Length; i++) {
					CreaturePhases [j].ExitRequirementGroup [k].ExitRequirements [i].SetMethod (this, _Traget, AnimatorVariables);
				}
			}
		}

		RunSetupBehaviours ();
	}

	void Update (){
		TheTime [0] += Time.deltaTime;

		if (CreaturePhases [_CreaturePhase].CheckAttackBehaviour == true) {//If Creature Can Move And Attack
			CreaturePhases [_CreaturePhase].Attack [_AttackIndex [0]].BehaviourMethod ();//Checking Attack Method
		}
		CreaturePhases [_CreaturePhase].Move [_MovementIndex [0]].BehaviourMethod ();//Checking Movement Method

		_MyTransform.eulerAngles = MyRotationVector[0];
		_MyTransform.position += MyMovementVector [0];

		CheckIfExitRequirementsAreMet ();
	}

	void CheckIfExitRequirementsAreMet(){//Checking If I Can Change Phase

		if (_MovementIndex [0] >= CreaturePhases [_CreaturePhase].Move.Length) {//If The Last Movement_Behaviour Is Complete, Then This Is True
			for (int k = 0; k < CreaturePhases [_CreaturePhase].ExitRequirementGroup.Length; k++) {//Going Through All Exit_Requirements Groups To Check What To Do Next
				for (int i = 0; i < CreaturePhases [_CreaturePhase].ExitRequirementGroup [k].ExitRequirements.Length; i++) {//Going Through All Exit_Requirements
					if (CreaturePhases [_CreaturePhase].ExitRequirementGroup [k].ExitRequirements [i].GetBool (0) == false) {//If Any Exit_Requirements In A Group Is False, Then Move To The Next Group
						i = CreaturePhases [_CreaturePhase].ExitRequirementGroup [k].ExitRequirements.Length + 2;
					} else {
						if (i == CreaturePhases [_CreaturePhase].ExitRequirementGroup [k].ExitRequirements.Length - 1) {//If I Am At The Last Exit_Requirement, Then All Are True And I Can Change Phase

							if (CreaturePhases [_CreaturePhase].ExitRequirementGroup [k].Transition.Length > 0) {
								for (int h = 0; h < CreaturePhases [_CreaturePhase].ExitRequirementGroup [k].Transition.Length; h++) {
									CreaturePhases [_CreaturePhase].ExitRequirementGroup [k].Transition [h].BehaviourMethod ();
								}
							}

							//	Debug.Log ("Chenged From Phase " + _CreaturePhase + " To Phase " + CreaturePhases [_CreaturePhase].ExitRequirements [k].ChangeToPhase);
							RunResetBehaviours ();
							_CreaturePhase = CreaturePhases [_CreaturePhase].ExitRequirementGroup [k].ChangeToPhase;
							RunSetupBehaviours ();
							return;
						}
					}
				}
			}
		
			SetMovementBehaviour (0);
		} else {
			for (int k = 0; k < CreaturePhases [_CreaturePhase].ExitRequirementGroup.Length; k++) {//Going Through All Exit_Requirements Groups To Check What To Do Next
				if (CreaturePhases [_CreaturePhase].ExitRequirementGroup [k].CheckAllTheTime == true) {
					for (int i = 0; i < CreaturePhases [_CreaturePhase].ExitRequirementGroup [k].ExitRequirements.Length; i++) {//Going Through All Exit_Requirements
						if (CreaturePhases [_CreaturePhase].ExitRequirementGroup [k].ExitRequirements [i].GetBool (0) == false) {//If Any Exit_Requirements In A Group Is False, Then Move To The Next Group
							i = CreaturePhases [_CreaturePhase].ExitRequirementGroup [k].ExitRequirements.Length + 2;//Abit Scared To Use Break To Go Out Of The Loop So Just Did This 
						} else {
							if (i == CreaturePhases [_CreaturePhase].ExitRequirementGroup [k].ExitRequirements.Length - 1) {//If I Am At The Last Exit_Requirement, Then All Are True And I Can Change Phase

								if (CreaturePhases [_CreaturePhase].ExitRequirementGroup [k].Transition.Length > 0) {//Going Through All Transition Behaviours
									for (int h = 0; h < CreaturePhases [_CreaturePhase].ExitRequirementGroup [k].Transition.Length; h++) {
										CreaturePhases [_CreaturePhase].ExitRequirementGroup [k].Transition [h].BehaviourMethod ();
									}
								}

								//	Debug.Log ("Chenged From Phase " + _CreaturePhase + " To Phase " + CreaturePhases [_CreaturePhase].ExitRequirements [k].ChangeToPhase);
								RunResetBehaviours ();

								if (_CreaturePhase == CreaturePhases [_CreaturePhase].ExitRequirementGroup [k].ChangeToPhase) {//If Its The Same, Dont Run Setup
									SetMovementBehaviour (0);
								} else {
									_CreaturePhase = CreaturePhases [_CreaturePhase].ExitRequirementGroup [k].ChangeToPhase;
									RunSetupBehaviours ();
								}

								return;
							}
						}
					}
				}
			}
		}
	}

	void RunResetBehaviours(){//Reseting Currently Used Behaviours
		for (int k = 0; k < CreaturePhases [_CreaturePhase].ExitRequirementGroup.Length; k++) {//Going Through All Exit_RequirementGroups To Reset Them
			for (int i = 0; i < CreaturePhases [_CreaturePhase].ExitRequirementGroup [k].ExitRequirements.Length; i++) {//Going Through All Exit_Requirements
				CreaturePhases [_CreaturePhase].ExitRequirementGroup [k].ExitRequirements[i].Reset();
			}
		}
	}

	void RunSetupBehaviours(){//Calls A Setup Method For All Behaviours In The Current Object_Phase
		_CollisionBehaviours = new List<Behaviour_Default> ();

		if (CreaturePhases [_CreaturePhase].CheckAttackBehaviour == true) {
			for (int i = 0; i < CreaturePhases [_CreaturePhase].Attack.Length; i++) {
				CreaturePhases [_CreaturePhase].Attack [i].OnSetup ();
			}
		}
		for (int i = 0; i < CreaturePhases [_CreaturePhase].Move.Length; i++) {
			CreaturePhases [_CreaturePhase].Move [i].OnSetup ();
		}
		for (int k = 0; k < CreaturePhases [_CreaturePhase].ExitRequirementGroup.Length; k++) {
			for (int i = 0; i < CreaturePhases [_CreaturePhase].ExitRequirementGroup [k].ExitRequirements.Length; i++) {
				CreaturePhases [_CreaturePhase].ExitRequirementGroup [k].ExitRequirements [i].OnSetup ();
			}
		}

		SetMovementBehaviour (0);
		for (int k = 0; k < CreaturePhases [_CreaturePhase].ExitRequirementGroup.Length; k++) {
			for (int i = 0; i < CreaturePhases [_CreaturePhase].ExitRequirementGroup [k].ExitRequirements.Length; i++) {
				CreaturePhases [_CreaturePhase].ExitRequirementGroup [k].ExitRequirements [i].OnEnter ();
			}
		}
	}

	public void SetAttackBehaviour(int theValue){//This Is Being Called From MovementBehaviours If There Is An Attack Behaviour
		_AttackIndex [0] = theValue;
		
		if (CreaturePhases [_CreaturePhase].Move [_MovementIndex [0]].GetBool (1) == true) {//If The Movement Wants To Reset The Attack Baheviour
			CreaturePhases [_CreaturePhase].Attack [_AttackIndex [0]].Reset ();
			CreaturePhases [_CreaturePhase].Attack [_AttackIndex [0]].OnEnter ();
		}

	}

	public void SetMovementBehaviour(int theValue){
		_MovementIndex [0] = theValue;

		if (_MovementIndex [0] < CreaturePhases [_CreaturePhase].Move.Length) {
			CreaturePhases [_CreaturePhase].Move [_MovementIndex [0]].Reset ();
			CreaturePhases [_CreaturePhase].Move [_MovementIndex [0]].OnEnter ();
		}
	}

		
	public void SetMovementVector(Vector3 theMoveDirectoin){
		MyMovementVector [0] = theMoveDirectoin;
	}
	public Vector3[] GetMovementVector(){
		return MyMovementVector;
	}

	public void SetRotationVector(Vector3 theRotationDirectoin){
		MyRotationVector [0] = theRotationDirectoin;
	}

	public Vector3[] GetRotationVector(){
		return MyRotationVector;
	}

	public float[] GetTheTime(){
		return TheTime;
	}

	public void SetCollisionBehaviour(Behaviour_Default theBehaviours){//Adding Behaviours To A List For CollisionDetection
		_CollisionBehaviours.Add (theBehaviours);
	}

	void OnCollisionEnter2D(Collision2D coll){

		if (_CollisionBehaviours != null) {
			for (int i = 0; i < _CollisionBehaviours.Count; i++) {//Going Through My List Of Behaviours That Need Collision Info. 
				_LayerMasks = _CollisionBehaviours [i].GetLayerMask ();//Getting The LayerMask Values From The Behaviours
				if (_LayerMasks != null) {
					for (int j = 0; j < _LayerMasks.Length; j++) {
						if (coll.gameObject.layer == Mathf.Log (_LayerMasks [i].value, 2)) {//The Mathf.Log Is Used To Change The Value From Byte/Bit To Normal Numbers To Match The Layer ID
							_CollisionBehaviours [i].SetCollision (coll);
							_CollisionBehaviours [i].BehaviourMethod ();
						}
					}
				}
			}
		}
	}


}