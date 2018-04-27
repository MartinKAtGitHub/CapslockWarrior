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

public class Mouly_Spike_Grouped : The_Default_Bullet {


	CreatureRoot _MyObject;
	StressCommonlyUsedInfo.TheAbility[] test = new StressCommonlyUsedInfo.TheAbility[1];
	bool TargetClose = false;
	float timeLeft = 0;
	float attackTime = 0;

	public The_Default_Bullet MoulySpikes;

	public override void SetMethod (CreatureRoot manager){
		
		_MyObject = manager;
		transform.parent = _MyObject.transform.Find("GFX");

		for (int t = 0; t < _MyObject.GetAbilityInfo().tes.Count; t++) {//Goint Through The Transitions To Find This Spell Transition
			for (int g = 0; g < _MyObject.GetAbilityInfo().tes [t].AllAbilities.Length; g++) {//Going Through This Spells Transition To Find The Spell
				if (_MyObject.GetAbilityInfo().tes [t].AllAbilities [g].SpellRef.bulletID == bulletID) {//If SpellRef ID == This SpellID. Then This Is That Spell
					test [0] = _MyObject.GetAbilityInfo().tes [t].AllAbilities [g];
				}
			}
		}

		transform.localPosition = Quaternion.Euler (0, transform.parent.rotation.y, transform.parent.rotation.z) * test [0].SpawnPosition;//Setting The Start Location
		timeLeft = ClockTest.TheTimes + test[0].SpellVariables[5];


	}


	void FixedUpdate(){
		if (_MyObject == null)//When Shooting Object Dies, Destroy This Object
			Destroy (this.gameObject);
		
		if (TargetClose == false) {

			if (Vector3.Distance (transform.position, _MyObject.GetWhatToTarget().MyMovementTarget.transform.position) < test [0].SpellVariables [4]) {

				TargetClose = true;
				_MyObject.GetAnimatorVariables().SetAnimatorStage (2);
				_MyObject.Stats.Speed += test [0].SpellVariables [1];
				timeLeft = ClockTest.TheTimes + (test [0].SpellVariables [2]);
			} else {
			
				if (timeLeft < ClockTest.TheTimes) {//Time To Scout
					_MyObject.GetAnimatorVariables().SetAnimatorStage (1000);//1000 - 1010 Is Values Used For The Purpose Of Animator State Changes Which Isnt Similar To Any Spell 'ID'
					_MyObject.GetAbilityInfo().AddLostTime(bulletID);
					_MyObject.GetAnimatorVariables().AbilityRunning = false;
					Destroy (this.gameObject);
				}

			}

		} else {
		
			if (attackTime < ClockTest.TheTimes) {
				attackTime = ClockTest.TheTimes + (test [0].SpellVariables [3]);
				Instantiate (MoulySpikes, transform.position, Quaternion.identity).SetMethod(_MyObject);
			}

			if (timeLeft < ClockTest.TheTimes) {//Time To Scout
				_MyObject.GetAnimatorVariables().SetAnimatorStage (1000);//1000 - 1010 Is Values Used For The Purpose Of Animator State Changes Which Isnt Similar To Any Spell 'ID'
				_MyObject.Stats.Speed -= test [0].SpellVariables [1];
			
				_MyObject.GetAbilityInfo().AddLostTime(bulletID);
				_MyObject.GetAnimatorVariables().AbilityRunning = false;
				Destroy (this.gameObject);

			}

		}


	}

}
