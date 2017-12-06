using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Target_LineCast : The_Default_Exit_Behaviour  {

	public LayerMask WhatToHit;
	public ObjectAI MyStats;
	public bool IfTrueOrFalseChange = true;

	public override bool GetBool(int index){
		if (index == 2) {

			if (Vector2.Distance (transform.position, MyStats.TheObject._TheTarget.transform.position) < MyStats.AttackRange) {
				if (Physics2D.Linecast (transform.position, MyStats.TheObject._TheTarget.transform.position, WhatToHit).collider != null) {
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
			}

			if (IfTrueOrFalseChange == true) 
				return true;

			return false; 
		} else {
			return base.GetBool (index);
		}
	}


}