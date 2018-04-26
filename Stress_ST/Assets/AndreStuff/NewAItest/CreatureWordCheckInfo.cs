using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class CreatureWordCheckInfo {

	[HideInInspector]
	public CreatureRoot myVariables;
	public Text TextElement;//Showing Text Health
	public HealthBackShower HealthShower;
	public TheWordChecker WordChecker;

	[HideInInspector]
	public List<KeyValuePair<GameObject, KeyValuePair<Color, string[]>> > _Players = new List<KeyValuePair<GameObject, KeyValuePair<Color, string[]>> >();//this is a list that is containing all players and their strings. so this must be shared between all players to get the effect we're after


	public void Setup(){
		TypingEvents.OnCompareStart += CompareStart;
		WordChecker.SetupFirstWord (myVariables);
	}

	void CompareStart(KeyValuePair<GameObject, KeyValuePair<Color, string[]>>  InputString){//Is Called When The Player Types Something 
		WordChecker.CompareStart(InputString,myVariables);
	}

}
