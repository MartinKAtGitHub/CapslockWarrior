using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoMoreThen : The_Default_Exit_Behaviour {

	public int TheAmmoMoreThen = 1;
	public ObjectStats MyStats;


	public override bool GetBool(int index){
		if (index == 2) {
			if (MyStats.Ammo > TheAmmoMoreThen) {
				return true;
			} else {
				return false;
			}
		} else {
			return base.GetBool (index);
		}
	}


}
