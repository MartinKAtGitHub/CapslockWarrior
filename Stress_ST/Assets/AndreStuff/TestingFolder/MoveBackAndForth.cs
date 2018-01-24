using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class MoveBackAndForth : MonoBehaviour {

	public Transform TheStart;
	public Transform TheEnd;
	public float delta;
	bool turnaround = false;
	public float TurnAtSpeedValue = 1;
	public float RotatingSpeed = 1;
	Vector3 RotatingVector = Vector3.right;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	//	transform.RotateAround (transform.position, Vector3.right, RotatingSpeed);
		transform.Rotate (Vector3.right * Time.deltaTime * RotatingSpeed);
		transform.Rotate (Vector3.up * Time.deltaTime * RotatingSpeed);

		if (turnaround == false) {
			transform.position = Vector3.MoveTowards (transform.position, TheEnd.position, delta * Time.deltaTime);

			if (Vector3.Distance (transform.position, TheEnd.position) <= TurnAtSpeedValue) {
				turnaround = true;
			}

		} else {
			transform.position = Vector3.MoveTowards (transform.position, TheStart.position, delta * Time.deltaTime);

			if (Vector3.Distance (transform.position, TheStart.position) <= TurnAtSpeedValue) {
				turnaround = false;
			}

		}
			
	}
}
