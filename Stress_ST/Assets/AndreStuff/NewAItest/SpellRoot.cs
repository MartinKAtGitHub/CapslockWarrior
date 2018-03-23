using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This Class Is Just The Root Class To Be Able To Create New Spells And Just Override And "New" Spells Are Made.
//For Specific Info About How To Use This, Go To EnemySpell1.
public class SpellRoot : MonoBehaviour {

	protected EnemyManaging _MyParent;


	public virtual int RunCriteriaCheck(EnemyManaging parent){

		return 0;
	}

}
