using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardFast : MonoBehaviour {

	public float DistanceForward = 1;
	public float TravelSpeed = 1;

	float traveled = 0;
	Vector3 startpoint = Vector3.zero;
	public bool starting = false;
	public bool StartParticle = false;

	void Start(){
		startpoint = transform.position;
	}
	int a = 0;
	// Update is called once per frame
	void Update () {

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
		}


	}
}
