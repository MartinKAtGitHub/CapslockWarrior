using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Desides_Attack : Default_Attack_Behaviour {

	public GameObject Bullet;

	Animator MyAnim;
	Object_Behaviour MyObject;

	public override void SetMethod (Object_Behaviour myTransform, Transform targetTransform, int[] AnimatorValues){
		MyObject = myTransform;
		MyAnim = myTransform.MyAnimator;
		_AnimatorVariables = AnimatorValues;
	}

	public override void BehaviourMethod (){
		if (MyAnim.GetBool (_AnimatorVariables[2]) == true) {

			Instantiate (Bullet, MyObject.transform.position + ChangeAttackPositionTo, Quaternion.identity);

			MyAnim.SetBool (_AnimatorVariables[2], false);
			MyAnim.SetFloat (_AnimatorVariables[1], WhenCompleteChangeAnimatorTo);
		}
	}
	 

}