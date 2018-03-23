using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//These Classes Are Just Refrenced, Nothing Created Just Called By The Creature.
//This Class Is Tailored To A Spell. There Might Be Standard Criteria Which Alot Of Spells Can Use, If Not Then A New Script Needs To Be Made.
//The Only Thing That Happends Here Is A Criteria Check. If True Then The Assigned AnimatorStage-Value Is Set And Spell Logic Starts.

public class EnemySpell1 : SpellRoot {
	
	public override int RunCriteriaCheck(EnemyManaging parent){//This Return True If All Criteria To Start Is Met.

		if (Vector3.Distance (parent.transform.position, parent.Targeting.MyMovementTarget.transform.position) < 0.15f) {
			return 1;
		} else {
			return 0;
		}
	}

}
