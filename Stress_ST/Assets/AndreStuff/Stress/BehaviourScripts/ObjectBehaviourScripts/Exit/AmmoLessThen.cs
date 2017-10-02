using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoLessThen : The_Default_Exit_Behaviour {

	public int TheAmmoLessThen = 1;
	public ObjectStats MyStats;


	public override bool GetBool(int index){
		if (index == 2) {
			if (MyStats.Ammo < TheAmmoLessThen) {
				return true;
			} else {
				return false;
			}
		} else {
			return base.GetBool (index);
		}
	}


}
