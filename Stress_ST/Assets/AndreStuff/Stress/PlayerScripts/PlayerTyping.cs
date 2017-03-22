using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerTyping : MonoBehaviour {

	//all enemies are listening to these events

	#region Events

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