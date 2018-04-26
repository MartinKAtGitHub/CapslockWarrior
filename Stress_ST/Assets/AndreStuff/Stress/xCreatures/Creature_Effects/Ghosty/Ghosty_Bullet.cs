using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spell Index Values. 
//0 - Damage
//1 - Speed
//2 - ColliderRadius

public class Ghosty_Bullet : The_Default_Bullet {
	
	public Animator MyAnimator;
	public LayerMask WhatCanIHit;

	Vector3 MyShootingDirection;
	bool _StartMoving = false;
	bool Dieing = false;
	bool RemoveParent = false;

	CreatureRoot _MyObject;
	StressCommonlyUsedInfo.TheAbility[] test = new StressCommonlyUsedInfo.TheAbility[1];
	RaycastHit2D[] _ObjectHit;


	public void StartMoving(){
		_StartMoving = true;
		MyShootingDirection = MyShootingDirection.normalized;
	}


	public override void SetMethod (CreatureRoot manager){
		_MyObject = manager;
		MyAnimator = GetComponent < Animator> ();
		transform.parent = _MyObject.transform.Find("GFX");

		for (int t = 0; t < _MyObject.GetAbilityInfo().tes.Count; t++) {//Goint Through The Transitions To Find This Spell Transition
			for (int g = 0; g < _MyObject.GetAbilityInfo().tes [t].AllAbilities.Length; g++) {//Going Through This Spells Transition To Find The Spell
				if (_MyObject.GetAbilityInfo().tes [t].AllAbilities [g].SpellRef.bulletID == bulletID) {//If SpellRef ID == This SpellID. Then This Is That Spell
					test [0] = _MyObject.GetAbilityInfo().tes [t].AllAbilities [g];
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

	void FixedUpdate () {

		if (_StartMoving == true) {
			if (Dieing == false) {

				if (RemoveParent == false) {
					RemoveParent = true;
					transform.parent = null;
					MyShootingDirection = MyShootingDirection.normalized;
				}

				transform.position += MyShootingDirection * test [0].SpellVariables [1] * Time.deltaTime;

				_ObjectHit = Physics2D.CircleCastAll (transform.position + (Vector3.right * 0.02f), test [0].SpellVariables [2], Vector2.zero, 1, WhatCanIHit);

				if (_ObjectHit.Length > 0) {
					foreach (RaycastHit2D s in _ObjectHit) {
						if (s.transform.gameObject.layer == 8 || s.transform.gameObject.layer == 15) {

							targets = s.transform.GetComponent<CreatureRoot> ();
							if (targets != null) {
								if (targets.Stats.HealthImmunity == false && targets.Stats.TotalImmunity == false) {

									if (test [0].SpellVariables [0] * (1 - targets.Stats.PhysicalResistence) > 0) {//If The Creature Have Resist > 1 Then The Attack Will Heal The Creature. Fire On Fire Might Heal?
										targets.TookDmg (Mathf.CeilToInt(test [0].SpellVariables [0] * (1 - targets.Stats.PhysicalResistence)));
									}
								}
							}
						} 
					}

					Dieing = true;
					MyAnimator.SetInteger ("SpellState", 1);
				}

			} else {
			
				transform.position += (MyShootingDirection * Time.deltaTime) / 2;
			
			}

		} else {

			MyShootingDirection = _MyObject.GetWhatToTarget ().MyMovementTarget.transform.position - transform.position;

			if (MyShootingDirection.y < 0) {
				transform.rotation = Quaternion.Euler (0, 0, Vector3.Angle (Vector3.right, MyShootingDirection) * -1);
			} else {
				transform.rotation = Quaternion.Euler (0, 0, Vector3.Angle (Vector3.right, MyShootingDirection));
			}
			transform.localPosition = Quaternion.Euler (0, transform.parent.rotation.y, transform.parent.rotation.z) * test [0].SpawnPosition;
		}

	}
		
}