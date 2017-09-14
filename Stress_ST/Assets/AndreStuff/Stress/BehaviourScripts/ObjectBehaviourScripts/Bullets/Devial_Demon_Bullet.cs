using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devial_Demon_Bullet : The_Default_Bullet {

	public Vector2 TheCapsuleSize = Vector2.zero;
	public LayerMask WhatCanIHit;
	

	public override void SetMethod (GameManagerTestingWhileWaiting.SpellAttackInfo SpellInfo, The_Object_Behaviour MySender){
		base.SetMethod (SpellInfo, MySender);

		if (_Shooter.MyAnimator.transform.eulerAngles.y == 0) {
			transform.position = (Quaternion.AngleAxis (_Shooter.MyAnimator.transform.eulerAngles.z, Vector3.forward) * _SpellInfo.AttackPosition) + MySender._MyTransform.position;
		} else {
			transform.position = (Quaternion.AngleAxis (_Shooter.MyAnimator.transform.eulerAngles.z, Vector3.back) * _SpellInfo.AttackPosition) + MySender._MyTransform.position;
		}
		transform.rotation = Quaternion.Euler (_Shooter.MyAnimator.transform.eulerAngles);


		

	}

	void FixedUpdate(){
		TheCapsuleSize.x *= 2;//Width need to be multiplyed with 2 to get the diameter correct

		foreach (RaycastHit2D s in Physics2D.CapsuleCastAll (transform.position, TheCapsuleSize, CapsuleDirection2D.Vertical, 0, Vector2.zero, 0, WhatCanIHit)) {//Capsule size is acting weird. the y value is the radius of the height, but the x value is the diameter of the width?????? WHAT why......
			s.transform.GetComponent<DefaultBehaviour>().RecievedDmg(Mathf.RoundToInt(_SpellInfo.DamageMultiplyer + _Shooter._TheObject.Dmg[0]));
		}
		Destroy (gameObject);

	}


}
