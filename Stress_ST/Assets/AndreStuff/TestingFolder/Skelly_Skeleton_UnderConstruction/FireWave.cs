using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWave : MonoBehaviour {

	Vector3 MyDirection = Vector3.zero;
	float counter = 0;
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
			transform.localPosition = Vector3.zero;
		}
			
	//	transform.eulerAngles += EulerAngles;
		transform.position += MyDirection * Time.deltaTime;
	}
}
