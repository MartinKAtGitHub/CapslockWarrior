using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spell Index Values. 
//0 - Damage
//1 - SpeedIncrease
//2 - TimeToAttack
//3 - TimeBetweenAttack
//4 - DistanceToCharge
//5 - TimeUntilScout

public class Mouly_Spikes : The_Default_Bullet {


	CreatureRoot _MyObject;
	float savedDmg = 0;

	CreatureRoot targets;

	public override void SetMethod (CreatureRoot manager){

		_MyObject = manager;
		for (int t = 0; t < _MyObject.GetAbilityInfo().tes.Count; t++) {//Goint Through The Transitions To Find This Spell Transition
			for (int g = 0; g < _MyObject.GetAbilityInfo().tes [t].AllAbilities.Length; g++) {//Going Through This Spells Transition To Find The Spell
				if (_MyObject.GetAbilityInfo().tes [t].AllAbilities [g].SpellRef.bulletID == bulletID) {//If SpellRef ID == This SpellID. Then This Is That Spell
					savedDmg = _MyObject.GetAbilityInfo().tes [t].AllAbilities [g].SpellVariables[0];
				}
			}
		}

	}


		void OnTriggerEnter2D(Collider2D col){//objects without rigidbody and box2d ontrigger true
		if (_MyObject.gameObject != col.gameObject) {
			targets = col.gameObject.GetComponent<CreatureRoot> ();
			if (targets != null) {

				if (targets.Stats.HealthImmunity == false && targets.Stats.TotalImmunity == false) {

					if (savedDmg * (1 - targets.Stats.PhysicalResistence) > 0) {//If The Creature Have Resist > 1 Then The Attack Will Heal The Creature. Fire On Fire Might Heal?
						targets.TookDmg (Mathf.CeilToInt(savedDmg * (1 - targets.Stats.PhysicalResistence)));
					}
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col){//objects with rigidbody and box2d ontrigger false
		if (_MyObject.gameObject != col.gameObject) {
			targets = col.gameObject.GetComponent<CreatureRoot> ();
			if (targets != null) {
				if (targets.Stats.HealthImmunity == false && targets.Stats.TotalImmunity == false) {

					if (savedDmg * (1 - targets.Stats.PhysicalResistence) > 0) {//If The Creature Have Resist > 1 Then The Attack Will Heal The Creature. Fire On Fire Might Heal?
						targets.TookDmg (Mathf.CeilToInt(savedDmg * (1 - targets.Stats.PhysicalResistence)));
					}
				}
			}
		}
	}

}
