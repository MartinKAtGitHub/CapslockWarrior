using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_BulletBehaviour : MonoBehaviour {


	FSM_DefaultBehavoirV2.SpellAttackInfo Modifyers;

	public enum HowToBehave { FollowTarget = 0, GoStraight = 1, StandStill = 2 }
	public HowToBehave BulletBehaviour;

	public delegate void FunctionPointerBullets();
	FunctionPointerBullets[] FunctionPointerBullet = new FunctionPointerBullets[3];
	Vector3 target = Vector3.zero;

	int BulletIndex = 0;
	public bool DestroyOnHit = true;

	public void SetDmgModifiers(FSM_DefaultBehavoirV2.SpellAttackInfo modifyers){
		Modifyers = modifyers;
	}

	// Use this for initialization
	void Awake () {
		if (BulletBehaviour == HowToBehave.FollowTarget) {
			FunctionPointerBullet [0] = FollowTheTarget;
			BulletIndex = 0;
		} else if (BulletBehaviour == HowToBehave.GoStraight) {
			FunctionPointerBullet [1] = GoingStraight;
			BulletIndex = 1;
		} else if (BulletBehaviour == HowToBehave.StandStill) {
			FunctionPointerBullet [2] = StandStill;
			BulletIndex = 2;
	}
	}
	
	// Update is called once per frame
	void Update () {
		FunctionPointerBullet [BulletIndex] ();
	}



	void FollowTheTarget(){
		transform.position += (transform.position - target).normalized * Modifyers.AttackMovement * Time.deltaTime;
	}

	void GoingStraight(){
		transform.position += Modifyers.NeededVectors[0] * Modifyers.AttackMovement * Time.deltaTime;
	}

	void StandStill(){
	//	transform.position += new Vector3 (0, 0.01f, 0);
	}

}
