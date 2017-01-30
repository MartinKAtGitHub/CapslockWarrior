using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	//The assigned camera is following this object

	public GameObject MainCamera;
	public float ZoomSpeed = 20f;

	void Start () {
		if (MainCamera == null) {
			Destroy (this);
		} else {
		MainCamera.GetComponent<Camera> ().orthographicSize = 12.5f;
		}
	}
		
	void Update () {
		if (Input.GetKey(KeyCode.KeypadMinus)) {
			if(MainCamera.GetComponent<Camera>().orthographicSize > 7.5f)
				MainCamera.GetComponent<Camera>().orthographicSize = MainCamera.GetComponent<Camera>().orthographicSize - (Time.smoothDeltaTime * ZoomSpeed);
		}

		if (Input.GetKey(KeyCode.KeypadPlus)) {
			if(MainCamera.GetComponent<Camera>().orthographicSize < 12.5f)
				MainCamera.GetComponent<Camera>().orthographicSize = MainCamera.GetComponent<Camera>().orthographicSize + (Time.smoothDeltaTime * ZoomSpeed);
		}
		MainCamera.transform.position = transform.position;
	}
}
