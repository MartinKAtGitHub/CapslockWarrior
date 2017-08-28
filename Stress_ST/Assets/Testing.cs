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
	public float[,] myPos = new float[1,2];
	public float x = 0;
	public float y = 0; 
	public float x1 = 0;
	public float y1 = 0;
	public float test = 0;


	const int MapLowerLeftPosition = -100;

	void Update () {
	
		myPos [0, 0] = transform.position.x;
		myPos [0, 1] = transform.position.y;



		//		x1 = ((myPos [0, 0] / 0.125f) - 0.0625f) - ((myPos [0, 0] / 0.125f) % 1);
		//		y1 = ((myPos [0, 0] / 0.125f) - 0.0625f) - ((myPos [0, 1] / 0.125f) % 1);

	
		x1 = ((transform.position.x - MapLowerLeftPosition) / 0.125f) - (((transform.position.x - MapLowerLeftPosition) / 0.125f) % 1);
		y1 = ((transform.position.y - MapLowerLeftPosition) / 0.125f) - (((transform.position.y - MapLowerLeftPosition) / 0.125f) % 1);


		test = (myPos [0, 0]) % -0.125f + ((myPos [0, 0] / 0.125f) - ((myPos [0, 0] / 0.125f) % 1)); 


	//	x1 = ((myPos [0, 0] / 0.125f)) - ((myPos [0, 0] / 0.125f) % 1);
	//	y1 = ((myPos [0, 1] / 0.125f)) - ((myPos [0, 1] / 0.125f) % 1);

		if (myPos [0, 0] < 0) {
			if ((myPos [0, 0] % 0.25f) < -0.125f) {
				myPos [0, 0] +=	-(myPos [0, 0] % 0.25f) - 0.25f;
			} else {
				myPos [0, 0] +=	-(myPos [0, 0] % 0.25f);
			}
		} else {
			if ((myPos [0, 0] % 0.25f) < 0.125f) {
				myPos [0, 0] +=	-(myPos [0, 0] % 0.25f);
			} else {
				myPos [0, 0] +=	-(myPos [0, 0] % 0.25f) + 0.25f;
			}
		}

		if (myPos [0, 1] < 0) {
			if ((myPos [0, 1] % 0.25f) < -0.125f) {
				myPos [0, 1] +=	-(myPos [0, 1] % 0.25f) - 0.25f;
			} else {
				myPos [0, 1] +=	-(myPos [0, 1] % 0.25f);
			}
		} else {
			if ((myPos [0, 1] % 0.25f) < 0.125f) {
				myPos [0, 1] +=	-(myPos [0, 1] % 0.25f);
			} else {
				myPos [0, 1] +=	-(myPos [0, 1] % 0.25f) + 0.25f;
			}
		}



		x = myPos [0, 0]; 
		y = myPos [0, 1]; 

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
