using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerTyping : MonoBehaviour {

	//all enemies are listening to these events

	public Text TextElement;

	public void ResetText(string deletethis){
		TextElement.text.Remove (0, deletethis.Length);
	}

	public Color TextColor;
	string[] myText = new string[1];
	KeyValuePair<GameObject, KeyValuePair<Color, string[]>>  keytest;

	void Start (){
		if (TextElement == null) {
			TextElement = GameObject.Find ("Canvas").transform.GetChild(0).FindChild("Text").gameObject.GetComponent<Text>();
		}
		keytest = new KeyValuePair<GameObject, KeyValuePair<Color, string[]>>  (this.gameObject, new KeyValuePair<Color, string[]>(TextColor, myText));
		myText [0] = "";
	}
	bool ClearText = false;
	void Update () {//checking when pressing down a button, and if it's an ok letter then it goes through and the enemies recieves it
		if (ClearText == true) {
			ClearText = false;
			myText [0] = "";
			TextElement.text = "";
		}

		if (Input.anyKeyDown && !(Input.GetKeyDown (KeyCode.Backspace) || Input.GetKeyDown (KeyCode.Return))) {
			if ((string)Input.inputString != "") {//max input = 5 letters
				for (int i = 0; i < ((string)Input.inputString).Length; i++) {
					if (char.GetNumericValue (((string)Input.inputString) [i]) < 0) {
						myText [0] += ((string)Input.inputString) [i];
					}
				}
				TextElement.text = myText [0];
				TypingEvents.OnStartCompareChanged (keytest);
			}
		} else {
			if (Input.GetKeyDown (KeyCode.Return)) {
				TypingEvents.OnEndCompareChanged (this.gameObject);
				myText [0] = "";
				TextElement.text = "";
			} else if (Input.GetKeyDown (KeyCode.Backspace)) {
				if (TextElement.text.Length > 0) {
					myText [0] = myText [0].Remove (myText [0].Length - 1);
					TextElement.text = myText [0];
					TypingEvents.OnStartCompareChanged (keytest);
				}
			}
		}
	}

	public void ResetTheText(){
		ClearText = true;
	}

}
/*#region Events

	public delegate void StartCompare(string word);
	public delegate void RestartCompare();
	public delegate void EndCompare();
	
	public static event StartCompare OnCompareStart;
	public static event RestartCompare OnCompareRestart;
	public static event EndCompare OnCompareEnd;

	public Text TextElement;

	public void OnStartCompareChanged(string word) {
		if (OnCompareStart != null) {
			OnCompareStart(word);
		}
	}
		
	public void OnRestartCompareChanged() {
		if (OnCompareRestart != null) {
			OnCompareRestart();
		}
	}

	public void OnEndCompareChanged() {
		if (OnCompareEnd != null) {
			OnCompareEnd();
		}
	}
	#endregion

	void Update () {//checking when pressing down a button, and if it's an ok letter then it goes through and the enemies recieves it
		if (Input.anyKeyDown && !(Input.GetKeyDown (KeyCode.Backspace) || Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown (KeyCode.Return))) {
			if ((string)Input.inputString != "") {
				TextElement.text += (string)Input.inputString;
				OnStartCompareChanged (TextElement.text);
			}
		} else if (Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)){
			OnEndCompareChanged ();
			TextElement.text = "";
			OnRestartCompareChanged ();
			OnStartCompareChanged (TextElement.text);
		}

		if (Input.GetKeyDown (KeyCode.Backspace)){
			if (TextElement.text.Length > 0) {
				TextElement.text = TextElement.text.Remove(TextElement.text.Length - 1);
				OnRestartCompareChanged ();
				OnStartCompareChanged (TextElement.text);
			}
		}
	}*/