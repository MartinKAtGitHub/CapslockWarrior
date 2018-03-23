using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using UnityEngine.Events;

//Holds All Spells Of The Object. 
//To Check If A Spell Can Be Used The SpellCD Is Checked On First.
//Next The Spell checks Its Own Spell Criteria And If All Of Them Are True Then The Next Part Happends.
//The Final Part Is Just To Change The AnimatorStage To Let It Know That A New Animation Need To Play.
//If Something Needs To Be Changed Then That Is Done In The Animator. (stop moving, cant rotate, start scenario logic....)

[System.Serializable]
public class AbilityInfo {

	[HideInInspector]
	public EnemyManaging MyManager;	
	public StressCommonlyUsedInfo.TransitionGrouped[] TransitionAbilities;

	int _AbilityArrayLength = 0;

	int AbilityCheck = 0;
	int AbilityUsed = 0;
	float OldTime = 0;

	public void AbilitySetter(){

		_AbilityArrayLength = TransitionAbilities[0].AllAbilities.Length;//Just So That I Dont Have To Do This Every Update
		SetSpellCD ();

	}

	public void AbilityChecker () {//Checking If An Ability Can Be Used. 

		for (int i = 0; i < _AbilityArrayLength; i++) {
			if (TransitionAbilities [MyManager.MyAnimatorVariables.SpellTransition].AllAbilities [i].SpellCurrentCD < ClockTest.TheTimes) {//If The Spell Dont Have A CD.
				AbilityCheck = TransitionAbilities [MyManager.MyAnimatorVariables.SpellTransition].AllAbilities [i].SpellInfo.RunCriteriaCheck (MyManager);

				if (AbilityCheck > 0) {//If True The An Ability Starts To Run
					MyManager.MyAnimatorVariables.SetAnimatorStage (AbilityCheck);
					AbilityUsed = i;
					MyManager.MyAnimatorVariables.AbilityRunning = true;
					OldTime = ClockTest.TheTimes;
					break;
				}
			}

		}

	}

	public void SetSpellCD(){//Reseting The Spell CD

		for (int i = 0; i < _AbilityArrayLength; i++) {
			TransitionAbilities [MyManager.MyAnimatorVariables.SpellTransition].AllAbilities [i].SpellCurrentCD = ClockTest.TheTimes + TransitionAbilities [MyManager.MyAnimatorVariables.SpellTransition].AllAbilities [i].SpellCD;
		}

	}

	public void AddLostTime(){//When SpellAnimation Cycle Is Complete, Add The Time Between Spell Used And Spell Complete To The Other Spells, Then Reseting The Used Spell

		for (int i = 0; i < _AbilityArrayLength; i++) {
			TransitionAbilities [MyManager.MyAnimatorVariables.SpellTransition].AllAbilities [i].SpellCurrentCD += ClockTest.TheTimes - OldTime;
		}
		TransitionAbilities [MyManager.MyAnimatorVariables.SpellTransition].AllAbilities [AbilityUsed].SpellCurrentCD = ClockTest.TheTimes + TransitionAbilities [MyManager.MyAnimatorVariables.SpellTransition].AllAbilities [AbilityUsed].SpellCD;

	}


}
