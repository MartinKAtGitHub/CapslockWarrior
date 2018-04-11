using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//For More Info Go To EnemySpell1

public class EnemySpell2 : SpellRoot {

	public LayerMask WhatToNotGoThrough;//If There Is A Wall Between The Object And The Target, Return False
	public float DistanceToTarget = 2.0f;

	public override int RunCriteriaCheck(EnemyManaging objectChecking){//This Return True If All Criteria To Start Is Met.
	
		if (Vector3.Distance (objectChecking.transform.position, objectChecking.Targeting.MyMovementTarget.transform.position) < DistanceToTarget) {
			if (Physics2D.LinecastAll (objectChecking.transform.position, objectChecking.Targeting.MyMovementTarget.transform.position, WhatToNotGoThrough).Length == 0) {
				if(objectChecking.MyAnimatorVariables.AnimatorStage == 0)
					return 2;
			}
			return 0;
		} else {
			return 0;
		}
	}

}