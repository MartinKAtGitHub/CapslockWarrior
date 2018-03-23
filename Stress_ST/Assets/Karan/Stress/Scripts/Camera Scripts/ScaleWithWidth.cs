using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ScaleWithWidth : MonoBehaviour {


	public int targetWidth;
	public int PixelTounit = 100;
	Camera Cam;
			// Use this for initialization
	void Start () {
		Cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		int height = Mathf.RoundToInt(targetWidth / (float)Screen.width * Screen.height);	
		Cam.orthographicSize = height / PixelTounit /2 ;
	}
	
	/*public float horizontalResolution = 1920;
	
	void OnGUI ()
	{
		float currentAspect = (float) Screen.width / (float) Screen.height;
		Camera.main.orthographicSize = horizontalResolution / currentAspect / 200;
	}*/

}
