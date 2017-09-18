using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class EnemyWordChecker {
	//Multiple Health Bars???

	public Text TextElement;

//	bool _StopChecking;
	int _WordsToRemove = 0;//a counter that is used to deside how many words to remove after the player presses enter
	public string _ObjectHealth = "ObjectHealth";
	string _OriginalWord;

	int[] Healths;
	int[] HealthsLength;
	int HealthIndex = 0;

//	int _CleaveDmg; TODO tobeused
	DefaultBehaviour _TheObject;
	//PlayerTyping currentTarget;
	List<KeyValuePair<GameObject, KeyValuePair<Color, string[]>> > _Players = new List<KeyValuePair<GameObject, KeyValuePair<Color, string[]>> >();//this is a list that is containing all players and their strings. so this must be shared between all players to get the effect we're after
	KeyValuePair<GameObject, KeyValuePair<Color, string[]>> _LongestPlayer;
	KeyValuePair<GameObject, KeyValuePair<Color, string[]>> _2ndLongestPlayer;


	Color32 PlayerColor;
	string ColorValue;
	List<bool> PlayersTypedCorrect = new List<bool>();
	int _WordLengths = 0;
	int NullCheck = 0;
	bool ScoreAtWordCompleteOrDeath = true;
	bool IgnoreTyping = false;

	public EnemyWordChecker(Text textElement, string enemyHealth, DefaultBehaviour parent){//Adding methods to events and setting startvalues
		Healths = parent.Health;

		TextElement = textElement;
		_ObjectHealth = enemyHealth;
		_OriginalWord = enemyHealth;
		_TheObject = parent;

		_WordsToRemove = 0;

		TextElement.text = _ObjectHealth;
//		_CleaveDmg = TalentBonusStats.CleaveDmg; TODO tobeused
//		_StopChecking = false;

		TypingEvents.OnCompareStart += CompareStart;
	//	TypingEvents.OnCompareRestart += CompareRestart;
		TypingEvents.OnCompareEnd += CompareEnd;
//		players.Add (new KeyValuePair<GameObject, KeyValuePair<Color, string[]>>(theParent.gameObject, new  KeyValuePair<Color, string[]> (Color.yellow, new string[1]{ "muaaaooo" })));

		//_SpawnedObject = Instantiate (ObjectsToSpawn [k].creature.gameObject, SpawnPoints [_SpawnSpot].transform.position, Quaternion.identity, SpawnPoints [_SpawnSpot].transform) as GameObject;
	//	theParent.setword(ListOfWords.GetRandomWords (Healths[0]));
	}

	public EnemyWordChecker(Text textElement, DefaultBehaviour parent){//Adding methods to events and setting startvalues
		Healths = parent.Health;
		HealthsLength = parent.HealthLength;
		_TheObject = parent;
		TextElement = textElement;

		_ObjectHealth = ListOfWords.GetRandomWords (HealthsLength[HealthIndex]);
		_OriginalWord = _ObjectHealth;

		TextElement.text = _ObjectHealth;

		TypingEvents.OnCompareStart += CompareStart;
		TypingEvents.OnCompareEnd += CompareEnd;
	}

	#region EventMethods

	void CompareRestart(){//When the player deletes a letter then this will run 
//		_StopChecking = false;
	}

	void CompareEnd(GameObject thisObject){//When the player presses enter then this will run    
	/*	if (_WordsToRemove > 0 && _WordsToRemove <= _ObjectHealth.Length) {
			_ObjectHealth = _ObjectHealth.Remove (0, _WordsToRemove);
			TextElement.text = string.Format("<color=black>{0}</color>", _ObjectHealth);
			shortest.Key.GetComponent<DefaultBehaviour> ().GotTheKill (0);//if multiple peeps are going to recieve score then you can create an event and give them score through the event or loop throgh players list
			_WordsToRemove = 0;
			if (_ObjectHealth.Length <= 0) {
				shortest.Key.GetComponent<DefaultBehaviour> ().GotTheKill (_OriginalWord.Length);//if multiple peeps are going to recieve score then you can create an even and give them score through the event or loop throgh players list
				TypingEvents.OnCompareStart -= CompareStart;
				TypingEvents.OnCompareEnd -= CompareEnd;
				theParent.OnDestroyed();
			}
		}*/
	}

	void CompareStart(KeyValuePair<GameObject, KeyValuePair<Color, string[]>>  InputString){//Compares _EnemyHealth with what the player typed(InputString) 

		if (IgnoreTyping == false) {

			if (!_Players.Contains (InputString)) {//Adding New Players To The List
				_Players.Add (InputString);
				PlayersTypedCorrect.Add (true);
			}

			for (int i = 0; i < _Players.Count; i++) {//Checking If The InputString/CurrentPlayer Is Corrent And Need Update
				if (_Players [i].Key == InputString.Key) {
					_WordLengths = _Players [i].Value.Value [0].Length;

					if (_WordLengths > _ObjectHealth.Length) {
						_WordLengths = _ObjectHealth.Length;
					}

					for (int j = (_WordLengths - 1); j >= 0; j--) {
						if (_Players [i].Value.Value [0] [j] != _ObjectHealth [j]) {
							PlayersTypedCorrect [i] = false;
							break;
						}
						if (j == 0) {
							PlayersTypedCorrect [i] = true;
							if (_WordLengths == _ObjectHealth.Length) {

								if (Healths [0] > 0) {
									Healths[0]--;

									if (HealthIndex >= HealthsLength.Length - 1) {
										HealthIndex = -1;

									}
									Debug.Log (HealthIndex + " § " + HealthsLength.Length);
									_ObjectHealth = ListOfWords.GetRandomWords (HealthsLength [++HealthIndex]);
									_OriginalWord = _ObjectHealth;
									if (ScoreAtWordCompleteOrDeath == true) {
										_Players [i].Key.GetComponent<DefaultBehaviour> ().GotTheKill (_OriginalWord.Length);//Giving The Player That Wrote The Last Word The Score  //TODO Give Each Player Tagged Score?
									}
									return;
								} else {
									if (ScoreAtWordCompleteOrDeath == false) {
										_Players [i].Key.GetComponent<DefaultBehaviour> ().GotTheKill (_OriginalWord.Length);//Giving The Player That Wrote The Last Word The Score  //TODO Give Each Player Tagged Score?
									}
									TypingEvents.OnCompareStart -= CompareStart;
									TypingEvents.OnCompareEnd -= CompareEnd;
									_TheObject.OnDestroyed ();
								}
							}
							break;
						}
					}
					break;
				}
			}

			_WordLengths = 0;
			NullCheck = 0;

			for (int i = 0; i < _Players.Count; i++) {//Iterating Through Once To Find The Two Players That Have Typed The Most. If Two Are The Same The The One That Tagged It Is The Leader
				if (PlayersTypedCorrect [i] == true) {
					if (_Players [i].Value.Value [0].Length > _WordLengths) {
						_2ndLongestPlayer = _LongestPlayer;
						_LongestPlayer = _Players [i];
						NullCheck++;
					}
				}
			}

			_WordsToRemove = 0;
			TextElement.text = "";//Removing Text So That I Can Add It Again With New Colors

			if (NullCheck > 1) {//Adding 2nd Place Player Color
				_WordLengths = _2ndLongestPlayer.Value.Value [0].Length;//Length Of Player Word
				//	ColorValue = ((Color32)_2ndLongestPlayer.Value.Key).ToString ("X2"); //Does'nt Work  Only White Comes Out
				PlayerColor = (Color32)_2ndLongestPlayer.Value.Key;//Player Color
				ColorValue = PlayerColor.r.ToString ("X2") + PlayerColor.g.ToString ("X2") + PlayerColor.b.ToString ("X2") + PlayerColor.a.ToString ("X2");//Setting Color, Only Way I Got It To Work

				for (int i = 0; i < _WordLengths; i++) {//Iterates Through And Adds The Letter Again But With Different Color
					_WordsToRemove++;
					TextElement.text += string.Format ("<color=#" + ColorValue + ">{0}</color>", _ObjectHealth [i]);
				} 
			}

			if (NullCheck > 0) {//Adding 1nd Place Player Color
				_WordLengths = _LongestPlayer.Value.Value [0].Length;
				//	ColorValue = ((Color32)_LongestPlayer.Value.Key).ToString ("X2"); //Does'nt Work  Only White Comes Out
				PlayerColor = (Color32)_LongestPlayer.Value.Key;
				ColorValue = PlayerColor.r.ToString ("X2") + PlayerColor.g.ToString ("X2") + PlayerColor.b.ToString ("X2") + PlayerColor.a.ToString ("X2");

				for (int i = _WordsToRemove; i < _WordLengths; i++) {
					_WordsToRemove++;
					TextElement.text += string.Format ("<color=#" + ColorValue + ">{0}</color>", _ObjectHealth [i]);
				} 
			}

			//	ColorValue = ((Color32)Color.black).ToString ("X2");//Adding The Rest That's Not Written  //Does'nt Work  Only White Comes Out
			PlayerColor = (Color32)Color.black;
			ColorValue = PlayerColor.r.ToString ("X2") + PlayerColor.g.ToString ("X2") + PlayerColor.b.ToString ("X2") + PlayerColor.a.ToString ("X2");

			for (int i = _WordsToRemove; i < _ObjectHealth.Length; i++) {
				TextElement.text += string.Format ("<color=#" + ColorValue + ">{0}</color>", _ObjectHealth [i]);
			}
		}

	}

	#endregion

	public void HideHealth(bool DissableHealthLoss){
		if (DissableHealthLoss == true) {
			IgnoreTyping = true;
		} else {
			IgnoreTyping = false;
		}
		TextElement.enabled = false;
	}

	public void ShowHealth(bool DissableHealthLoss){
		if (DissableHealthLoss == true) {
			IgnoreTyping = true;
		} else {
			IgnoreTyping = false;
		}
		TextElement.enabled = true;
	}

	public void HealthIncrease(int healthIncrease){
		Healths [0] += healthIncrease;
	}

	public void HealthDecrease(int healthDecrease){
		Healths [0] -= healthDecrease;

		if (Healths [0] < 0) {

			for (int i = 0; i < _Players.Count; i++) {
				if (PlayersTypedCorrect [i] == true) {
					_WordLengths = _Players [i].Value.Value [0].Length;

					if (_WordLengths > _ObjectHealth.Length) {
						_WordLengths = _ObjectHealth.Length;
					}

					for (int j = (_WordLengths - 1); j >= 0; j--) {
						if (_Players [i].Value.Value [0] [j] != _ObjectHealth [j]) {
							break;
						}
						if(j == 0){
							_Players [i].Key.GetComponent<DefaultBehaviour> ().GotTheKill (_OriginalWord.Length);//Giving The Player That Wrote The Last Word The Score  //TODO Give Each Player Tagged Score?
							TypingEvents.OnCompareStart -= CompareStart;
							TypingEvents.OnCompareEnd -= CompareEnd;
							_TheObject.OnDestroyed ();
						}
					}
				}
			}
		}

	}



	//TODO This Is Old Code, Currently Not Used
	/*
	#region DifferentWordComparers

	void PerfectWord(string InputString){//if the player types something then that's what's being deleted. if we have silver and silverpine, and the player type silverp, then only silverp from silverpine will be deleted because silver does not contain p at the end

		if (InputString.Length <= _ObjectHealth.Length) {
			for (int i = 0; i < _ObjectHealth.Length; i++) {
				if (i <= InputString.Length - 1) {
					if (InputString [i] == _ObjectHealth [i]) {
						_WordsToRemove++;
						TextElement.text += string.Format ("<color=green>{0}</color>", _ObjectHealth [i]);
					} else {
						TextElement.text = string.Format ("<color=black>{0}</color>", _ObjectHealth);
		//				_StopChecking = true;
						return;
					}
				} else {
					TextElement.text += string.Format ("<color=red>{0}</color>", _ObjectHealth [i]);
				}
			}
		} else {
			TextElement.text = string.Format ("<color=black>{0}</color>", _ObjectHealth);
		//	_StopChecking = true;
			return;
		}
	}

	void PerfectWordCleave(string InputString){//if player type Silverpine and Silver exists then Silver will count as well. the cleave word must be completed, Siv would not count because of the v at the end

		for (int i = 0; i < _ObjectHealth.Length; i++) {
			if (i <= InputString.Length - 1) {
				if (InputString [i] == _ObjectHealth [i]) {
					_WordsToRemove++;
					TextElement.text += string.Format ("<color=green>{0}</color>", _ObjectHealth [i]);
				} else {
					TextElement.text = string.Format ("<color=black>{0}</color>", _ObjectHealth);
				//	_StopChecking = true;
					return;
				}
			} else {
				TextElement.text += string.Format ("<color=red>{0}</color>", _ObjectHealth [i]);
			}
		}
	}

	void SuperCleave(string InputString){//Siv Silver Silverpine, if player types Silverpine then Si from Siv is removed, entire Silver and Silverpine is removed

		for (int i = 0; i < _ObjectHealth.Length; i++) {
			if (i <= InputString.Length - 1) {
				if (InputString [i] == _ObjectHealth [i]) {
					_WordsToRemove++;
					TextElement.text += string.Format ("<color=green>{0}</color>", _ObjectHealth [i]);
				} else {
					for (int j = i; j < _ObjectHealth.Length; j++) {
						if (j <= _ObjectHealth.Length - 1) 
							TextElement.text += string.Format ("<color=red>{0}</color>", _ObjectHealth [j]);
					}
					return;
				}
			} else {
				TextElement.text += string.Format ("<color=red>{0}</color>", _ObjectHealth [i]);
			}
		}
	}

	#endregion
	*/

	void SetHealthString(string words){
		_ObjectHealth = words;
		_OriginalWord = _ObjectHealth;
	}

	public void RemoveEvent(){ 
		TypingEvents.OnCompareStart -= CompareStart;
		TypingEvents.OnCompareEnd -= CompareEnd;
	}

}