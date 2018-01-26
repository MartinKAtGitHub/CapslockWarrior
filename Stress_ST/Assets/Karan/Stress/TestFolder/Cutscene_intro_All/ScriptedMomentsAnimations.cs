using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedMomentsAnimations : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(Cutscene());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Cutscene()
	{
		Debug.Log("Start Intro");

		yield return new WaitForSeconds(3f);

		Debug.Log("Start Countown");

		yield return new WaitForSeconds(6f);

		Debug.Log("Start Gameplay");


	}
}
