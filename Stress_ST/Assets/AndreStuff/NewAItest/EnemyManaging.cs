using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Thoughts. Create Stats Which Decide Object State Of Mind. 
//* Like If Going To A Healing Sphere Then Use A Mobility-Ability Or If Player Low On Health Go Into Agressive State.
//* Or A State Like A Support, Where The Support Is Hiding In The Back And Follow 'Friends' But Stay Away From The Player.

//* Change Player And Enemy, To Teams. Then We Can Make It So That Spells Dont Hit My Team But Everything Else. 
//* And If Something Is Confused/Mindcontrolled Then We Can Simply Change Its State To Frendly To Red Team And So On 


//This Class Is Formed With The Needs Of The Object In Mind. A Bunker Cannot Move And Therefore Dont Need MovementLogic. 
public class EnemyManaging : CreatureRoot {

	public ObjectNodeInfo Node;

	public AnimatorVariables MyAnimatorVariables;
	public AbilityInfo MyAbilityInfo;

	[HideInInspector]
	public EnergyBar MyEnergyBar = new EnergyBar();

	public NodeInfo MyNodeInfo;//TODO CreateNodeMap Collision Changing And Add/Remove (Just Copy-Paste And Some Changes)
	public WhatToTarget Targeting;
	public CreatureWordCheckInfo CreatureWords;//Just A Container For Holding WordForHealth

	public ObjectMovement MyMovement;



	[HideInInspector]
	public Vector3 targetPoint = Vector3.zero;
	public bool velocityPushback = false;
	Rigidbody2D _MyRigidbody;
	float StunImmunity = 0;

	void Awake(){//Just Setting Stuff Here So That We Dont Have To Drag And Drop So Much In The Inspector
		MyEnergyBar.myVariables = this;
		MyAbilityInfo.myVariables = this;
		CreatureWords.myVariables = this;
		_MyRigidbody = GetComponent<Rigidbody2D> ();

		MyMovement.Setup (this);
	}

	void Start(){

		if (CanIUseAbilities == true) {
			MyAbilityInfo.AbilitySetter ();
		}

		CreatureWords.Setup();

	}


	void Update () {

		if (DissableForScenario == false) {


			if (CanIDoMovement == true && velocityPushback == false) {
				
				if (Targeting.MyMovementTarget == null) {
					Targeting.SearchAfterTarget (transform.position);	
				}

				if (MyAnimatorVariables.CanIMove == true) {
					transform.position += MyMovement.DoMovement () * (Stats.Speed * Time.deltaTime);
					Node.MyCollisionInfo.CalculateNodePos (transform.position);
				}
				if (MyAnimatorVariables.CanIRotate == true) {
					MyMovement.DoRotation ();
				}


			} else {
				if (velocityPushback == true) {
					if (Mathf.Abs(_MyRigidbody.velocity.x) + Mathf.Abs(_MyRigidbody.velocity.y) < 0.35f && Mathf.Abs(_MyRigidbody.velocity.x) + Mathf.Abs(_MyRigidbody.velocity.y) > -0.35f) {
					
						_MyRigidbody.velocity = Vector2.zero;
						velocityPushback = false;
					}
				}
				if (UpdatePosition == true)
					Node.MyCollisionInfo.CalculateNodePos (transform.position);
			}


			if (CanIRegenEnergy == true) {//TODO Not Yet Subtracting When Using Abilities 
				MyEnergyBar.RunEnergyBar ();
			}


			if (CanIUseAbilities == true) {
				
				if (Targeting.MyMovementTarget == null) {
					Targeting.SearchAfterTarget (transform.position);	
				}
				if (Targeting.AttackClass == true) {

					if (MyAnimatorVariables.AbilityRunning == false) {//Only One Ability Can Be Used At A Time
						MyAbilityInfo.AbilityChecker ();//Checking If An Ability's Criteria To Run Is Met 
					}
				}

			}

		}
	
	}

	public override void VelocityChange (float velocityPower){

		velocityPushback = true;


		if (StunImmunity <= ClockTest.TheTimes) {
			StunImmunity = ClockTest.TheTimes + 1;
			velocityPushback = true;
	//		_MyRigidbody.velocity = goDirection.normalized * moveValue;
		}
	}







	//Cuz Different Creature Need Different Things, Then This Makes It Work
	//Calling Method From Child -> Override To Get From Parent (Other Script Logic. (Basic Use Of Virtual))

	public override ObjectNodeInfo GetObjectNodeInfo (){
		return Node;
	}

	public override AnimatorVariables GetAnimatorVariables (){
		return MyAnimatorVariables;
	}

	public override AbilityInfo GetAbilityInfo (){
		return MyAbilityInfo;
	}

	public override EnergyBar GetEnergyBar (){
		return MyEnergyBar;
	}

	public override NodeInfo GetNodeInfo (){
		return MyNodeInfo;
	}

	public override WhatToTarget GetWhatToTarget (){
		return Targeting;
	}

	public override CreatureWordCheckInfo GetCreatureWordCheckInfo (){
		return CreatureWords;
	}

	public override ObjectMovement GetObjectMovement (){
		return MyMovement;
	}
































	void OnDrawGizmosSelected(){


		foreach (NodeTest s in MyNodeInfo.MyAStar.cl()) {

			if (s != null && s.NodeSearchedThrough == true) {
				Gizmos.color = new Color (100,100,100, 0.5f);
				Gizmos.DrawSphere (new Vector3((Node.MyCollisionInfo.XNode + (s.PosX - ((StressCommonlyUsedInfo.NodesWidth - 1) / 2))) * StressCommonlyUsedInfo.DistanceBetweenNodes, (Node.MyCollisionInfo.YNode + (s.PosY - ((StressCommonlyUsedInfo.NodesWidth - 1) / 2))) * StressCommonlyUsedInfo.DistanceBetweenNodes, 0), 0.025f);
			}

		}

		foreach (NodeTest s in MyNodeInfo.MyAStar.ol()) {

			if (s != null && s.NodeSearchedThrough == true) {
				Gizmos.color = new Color (1, 0, 0, 0.5f);
				Gizmos.DrawSphere (new Vector3((Node.MyCollisionInfo.XNode + (s.PosX - ((StressCommonlyUsedInfo.NodesWidth - 1) / 2))) * StressCommonlyUsedInfo.DistanceBetweenNodes, (Node.MyCollisionInfo.YNode + (s.PosY - ((StressCommonlyUsedInfo.NodesWidth - 1) / 2))) * StressCommonlyUsedInfo.DistanceBetweenNodes, 0), 0.025f);
			}

		}

		foreach (NodeTest s in MyNodeInfo.MyNodePath) {

			if (s != null && s.NodeSearchedThrough == true) {
				Gizmos.color = new Color (255,255,255, 0.5f);
				Gizmos.DrawSphere (new Vector3((Node.MyCollisionInfo.XNode + (s.PosX - ((StressCommonlyUsedInfo.NodesWidth - 1) / 2))) * StressCommonlyUsedInfo.DistanceBetweenNodes, (Node.MyCollisionInfo.YNode + (s.PosY - ((StressCommonlyUsedInfo.NodesWidth - 1) / 2))) * StressCommonlyUsedInfo.DistanceBetweenNodes, 0), 0.025f);
			}

		}

	}
}
