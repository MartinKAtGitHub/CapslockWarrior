using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camsamesize : MonoBehaviour {

	public Camera came;

	// Use this for initialization
	void Start () {
		GetComponent<Camera> ().orthographicSize = came.orthographicSize;
	}

}
