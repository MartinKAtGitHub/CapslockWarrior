using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClockTest : MonoBehaviour {

	public float[] TheTime = new float[1];

	// Update is called once per frame
	void Update () {
		TheTime[0] += Time.deltaTime;
	}

	public float[] GetTime(){
		if(TheTime.Length == 0)
			TheTime = new float[1];
		return TheTime;
	}
}
