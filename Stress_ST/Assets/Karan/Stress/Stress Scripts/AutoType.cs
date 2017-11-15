﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class AutoType : MonoBehaviour {
 
	public float letterPause = 0.2f;
	public AudioClip sound;
	public GameObject TextGUI;
	private Text Discription;
 
	string message;
 
	// Use this for initialization
	void Start () {
		Discription = TextGUI.GetComponent<Text>();
		message = Discription.text;
		Discription.text = "";
		StartCoroutine(TypeText ());
	}
 
	IEnumerator TypeText () {
		foreach (char letter in message.ToCharArray()) {
			Discription.text += letter;
			if (sound)
			//	audio.PlayOneShot (sound);
				yield return 0;
			yield return new WaitForSeconds (letterPause);
		}      
	}
}