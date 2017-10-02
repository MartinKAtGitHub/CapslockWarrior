using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Less_Then : The_Default_Exit_Behaviour {

	public int HealthLessThen = 2;
	public ObjectStats MyStats;


	public override bool GetBool(int index){
		if (index == 2) {
			if (MyStats.HealthWords < HealthLessThen) {
				return true;
			} else {
				return false;
			}
		} else {
			return base.GetBool (index);
		}
	}


}
