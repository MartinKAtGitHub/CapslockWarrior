using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Decide_When_To_Attack : The_Default_Attack_Behaviour {//TODO multiple bullets and bullet switching

	int[] _AnimatorVariables;
	public bool ChangeMovementOnShot = false;

	public override void SetMethod (The_Object_Behaviour myInfo){
		base.SetMethod (myInfo);
		_MyObject = myInfo;
		_MyAnimator = _MyObject.MyAnimator;
		_AnimatorVariables = _MyObject.AnimatorVariables;
	}

	public override void OnEnter (){
		base.OnEnter ();

		for (int i = 0; i < Movement.Length; i++) {
			if(Movement[i].TheResetState == ResetState.ResetOnEnter)
				Movement [i].Reset ();
		}
		if (TheResetState == ResetState.ResetOnEnter) {
			Reset ();
		}
	
		_MyAnimator.SetFloat (_AnimatorVariables[1], AnimatorStageValueOnEnter);//Overrides Movement AnimatorStage OnEnter. But Only In OnEnter
		
	}

	public override void BehaviourUpdate (){
		if (_MyAnimator.GetBool (_AnimatorVariables [2]) == true) {//If Animator Say That I Can Shoot
			Instantiate (Bullet, _MyObject.transform.position + ChangeAttackPositionTo, Quaternion.identity);
			_MyAnimator.SetBool (_AnimatorVariables [2], false);//When Shot, Set Bool To False

			if (ChangeMovementOnShot == true) {
				if (_MovementIndex < Movement.Length) {
					int check = Movement [_MovementIndex].GetInt (2);
					_MovementIndex = check;
					if (_MovementIndex < Movement.Length) {
						Movement [_MovementIndex].OnEnter ();
					} else {
						for (int i = 0; i < Movement.Length; i++) {
							if(Movement[i].TheResetState == ResetState.ResetWhenComplete)
								Movement [i].Reset ();
						}
						if (TheResetState == ResetState.ResetWhenComplete) {
							Reset ();
						}
						_MyObject.SetMovementBehaviour (WhenCompleteChangeToBehaviourIndex);//If All Movement Behaviours In The AttackBehaviour Are Complete, Then The Behaviour Change To The Next
					}
				}
			}
		}

		if (ChangeMovementOnShot == false) {
			if (_MovementIndex < Movement.Length && Movement [_MovementIndex].GetBool (5) == true) {//Check If Movement Is Finished
				int check = Movement [_MovementIndex].GetInt (2);
				if (check != _MovementIndex) {
					_MovementIndex = check;
					if (_MovementIndex < Movement.Length) {
						Movement [_MovementIndex].OnEnter ();
					} else {
						for (int i = 0; i < Movement.Length; i++) {
							if (Movement [i].TheResetState == ResetState.ResetWhenComplete)
								Movement [i].Reset ();
						}
						if (TheResetState == ResetState.ResetWhenComplete) {
							Reset ();
						}
						_MyObject.SetMovementBehaviour (WhenCompleteChangeToBehaviourIndex);//If All Movement Behaviours In The AttackBehaviour Are Complete, Then The Behaviour Change To The Next
					}
				}
			}
		}
	}

	public override void Reset (){
			_MovementIndex = 0;
	}


}
