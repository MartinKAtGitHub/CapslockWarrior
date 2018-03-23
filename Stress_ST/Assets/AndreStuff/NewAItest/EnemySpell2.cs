using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//For More Info Go EnemySpell1
public class EnemySpell2 : SpellRoot {

	int AnimatorStage = 2;
	public int test2 = 0;


	public override int RunCriteriaCheck(EnemyManaging parent){

		if (test2 == 20) {
			return AnimatorStage;
		} else {
			return 0;
		}
	}

}
