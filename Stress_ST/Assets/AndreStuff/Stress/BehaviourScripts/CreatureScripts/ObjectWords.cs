using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWords : ObjectAI {

	public EnemyWordChecker MyWord;


	public override void HealthWordChange(int _Damage){
		if (_Damage > 0)
			MyWord.HealthIncrease (_Damage);
		else
			MyWord.HealthDecrease (-_Damage);

	}

	public override void OnDestroyed(){
		MyWord.RemoveEvent ();

	}


}
