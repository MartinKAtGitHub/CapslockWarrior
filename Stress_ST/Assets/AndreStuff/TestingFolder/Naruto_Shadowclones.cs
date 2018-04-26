using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Naruto_Shadowclones : The_Default_Bullet {

	float thetime = 0;

	public int SummoningAmount = 4;
	public float SummonedAtOnce = 1;
	public float TimeBetweenSummon = 0.5f;
	public GameObject Clones;

	int summoned = 0;
//	float theTime = 0;

	void Start(){

		thetime = _Shooter.TheTime [0];
		Debug.Log ("HERE");
	}

	void FixedUpdate () {
		if (_Shooter._CreaturePhase > 8)//Quickfix need to look into this problem
			Destroy (gameObject);
		
		if (thetime + TimeBetweenSummon < _Shooter.TheTime [0]) {
			thetime = _Shooter.TheTime [0];
			for (int i = 0; i < SummonedAtOnce; i++) {
				if (_Shooter.ObjectCurrentVector [0].x > 0) {
					Instantiate (Clones, transform.position + new Vector3 (0.25f, 0 - (i * 0.25f), 0), Quaternion.identity);
				} else {
					Instantiate (Clones, transform.position + new Vector3 (-0.25f, 0 - (i * 0.25f), 0), Quaternion.identity);

				}
				summoned++;
				if (summoned >= SummoningAmount) {
					_Shooter.MyAnimator.SetInteger (_Shooter.MyAnimator.GetComponent<TheAnimator> ().AnimatorVariables [1], 2);
					Destroy (gameObject);
				}
			}

		}


	}

}