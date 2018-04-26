using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureRoot : MonoBehaviour {

	public CreatureStats Stats;

	public Rigidbody2D MyRigidBody;

/*	public virtual void TookDmg(float dmg){
		
	}

	public virtual void Healed(float dmg){

	}

	public virtual void Shielded(float dmg){

	}*/


	public virtual void TookDmg(int dmg){
		if (Stats.Shield > 0) {
			Stats.Shield -= dmg;

			if (Stats.Shield < 0) {
				Stats.Health += Stats.Shield;
				Stats.Shield = 0;
			}
		} else {
			Stats.Health -= dmg;

			if (Stats.Health <= 0) {
				Stats.Health = 0;
				Destroy (this.gameObject);
			}
		}
	}

	public virtual void GainShield(float shield){
		shield += Mathf.FloorToInt(shield);;
	}

	public virtual void GainHealth(float health){
		Stats.Health += Mathf.FloorToInt(health);
	}

	public virtual void VelocityChange (float movePower){}//If The Object Is Suseptable To This Then Add The Logic In The Parent Script



	public virtual void MovementSpeedChange(float a)//TODO Speed Change Logic
	{
		/*	
		totalmovementdecrease += a;

		if (totalmovementdecrease < 0) {
			GetComponent<PlayerController> ().MaxSpeed = 0.1f;
		} else {
			GetComponent<PlayerController> ().MaxSpeed = totalmovementdecrease;
		}
		*/
	}

	public virtual void OnCharacterDeath()
	{

	}


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


	public virtual ObjectNodeInfo GetObjectNodeInfo (){
		return null;
	}

	public virtual AnimatorVariables GetAnimatorVariables (){
		Debug.Log ("Retungnins");
		return null;
	}

	public virtual AbilityInfo GetAbilityInfo (){
		return null;
	}

	public virtual EnergyBar GetEnergyBar (){
		return null;
	}

	public virtual NodeInfo GetNodeInfo (){
		return null;
	}

	public virtual WhatToTarget GetWhatToTarget (){
		return null;
	}

	public virtual CreatureWordCheckInfo GetCreatureWordCheckInfo (){
		return null;
	}

	public virtual ObjectMovement GetObjectMovement (){
		return null;
	}



}
