using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTESTViewAspectReselutionChange : MonoBehaviour {

	public float Width;
	public float Hight;
	private Camera Cam;
// THIS WILL KEEP THE CAM SIZE NO MATTER THE RESULOTION

// THE ASPECT WILL BE PERCERVED WHEN CHANGEING THE RESELUTON.

// POINT IS ---------> You WIll always see the same thing not MORE OR LESS depending os RESELUTION 

// MOTE THIS DOSE NOT WORK WITH PIXEL PERFECT!!!!!!!!!!!

	void Start () 
	{
		Cam = GetComponent<Camera>();
		Cam.aspect = Width/Hight;
		//Camera.main.aspect = 1.777778f;	
		//Camera.main.aspect = Width/Hight;	

		Debug.Log("Forcing ASPECT ratio -> " + Camera.main.aspect);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("SETTING ASPECT " + Camera.main.aspect);
	}
}
