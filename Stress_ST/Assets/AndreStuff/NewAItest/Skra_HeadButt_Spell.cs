using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spell Index Values. 
//0 - Damage
//1 - CircleRadius1 
//2 - CircleRadius2 
//3 - SpellDuration
//4 - PushBackStrength
//5 - SpeedIncrease
//6 - StunnedTime

public class Skra_HeadButt_Spell : The_Default_Bullet {

	public LayerMask WhatCanIHit;

	float _TimeToComplete = 0;
	EnemyManaging _MyObject;
	RaycastHit2D[] _ObjectHit;
	StressCommonlyUsedInfo.TheAbility[] test = new StressCommonlyUsedInfo.TheAbility[1];
	bool Collided = false;


	public override void SetMethod (EnemyManaging manager){
		_MyObject = manager;
		transform.parent = _MyObject.transform.Find ("GFX");

		for (int t = 0; t < _MyObject.MyAbilityInfo.tes.Count; t++) {//Goint Through The Transitions To Find This Spell Transition
			for (int g = 0; g < _MyObject.MyAbilityInfo.tes [t].AllAbilities.Length; g++) {//Going Through This Spells Transition To Find The Spell
				if (_MyObject.MyAbilityInfo.tes [t].AllAbilities [g].SpellRef.bulletID == bulletID) {//If SpellRef ID == This SpellID. Then This Is That Spell
					test [0] = _MyObject.MyAbilityInfo.tes [t].AllAbilities [g];
				}
			}
		}

		_TimeToComplete = (test[0].SpellVariables[3] / manager.Stats.Speed) + ClockTest.TheTimes;
	
		transform.localPosition = Quaternion.Euler (0, transform.parent.rotation.y, transform.parent.rotation.z) * test [0].SpawnPosition;//Setting The Start Location
		_MyObject.Stats.Speed += test [0].SpellVariables [5];

	}


	void FixedUpdate(){//If I Hit Something I Do A Pushback On Everything Within (CircleRadius * 2.5f) 

		if (Collided == false) {
			if (_TimeToComplete > ClockTest.TheTimes) {
		
				_ObjectHit = Physics2D.CircleCastAll (transform.position, test [0].SpellVariables [1], Vector2.zero, 1, WhatCanIHit);//If Collided
				if (_ObjectHit.Length > 0) {

					_ObjectHit = Physics2D.CircleCastAll (transform.position, test [0].SpellVariables [2], Vector2.zero, 1, WhatCanIHit);//If Collided Do A Expanded Pushback
					foreach (RaycastHit2D s in _ObjectHit) {
						if (s.transform.gameObject.layer == 8 || s.transform.gameObject.layer == 15) {
							s.transform.GetComponent<CreatureRoot> ().TookDmg (test [0].SpellVariables [0]);
							s.transform.GetComponent<CreatureRoot> ().VelocityChange (test [0].SpellVariables [4], (s.transform.position - transform.position));
						}
					}
			
					_MyObject.MyAnimatorVariables.SetAnimatorStage (1000);//1000 - 1010 Is Values Used For The Purpose Of Animator State Changes Which Isnt Similar To Any Spell 'ID'
					_MyObject.Stats.Speed -= test [0].SpellVariables [5];
					_TimeToComplete = ClockTest.TheTimes + test [0].SpellVariables [6];
					Collided = true;
				}

			} else {

				_MyObject.MyAnimatorVariables.SetAnimatorStage (0);
				_MyObject.Stats.Speed -= test [0].SpellVariables [5];
				Destroy (this.gameObject);
			}
		} else {
			
			if (_TimeToComplete <= ClockTest.TheTimes) {
				_MyObject.MyAnimatorVariables.SetAnimatorStage (0);//1000 - 1010 Is Values Used For The Purpose Of Animator State Changes Which Isnt Similar To Any Spell 'ID'
				Destroy (this.gameObject);
			}

		}

	}
		
}
