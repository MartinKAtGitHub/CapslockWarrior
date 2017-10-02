using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircleCast : The_Default_Exit_Behaviour  {

	public LayerMask WhatToHit;
	public bool RangeAsModifyer = true;
	public float TheRange = 1;
	public ObjectStats MyStats;


	public override bool GetBool(int index){
		if (index == 2) {

			if (Physics2D.CircleCastAll (transform.position, TheRange * MyStats.AttackRange, Vector2.zero, 0, WhatToHit).Length > 0) {
				return true;
			} else {
				return false;
			}

		} else {
			return base.GetBool (index);
		}
	}


}
