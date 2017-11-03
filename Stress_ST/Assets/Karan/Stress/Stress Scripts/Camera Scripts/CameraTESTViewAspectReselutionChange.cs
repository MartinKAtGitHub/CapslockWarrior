using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTESTViewAspectReselutionChange : MonoBehaviour {

public int Width;
public int Hight;
// THIS WILL KEEP THE CAM SIZE NO MATTER THE RESULOTION

// THE ASPECT WILL BE PERCERVED WHEN CHANGEING THE RESELUTON.

// POINT IS ---------> You WIll always see the same thing not MORE OR LESS depending os RESELUTION 

// MOTE THIS DOSE NOT WORK WITH PIXEL PERFECT!!!!!!!!!!!

	void Start () 
	{
		Camera.main.aspect = 1.777778f;	// For some reason when i do Width(16)/Hight(9) i get 1. I should be getting 1.777778f

		Debug.Log("Forcing ASPECT ratio -> " + Camera.main.aspect);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("SETTING ASPECT " + Camera.main.aspect);
	}
}
