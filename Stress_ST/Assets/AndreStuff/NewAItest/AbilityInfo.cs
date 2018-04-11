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
	float OldTime = 0;

	public List<StressCommonlyUsedInfo.TransitionGrouped> tes = new List<StressCommonlyUsedInfo.TransitionGrouped>();

	public void AbilitySetter(){

		_AbilityArrayLength = TransitionAbilities[0].AllAbilities.Length;//Just So That I Dont Have To Do This Every Update
		SetSpellCD ();

	}

	public void AbilityChecker () {//Checking If An Ability Can Be Used. 

		for (int i = 0; i < _AbilityArrayLength; i++) {
		
			if (TransitionAbilities [MyManager.MyAnimatorVariables.SpellTransition].AllAbilities [i].SpellCurrentCD < ClockTest.TheTimes) {//If The Spell Dont Have A CD.
			
				AbilityCheck = TransitionAbilities [MyManager.MyAnimatorVariables.SpellTransition].AllAbilities [i].SpellInfo.RunCriteriaCheck (MyManager);

				if (AbilityCheck > 0) {//If True The An Ability Starts To Run
					TransitionAbilities [MyManager.MyAnimatorVariables.SpellTransition].AllAbilities [i].SpellCurrentCD = ClockTest.TheTimes;//Setting Inisiated Time
					tes.Add(TransitionAbilities [MyManager.MyAnimatorVariables.SpellTransition]);
					MyManager.MyAnimatorVariables.SetAnimatorStage (AbilityCheck);
					MyManager.MyAnimatorVariables.AbilityRunning = true;
					OldTime = ClockTest.TheTimes;
					break;
				}
			}

		}

	}

	public void SpellComplete(int spellID){
		
		/*for (int i = 0; i < te.Count; i++) {
			if (te [i].Key == spellID) {
				
				for (int i = 0; i < _AbilityArrayLength; i++) {
					TransitionAbilities [s.Value.Key].AllAbilities [i].SpellCurrentCD += ClockTest.TheTimes - OldTime;
				}

				TransitionAbilities [s.Value.Key].AllAbilities [s.Value.Value].SpellCurrentCD = ClockTest.TheTimes + TransitionAbilities [s.Value.Key].AllAbilities [s.Value.Value].SpellCD;

				te.Remove (te [i]);
			}
		}*/

	}

	public void SetSpellCD(){//Reseting The Spell CD

		for (int i = 0; i < _AbilityArrayLength; i++) {
			TransitionAbilities [MyManager.MyAnimatorVariables.SpellTransition].AllAbilities [i].SpellCurrentCD = ClockTest.TheTimes + TransitionAbilities [MyManager.MyAnimatorVariables.SpellTransition].AllAbilities [i].SpellCD;
		}

	}

	float durationTimeToAdd = 0;

	public void AddLostTime(int abilityID){//When SpellAnimation Cycle Is Complete, Add The Time Between Spell Used And Spell Complete To The Other Spells, Then Reseting The Used Spell

		for (int j = 0; j < tes.Count; j++) {
			for (int i = 0; i < tes[j].AllAbilities.Length; i++) {
				if (tes[j].AllAbilities [i].SpellRef.bulletID == abilityID) {
					durationTimeToAdd = ClockTest.TheTimes - tes[j].AllAbilities [i].SpellCurrentCD;

					for (int t = 0; t < tes[j].AllAbilities.Length; t++) {//Adding The Duration Time To Other Spells. 
						if (tes[j].AllAbilities [t].SpellRef.bulletID == abilityID) {
							tes[j].AllAbilities [t].SpellCurrentCD = ClockTest.TheTimes + tes[j].AllAbilities [t].SpellCD;
						}else{
							tes[j].AllAbilities [t].SpellCurrentCD += durationTimeToAdd;
						}
					}
					return;

				}
			}
		}


		foreach (StressCommonlyUsedInfo.TransitionGrouped s in tes) {
			for (int i = 0; i < s.AllAbilities.Length; i++) {
				if (s.AllAbilities [i].SpellRef.bulletID == abilityID) {
					durationTimeToAdd = ClockTest.TheTimes - s.AllAbilities [i].SpellCurrentCD;

					for (int t = 0; t < s.AllAbilities.Length; t++) {//Adding The Duration Time To Other Spells. 
						if (s.AllAbilities [t].SpellRef.bulletID == abilityID) {
							s.AllAbilities [t].SpellCurrentCD = ClockTest.TheTimes + s.AllAbilities [t].SpellCD;
						}else{
							s.AllAbilities [t].SpellCurrentCD += durationTimeToAdd;
						}
					}
					return;

				}
			}

		/*	Debug.Log (s);
			if (s.AllAbilities == abilityID) {
				for (int i = 0; i < _AbilityArrayLength; i++) {
					TransitionAbilities [s.Value.Key].AllAbilities [i].SpellCurrentCD += ClockTest.TheTimes - OldTime;
				}

				TransitionAbilities [s.Value.Key].AllAbilities [s.Value.Value].SpellCurrentCD = ClockTest.TheTimes + TransitionAbilities [s.Value.Key].AllAbilities [s.Value.Value].SpellCD;
				Debug.Log ("HERE2");
				//te.Remove (s);
				Debug.Log ("HERE4");
			}*/
		}
	}

}
