using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class The_Object_Behaviour {

	public AStarPathfinding_RoomPaths _CreateThePath;//Pathfinding For Room To Room
	public CreatingObjectNodeMap _PersonalNodeMap;//Personal Node Pathfinding. Creating A Small "Room" Which The Creature Know Whats Inside
	public DefaultBehaviour _TheObject;

	[Tooltip("PathfindingNodeID is the cost to move to the different nodes //// 0 = normal nodes(1) //// 1 = undestructable walls(100) //// 2 = other units(3)")]
	public float[] PathfindingNodeID = new float[3];//when going through the nodemap the this is the value for the different tiles when navigating


	[Tooltip("Object Phases Of Behaviours")]
	public The_Object_Phase_Behaviour[] ObjectPhases;



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
	[HideInInspector] public bool[] GotPushed = new bool[1];


	[Space(50)]
	public Vector3[] ObjectCurrentVector = new Vector3[1];
	public Vector3[] ObjectTargetVector = new Vector3[1];


	///<summary>
	///[0] == Stop, [1] == AnimatorStage, [2] == Shoot, [3] == LockDirection
	/// </summary>
	[HideInInspector] public int[] AnimatorVariables;

	public void BehaviourStart(){//Setting All FunctionPointer Refrences + casting enum value to int

		MyAnimator = _TheObject.MyAnimator.MyAnimator;
		AnimatorVariables = _TheObject.MyAnimator.AnimatorVariables;
		_Target = _TheObject._TheTarget.transform;

		ObjectCurrentVector[0] = Vector3.right;
		ObjectTargetVector[0] = _Target.position;

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

		ObjectPhases [_CreaturePhase].Behaviours [_BehaviourIndex [0]].OnEnter ();

	}

	public void InBetween(){
//		ObjectPhases [_CreaturePhase].Behaviours [_BehaviourIndex [0]].OnEnter ();
	}

	public void BehaviourUpdate (){
		TheTime [0] += Time.deltaTime;

		ObjectPhases [_CreaturePhase].Behaviours [_BehaviourIndex [0]].BehaviourUpdate();
	
		if(MyAnimator.GetBool(AnimatorVariables[3]) == false)
			_MyTransform.eulerAngles = MyRotationVector[0];//Updating Rotations

		if(MyAnimator.GetBool(AnimatorVariables[0]) == false)
			_TheObject.GetComponent<Rigidbody2D>().velocity = MyMovementVector [0] * 30;//Updating Positions

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
		
	public void SetMovementBehaviour(int theValue){//If TheValue Is Bigger Then The Amount Of Behaviours In The Phase. Then It Forces Itself To Exit To The First Exitgroup ChangeToPhase Value
		if (theValue < ObjectPhases [_CreaturePhase].Behaviours.Length) {
			_BehaviourIndex [0] = theValue;
			ObjectPhases [_CreaturePhase].Behaviours [_BehaviourIndex [0]].OnEnter ();
		} else {
		//	_BehaviourIndex [0] = 0;
		//	ObjectPhases [_CreaturePhase].Behaviours [_BehaviourIndex [0]].OnEnter ();

			for (int s = 0; s < ObjectPhases [_CreaturePhase].PhaseChangeInfo.Length; s++) {//If You Want To ForceQuit. Then Make This True. 
				ObjectPhases [_CreaturePhase].PhaseChangeInfo [s].OnExit ();
			}
			_CreaturePhase = ObjectPhases [_CreaturePhase].ExitGroups [0].ChangeToPhase;
			RunResetBehaviours ();
			return;
		}
	}
		
	public Vector3[] GetMovementVector(){
		return MyMovementVector;
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

	public void OnCollisions(Collision2D coll){
		
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