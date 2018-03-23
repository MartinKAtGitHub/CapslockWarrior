using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Thoughts. Create Stats Which Decide Object State Of Mind. 
//* Like If Going To A Healing Sphere Then Use A Mobility-Ability Or If Player Low On Health Go Into Agressive State.
//* Or A State Like A Support, Where The Support Is Hiding In The Back And Follow 'Friends' But Stay Away From The Player.


public class EnemyManaging : CreatureRoot {

	public ObjectNodeInfo Node;

	public AnimatorVariables MyAnimatorVariables;
	public AbilityInfo MyAbilityInfo;

	[HideInInspector]
	public EnergyBar MyEnergyBar = new EnergyBar();

	public NodeInfo MyNodeInfo;//TODO CreateNodeMap Collision Changing And Add/Remove (Just Copy-Paste And Some Changes)
	public WhatToTarget Targeting;
	public CreatureWordCheckInfo CreatureWords;//Just A Container For Holding WordForHealth

	[Tooltip("This Dissables Everything Related To Creature AI/Behaviour/Logic.")]
	public bool DissableForScenario = false;

	[Tooltip("Dissables Movement If False. (Including A* Search")]
	public bool CanIDoMovement = true;

	[Tooltip("Update Node Position, Every Single Frame. Always Runs If True (Used For Testing Mainly Atm)")]
	public bool UpdatePosition = true;

	[Tooltip("All Creature That Have Abilities Also Have An Energy 'Bar'. So Abilities Cost Energy And Have A CD (Might Remove The CD)")]
	public bool CanIRegenEnergy = true;

	[Tooltip("Can The Creature Use Its Abilities")]
	public bool CanIUseAbilities = true;

	Vector3 targetPoint = Vector3.zero;


	void Awake(){//Just Setting Stuff Here So That We Dont Have To Drag And Drop So Much In The Inspector
		MyEnergyBar.MyManager = this;
		MyAbilityInfo.MyManager = this;
	    CreatureWords.myManager = this;
	}

	void Start(){

		if (CanIUseAbilities == true) {
			MyAbilityInfo.AbilitySetter ();
		}

		CreatureWords.Setup();

	}


	void Update () {

		if (DissableForScenario == false) {

			if (CanIDoMovement == true) {

				if (Targeting.MyMovementTarget == null) {
					Targeting.SearchAfterTarget (transform.position);	
					Debug.Log (Targeting.MyMovementTarget);
				}
				Debug.Log (Targeting.MyMovementTarget);

				MyNodeInfo.MyAStar.StartRunning (this);
				Debug.Log ("here1 " + MyAnimatorVariables.CanIMove);

				if (MyAnimatorVariables.CanIMove == true) {
					//TODO Make Movement Script That Handles Stuff + Rotations
					Debug.Log ("here1");

					if (!(MyNodeInfo.MyNodePath [1].PosX == 39 && MyNodeInfo.MyNodePath [1].PosY == 39)) {
						Debug.Log ("here");

						targetPoint.x = ((Node.MyCollisionInfo.XNode + (MyNodeInfo.MyNodePath [1].PosX - 39)) * 0.0625f);//Finding My Position In The World As A Node, then Adding The Next Node Direction To Find The New Position In WoldPos
						targetPoint.y = ((Node.MyCollisionInfo.YNode + (MyNodeInfo.MyNodePath [1].PosY - 39)) * 0.0625f);//Finding My Position In The World As A Node, then Adding The Next Node Direction To Find The New Position In WoldPos
		
						transform.position += ((targetPoint - transform.position).normalized) * (Stats.Speed * Time.deltaTime);
						Node.MyCollisionInfo.CalculateNodePos (transform.position);
					}
		
				}

				if (MyAnimatorVariables.FlipToTarget == true) {
					if (MyNodeInfo.MyNodePath [0].PosX < MyNodeInfo.MyNodePath [1].PosX) {
						MyAnimatorVariables.transform.eulerAngles = Vector3.zero;
					} else if (MyNodeInfo.MyNodePath [0].PosX > MyNodeInfo.MyNodePath [1].PosX) {
						MyAnimatorVariables.transform.eulerAngles = Vector3.up * 180;
					}
				}

				//--------------------------------------------------------
			} else {
				if (UpdatePosition == true)
					Node.MyCollisionInfo.CalculateNodePos (transform.position);
			}

			if (CanIRegenEnergy == true) {//TODO Not Yet Subtracting When Using Abilities 
				MyEnergyBar.RunEnergyBar ();
			}


			if (CanIUseAbilities == true) {

				if (Targeting.AttackClass == true) {
				
					if (Targeting.MyMovementTarget == null) {
						Targeting.SearchAfterTarget (transform.position);	
					}

					if (MyAnimatorVariables.AbilityRunning == false) {//Only One Ability Can Be Used At A Time
						MyAbilityInfo.AbilityChecker ();//Checking If An Ability's Criteria To Run Is Met 
					}
				}

			}

		}
	
	}


	void OnDrawGizmosSelected(){


		foreach (NodeTest s in MyNodeInfo.MyAStar.cl()) {

			if (s != null && s.NodeSearchedThrough == true) {
				Gizmos.color = new Color (100,100,100, 0.5f);
				Gizmos.DrawSphere (new Vector3((Node.MyCollisionInfo.XNode + (s.PosX - 39)) * 0.0625f, (Node.MyCollisionInfo.YNode + (s.PosY - 39)) * 0.0625f, 0), 0.025f);
			}

		}

		foreach (NodeTest s in MyNodeInfo.MyAStar.ol()) {

			if (s != null && s.NodeSearchedThrough == true) {
				Gizmos.color = new Color (1, 0, 0, 0.5f);
				Gizmos.DrawSphere (new Vector3((Node.MyCollisionInfo.XNode + (s.PosX - 39)) * 0.0625f, (Node.MyCollisionInfo.YNode + (s.PosY - 39)) * 0.0625f, 0), 0.025f);
			}

		}

		foreach (NodeTest s in MyNodeInfo.MyNodePath) {

			if (s != null && s.NodeSearchedThrough == true) {
				Gizmos.color = new Color (255,255,255, 0.5f);
				Gizmos.DrawSphere (new Vector3((Node.MyCollisionInfo.XNode + (s.PosX - 39)) * 0.0625f, (Node.MyCollisionInfo.YNode + (s.PosY - 39)) * 0.0625f, 0), 0.025f);
			}

		}

	}
}
