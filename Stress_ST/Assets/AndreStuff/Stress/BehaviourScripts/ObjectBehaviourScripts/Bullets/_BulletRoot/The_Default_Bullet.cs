using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Default_Bullet : MonoBehaviour {

	protected GameManagerTestingWhileWaiting.SpellAttackInfo _SpellInfo;
	protected The_Object_Behaviour _Shooter;


	public virtual void SetMethod (GameManagerTestingWhileWaiting.SpellAttackInfo SpellInfo, The_Object_Behaviour MySender){
		_SpellInfo = SpellInfo;
		_Shooter = MySender;
	}


}
