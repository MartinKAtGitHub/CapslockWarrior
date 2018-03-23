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

			if (Stats.Health < 0) {
				Stats.Health = 0;
			}
		}
	}

	public virtual void GainShield(float shield){
		shield += Mathf.FloorToInt(shield);;
	}

	public virtual void GainHealth(float health){
		Stats.Health += Mathf.FloorToInt(health);
	}

}
