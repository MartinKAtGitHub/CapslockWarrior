using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//For More Info Go To EnemySpell1

//Spell Index Values. 
//0 - Damage
//1 - TeleportDistToTarget


public class EnemySpell3 : SpellRoot {

	public int CheckAgainsAnimatorState = 3;

	int theX = 0;
	int theY = 0;
	byte iterations = 0; 
	public int AnimatorStateCheck = 0;

	public LayerMask WhatNotToHit;
	 
	Vector3 test = Vector3.zero;
	RaycastHit2D[] hitted;

	public float TeleportDistance = 2;

	int rngtries = 4;
	bool foundIt = false;

	public override int RunCriteriaCheck(EnemyManaging objectChecking){//This Return True If All Criteria To Start Is Met. TODO Setup The New Collision System

		if (objectChecking.MyAnimatorVariables.AnimatorStage == AnimatorStateCheck) {
			return 3;
		}

		return 0;






	/*	foundIt = false;

		for (int i = 0; i < rngtries; i++) {
			test = (Quaternion.Euler (0, 0, Random.Range (1, rngtries + 1) * Random.Range (0, 90)) * (Vector3.right * TeleportDistance));//Deviding The Circle In 4, Then I Make Choose A RNG Side To Check

			hitted = Physics2D.LinecastAll (objectChecking.Targeting.MyMovementTarget.transform.position + test, objectChecking.Targeting.MyMovementTarget.transform.position, WhatNotToHit);
			if (hitted.Length > 0) {
		//		Debug.Log ("Hit Something " + hitted [hitted.Length - 1].point.x + " | " + hitted [hitted.Length - 1].point.y + " | " + test.normalized);
			} else {
				foundIt = true;
		//		Debug.Log ("No Wall Found");
				test = objectChecking.Targeting.MyMovementTarget.transform.position + test;
				i = rngtries;
			}

		}

		if (foundIt == false) {
			test = (Quaternion.Euler (0, 0, Random.Range (1, rngtries + 1) * Random.Range (0, 90)) * (Vector3.right * TeleportDistance));
			hitted = Physics2D.LinecastAll (objectChecking.Targeting.MyMovementTarget.transform.position + test, objectChecking.Targeting.MyMovementTarget.transform.position, WhatNotToHit);
			if (hitted.Length > 0) {
				hitted = Physics2D.LinecastAll (objectChecking.Targeting.MyMovementTarget.transform.position, (Vector3)hitted [hitted.Length - 1].point, WhatNotToHit);
			//	Debug.Log ( "Oposite Side Of Wall ");
				test = (Vector3)(hitted [0].point) - (test.normalized * 0.05f);

			} else {
			//	Debug.Log ("No New Wall Found");
				test = objectChecking.Targeting.MyMovementTarget.transform.position + test;
			}
		}

		objectChecking.transform.position = test;*/
	//	Debug.Log (test);
	

//		objectChecking.transform.eulerAngles = (Quaternion.Euler(0,0,Random.Range(0, 360) * Vector3.right));



			/*

	
		if (objectChecking.MyAnimatorVariables.AnimatorStage == AnimatorStateCheck) {
			return 0;
		}
		
		//If Nothing Collides With The Object

		if (objectChecking.MyNodeInfo.MyAStar._WalkCost.ValidPositions (objectChecking.Node.MyCollisionInfo.XNode, objectChecking.Node.MyCollisionInfo.YNode) == false)
			return 0;

		theX = objectChecking.MyNodeInfo.MyAStar._WalkCost.GetXPos (objectChecking.Node.MyCollisionInfo.XNode);
		theY = objectChecking.MyNodeInfo.MyAStar._WalkCost.GetYPos (objectChecking.Node.MyCollisionInfo.YNode);
	
		if (objectChecking.MyNodeInfo.MyAStar._WalkCost.BaseGroundLayer [theX, theY] == 1) {//If ID == Wall, Return 'False'.
			return 0;
		}

		iterations = objectChecking.MyNodeInfo.MyAStar._WalkCost.CollisionAmount [theX, theY];

		for (int i = 0; i < iterations; i++) {//Iterating Through The Taken Collision IDs
			if (objectChecking.MyNodeInfo.MyAStar._WalkCost.CollisionMap [theX, theY, i].NodesCollisionID == 1) {//If ID == Wall, Return 'False'.
				return 0;
			}
		}

		if (objectChecking.MyAnimatorVariables.AnimatorStage != 0)
			return 0;

			
		return 3;//If Nothing Failed, Return 'True'.
*/
			return 0;
	}

}