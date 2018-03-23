using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClockTest : MonoBehaviour {

/*  Profiler Numbers
 	a = ClockTest.TheTimes; //is slightly ahead of arrays[0]

	a = ClockTest.TheTime[0];//is massively ahead of gettime()[0]; and time.time;
	a = _TheTime[0];

	a = TestTheTime.GetTime () [0];//the profiler tells me that from the ways above (with 225fps) then this is faaaaaar below what they use (with 110 fps). im not sure if this is because of the profiler recording the method or not but the difference is there
	a = Time.time;
*/

	public static float[] TheTime = new float[1];//going to remove this TODO
	public static float TheTimes = 0;//This will be the new time TODO
	public int RoomPathsCount = 100;

	// Update is called once per frame
	void Update () {
		TheTime[0] += Time.deltaTime;
		TheTimes = TheTime [0];
	}

	public float[] GetTime(){
		if(TheTime.Length == 0)
			TheTime = new float[1];
		return TheTime;
	}
}
