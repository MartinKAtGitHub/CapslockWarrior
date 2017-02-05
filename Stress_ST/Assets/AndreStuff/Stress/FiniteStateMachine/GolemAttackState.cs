using UnityEngine;
using System.Collections;

public class GolemAttackState : DefaultState {
	DefaultBehaviour _TargetInfo;
	DefaultBehaviour _MyInfo;

	Transform _MyTransform;

	Rigidbody2D _MyRigidbody2D;
//	bool[] CanIRanged;//TODO DONT DELETE

	Animator _MyAnimator;
	ShootingAfterAnimation _EndOfShootingAnimation;

	bool AnimationStarted = false;

	public GolemAttackState(CreatureOneBehaviour myInfo, bool[] canIRanged, float theRange) {//giving copies of info to this class
		Id = "AttackState";
		_MyInfo = myInfo;
		_MyTransform = _MyInfo.transform;
		_MyRigidbody2D = _MyInfo.GetComponent<Rigidbody2D> ();
		//		CanIRanged = canIRanged;//TODO DONT DELETE

		_MyAnimator = myInfo.transform.FindChild("GFX").GetComponent<Animator> ();
		_EndOfShootingAnimation = _MyAnimator.GetBehaviour<ShootingAfterAnimation> ();

		_LineOfSight = 1 << LayerMask.NameToLayer ("Walls");

	
		_Range = theRange;

	}//for liten scale

	public override string EnterState() {//When it switches to this state this is the first thing thats being called

		if (_MyInfo._GoAfter == null) {//having a failsafe here, so if i dont have a target, ill switch state to something that can search
			return _ReturnState; //TODO TODO "TargetSearch";
		}else{
			if (_TargetInfo != _MyInfo.GetTargetBehaviour ()) {//if this target isnt the same as what im after, change it
				_TargetInfo = _MyInfo.GetTargetBehaviour ();
			}
		}
		AnimationStarted = false;
		_ReturnState = "";
		return _ReturnState;
	}


	public override string ProcessState() {//this is called every frame
		if (AnimationStarted == false) {
			_MyAnimator.SetFloat ("ChangeAnimation", 5);
			AnimationStarted = true;
		} else {
			if (_EndOfShootingAnimation.ShootingAnimationFinished == true) {
				_EndOfShootingAnimation.ShootingAnimationFinished = false;
				_MyAnimator.SetFloat ("ChangeAnimation", 0);
				AnimationStarted = false;
				if (Vector2.Distance ((Vector2)_MyTransform.position, _TargetInfo.GetMyPositionVector2 ()) < _Range) {//checking if im withing range of the target 
					//ReturnState = "WalkToTargetState";
				 
					if (Physics2D.Linecast ((Vector2)_MyTransform.position, _TargetInfo.GetMyPositionVector2 (), _LineOfSight).transform == null) {//if im in range do a raycast and see if there is an obsacle in the way
						_MyInfo.AttackTarget ();
					} else {
						_ReturnState = "WalkToTargetState";
					}
				} else {
					//TODO Movement Delay
					_ReturnState = "WalkToTargetState";
				}
			}

		}

		_MyRigidbody2D.velocity = new Vector2(0,0);
	
		return _ReturnState;
	}

	public override void ExitState(){//when i want to switch to another state this is called before the enterstate of the next state

	}

}
