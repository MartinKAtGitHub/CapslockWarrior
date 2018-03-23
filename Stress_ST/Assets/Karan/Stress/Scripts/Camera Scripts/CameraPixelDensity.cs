using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraPixelDensity : MonoBehaviour {

	public int PixelToUnit = 100;
	Camera cam;
	void Start()
	{
		cam = GetComponent<Camera>();
	}
	// Update is called once per frame
	void Update () 
	{
			cam.orthographicSize = Screen.width / PixelToUnit / 2;
	}
}
