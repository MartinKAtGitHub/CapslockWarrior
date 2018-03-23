using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devial_Demon_Bullet : The_Default_Bullet {//TODO Add new logic

	public Vector2 TheCapsuleSize = Vector2.zero;
	public LayerMask WhatCanIHit;
	public int spellDmg = 1;

	EnemyManaging me;
	string targetTag = "";

	public override void SetMethod (GameManagerTestingWhileWaiting.SpellAttackInfo SpellInfo, The_Object_Behaviour MySender){//Setting Rotation Of THis Object.
		base.SetMethod (SpellInfo, MySender);

		if (_Shooter.MyAnimator.transform.eulerAngles.y == 0) {
			transform.position = (Quaternion.AngleAxis (_Shooter.MyAnimator.transform.eulerAngles.z, Vector3.forward) * _SpellInfo.AttackPosition) + MySender._MyTransform.position;
		} else {
			transform.position = (Quaternion.AngleAxis (_Shooter.MyAnimator.transform.eulerAngles.z, Vector3.back) * _SpellInfo.AttackPosition) + MySender._MyTransform.position;
		}
		transform.rotation = Quaternion.Euler (_Shooter.MyAnimator.transform.eulerAngles);

	}

	public override void SetMethod (EnemyManaging manager){
		me = manager;
		targetTag = manager.Targeting.MyMovementTarget.tag.ToString();
	/*	if (_Shooter.MyAnimator.transform.eulerAngles.y == 0) {
			transform.position = (Quaternion.AngleAxis (manager.MyAnimatorVariables.transform.eulerAngles.z, Vector3.forward) * _SpellInfo.AttackPosition) + manager.transform.position;
		} else {
			transform.position = (Quaternion.AngleAxis (manager.MyAnimatorVariables.transform.eulerAngles.z, Vector3.back) * _SpellInfo.AttackPosition) + manager.transform.position;
		}
		transform.rotation = Quaternion.Euler (_Shooter.MyAnimator.transform.eulerAngles);*/

	}


	void FixedUpdate(){

		foreach (RaycastHit2D s in Physics2D.CapsuleCastAll (transform.position, TheCapsuleSize, CapsuleDirection2D.Vertical, 0, Vector2.zero, 0, WhatCanIHit)) {//Capsule size is acting weird. the y value is the radius of the height, but the x value is the diameter of the width?????? WHAT why......
			if(s.transform.CompareTag(targetTag)){
				s.transform.GetComponent<CreatureRoot>().TookDmg(spellDmg);
			}
			
		}

		Destroy (gameObject);

	}


}
