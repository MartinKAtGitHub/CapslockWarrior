using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_Passed_Since_Enter : The_Default_Exit_Behaviour {

	public float TimePassed = 1;
	float StartTime = 0;


	public override void SetMethod (The_Object_Behaviour myTransform){
	
	}

	public override void OnEnter (){
		StartTime = ClockTest.TheTime[0];
	
	}

	public override bool GetBool(int index){
		if (index == 2) {
			if (StartTime + TimePassed <= ClockTest.TheTime[0]) {
				return true;
			} else {
				return false;
			}
		} else {
			return base.GetBool (index);
		}
	}


}
