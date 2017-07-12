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


	/*	if (turnRight == false) {
			testing.z = (RotatingSpeed * Time.deltaTime);
			transform.eulerAngles += testing;
			tes = (Quaternion.Euler (0, 0, transform.eulerAngles.z + 180) * MyMovementVector).normalized * MovementSpeed * Time.deltaTime;
			Debug.Log ((Quaternion.Euler (0, 0, transform.eulerAngles.z + 180) * MyMovementVector) + " | " + (Quaternion.Euler (0, 0, transform.eulerAngles.z + 180) * MyMovementVector).normalized);

			transform.position += (Quaternion.Euler (0, 0, transform.eulerAngles.z + 180) * MyMovementVector).normalized * MovementSpeed * Time.deltaTime;
		} else {
			testing.z = (RotatingSpeed * Time.deltaTime);
			transform.eulerAngles -= testing;
			tes =(Quaternion.Euler (0, 0, transform.eulerAngles.z + 180) * MyMovementVector).normalized * MovementSpeed * Time.deltaTime;
			transform.position -= (Quaternion.Euler (0, 0, transform.eulerAngles.z + 180) * MyMovementVector).normalized * MovementSpeed * Time.deltaTime;
		}

		if (angle < 1 && angle > -1) {
			point++;
			if (point == HowManyPoints)
				point = 0;
		}*/




//		transform.eulerAngles += angle * Time.deltaTime * RotatingSpeed;









	}






















/*	public float Width = 1;//how far from center in X-coordinate 
	public float WhenToRotateBottom = 1;//distance from center in Y-coordinates to start curving
	public float WhenToRotateTop = 1;//distance from center in Y-coordinates to start curving

	public Vector2 StartPoint;
	public Vector3 StartDirection;
	public Vector3 Rotates;
	public float RotateSpeed = 10;
	public float StraightSpeed = 10;
	public float currentrotate = 0;
	Vector3 downdegree = new Vector3(0,0,90);
	public bool UpOrDown = false;

	void Start(){
		transform.localPosition = StartPoint;
		currentrotate = transform.eulerAngles.z;
		Rotates = transform.eulerAngles;
	}


	bool curve = false;
	bool leftOrRight = false;

	void Update () {
		if (curve == true) {


			if (leftOrRight == true) {
				Rotates.z += RotateSpeed;
			
				StartDirection.x = ((Width) * Mathf.Cos ((Rotates.z + 90) * 0.017453292519f));
				StartDirection.y = ((Width) * Mathf.Sin ((Rotates.z + 90) * 0.017453292519f)) + WhenToRotateBottom;

				transform.localPosition = StartDirection;
				transform.eulerAngles = Rotates;

				if (Rotates.z >= 270) {
					curve = false;
					UpOrDown = true;
					Rotates.z = -90;
					transform.eulerAngles = downdegree * -1;
					StartDirection.x = Width;
					StartDirection.y = WhenToRotateBottom;
					transform.localPosition = StartDirection;
				}
			} else {
				Rotates.z += RotateSpeed;

				StartDirection.x = ((Width) * Mathf.Cos ((Rotates.z + 90) * 0.017453292519f));
				StartDirection.y = ((Width) * Mathf.Sin ((Rotates.z + 90) * 0.017453292519f)) + WhenToRotateTop;
				transform.localPosition = StartDirection;
				transform.eulerAngles = Rotates;

				if (Rotates.z >= 90) {
					curve = false;
					UpOrDown = false;
					Rotates.z = 90;
					transform.eulerAngles = downdegree;
					StartDirection.x = -Width;
					StartDirection.y = WhenToRotateTop;
					transform.localPosition = StartDirection;
				}
			}

		//	} else {
			
		//	}


		} else {
			if (UpOrDown == false) {
				transform.localPosition += Vector3.down * Time.deltaTime * StraightSpeed;

				if (transform.localPosition.y < WhenToRotateBottom) {
					curve = true;
					leftOrRight = true;
				}

			} else {
				transform.localPosition += Vector3.up * Time.deltaTime * StraightSpeed;

				if (transform.localPosition.y > WhenToRotateTop) {
					curve = true;
					leftOrRight = false;
				}

			}
		}
	}*/
}
