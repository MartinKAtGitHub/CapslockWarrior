using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EnemyWordChecker {
	//Multiple Health Bars???

	public Text TextElement;

	bool _StopChecking;
	int _WordsToRemove;//a counter that is used to deside how many words to remove after the player presses enter
	public string _EnemyHealth;
	int _CleaveDmg;
	DefaultBehaviour theParent;

	public EnemyWordChecker(Text textElement, string enemyHealth, DefaultBehaviour parent){//Adding methods to events and setting startvalues
		TextElement = textElement;
		_EnemyHealth = enemyHealth;
		theParent = parent;

		_WordsToRemove = 0;

		TextElement.text = _EnemyHealth;
		_CleaveDmg = TalentBonusStats.CleaveDmg;
		_StopChecking = false;

		PlayerTyping.OnCompareStart += CompareStart;
		PlayerTyping.OnCompareRestart += CompareRestart;
		PlayerTyping.OnCompareEnd += CompareEnd;

	}

	#region EventMethods

	void CompareRestart(){//When the player deletes a letter then this will run 
		_StopChecking = false;
	}

	void CompareEnd(){//When the player presses enter then this will run 
		if (_WordsToRemove > 0 && _WordsToRemove <= _EnemyHealth.Length) {
			_EnemyHealth = _EnemyHealth.Remove (0, _WordsToRemove);
			TextElement.text = string.Format("<color=black>{0}</color>", _EnemyHealth);
			if (_EnemyHealth.Length <= 0) {
//				GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<InTheMiddleManager>().RemoveObject(this.gameObject);
				theParent.OnDestroyed();
			}
		}
	}

	void CompareStart(string InputString){//Compares _EnemyHealth with what the player typed(InputString) 

		if (!_StopChecking && InputString.Length > 0) {
			if (TextElement == null) {} else {
			
				_WordsToRemove = 0;
				TextElement.text = "";

				if (_CleaveDmg == 0) {
					PerfectWord (InputString);

				} else if (_CleaveDmg == 1) {
					PerfectWordCleave (InputString);

				} else if (_CleaveDmg == 2) {
					SuperCleave (InputString);
				}	
			} 
		} else {
			if (TextElement == null) {} else {
				TextElement.text = string.Format ("<color=black>{0}</color>", _EnemyHealth);
				_WordsToRemove = 0;
			}
		}
	}
		
	#region DifferentWordComparers

	void PerfectWord(string InputString){//if the player types something then that's what's being deleted. if we have silver and silverpine, and the player type silverp, then only silverp from silverpine will be deleted because silver does not contain p at the end
	
		if (InputString.Length <= _EnemyHealth.Length) {
			for (int i = 0; i < _EnemyHealth.Length; i++) {
				if (i <= InputString.Length - 1) {
					if (InputString [i] == _EnemyHealth [i]) {
						_WordsToRemove++;
						TextElement.text += string.Format ("<color=green>{0}</color>", _EnemyHealth [i]);
					} else {
						TextElement.text = string.Format ("<color=black>{0}</color>", _EnemyHealth);
						_StopChecking = true;
						return;
					}
				} else {
					TextElement.text += string.Format ("<color=red>{0}</color>", _EnemyHealth [i]);
				}
			}
		} else {
			TextElement.text = string.Format ("<color=black>{0}</color>", _EnemyHealth);
			_StopChecking = true;
			return;
		}
	}

	void PerfectWordCleave(string InputString){//if player type Silverpine and Silver exists then Silver will count as well. the cleave word must be completed, Siv would not count because of the v at the end

		for (int i = 0; i < _EnemyHealth.Length; i++) {
			if (i <= InputString.Length - 1) {
				if (InputString [i] == _EnemyHealth [i]) {
					_WordsToRemove++;
					TextElement.text += string.Format ("<color=green>{0}</color>", _EnemyHealth [i]);
				} else {
					TextElement.text = string.Format ("<color=black>{0}</color>", _EnemyHealth);
					_StopChecking = true;
					return;
				}
			} else {
				TextElement.text += string.Format ("<color=red>{0}</color>", _EnemyHealth [i]);
			}
		}
	}

	void SuperCleave(string InputString){//Siv Silver Silverpine, if player types Silverpine then Si from Siv is removed, entire Silver and Silverpine is removed

		for (int i = 0; i < _EnemyHealth.Length; i++) {
			if (i <= InputString.Length - 1) {
				if (InputString [i] == _EnemyHealth [i]) {
					_WordsToRemove++;
					TextElement.text += string.Format ("<color=green>{0}</color>", _EnemyHealth [i]);
				} else {
					for (int j = i; j < _EnemyHealth.Length; j++) {
						if (j <= _EnemyHealth.Length - 1) 
							TextElement.text += string.Format ("<color=red>{0}</color>", _EnemyHealth [j]);
					}
					return;
				}
			} else {
				TextElement.text += string.Format ("<color=red>{0}</color>", _EnemyHealth [i]);
			}
		}
	}

	#endregion

	#endregion

	void SetHealthString(string words){
		_EnemyHealth = words;
	}


}

/*	public Text TextElement;

	bool _StopChecking;
	int _WordsToRemove;//a counter that is used to deside how many words to remove after the player presses enter
	public string _EnemyHealth;
	int _CleaveDmg;

	void Start(){//Adding methods to events and setting startvalues
	
		if (TextElement != null) {
			_WordsToRemove = 0;

			TextElement.text = _EnemyHealth;
			_CleaveDmg = TalentBonusStats.CleaveDmg;
			_StopChecking = false;

			PlayerTyping.OnCompareStart += CompareStart;
			PlayerTyping.OnCompareRestart += CompareRestart;
			PlayerTyping.OnCompareEnd += CompareEnd;
		} else {
			Debug.Log ("imnull");
		}
	}

	#region EventMethods

	void CompareRestart(){//When the player deletes a letter then this will run 
		_StopChecking = false;
	}

	void CompareEnd(){//When the player presses enter then this will run 
		if (_WordsToRemove > 0 && _WordsToRemove <= _EnemyHealth.Length) {
			_EnemyHealth = _EnemyHealth.Remove (0, _WordsToRemove);
			TextElement.text = string.Format("<color=black>{0}</color>", _EnemyHealth);
			if (_EnemyHealth.Length <= 0) {
//				GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<InTheMiddleManager>().RemoveObject(this.gameObject);
				GetComponent<DefaultBehaviour>().OnDestroyed();
			}
		}
	}

	void CompareStart(string InputString){//Compares _EnemyHealth with what the player typed(InputString) 

		if (!_StopChecking && InputString.Length > 0) {
			if (TextElement == null) {} else {
			
				_WordsToRemove = 0;
				TextElement.text = "";

				if (_CleaveDmg == 0) {
					PerfectWord (InputString);

				} else if (_CleaveDmg == 1) {
					PerfectWordCleave (InputString);

				} else if (_CleaveDmg == 2) {
					SuperCleave (InputString);
				}	
			} 
		} else {
			if (TextElement == null) {} else {
				TextElement.text = string.Format ("<color=black>{0}</color>", _EnemyHealth);
				_WordsToRemove = 0;
			}
		}
	}
		
	#region DifferentWordComparers

	void PerfectWord(string InputString){//if the player types something then that's what's being deleted. if we have silver and silverpine, and the player type silverp, then only silverp from silverpine will be deleted because silver does not contain p at the end
	
		if (InputString.Length <= _EnemyHealth.Length) {
			for (int i = 0; i < _EnemyHealth.Length; i++) {
				if (i <= InputString.Length - 1) {
					if (InputString [i] == _EnemyHealth [i]) {
						_WordsToRemove++;
						TextElement.text += string.Format ("<color=green>{0}</color>", _EnemyHealth [i]);
					} else {
						TextElement.text = string.Format ("<color=black>{0}</color>", _EnemyHealth);
						_StopChecking = true;
						return;
					}
				} else {
					TextElement.text += string.Format ("<color=red>{0}</color>", _EnemyHealth [i]);
				}
			}
		} else {
			TextElement.text = string.Format ("<color=black>{0}</color>", _EnemyHealth);
			_StopChecking = true;
			return;
		}
	}

	void PerfectWordCleave(string InputString){//if player type Silverpine and Silver exists then Silver will count as well. the cleave word must be completed, Siv would not count because of the v at the end

		for (int i = 0; i < _EnemyHealth.Length; i++) {
			if (i <= InputString.Length - 1) {
				if (InputString [i] == _EnemyHealth [i]) {
					_WordsToRemove++;
					TextElement.text += string.Format ("<color=green>{0}</color>", _EnemyHealth [i]);
				} else {
					TextElement.text = string.Format ("<color=black>{0}</color>", _EnemyHealth);
					_StopChecking = true;
					return;
				}
			} else {
				TextElement.text += string.Format ("<color=red>{0}</color>", _EnemyHealth [i]);
			}
		}
	}

	void SuperCleave(string InputString){//Siv Silver Silverpine, if player types Silverpine then Si from Siv is removed, entire Silver and Silverpine is removed

		for (int i = 0; i < _EnemyHealth.Length; i++) {
			if (i <= InputString.Length - 1) {
				if (InputString [i] == _EnemyHealth [i]) {
					_WordsToRemove++;
					TextElement.text += string.Format ("<color=green>{0}</color>", _EnemyHealth [i]);
				} else {
					for (int j = i; j < _EnemyHealth.Length; j++) {
						if (j <= _EnemyHealth.Length - 1) 
							TextElement.text += string.Format ("<color=red>{0}</color>", _EnemyHealth [j]);
					}
					return;
				}
			} else {
				TextElement.text += string.Format ("<color=red>{0}</color>", _EnemyHealth [i]);
			}
		}
	}

	#endregion

	#endregion

	void SetHealthString(string words){
		_EnemyHealth = words;
	}*/