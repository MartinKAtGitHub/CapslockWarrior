using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateYaxis : MonoBehaviour {

	public float rotatespeedx = 5;
	public float rotatespeedy = 5;
	public float rotatespeedz = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.up * Time.deltaTime * rotatespeedx);
		transform.Rotate (Vector3.left * Time.deltaTime * rotatespeedy);
		transform.Rotate (Vector3.back * Time.deltaTime * rotatespeedz);
	}
}
