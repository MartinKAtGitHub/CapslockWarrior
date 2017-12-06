using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircleCast : The_Default_Exit_Behaviour  {

	public LayerMask WhatToHit;
	public ObjectStats MyStats;
	public bool IfTrueOrFalseChange = true;

	public override bool GetBool(int index){
		if (index == 2) {
			
			if (Physics2D.CircleCastAll (transform.position, MyStats.AttackRange, Vector2.zero, 0, WhatToHit).Length > 0) {
				if (IfTrueOrFalseChange == true) {
					return true;
				} else {
					return false;
				}
			} else {
				if (IfTrueOrFalseChange == false) {
					return true;
				} else {
					return false;
				}
			}

		} else {
			return base.GetBool (index);
		}
	}


}
