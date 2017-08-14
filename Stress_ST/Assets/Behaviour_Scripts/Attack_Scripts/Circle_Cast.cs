using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle_Cast : Default_Attack_Behaviour {

	public float DamageMultiplyer = 1;
	public float Radius = 1;
	public LayerMask WhatCanIHit;

	Animator MyAnim;
	Object_Behaviour MyObject;

	public override void SetMethod (Object_Behaviour myTransform, Transform targetTransform, int[] AnimatorValues){
		MyObject = myTransform;
		MyAnim = myTransform.MyAnimator;
		_AnimatorVariables = AnimatorValues;
	}

	public override void BehaviourMethod (){
		if (MyAnim.GetBool (_AnimatorVariables[2]) == true) {
			
			Collider2D[] SavedCast = Physics2D.OverlapCircleAll (MyObject.transform.position + ChangeAttackPositionTo, Radius , WhatCanIHit);
			if (SavedCast.Length > 0) {
				for (int i = 0; i < SavedCast.Length; i++) {
					Debug.Log (SavedCast [i].transform.name + " Took DMG " + (MyObject.Damage * DamageMultiplyer));
				}
			}

			MyAnim.SetBool (_AnimatorVariables[2], false);
			MyAnim.SetFloat (_AnimatorVariables[1], WhenCompleteChangeAnimatorTo);
		}
	}
}