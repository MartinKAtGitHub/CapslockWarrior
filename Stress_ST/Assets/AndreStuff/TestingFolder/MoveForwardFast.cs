using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardFast : MonoBehaviour {

	public float DistanceForward = 1;
	public float TravelSpeed = 1;

//	float traveled = 0;
	public 	Vector3 startpoint = Vector3.zero;
	public bool starting = false;
	public bool StartParticle = false;

	public Vector3 EndPoint = Vector3.zero;

	void Start(){
//		startpoint = transform.position;
//		startpoint = (EndPoint - transform.position).normalized * Time.deltaTime * TravelSpeed;
	//	EndPoint = EndPoint - transform.position;
	}
//	int a = 0;
	// Update is called once per frame
	void Update () {

		if (starting == true) {
			if (StartParticle == false) {
		//		EndPoint = transform.position - EndPoint;
				startpoint = (EndPoint - transform.position).normalized * TravelSpeed;
				StartParticle = true;
				GetComponent<ParticleSystem> ().Play ();
			}
			Debug.Log (Vector3.Distance (transform.position, transform.position + (startpoint * Time.deltaTime)) + " | " + Vector3.Distance (transform.position, EndPoint));
			if (Vector3.Distance (transform.position, transform.position + startpoint) > Vector3.Distance (transform.position, EndPoint)) {
				Debug.Log ("TRUE");
				transform.position = EndPoint;
				GetComponent<ParticleSystem> ().Stop();
			//	starting = false;
			} else {
				transform.position += startpoint * Time.deltaTime;
			}

		}

	/*	transform.position += Vector3.MoveTowards (transform.position, EndPoint);

		if (starting == true) {
			if (StartParticle == false) {
				StartParticle = true;
				GetComponent<ParticleSystem> ().Play ();
			} else {

				if (Vector3.Distance (startpoint, transform.position) < DistanceForward) {



					transform.position =  Vector3.MoveTowards (transform.position, transform.position + (new Vector3 (Mathf.Cos(Mathf.Deg2Rad * transform.parent.eulerAngles.z), Mathf.Sin(Mathf.Deg2Rad * transform.parent.eulerAngles.z), 0).normalized * DistanceForward), (TravelSpeed * Time.deltaTime));
					if(Vector3.Distance (startpoint, transform.position) > DistanceForward){

						transform.position = startpoint + (new Vector3 (Mathf.Cos (Mathf.Deg2Rad * transform.parent.eulerAngles.z), Mathf.Sin (Mathf.Deg2Rad * transform.parent.eulerAngles.z), 0).normalized * DistanceForward);
						
					}

		
		
				} else {
					GetComponent<ParticleSystem> ().Stop();
					starting = false;

					transform.position = startpoint;
				}
			}
		}*/


	}
}
