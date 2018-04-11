using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
//The Spell Criteria Script Is Just Refrenced. Meaning That Nothing Is Saved Only Cehecked Upon, And Saved On 1 Object Only.
//This Script Is Tailored To A Specific Spell. So If An Object Need Something Different Then The Criteria That Exist Then A New Criteria Script Need To Be Made.

//Only RunCriteriaCheck Is Called Upon. So If There Is A Need For A Move Complicated Check, Feel Free To Create Methods.
//If The Check Is True Then The A Number Is Returned Which Is The Number Of the Spell (The Number Needs To Be The EnemySpell'x').

//Warning :: AnimatorStage 1000-1010 Is Used For Different Checks Within The Animator. So If/When We Get Spells Exceeding 999, Then Add +10 To The Spell Number. ::;
*/

public class EnemySpell1 : SpellRoot {//If Within Distance To The Target, Return 'True'

	public float DistanceToTarget = 0.15f;

	public override int RunCriteriaCheck(EnemyManaging objectChecking){//This Return 'True' (Which Is AnimatorState Number) If All Criteria To Start Is Met.

		if (Vector3.Distance (objectChecking.transform.position, objectChecking.Targeting.MyMovementTarget.transform.position) < DistanceToTarget) {
			if (objectChecking.MyAnimatorVariables.AnimatorStage == 0) {
				return 1;
			} else {
				return 0;
			}
		} else {
			return 0;
		}
	}

}
