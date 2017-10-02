using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skelly_Egg_Slow : The_Default_Bullet {

	public float MovementDecrease = 0;
	GameObject[] _Targets;

	public override void SetMethod (GameManagerTestingWhileWaiting.SpellAttackInfo SpellInfo, The_Object_Behaviour MySender){
		base.SetMethod (SpellInfo, MySender);
		_Targets = GameObject.FindGameObjectsWithTag ("Player1");
		for (int i = 0; i < _Targets.Length; i++)
			_Targets [i].GetComponent<PlayerManager> ().MovementSpeedChange (MovementDecrease );
		
	}

	void FixedUpdate(){
		if (_Shooter == null) {
			for (int i = 0; i < _Targets.Length; i++)
				_Targets [i].GetComponent<PlayerManager> ().MovementSpeedChange (-MovementDecrease);
			Destroy (gameObject);
		} 
	}


}
