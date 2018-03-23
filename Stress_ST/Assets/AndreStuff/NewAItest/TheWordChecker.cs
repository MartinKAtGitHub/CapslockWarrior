using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheWordChecker : MonoBehaviour {

	EnemyManaging MyManager;
	KeyValuePair<GameObject, KeyValuePair<Color, string[]>> _LongestPlayer;
	KeyValuePair<GameObject, KeyValuePair<Color, string[]>> _2ndLongestPlayer;

	string _ObjectHealth = "ObjectHealth";
	int _WordsToRemove = 0;//a counter that is used to deside how many words to remove after the player presses enter
	string _OriginalWord;

	Color32 PlayerColor;
	string ColorValue;
	List<bool> PlayersTypedCorrect = new List<bool>();
	int _WordLengths = 0;
	int NullCheck = 0;
	bool IgnoreTyping = false;


	public void SetupFirstWord(EnemyManaging val){

		_ObjectHealth = ListOfWords.GetRandomWords ((int)val.Stats.WordDifficulty);
		_OriginalWord = _ObjectHealth;

		val.CreatureWords.TextElement.text = _ObjectHealth;

	}


	public void CompareStart(KeyValuePair<GameObject, KeyValuePair<Color, string[]>>  InputString, EnemyManaging val){//Is Called When The Player Types Something 
		
			MyManager = val;

			if (!MyManager.CreatureWords._Players.Contains (InputString)) {//Adding New Players To The List
				MyManager.CreatureWords._Players.Add (InputString);
				PlayersTypedCorrect.Add (true);
			}

			for (int i = 0; i < MyManager.CreatureWords._Players.Count; i++) {//Checking If The InputString/CurrentPlayer Is Corrent And Need Update
				if (MyManager.CreatureWords._Players [i].Key == InputString.Key) {
					_WordLengths = MyManager.CreatureWords._Players [i].Value.Value [0].Length;

					if (_WordLengths > _ObjectHealth.Length) {
						_WordLengths = _ObjectHealth.Length;
					}

					for (int j = (_WordLengths - 1); j >= 0; j--) {
						if (MyManager.CreatureWords._Players [i].Value.Value [0] [j] != _ObjectHealth [j]) {
							PlayersTypedCorrect [i] = false;
							break;
						}
						if (j == 0) {
							PlayersTypedCorrect [i] = true;
							if (_WordLengths == _ObjectHealth.Length) {

								if (MyManager.Stats.Health > 0) {
									MyManager.Stats.Health--;




									MyManager.CreatureWords._Players [i].Key.GetComponent<PlayerManager> ().GotTheKill (_OriginalWord.Length);//Giving The Player That Wrote The Last Word The Score  //TODO Give Each Player Tagged Score?
									MyManager.CreatureWords._Players [i].Key.GetComponent<PlayerManager> ().ResetWord ();//Giving The Player That Wrote The Last Word The Score  //TODO Give Each Player Tagged Score?

									_ObjectHealth = ListOfWords.GetRandomWords ((int)MyManager.Stats.WordDifficulty);
									_OriginalWord = _ObjectHealth;

									MyManager.CreatureWords.TextElement.text = "";//Removing Text So That I Can Add It Again With New Colors
									PlayerColor = (Color32)Color.black;
									ColorValue = PlayerColor.r.ToString ("X2") + PlayerColor.g.ToString ("X2") + PlayerColor.b.ToString ("X2") + PlayerColor.a.ToString ("X2");

									for (int k = 0; k < _ObjectHealth.Length; k++) {
										MyManager.CreatureWords.TextElement.text += string.Format ("<color=#" + ColorValue + ">{0}</color>", _ObjectHealth [k]);
									}

									return;
								} else {
									MyManager.CreatureWords._Players [i].Key.GetComponent<PlayerManager> ().ResetWord ();//Giving The Player That Wrote The Last Word The Score  //TODO Give Each Player Tagged Score?
									MyManager.CreatureWords._Players [i].Key.GetComponent<PlayerManager> ().GotTheKill (_OriginalWord.Length);//Giving The Player That Wrote The Last Word The Score  //TODO Give Each Player Tagged Score?

									//TheCreature.OnDestroyed ();TODO Destroy/SendDestroy Request
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

			for (int i = 0; i < MyManager.CreatureWords._Players.Count; i++) {//Iterating Through Once To Find The Two Players That Have Typed The Most. If Two Are The Same The The One That Tagged It Is The Leader
				if (PlayersTypedCorrect [i] == true) {
					if (MyManager.CreatureWords._Players [i].Value.Value [0].Length > _WordLengths) {
						_2ndLongestPlayer = _LongestPlayer;
						_LongestPlayer = MyManager.CreatureWords._Players [i];
						NullCheck++;
					}
				}
			}

			_WordsToRemove = 0;
			MyManager.CreatureWords.TextElement.text = "";//Removing Text So That I Can Add It Again With New Colors

			if (NullCheck > 1) {//Adding 2nd Place Player Color
				_WordLengths = _2ndLongestPlayer.Value.Value [0].Length;//Length Of Player Word
				PlayerColor = (Color32)_2ndLongestPlayer.Value.Key;//Player Color
				ColorValue = PlayerColor.r.ToString ("X2") + PlayerColor.g.ToString ("X2") + PlayerColor.b.ToString ("X2") + PlayerColor.a.ToString ("X2");//Setting Color, Only Way I Got It To Work

				for (int i = 0; i < _WordLengths; i++) {//Iterates Through And Adds The Letter Again But With Different Color
					_WordsToRemove++;
					MyManager.CreatureWords.TextElement.text += string.Format ("<color=#" + ColorValue + ">{0}</color>", _ObjectHealth [i]);
				} 
			}

			if (NullCheck > 0) {//Adding 1nd Place Player Color
				_WordLengths = _LongestPlayer.Value.Value [0].Length;
				PlayerColor = (Color32)_LongestPlayer.Value.Key;
				ColorValue = PlayerColor.r.ToString ("X2") + PlayerColor.g.ToString ("X2") + PlayerColor.b.ToString ("X2") + PlayerColor.a.ToString ("X2");

				for (int i = _WordsToRemove; i < _WordLengths; i++) {
					_WordsToRemove++;
					MyManager.CreatureWords.TextElement.text += string.Format ("<color=#" + ColorValue + ">{0}</color>", _ObjectHealth [i]);
				} 
			}

			PlayerColor = (Color32)Color.black;
			ColorValue = PlayerColor.r.ToString ("X2") + PlayerColor.g.ToString ("X2") + PlayerColor.b.ToString ("X2") + PlayerColor.a.ToString ("X2");

			for (int i = _WordsToRemove; i < _ObjectHealth.Length; i++) {
				MyManager.CreatureWords.TextElement.text += string.Format ("<color=#" + ColorValue + ">{0}</color>", _ObjectHealth [i]);
			}
		}

}