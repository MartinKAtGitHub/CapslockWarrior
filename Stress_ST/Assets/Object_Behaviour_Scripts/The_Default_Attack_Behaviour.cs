using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Default_Attack_Behaviour : The_Default_Behaviour {

	public int WhenCompleteChangeToBehaviourIndex = 0;

	[Space(10)]
	public The_Default_Behaviour[] Movement;

	[Header("Attack Values")]
	public GameManagerTestingWhileWaiting.SpellAttackInfo[] Bullets;

	public float AnimatorStageValueOnEnter = 0;
	

	protected int _MovementIndex = 0;
	protected Animator _MyAnimator;

	public override void SetMethod (The_Object_Behaviour myInfo){
		for (int i = 0; i < Movement.Length; i++) {
			Movement [i].SetMethod (myInfo);
		}
	}

	public override void OnEnter (){
		Movement [_MovementIndex].OnEnter ();
	}
}
