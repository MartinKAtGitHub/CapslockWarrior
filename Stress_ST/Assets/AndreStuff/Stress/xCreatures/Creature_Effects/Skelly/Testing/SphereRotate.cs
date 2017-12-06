using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRotate : MonoBehaviour {

	public Vector3[] RotatePoints;//points that the object is going to/ rotating to
	public bool SetRandomPoints = false;
	public int HowManyPoints;

	public float MovementSpeed;
	public float RotatingSpeed;

	public bool SetStartVector = false;//if true then it starts with that vector
	public Vector3 MyMovementVector = Vector3.zero;


	void Start(){
		if (SetRandomPoints == true) {
			RotatePoints = new Vector3[HowManyPoints];
			RotatePoints [0] = new Vector3(1,0,0);
			RotatePoints [1] = new Vector3(-1,0,0);
			RotatePoints [2] = new Vector3(0,1,0);
			RotatePoints [3] = new Vector3(-0.45f,-1,0);
		}

		if (SetStartVector == false) {
			MyMovementVector = new Vector3 (0, Mathf.Sin (Mathf.Deg2Rad * (transform.eulerAngles.z + 180)), Mathf.Cos (Mathf.Deg2Rad * (transform.eulerAngles.z + 180)));
		}
	//	transform.rotation = Quaternion.Euler (_Direction);
	}

	public int point = 0;
	public 	float angle = 0;
	public float checkangle = 0;
	public bool turnRight = false;
	Vector3 testing = Vector3.zero;
	public Vector3 tes = Vector3.zero;
	void Update(){

	//	MyMovementVector = new Vector3 (Mathf.Cos (Mathf.Deg2Rad * (transform.eulerAngles.z + 180)), Mathf.Sin (Mathf.Deg2Rad * (transform.eulerAngles.z + 180)), 0);

		angle = Vector3.Angle (MyMovementVector, RotatePoints [point] - transform.localPosition);

		if (180 == angle) {
			Debug.Log ("RNG");
			if (Random.Range (0, 2) == 0) {//not always turn the same way. so lets rng decide
				turnRight = false;
			} else {
				turnRight = true;
			}
		} else {
			checkangle = Vector3.Angle (Quaternion.Euler(0,0, angle * 0.01f) * MyMovementVector, RotatePoints [point] - transform.localPosition);
		
			if (checkangle < angle) {
				turnRight = false;
			} else {
				turnRight = true;
			}
		}

		if (turnRight == true) {
			testing.z = (RotatingSpeed * Time.deltaTime);
			transform.eulerAngles -= testing;
			MyMovementVector = new Vector3 (Mathf.Cos (Mathf.Deg2Rad * (transform.eulerAngles.z + 180)), Mathf.Sin (Mathf.Deg2Rad * (transform.eulerAngles.z + 180)), 0);
		} else {
			testing.z = (RotatingSpeed * Time.deltaTime);
			transform.eulerAngles += testing;
			MyMovementVector = new Vector3 (Mathf.Cos (Mathf.Deg2Rad * (transform.eulerAngles.z + 180)), Mathf.Sin (Mathf.Deg2Rad * (transform.eulerAngles.z + 180)), 0);
		}
		transform.position += MyMovementVector.normalized * MovementSpeed * Time.deltaTime;

		if (Vector3.Distance(transform.localPosition, RotatePoints [point]) < 0.1f) {
			point++;
			if (point == HowManyPoints)
				point = 0;
		}
	}
}
