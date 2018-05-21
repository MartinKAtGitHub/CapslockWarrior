using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerTyping : MonoBehaviour {


	public Text SentenceTextBox;
	public Text SentenceTextBox2;
	public PlayerAttack ThePlayerAttack;

	public Color32 BaseColor;//Base Color To The Sentence
	public Color32 CompleteColor;//When A Word Complete This Is The Color
	public Color32 CorrectTypedColor;//Color Applyed To The Letters Which The Player Has Currently Writing
	public Color32 WrongTypedColor;//Color Applyed To The Letters Which The Player Has Written Wrong

	public float LetterBonusMultiplyer = 1.5f;//Bonus Multiplyer To Longer Words. TODO Make It Scale With Longer Words?????? ('Are' Give 3 * 1.5 == 4.5, 'Folder' Gives 6 * 1.5 == 9. Meaning That It Doesnt Scale So Might Make The Multiplyer Exponential)
	public bool CanGetPointsAfterCorrectWord = true;//If True Then Points Are Gained After A Word Is Typed Correctly.
	public bool CanSentenceChangeAtEnd = true;//If True Then The Sentence Changes When The Laste Word Is Typed Correctly
	//	public float WordCombo = 1.00f;

	int _WordIndex = 0;//The Current Word In The Sentence
	int _CurrentWordIndex = 0;//Used To Iterate Through The Different Words When Giving Color

	List<List<string>> _AllSentences = new List<List<string>> ();

	string TheSentence = "";//Used For A Saver For Adding The New Colored Line Back To The Text.UI Object
	string _CheckWord = "";//Used As A CheckPoint For The Current Words In The Sentence
	string _TypedLetters = "";//The Letters That The Player Have Typed
	string _TypedSaved = "";//Used To Store The Player Typed Sentence, To Be Able To Remove Letters And Stuff
	string _ColoredTypedWords = "";//Used To Store The New Colored String

	string _ColorValue;
	int _CheckWordLength = 0;

	int _StoredPoints = 0;//Points Stored If Player Writes Several Words In On Go. More Words == More Points



	public int SentencesToHave = 10;

	void Start (){

		for(int i = 0; i < SentencesToHave; i++){
			_AllSentences.Add (new List<string>(AllOurWords.sentencetypeone()));
		}

		TheSentence = "";
		for (int i = 0; i < _AllSentences[0].Count; i++) {
			TheSentence += _AllSentences[0][i] + " ";
		}

		SentenceTextBox.text = TheSentence;

		TheSentence = "";
		for (int i = 0; i < _AllSentences[1].Count; i++) {
			TheSentence += _AllSentences[1][i] + " ";
		}

		SentenceTextBox2.text = TheSentence;

	}



	void Update () {//checking when pressing down a button, and if it's an ok letter then it goes through and the enemies recieves it
	
		if (Input.anyKeyDown) {
		
			if (Input.GetKeyDown (KeyCode.Return)) {
				SentenceCheck ();//WordCheck
				_TypedLetters = "";
				SentenceUpdate ();//Color Update

			} else if (Input.GetKeyDown (KeyCode.Backspace)) {
				if (_TypedLetters.Length > 0) {
					_TypedLetters = _TypedLetters.Remove (_TypedLetters.Length - 1);
					SentenceUpdate ();//Color Update
				}
			} else {//Any Other Key
				
				if (Input.inputString != "") {//Max Input == 5 Letters, And This Remove MouseClick As A Letter
					for (int i = 0; i < Input.inputString.Length; i++) {
						if (char.GetNumericValue (Input.inputString [i]) < 0) {//Removing Numbers By Doing This
							_TypedLetters += Input.inputString [i];
						}
					}
		
					if (_TypedLetters.Length > 0)
						SentenceUpdate ();//Color Update
				}
			}
		}
	}

	public void CanChangeSentenceAgain(bool state){//Starting The SentenceChange Again

		if (state == false) {
			CanSentenceChangeAtEnd = false;
		} else {
			CanSentenceChangeAtEnd = true;
			if (_WordIndex >= _AllSentences [0].Count) {
				_WordIndex = 0;
				_AllSentences.RemoveAt (0);
				_AllSentences.Add (new List<string> (AllOurWords.sentencetypeone ()));
				_TypedLetters = "";
				SentenceUpdate ();//Color Update
			}
		}

	}

	void SetWordColor (int wordsCleared, string coloredText){
	
		_CurrentWordIndex = _WordIndex;
		SentenceTextBox.text = "";
		TheSentence = "";

		_ColorValue = CompleteColor.r.ToString ("X2") + CompleteColor.g.ToString ("X2") + CompleteColor.b.ToString ("X2") + CompleteColor.a.ToString ("X2");//Setting Color
		for (int i = 0; i < _WordIndex; i++) {//Alrdy Complete Words
			TheSentence += string.Format ("<color=#" + _ColorValue + ">{0}</color>", _AllSentences [0] [i]);
			TheSentence += " ";
		}

		TheSentence += coloredText;//Typed Colors


		_ColorValue = BaseColor.r.ToString ("X2") + BaseColor.g.ToString ("X2") + BaseColor.b.ToString ("X2") + BaseColor.a.ToString ("X2");//Setting Color
		for (int i = wordsCleared; i < _AllSentences [0].Count; i++) {//Words Yet Untouched
			TheSentence += string.Format ("<color=#" + _ColorValue + ">{0}</color>", _AllSentences [0] [i]);
			if(i < _AllSentences [0].Count - 1)
				TheSentence += " ";
		}

		SentenceTextBox.text = TheSentence;

		TheSentence = "";
		_ColorValue = BaseColor.r.ToString ("X2") + BaseColor.g.ToString ("X2") + BaseColor.b.ToString ("X2") + BaseColor.a.ToString ("X2");//Setting Color
		for (int i = 0; i < _AllSentences [1].Count; i++) {//Second Line
			TheSentence += string.Format ("<color=#" + _ColorValue + ">{0}</color>", _AllSentences [1] [i]);
			if(i < _AllSentences [1].Count - 1)
				TheSentence += " ";
		}
		SentenceTextBox2.text = TheSentence;

	}

	void SentenceUpdate(){

		_CurrentWordIndex = _WordIndex;
		_TypedSaved = _TypedLetters;
		_ColoredTypedWords = "";

		while (_TypedSaved.Length > 0) {

			if (_CurrentWordIndex < _AllSentences [0].Count) {//If False Then The Sentence Is Complete But Cannot Get A New One Yet

				_CheckWord = _AllSentences [0] [_CurrentWordIndex];//Sentence Word
				_CheckWordLength = _CheckWord.Length;//CurrentTypedWord
			

				if (_TypedSaved.Length >= _CheckWordLength) {
				
					for (int i = 0; i < _CheckWordLength; i++) {
						if (_TypedSaved [i] == _CheckWord [i]) {//Setting Correct Color
							_ColorValue = CorrectTypedColor.r.ToString ("X2") + CorrectTypedColor.g.ToString ("X2") + CorrectTypedColor.b.ToString ("X2") + CorrectTypedColor.a.ToString ("X2");//Setting Color
							_ColoredTypedWords += string.Format ("<color=#" + _ColorValue + ">{0}</color>", _CheckWord [i]);
						} else {
							_ColorValue = WrongTypedColor.r.ToString ("X2") + WrongTypedColor.g.ToString ("X2") + WrongTypedColor.b.ToString ("X2") + WrongTypedColor.a.ToString ("X2");//Setting Color
							_ColoredTypedWords += string.Format ("<color=#" + _ColorValue + ">{0}</color>", _CheckWord [i]);
						}
					}
			
					if (_TypedSaved.Length > _CheckWordLength) {
						if (!_TypedSaved [_CheckWordLength].Equals (' ')) {//If Next Letter Isnt Space. Then The Next Word Is Wrong
							_ColorValue = WrongTypedColor.r.ToString ("X2") + WrongTypedColor.g.ToString ("X2") + WrongTypedColor.b.ToString ("X2") + WrongTypedColor.a.ToString ("X2");//Setting Color
							_ColoredTypedWords += string.Format ("<color=#" + _ColorValue + ">{0}</color>", " ");
							_TypedSaved = _TypedSaved.Remove (0, _CheckWordLength + 1);
						} else {
							_ColorValue = CorrectTypedColor.r.ToString ("X2") + CorrectTypedColor.g.ToString ("X2") + CorrectTypedColor.b.ToString ("X2") + CorrectTypedColor.a.ToString ("X2");//Setting Color
							_ColoredTypedWords += string.Format ("<color=#" + _ColorValue + ">{0}</color>", " ");
							_TypedSaved = _TypedSaved.Remove (0, _CheckWordLength + 1);
						}

						if (_TypedSaved.Length == 0) {
							SetWordColor (++_CurrentWordIndex, _ColoredTypedWords);
							return;
						} else {
							_CurrentWordIndex++;
						}
					} else {
						_ColoredTypedWords += " ";
						SetWordColor (++_CurrentWordIndex, _ColoredTypedWords);
						return;
					}

				} else {
					for (int i = 0; i < _TypedSaved.Length; i++) {
						if (_TypedSaved [i] == _CheckWord [i]) {//Setting Correct Color
							_ColorValue = CorrectTypedColor.r.ToString ("X2") + CorrectTypedColor.g.ToString ("X2") + CorrectTypedColor.b.ToString ("X2") + CorrectTypedColor.a.ToString ("X2");//Setting Color
							_ColoredTypedWords += string.Format ("<color=#" + _ColorValue + ">{0}</color>", _CheckWord [i]);
						} else {
							_ColorValue = WrongTypedColor.r.ToString ("X2") + WrongTypedColor.g.ToString ("X2") + WrongTypedColor.b.ToString ("X2") + WrongTypedColor.a.ToString ("X2");//Setting Color
							_ColoredTypedWords += string.Format ("<color=#" + _ColorValue + ">{0}</color>", _CheckWord [i]);
						}
					}

					for (int i = _TypedSaved.Length; i < _CheckWordLength; i++) {
						_ColorValue = BaseColor.r.ToString ("X2") + BaseColor.g.ToString ("X2") + BaseColor.b.ToString ("X2") + BaseColor.a.ToString ("X2");//Setting Color
						_ColoredTypedWords += string.Format ("<color=#" + _ColorValue + ">{0}</color>", _CheckWord [i]);
					}

					_ColoredTypedWords += " ";
					SetWordColor (++_CurrentWordIndex, _ColoredTypedWords);
					return;
				}
			} else {
				return;
			}
		}

		SetWordColor (_CurrentWordIndex, _ColoredTypedWords);
	}

	void SentenceCheck(){
		_StoredPoints = 0;

		while (true) {
		
			if (_WordIndex < _AllSentences [0].Count) {
				_CheckWord = _AllSentences [0] [_WordIndex];//Sentence Word
				_CheckWordLength = _CheckWord.Length;//CurrentTypedWord

				if (_TypedLetters.Length >= _CheckWordLength) {
					for (int j = 0; j < _CheckWordLength; j++) {//Checking If Words Are The Same
						if (_TypedLetters [j] != _CheckWord [j]) {
							if (CanGetPointsAfterCorrectWord == true) {
								ThePlayerAttack.DmgGained (_StoredPoints);//Adding Dmg To The DmgCharger
							}
							return;
						}
					}
				} else {
					if (CanGetPointsAfterCorrectWord == true) {
						ThePlayerAttack.DmgGained (_StoredPoints);//Adding Dmg To The DmgCharger
					}
			//		WordCombo -= 0.01f;//Used Space While Nothing Complete, Results In A Penalty
					return;
				}

			//	WordCombo += 0.01f;//Adding Combo Points
				_StoredPoints += Mathf.FloorToInt ((_CheckWordLength * LetterBonusMultiplyer) /* * WordCombo*/);//Combo Is A % Increase To Points
				_TypedLetters = _TypedLetters.Remove (0, _CheckWordLength);//Removing Complete Word.length from the string

				if (_WordIndex + 1 < _AllSentences [0].Count) {
					_WordIndex++;
				} else {
					if (CanSentenceChangeAtEnd == true) {
						_WordIndex = 0;
						_AllSentences.RemoveAt (0);
						_AllSentences.Add (new List<string> (AllOurWords.sentencetypeone ()));
						_TypedLetters = "";
					} else {
						_WordIndex = _AllSentences [0].Count;
					}

					if (_StoredPoints > 0) {
						if (CanGetPointsAfterCorrectWord == true) {
							ThePlayerAttack.DmgGained (_StoredPoints);//Adding Dmg To The DmgCharger
						}
					}
					return;
				}

				if (_TypedLetters.Length > 0) {
					if (!_TypedLetters [0].Equals (' ')) {//If Next Letter Isnt Space. Then There Is No Next Word, Cuz There Is Always Space Between
						_TypedLetters = "";
						if (CanGetPointsAfterCorrectWord == true) {
							ThePlayerAttack.DmgGained (_StoredPoints);//Adding Dmg To The DmgCharger
						}
						return;
					} else {
						_TypedLetters = _TypedLetters.Remove (0, 1);//Removing Space
					}
				}

			} else {
				
				return;
			}
		}
	}
}