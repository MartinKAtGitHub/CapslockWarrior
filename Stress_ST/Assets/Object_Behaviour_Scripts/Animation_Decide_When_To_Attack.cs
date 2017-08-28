using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This Script Listens To The Animator For The Shoot Boolean To Be True, Then It Fires The Assigned Bullet x Amount Of Times.
//The "Movement Behaviour" Decides When To Switch Behaviour From The Current AttackBehaviour To The Next Wether It's A AttackBehaviour Or Not
//The Scripts Are Designed To Be As Changeable As Possible. Which Means That Error May Occur If Same Behaviour Is Used Multiple Times Unless The Behaviours Are Setup Carefully
public class Animation_Decide_When_To_Attack : The_Default_Attack_Behaviour {//TODO multiple bullets and bullet switching

	public int[] WhichBulletToShootWhen;
	public bool ChangeMovementOnShot = false;

	int[] _AnimatorVariables;
	int _BulletCounter = 0;

	public override void SetMethod (The_Object_Behaviour myInfo){
		base.SetMethod (myInfo);
		_MyObject = myInfo;
		_MyAnimator = _MyObject.MyAnimator;
		_AnimatorVariables = _MyObject.AnimatorVariables;
	}

	public override void OnEnter (){
		base.OnEnter ();

		if (TheResetState == ResetState.ResetOnEnter) {
			Reset ();
		}
	
		_MyAnimator.SetFloat (_AnimatorVariables[1], AnimatorStageValueOnEnter);//Overrides Movement AnimatorStage OnEnter. But Only In OnEnter
		
	}

	public override void BehaviourUpdate (){
		
		if (_MyAnimator.GetBool (_AnimatorVariables [2]) == true) {//If Animator Say That I Can Shoot
			(Instantiate (Bullets[WhichBulletToShootWhen[_BulletCounter]].Bullets, _MyObject._MyTransform.transform.position + Bullets[0].AttackPosition, Quaternion.identity) as GameObject).GetComponent<The_Default_Bullet> ().SetMethod(Bullets[WhichBulletToShootWhen[_BulletCounter]], _MyObject);
			_BulletCounter++;

			if (_BulletCounter >= Bullets.Length) {
				_BulletCounter = 0;
			}
			_MyAnimator.SetBool (_AnimatorVariables [2], false);//When Shot, Set Bool To False

			if (ChangeMovementOnShot == true) {
				if (Movement [_MovementIndex].TheResetState == ResetState.ResetWhenComplete) {//Reseting MovementBehaviour Used
					Movement [_MovementIndex].Reset ();
				}
				_MovementIndex = Movement [_MovementIndex].GetInt (2);
				if (_MovementIndex < Movement.Length) {
					Movement [_MovementIndex].OnEnter ();
				} else {
					if (TheResetState == ResetState.ResetWhenComplete) {
						Reset ();
					}
					_MyObject.SetMovementBehaviour (WhenCompleteChangeToBehaviourIndex);//If All Movement Behaviours In The AttackBehaviour Are Complete, Then The Behaviour Change To The Next
				}
			}
		}
	
		if (_MovementIndex < Movement.Length && Movement [_MovementIndex].GetBool (5) == true) {//Check If Movement Is Finished
			if (Movement [_MovementIndex].TheResetState == ResetState.ResetWhenComplete) {
				Movement [_MovementIndex].Reset ();
			}
			_MovementIndex = Movement [_MovementIndex].GetInt (2);
			if (_MovementIndex < Movement.Length) {
				Movement [_MovementIndex].OnEnter ();
			} else {
				if (TheResetState == ResetState.ResetWhenComplete) {
					Reset ();
				}
				_MyObject.SetMovementBehaviour (WhenCompleteChangeToBehaviourIndex);//If All Movement Behaviours In The AttackBehaviour Are Complete, Then The Behaviour Change To The Next
			}
		}
	}

	public override void Reset (){
		_MovementIndex = 0;
		_BulletCounter = 0;
	}


}
/*


public class The_Timed_Attack : The_Default_Attack_Behaviour {
	[Space(10)]
	public int TimesToAttack = 10;
	public float AttackSpeed = 1;
	public bool CdAtStart = true;
	public bool IgnoreShootingBool = false;
	public bool ChangeMovementOnShot = false;

	float TimeStarted = 0;
	float[] _TheTime;
	float _ValueWhenLastUpdated = 0;
	int[] _AnimatorVariables;
	int _Counter = 0;

	public override void SetMethod (The_Object_Behaviour myInfo){
		base.SetMethod (myInfo);
		_MyObject = myInfo;
		_TheTime = _MyObject.TheTime;
		_AnimatorVariables = _MyObject.AnimatorVariables;
		_MyAnimator = _MyObject.MyAnimator;
	}

	public override void OnEnter (){
		base.OnEnter ();

		if (TheResetState == ResetState.ResetOnEnter) {
			Reset ();
		} else {
			TimeStarted = _TheTime [0];
		}
	
		if (CdAtStart == false) {
			_ValueWhenLastUpdated = AttackSpeed;
		}

		_MyAnimator.SetFloat (_AnimatorVariables[1], AnimatorStageValueOnEnter);//Overrides Movement AnimatorStage OnEnter. But Only In OnEnter
	}


	public override void BehaviourUpdate (){
		_ValueWhenLastUpdated = _TheTime [0] - TimeStarted;//Time Spent 

		if (_ValueWhenLastUpdated >= AttackSpeed) {
			TimeStarted = _TheTime [0];
			_ValueWhenLastUpdated = 0;
			_Counter++;

			if (IgnoreShootingBool == true) {
				Instantiate (Bullet, _MyObject.transform.position + ChangeAttackPositionTo, Quaternion.identity);

				if (ChangeMovementOnShot == true) {
					if (Movement [_MovementIndex].TheResetState == ResetState.ResetWhenComplete) {
						Movement [_MovementIndex].Reset ();
					}
					_MovementIndex = Movement [_MovementIndex].GetInt (2);
					if (_MovementIndex < Movement.Length) {
						Movement [_MovementIndex].OnEnter ();
					}
				}

				if (_Counter >= TimesToAttack) {//Attack Behaviour Complete -> Reset And Change Behaviour
					if (TheResetState == ResetState.ResetWhenComplete) {
						Reset ();
					}
					_MyObject.SetMovementBehaviour (WhenCompleteChangeToBehaviourIndex);//If The Attack Behaviours Is Complete, Then The Behaviour Change To The Next
					return;
				}
			} else {
				if (_MyAnimator.GetBool (_AnimatorVariables [2]) == true) {//If Animator Say That I Can Shoot
					Instantiate (Bullet, _MyObject.transform.position + ChangeAttackPositionTo, Quaternion.identity);
					_MyAnimator.SetBool (_AnimatorVariables [2], false);//When Shot, Set Bool To False

					if (ChangeMovementOnShot == true) {
						if (Movement [_MovementIndex].TheResetState == ResetState.ResetWhenComplete) {
							Movement [_MovementIndex].Reset ();
						}
						_MovementIndex = Movement [_MovementIndex].GetInt (2);
						if (_MovementIndex < Movement.Length) {
							Movement [_MovementIndex].OnEnter ();
						}
					}

					if (_Counter >= TimesToAttack) {
						if (TheResetState == ResetState.ResetWhenComplete) {
							Reset ();
						}
						_MyObject.SetMovementBehaviour (WhenCompleteChangeToBehaviourIndex);//If All Movement Behaviours In The AttackBehaviour Are Complete, Then The Behaviour Change To The Next
						return;
					}
				}
			}
		}

		if (_MovementIndex < Movement.Length && Movement [_MovementIndex].GetBool (5) == true) {//Check If Movement Is Finished
			Debug.Log("HERE");
			if (Movement [_MovementIndex].TheResetState == ResetState.ResetWhenComplete) {
				Movement [_MovementIndex].Reset ();
			}
			_MovementIndex = Movement [_MovementIndex].GetInt (2);
			if (_MovementIndex < Movement.Length) {
				Movement [_MovementIndex].OnEnter ();
			} else {
				if (TheResetState == ResetState.ResetWhenComplete) {
					Reset ();
				}
				_MyObject.SetMovementBehaviour (WhenCompleteChangeToBehaviourIndex);//If All Movement Behaviours In The AttackBehaviour Are Complete, Then The Behaviour Change To The Next
			}
		}

	}

	public override void Reset (){
		_MovementIndex = 0;
		TimeStarted = _TheTime [0];
		_ValueWhenLastUpdated = 0;
		_Counter = 0;
	}


}



*/