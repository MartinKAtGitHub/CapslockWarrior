using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotationTesting : MonoBehaviour {

	public Vector3 Rotating = Vector3.zero;
	public float RotatingSpeed = 1;

	
	// Update is called once per frame
	void Update () {

		transform.Rotate (Rotating * Time.deltaTime * RotatingSpeed);

	
	}
}
