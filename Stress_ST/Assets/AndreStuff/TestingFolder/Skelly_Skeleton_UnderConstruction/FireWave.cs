using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWave : MonoBehaviour {

	Vector3 MyDirection = Vector3.zero;
	float counter = 0;
	public float speed = 1;
	public float whentodrop = 8;
	bool havedroped = false;
	bool Dropping = false;

//	Vector3 EulerAngles = Vector3.zero;
	// Use this for initialization
	void Start () {
		
		MyDirection = Quaternion.Euler (0,0,transform.rotation.eulerAngles.z - 45f) * Vector3.right;

	//	EulerAngles.z = transform.eulerAngles.z;

	//	EulerAngles.z = 25 * Time.deltaTime;
	//	transform.eulerAngles += EulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		counter += Time.deltaTime;

		if (counter > 7) {
			counter = 0;
			havedroped = false;
			Dropping = false;
			GetComponent<Animator> ().SetFloat ("Changeanim", 0);
			transform.localPosition = Vector3.zero;
		}
		if (Dropping == false && counter > whentodrop) {
			Dropping = true;
			GetComponent<Animator> ().SetFloat ("Changeanim", 1);	
		}

		if (Dropping == true && havedroped == false) {
			if (GetComponent<Animator> ().GetBool ("Landed") == true) {
				havedroped = true;
			}
		}

		if (havedroped == false) {
			transform.position += MyDirection * Time.deltaTime * speed;
		}
	
	//	transform.eulerAngles += EulerAngles;
	}
}
