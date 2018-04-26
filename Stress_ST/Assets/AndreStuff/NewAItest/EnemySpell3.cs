using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//For More Info Go To EnemySpell1

//Spell Index Values. 
//0 - Damage
//1 - TeleportDistToTarget


public class EnemySpell3 : SpellRoot {

	public int CheckAgainsAnimatorState = 3;

//	int theX = 0;
//	int theY = 0;
//	byte iterations = 0; 
	public int AnimatorStateCheck = 0;

	public LayerMask WhatNotToHit;
	 
	Vector3 test = Vector3.zero;
	RaycastHit2D[] hitted;

	public float TeleportDistance = 2;

	int rngtries = 4;
	bool foundIt = false;

	public override int RunCriteriaCheck(CreatureRoot objectChecking){//This Return True If All Criteria To Start Is Met. TODO Setup The New Collision System


		if (objectChecking.GetAnimatorVariables ().AnimatorStage == AnimatorStateCheck) {
			return 0;
		
		} else {


			foundIt = false;

			for (int i = 0; i < rngtries; i++) {//Going Through To Check If There Are Any Spots Around Target That Isnt busy/taken
				test = (Quaternion.Euler (0, 0, Random.Range (1, rngtries + 1) * Random.Range (0, 90)) * (Vector3.right * TeleportDistance));//Deviding The Circle In 4, Then I Make Choose A RNG Side To Check
				hitted = Physics2D.LinecastAll (objectChecking.GetWhatToTarget ().MyMovementTarget.transform.position + test, objectChecking.GetWhatToTarget ().MyMovementTarget.transform.position, WhatNotToHit);

				if (hitted.Length == 0) {//Didnt Hit Anything
					foundIt = true;
					objectChecking.transform.position = objectChecking.GetWhatToTarget ().MyMovementTarget.transform.position + test;
					i = rngtries;
				} 

			}

			if (foundIt == false) {//If It Still Is busy/taken Then Search Through Another Time And Go As Close To The Target As Possible
				test = (Quaternion.Euler (0, 0, Random.Range (1, rngtries + 1) * Random.Range (0, 90)) * (Vector3.right * TeleportDistance));
				hitted = Physics2D.LinecastAll (objectChecking.GetWhatToTarget ().MyMovementTarget.transform.position + test, objectChecking.GetWhatToTarget ().MyMovementTarget.transform.position, WhatNotToHit);

				if (hitted.Length > 0) {//Hit Something
					hitted = Physics2D.LinecastAll (objectChecking.GetWhatToTarget ().MyMovementTarget.transform.position, (Vector3)hitted [hitted.Length - 1].point, WhatNotToHit);
					objectChecking.transform.position = (Vector3)(hitted [0].point) - (test.normalized * 0.05f);
				} else {//Didnt Hit Anything
					objectChecking.transform.position = objectChecking.GetWhatToTarget ().MyMovementTarget.transform.position + test;
				}
			}
	

		
			/*
			//If Nothing Collides With The Object

			if (objectChecking.GetNodeInfo ().MyAStar._WalkCost.ValidPositions (objectChecking.GetObjectNodeInfo ().MyCollisionInfo.XNode, objectChecking.GetObjectNodeInfo ().MyCollisionInfo.YNode) == false)
				return 0;

			theX = objectChecking.GetNodeInfo ().MyAStar._WalkCost.GetXPos (objectChecking.GetObjectNodeInfo ().MyCollisionInfo.XNode);
			theY = objectChecking.GetNodeInfo ().MyAStar._WalkCost.GetYPos (objectChecking.GetObjectNodeInfo ().MyCollisionInfo.YNode);
	
			if (objectChecking.GetNodeInfo ().MyAStar._WalkCost.BaseGroundLayer [theX, theY] == 1) {//If ID == Wall, Return 'False'.
				return 0;
			}

			iterations = objectChecking.GetNodeInfo ().MyAStar._WalkCost.CollisionAmount [theX, theY];

			for (int i = 0; i < iterations; i++) {//Iterating Through The Taken Collision IDs
				if (objectChecking.GetNodeInfo ().MyAStar._WalkCost.CollisionMap [theX, theY, i].NodesCollisionID == 1) {//If ID == Wall, Return 'False'.
					return 0;
				}
			}

			if (objectChecking.GetAnimatorVariables ().AnimatorStage != 0)
				return 0;
			*/
		}
			
		return 3;//If Nothing Failed, Return 'True'.

	}

}