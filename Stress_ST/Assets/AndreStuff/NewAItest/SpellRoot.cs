using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This Is Just The Root Script Which All Spells Inherit From So That The Creation Of New Spells Is Easier.
//For More Info Go To EnemySpell1, For A "How To".

public class SpellRoot : MonoBehaviour {

	public virtual int RunCriteriaCheck(EnemyManaging objectChecking){

		return 0;
	}

}
