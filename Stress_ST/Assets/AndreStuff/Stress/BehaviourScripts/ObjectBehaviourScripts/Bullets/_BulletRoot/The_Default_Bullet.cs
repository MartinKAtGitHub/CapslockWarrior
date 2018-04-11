using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Default_Bullet : MonoBehaviour {

	protected GameManagerTestingWhileWaiting.SpellAttackInfo _SpellInfo;
	protected The_Object_Behaviour _Shooter;
	public int bulletID;

	public virtual void SetMethod (GameManagerTestingWhileWaiting.SpellAttackInfo SpellInfo, The_Object_Behaviour MySender){//Outdated, Only There Cuz Of I Have To Clean Up Alot Of Old Stuff. TODO
		_SpellInfo = SpellInfo;
		_Shooter = MySender;
	}

	public virtual void SetMethod (EnemyManaging manager){

	}


}
