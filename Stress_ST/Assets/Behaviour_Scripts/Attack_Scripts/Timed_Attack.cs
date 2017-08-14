using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timed_Attack : Default_Attack_Behaviour {

	//public float WhenAttackedChangeAnimatorTo = 0;
	public GameObject Bullet;

	[Tooltip("Movement Can Override The Time To Exit. And Times Taht The Object Will Attack. So If You Want It To Shoot Infinitely, Just Choose A High Number")]
	public int TimesToAttack = 10;
	public float AttackSpeed = 1;

	Object_Behaviour MyObject;
	float[] _TimeCounter;
	float _TimeCheck = 0;
	float _AttackAmountChecker = 0;

	public override void SetMethod (Object_Behaviour myTransform, Transform targetTransform, int[] AnimatorValues){
		MyObject = myTransform;
		_AnimatorVariables = AnimatorValues;
		_TimeCounter = MyObject.GetTheTime ();
	}

	public override void OnEnter (){
		_TimeCheck = _TimeCounter [0] + AttackSpeed;
	}

	public override void Reset (){
		_AttackAmountChecker = 0;
	}

	public override void BehaviourMethod (){
		 
		if (_TimeCounter [0] >= _TimeCheck) {
			_TimeCheck = _TimeCounter [0] + AttackSpeed;
			if (_AttackAmountChecker < TimesToAttack) {
				_AttackAmountChecker++;
				Instantiate (Bullet, MyObject.transform.position + ChangeAttackPositionTo, Quaternion.identity);
			}
		}
	}


}