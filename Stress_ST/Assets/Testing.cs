using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour {


	public Vector3 CurrentDirection = Vector3.zero;
	public Vector3 WalkingDirection = Vector3.zero;
	public Vector3 MovingVector = Vector3.zero;
	float AngleToMove = 0;
	public float RotateSpeed = 1;
	public bool Turned = true;

	// Use this for initialization
	void Start () {
	//	player = GameObject.Find ("Hero v5").transform;
	}
	// Update is called once per frame
	void Update () {
		WalkingDirection = GameObject.Find ("Hero v5").transform.position;

		if (WalkingDirection.x > transform.position.x) {
			if (Turned == true) {
				if (transform.eulerAngles.z > 90 || transform.eulerAngles.z < -90) {
					MovingVector.y = 0;
					Turned = false;
				}
			} 
		} else if (WalkingDirection.x <= transform.position.x) {
			if (Turned == false) {
				if (transform.eulerAngles.z > 90 || transform.eulerAngles.z < -90) {
					MovingVector.y = 180;
					Turned = true;
				}
			} 
		}




		if (transform.eulerAngles.y == 180) {
			CurrentDirection = Quaternion.AngleAxis (transform.eulerAngles.z, Vector3.back) * Vector3.right;
			if (WalkingDirection.y < 0) {
				AngleToMove = Vector3.Angle (CurrentDirection * -1, WalkingDirection) * -1;
				if (Vector3.Cross (CurrentDirection * -1, WalkingDirection).z < 0) {
					AngleToMove *= -1;
				}
			} else {
				AngleToMove = Vector3.Angle (CurrentDirection * -1, WalkingDirection);	
				if (Vector3.Cross (CurrentDirection * -1, WalkingDirection).z > 0) {
					AngleToMove *= -1;
				}
			}
		} else {
			CurrentDirection = Quaternion.AngleAxis (transform.eulerAngles.z, Vector3.forward) * Vector3.right;
			if (WalkingDirection.y < 0) {
				AngleToMove = Vector3.Angle (CurrentDirection, WalkingDirection) * -1;
				if (Vector3.Cross (CurrentDirection, WalkingDirection).z > 0) {
					AngleToMove *= -1;
				}
			} else {
				AngleToMove = Vector3.Angle (CurrentDirection, WalkingDirection);
				if (Vector3.Cross (CurrentDirection, WalkingDirection).z < 0) {
					AngleToMove *= -1;
				}
			}
		}
	
		MovingVector.z += (AngleToMove * Time.deltaTime) * RotateSpeed;
		if (MovingVector.z < -180) {
			MovingVector.z = 360 + MovingVector.z;
		}
		if (MovingVector.z > 180) {
			MovingVector.z = MovingVector.z - 360;
		}
		transform.eulerAngles = MovingVector;


	}
}
