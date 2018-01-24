using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTestChanger : MonoBehaviour {

	public int StartRate = 0;
	public float StartSize = 0;
	public float StartRadius = 0;

	public int EndRate = 1;
	public float EndSize = 1;
	public float EndRadius = 1;

	public float TimeToFull = 1;
	public float TimeFull = 1;
	public float TimeRetract = 1;

	public float Timer = 0;
	public float Delayer = 1;
	ParticleSystem.EmissionModule test;
	ParticleSystem.ShapeModule test2;
	ParticleSystem.MainModule test3;

	void Start(){
		test = GetComponent<ParticleSystem> ().emission;
		test2 = GetComponent<ParticleSystem> ().shape;
		test3 = GetComponent<ParticleSystem> ().main;
	}

	// Update is called once per frame
	void Update () {

		if (Timer <= TimeToFull) {
		
			if (test.rateOverTimeMultiplier <= EndRate) {
				test.rateOverTimeMultiplier += ((EndRate - StartRate) * Time.deltaTime / Delayer);
			}

			if (test2.radius <= EndRadius) {
				test2.radius += ((EndRadius - StartRadius) * Time.deltaTime / Delayer);
			}

			if (test3.startSizeMultiplier <= EndSize) {
				test3.startSizeMultiplier += ((EndSize - StartSize) * Time.deltaTime / Delayer);
			}

		/*	if (GetComponent<ParticleSystem> ().emission.rateOverTime.constant <= EndRate) {
				GetComponent<ParticleSystem> ().emission.rateOverTime.constant = StartRate + ((EndRate - StartRate) * Time.deltaTime);
			}*/

		}  else if (Timer <= TimeFull) {
			
		}  else if (Timer <= TimeRetract) {
			if (test.rateOverTimeMultiplier >= 0) {
				test.rateOverTimeMultiplier -= ((EndRate - StartRate) * Time.deltaTime / (Delayer / 2));
			}

			if (test2.radius >= 0) {
				test2.radius -= ((EndRadius - StartRadius) * Time.deltaTime / (Delayer / 2));
			}

			if (test3.startSizeMultiplier >= 0) {
				test3.startSizeMultiplier -= ((EndSize - StartSize) * Time.deltaTime / (Delayer / 2));
			}
		} else {
			Destroy (transform.parent.gameObject);
		}








		Timer += Time.deltaTime;

	}
}
