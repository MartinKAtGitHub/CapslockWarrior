using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spell Index Values. 
//0 - Damage


public class Devial_Demon_Bullet : The_Default_Bullet {

	public Vector2 TheCapsuleSize = Vector2.zero;//The Size Itself Is Abit Wrong. To Get An Accurate Size. Test With A Capsule Collider
	public LayerMask WhatCanIHit;

	CreatureRoot _MyObject;
	StressCommonlyUsedInfo.TheAbility[] test = new StressCommonlyUsedInfo.TheAbility[1];
	float dmg = 0;

	public override void SetMethod (CreatureRoot manager){
		_MyObject = manager;
		transform.parent = _MyObject.transform.Find ("GFX");

		for (int t = 0; t < _MyObject.GetAbilityInfo().tes.Count; t++) {//Goint Through The Transitions To Find This Spell Transition
			for (int g = 0; g < _MyObject.GetAbilityInfo().tes [t].AllAbilities.Length; g++) {//Going Through This Spells Transition To Find The Spell
				if (_MyObject.GetAbilityInfo().tes [t].AllAbilities [g].SpellRef.bulletID == bulletID) {//If SpellRef ID == This SpellID. Then This Is That Spell
					dmg = _MyObject.GetAbilityInfo().tes [t].AllAbilities [g].SpellVariables[0];
				}
			}
		}
		transform.localPosition = Quaternion.Euler (0, transform.parent.rotation.y, transform.parent.rotation.z) * test [0].SpawnPosition;//Setting The Start Location

		if ((_MyObject.GetWhatToTarget().MyMovementTarget.transform.position - transform.position).y < 0) {
			transform.rotation = Quaternion.Euler (0, 0, Vector3.Angle (Vector3.right, (_MyObject.GetWhatToTarget().MyMovementTarget.transform.position - transform.position)) * -1);
		} else {
			transform.rotation = Quaternion.Euler (0, 0, Vector3.Angle (Vector3.right, (_MyObject.GetWhatToTarget().MyMovementTarget.transform.position - transform.position)));
		}

	}

	CreatureRoot targets;

	void FixedUpdate(){//Only Here For One Update, If Nothing Is Hit, The 'Spell' Is Destroyed

		foreach (RaycastHit2D s in Physics2D.CapsuleCastAll (transform.position, TheCapsuleSize, CapsuleDirection2D.Vertical, 0, Vector2.zero, 0, WhatCanIHit)) {//Capsule size is acting weird. the y value is the radius of the height, but the x value is the diameter of the width?????? WHAT why......
		
			targets = s.transform.GetComponent<CreatureRoot> ();
			if (targets != null) {
				if (targets.Stats.HealthImmunity == false && targets.Stats.TotalImmunity == false) {
					if (dmg * (1 - targets.Stats.PhysicalResistence) > 0) {//If The Creature Have Resist > 1 Then The Attack Will Heal The Creature. Fire On Fire Might Heal?
						targets.TookDmg (Mathf.CeilToInt(dmg * (1 - targets.Stats.PhysicalResistence)));
					}
				}
			}
		}

		Destroy (gameObject);

	}


}
