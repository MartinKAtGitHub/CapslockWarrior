using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureRoot : MonoBehaviour {

	public CreatureStats Stats;

/*	public virtual void TookDmg(float dmg){
		
	}

	public virtual void Healed(float dmg){

	}

	public virtual void Shielded(float dmg){

	}*/


	public virtual void TookDmg(float dmg){
		if (Stats.Shield > 0) {
			Stats.Shield -= Mathf.FloorToInt(dmg);

			if (Stats.Shield < 0) {
				Stats.Health += Stats.Shield;
				Stats.Shield = 0;
			}
		} else {
			Stats.Health -= Mathf.FloorToInt(dmg);

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

	public virtual void VelocityChange (float moveValue, Vector3 goDirection){}//If The Object Is Suseptable To This Then Add The Logic In The Parent Script



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

}
