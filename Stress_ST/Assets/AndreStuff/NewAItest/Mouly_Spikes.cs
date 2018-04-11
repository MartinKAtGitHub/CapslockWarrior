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


	EnemyManaging _MyObject;
	float saveDmg = 0;

	public override void SetMethod (EnemyManaging manager){

		_MyObject = manager;
		for (int t = 0; t < _MyObject.MyAbilityInfo.tes.Count; t++) {//Goint Through The Transitions To Find This Spell Transition
			for (int g = 0; g < _MyObject.MyAbilityInfo.tes [t].AllAbilities.Length; g++) {//Going Through This Spells Transition To Find The Spell
				if (_MyObject.MyAbilityInfo.tes [t].AllAbilities [g].SpellRef.bulletID == bulletID) {//If SpellRef ID == This SpellID. Then This Is That Spell
					saveDmg = _MyObject.MyAbilityInfo.tes [t].AllAbilities [g].SpellVariables[0];
				}
			}
		}

	}


		void OnTriggerEnter2D(Collider2D col){//objects without rigidbody and box2d ontrigger true
		if (_MyObject.gameObject != col.gameObject) {
			if (col.gameObject.GetComponent<CreatureRoot> () != null) {
				col.gameObject.GetComponent<CreatureRoot> ().TookDmg (saveDmg);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col){//objects with rigidbody and box2d ontrigger false
		if (_MyObject.gameObject != col.gameObject) {
			if (col.gameObject.GetComponent<CreatureRoot> () != null) {
				col.gameObject.GetComponent<CreatureRoot> ().TookDmg (saveDmg);
			}
		}
	}

}
