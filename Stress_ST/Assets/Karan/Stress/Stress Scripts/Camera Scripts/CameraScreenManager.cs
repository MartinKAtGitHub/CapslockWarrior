using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraScreenManager : MonoBehaviour {

	public float Width = 16f;
	public float Hight = 9f;
	private Camera Cam;
// THIS WILL KEEP THE CAM SIZE NO MATTER THE RESULOTION

// THE ASPECT WILL BE PERCERVED WHEN CHANGEING THE RESELUTON.

// POINT IS ---------> You WIll always see the same thing not MORE OR LESS depending os RESELUTION 

// MOTE THIS DOSE NOT WORK WITH PIXEL PERFECT!!!!!!!!!!!

	void Start () 
	{
		Cam = GetComponent<Camera>();
		Debug.Log("Cam = " + Cam.gameObject.name);
		//GetResolutions();
		ForceAspectRatio();
		//GetAspectRatio(Screen.width,Screen.height,true);
		//ForceRes();
		
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("SETTING ASPECT " + Camera.main.aspect);
		//GetAspectRatio(Screen.width,Screen.height,true);
	}

	private void GetResolutions()
	{
		Resolution[] resolutions = Screen.resolutions;
        foreach (Resolution res in resolutions) {
            print(res.width + "x" + res.height);
        }
       // Screen.SetResolution(resolutions[0].width, resolutions[0].height, true);
	}

	private void ForceAspectRatio()
	{
		//Camera.main.aspect = 1.777778f;	<--- this num means its a 16:9 res
		Cam.aspect = Width/Hight;
		Debug.Log("Forcing ASPECT ratio -> " + Cam.aspect);
	}

	public static Vector2 GetAspectRatio(int x, int y, bool debug)
	{
		float f = (float)x / (float)y;
		int i = 0;
		while(true){
			i++;
			if(System.Math.Round(f * i, 2) == Mathf.RoundToInt(f * i))
				break;
		}
		if(debug)
			Debug.Log("Aspect ratio is "+ f * i +":"+ i +" (Resolution: "+ x +"x"+ y +")");
		return new Vector2((float)System.Math.Round(f * i, 2), i);
	}
	
	 private void ForceRes()
	{
		Screen.SetResolution(800,600,true);
	}
	
}
