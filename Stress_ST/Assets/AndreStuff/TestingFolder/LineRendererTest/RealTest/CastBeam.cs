using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastBeam : MonoBehaviour {

	public Transform LineSource;
	LineRenderer Line;

	float counter;
	float dist;

	public float lineDrawSpeed = 6f;

	public Material Material0;
	public Material Material1;
	public Material Material2;
	public Material Material3;

	public float changematerialtime = 0.1f;
	float counterr = 0;
	int materialcount;
	public bool ropeorlightning = false;
	float x;

	// Use this for initialization
	void Start () {

		Line = GetComponent<LineRenderer> ();
		Line.SetPosition (0, LineSource.position);
		Line.SetPosition (1, StartVector);

		dist = Vector3.Distance (LineSource.position, StartVector);
	}

	public Vector3 StartVector = Vector3.right;
	RaycastHit2D[] LineChecker;
	public Vector3 CollisionPoint = Vector3.zero;
	public LayerMask WhatCanIHit;
	public GameObject EndOfBeam;
	public Vector3 testing = new Vector3(0,0,90);
	public float RotatingSpeed = 0;
	public float LineLength = 1;
	void Update () {

		counterr += Time.deltaTime;
		if (changematerialtime < counterr) {
			materialcount++;
			counterr = 0;

			if (materialcount == 0) {
				Line.material = Material0;
			} else if (materialcount == 1) {
				Line.material = Material1;
			} else if (materialcount == 2) {
				Line.material = Material2;
			} else if (materialcount == 3) {
				Line.material = Material3;
				materialcount = 0;
			} 
		}

		LineChecker = Physics2D.LinecastAll (LineSource.position, LineSource.position + (StartVector), WhatCanIHit);
		if (LineChecker.Length > 0) {
			
			CollisionPoint = LineChecker [0].point;
			EndOfBeam.gameObject.SetActive (true);
			EndOfBeam.transform.position = CollisionPoint;
		} else {
			CollisionPoint = LineSource.position + StartVector;
			EndOfBeam.gameObject.SetActive (false);
		}
	

		Line.SetPosition (1, CollisionPoint);
		Line.SetPosition (0, LineSource.position);

		StartVector = (Quaternion.Euler(0,0, (testing.z = RotatingSpeed * Time.deltaTime)) * StartVector);
	}
}
