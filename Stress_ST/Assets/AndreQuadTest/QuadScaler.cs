using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class QuadScaler : MonoBehaviour {

	public Vector2 DefaultResolution_16_9 = Vector2.zero;
	public Vector2 DefaultResolution_4_3 = Vector2.zero;

	public bool Ration1_Or_2 = true;
	public bool UpdateResolution = false;
	public Vector3 Scale16_9 = Vector3.one;
	public Vector3 Scale4_3 = Vector3.one;
	int previousX = 0;
	int previousY = 0;

	public Camera TheMainCamera;
	// Update is called once per frame

	/*void Start(){
		previousX = Screen.width;
		previousY = Screen.height;
		UpdateResolution = true;
	}*/

	void Update () {

		if (previousX != Screen.width || previousY != Screen.height) {
			previousX = Screen.width;
			previousY = Screen.height;
			UpdateResolution = true;
		}
		
		if (UpdateResolution == true) {
			UpdateResolution = false;

			if (Ration1_Or_2 == true) {
			
			
				float a = ((((float)Screen.width / (float)Screen.height) * 100) / (DefaultResolution_16_9.x / DefaultResolution_16_9.y)) / 100;//getting The % reduction or increase in screen size.

				TheMainCamera.orthographicSize = 1 * a;

				transform.localScale = new Vector3 (Scale16_9.x * a, Scale16_9.y * a, 1);
			
			} else {
			
				float a = ((((float)Screen.width / (float)Screen.height) * 100) / (DefaultResolution_4_3.x / DefaultResolution_4_3.y)) / 100;//getting The % reduction or increase in screen size.

				TheMainCamera.orthographicSize = 1 * a;

				transform.localScale = new Vector3 (Scale4_3.x * a, Scale4_3.y * a, 1);
			
			}
		}
	
	}
}