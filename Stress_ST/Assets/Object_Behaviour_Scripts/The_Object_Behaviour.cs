using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Object_Behaviour : MonoBehaviour {

	[Tooltip("Object Phases Of Behaviours")]
	public The_Object_Phase_Behaviour[] ObjectPhases;
	[Space(20)]

	public float Health = 5;
	public float Damage = 2;
	public float MovementSpeed = 3;

	int[] _BehaviourIndex = new int[1];
	int _CreaturePhase = 0;

	[HideInInspector]
	public Transform _MyTransform;
	[HideInInspector]
	public Transform _Target;
	List<Behaviour_Default> _CollisionBehaviours;
	LayerMask[] _LayerMasks;

	[HideInInspector]
	public Animator MyAnimator;

	[HideInInspector]
	public float[] TheTime = new float[1];//TODO Going To Add This To The GameManager, Depending On How Expensive This Is
	Vector3[] MyMovementVector = new Vector3[1];
	Vector3[] MyRotationVector = new Vector3[1];
	The_Default_Behaviour.ResetState _ResetEnum = The_Default_Behaviour.ResetState.ResetOnPhaseChange;


	///<summary>
	///[0] == Stop, [1] == AnimatorStage, [2] == Shoot, [3] == LockDirection
	/// </summary>
	public int[] AnimatorVariables = new int[5] {//TODO Going To Add This To The GameManager, Depending On How Expensive This Is
		Animator.StringToHash ("Stop"),
		Animator.StringToHash ("AnimatorStage"),
		Animator.StringToHash ("Shoot"),
		Animator.StringToHash ("LockDirection"),
		Animator.StringToHash ("StopExitCheck")
	};

	void Start(){//Setting All FunctionPointer Refrences + casting enum value to int
	//	Health *= difficulty TODO
	//	Damage *= difficulty TODO
	//	MovementSpeed *= difficulty TODO
		MyAnimator = GetComponent<Animator>();
		_MyTransform = transform;
		_Target = GameObject.Find ("Hero v5").transform;

		for (int j = 0; j < ObjectPhases.Length; j++) {
				
			for (int i = 0; i < ObjectPhases [j].Behaviours.Length; i++) {
				ObjectPhases [j].Behaviours [i].SetMethod (this);
			}
			for (int k = 0; k < ObjectPhases [j].ExitGroups.Length; k++) {
				for (int i = 0; i < ObjectPhases [j].ExitGroups [k].ExitRequirements.Length; i++) {
					ObjectPhases [j].ExitGroups [k].ExitRequirements [i].SetMethod (this);
				}
			}
		}

		for (int i = 0; i < ObjectPhases [_CreaturePhase].PhaseChangeInfo.Length; i++) {//If The Object Start With A Transition
			ObjectPhases [_CreaturePhase].PhaseChangeInfo [i].OnEnter ();
		}
	}

	void Update (){
		TheTime [0] += Time.deltaTime;

		ObjectPhases [_CreaturePhase].Behaviours [_BehaviourIndex [0]].BehaviourUpdate();

	
		if(MyAnimator.GetBool(AnimatorVariables[3]) == false)
			_MyTransform.eulerAngles = MyRotationVector[0];//Updating Rotations

		if(MyAnimator.GetBool(AnimatorVariables[0]) == false)
			_MyTransform.position += MyMovementVector [0];//Updating Positions

		if (MyAnimator.GetBool(AnimatorVariables [4]) == false) {
			CheckIfExitRequirementsAreMet ();
		}
	}

	void CheckIfExitRequirementsAreMet(){//Checking If I Can Change Phase

		if (ObjectPhases [_CreaturePhase].Behaviours.Length <= _BehaviourIndex [0]) {
			for (int k = 0; k < ObjectPhases [_CreaturePhase].ExitGroups.Length; k++) {//Going Through All Exit_Requirements Groups To Check What To Do Next
				for (int i = 0; i < ObjectPhases [_CreaturePhase].ExitGroups [k].ExitRequirements.Length; i++) {//Going Through All Exit_Requirements
					if (ObjectPhases [_CreaturePhase].ExitGroups [k].ExitRequirements [i].GetBool (2) == false) {//Requirement Check If True
						i = ObjectPhases [_CreaturePhase].ExitGroups [k].ExitRequirements.Length + 2;
					} else {
						if (i == ObjectPhases [_CreaturePhase].ExitGroups [k].ExitRequirements.Length - 1) {//If I Am At The Last Exit_Requirement, Then All Are True And I Can Change Phase
							for (int s = 0; s < ObjectPhases [_CreaturePhase].PhaseChangeInfo.Length; s++) {
								ObjectPhases [_CreaturePhase].PhaseChangeInfo [s].OnExit ();
							}
							_CreaturePhase = ObjectPhases [_CreaturePhase].ExitGroups [k].ChangeToPhase;
							RunResetBehaviours ();
							return;
						}
					}
				}
			}
			for (int s = 0; s < ObjectPhases [_CreaturePhase].PhaseChangeInfo.Length; s++) {//If The Code Reaches Here, Then Non If The Exit Requirements Were Met, So Then Im reseting The Phase
				ObjectPhases [_CreaturePhase].PhaseChangeInfo [s].OnExit ();
			}
			RunResetBehaviours ();
		} else {
			for (int k = 0; k < ObjectPhases [_CreaturePhase].ExitGroups.Length; k++) {//Going Through All Exit_Requirements Groups To Check What To Do Next
				if (ObjectPhases [_CreaturePhase].ExitGroups [k].CheckAllTheTime == true) {
					for (int i = 0; i < ObjectPhases [_CreaturePhase].ExitGroups [k].ExitRequirements.Length; i++) {//Going Through All Exit_Requirements
						if (ObjectPhases [_CreaturePhase].ExitGroups [k].ExitRequirements [i].GetBool (2) == false) {//Requirement Check If True
							i = ObjectPhases [_CreaturePhase].ExitGroups [k].ExitRequirements.Length + 2;
						} else {
							Debug.Log ("HERE " + i + " | " + (ObjectPhases [_CreaturePhase].ExitGroups [k].ExitRequirements.Length - 1));
							if (i == ObjectPhases [_CreaturePhase].ExitGroups [k].ExitRequirements.Length - 1) {//If I Am At The Last Exit_Requirement, Then All Are True And I Can Change Phase
								for (int s = 0; s < ObjectPhases [_CreaturePhase].PhaseChangeInfo.Length; s++) {
									ObjectPhases [_CreaturePhase].PhaseChangeInfo [s].OnExit ();
								}
								_CreaturePhase = ObjectPhases [_CreaturePhase].ExitGroups [k].ChangeToPhase;
								RunResetBehaviours ();
								return;
							}
						}
					}
				}
			}
		}
	}

	void RunResetBehaviours (){//Reseting Currently Used Behaviours
		Debug.Log ("HERE");
		_CollisionBehaviours = new List<Behaviour_Default> ();
		_ResetEnum = The_Default_Behaviour.ResetState.ResetOnPhaseChange;
		_BehaviourIndex [0] = 0;
		
		for (int i = 0; i < ObjectPhases [_CreaturePhase].Behaviours.Length; i++) {//Reseting Current Behaviours
			if(	ObjectPhases [_CreaturePhase].Behaviours [i].TheResetState == _ResetEnum){
				ObjectPhases [_CreaturePhase].Behaviours [i].Reset ();
			}
		}

		for (int k = 0; k < ObjectPhases [_CreaturePhase].ExitGroups.Length; k++) {//Going Through All Exit_RequirementGroups To Reset Them
			for (int i = 0; i < ObjectPhases [_CreaturePhase].ExitGroups [k].ExitRequirements.Length; i++) {//Resetting all Exit Requirements
				if (ObjectPhases [_CreaturePhase].ExitGroups [k].ExitRequirements [i].TheResetState == _ResetEnum) {
					ObjectPhases [_CreaturePhase].ExitGroups [k].ExitRequirements [i].Reset ();
				}
			}
		}

		for (int i = 0; i < ObjectPhases [_CreaturePhase].PhaseChangeInfo.Length; i++) {
			ObjectPhases [_CreaturePhase].PhaseChangeInfo [i].OnReset ();
			ObjectPhases [_CreaturePhase].PhaseChangeInfo [i].OnEnter ();
		}

		ObjectPhases [_CreaturePhase].Behaviours [_BehaviourIndex [0]].OnEnter ();
	}
		
	public void SetMovementBehaviour(int theValue){
		Debug.Log ("SETTING");
		_BehaviourIndex [0] = theValue;

		if (_BehaviourIndex [0] < ObjectPhases [_CreaturePhase].Behaviours.Length) {
			ObjectPhases [_CreaturePhase].Behaviours [_BehaviourIndex [0]].OnEnter ();
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

	//-------------------------------------------------------------------------------------

	void SetShooting(int boolValue){
		if (boolValue == 0) {
			MyAnimator.SetBool (AnimatorVariables [2], false);
		} else {
			MyAnimator.SetBool(AnimatorVariables[2], true);
		}
	}

	void SetLockDirection(int boolValue){
		if (boolValue == 0) {
			MyAnimator.SetBool (AnimatorVariables [3], false);
		} else {
			MyAnimator.SetBool(AnimatorVariables[3], true);
		}
	}

	void SetStop(int boolValue){
		if (boolValue == 0) {
			MyAnimator.SetBool (AnimatorVariables [0], false);
		} else {
			MyAnimator.SetBool(AnimatorVariables[0], true);
		}
	}

}