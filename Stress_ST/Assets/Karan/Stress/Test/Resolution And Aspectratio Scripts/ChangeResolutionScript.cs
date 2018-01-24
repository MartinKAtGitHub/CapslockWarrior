using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeResolutionScript : MonoBehaviour {

	public int Width = 1920;
	public int Heigth = 1080;


	
	// Update is called once per frame
	void Update () 
	{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				Screen.SetResolution(Width, Heigth, false);
				Debug.Log("Res (" + Width + " X " + Heigth +")");
			}
	}
}
