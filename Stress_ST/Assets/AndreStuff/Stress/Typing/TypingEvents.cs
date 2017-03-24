using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class TypingEvents {

	public delegate void StartCompare(KeyValuePair<GameObject, KeyValuePair<Color, string[]>>  word);
	public delegate void RestartCompare();
	public delegate void EndCompare(GameObject word);

	public static event StartCompare OnCompareStart;
	public static event RestartCompare OnCompareRestart;
	public static event EndCompare OnCompareEnd;

	public static void OnStartCompareChanged(KeyValuePair<GameObject, KeyValuePair<Color, string[]>>  word) {
		if (OnCompareStart != null) {
			OnCompareStart(word);
		}
	}

	public static void OnRestartCompareChanged() {
		if (OnCompareRestart != null) {
			OnCompareRestart();
		}
	}

	public static void OnEndCompareChanged(GameObject theObject) {
		if (OnCompareEnd != null) {
			OnCompareEnd(theObject);
		}
	}
}
