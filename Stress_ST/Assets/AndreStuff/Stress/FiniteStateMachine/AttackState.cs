using UnityEngine;
using System.Collections;

public class AttackState : DefaultState {
	DefaultBehaviour _TargetInfo;
	CreatureBehaviour _MyInfo;

	Transform _MyTransform;

	Rigidbody2D _MyRigidbody2D;
	bool[] CanIRanged;//TODO DONT DELETE

	Animator _MyAnimator;

	OnlyShootAfterAnimation ShootingAnimation;
	OnlyQuitAfterAnimation QuittingAnimation;

	bool AnimationStarted = false;
	Vector2 targetPos = Vector2.zero;
	Vector3 targetPoss;

	public AttackState(CreatureBehaviour myInfo, bool[] canIRanged, float theRange, LayerMask lineOfSight) {//giving copies of info to this class
		Id = "AttackState";
		_MyInfo = myInfo;
		_MyTransform = _MyInfo.transform;
		_MyRigidbody2D = _MyInfo.GetComponent<Rigidbody2D> ();
		CanIRanged = canIRanged;//TODO DONT DELETE

		_MyAnimator = myInfo.transform.FindChild("GFX").GetComponent<Animator> ();
		ShootingAnimation = _MyAnimator.GetBehaviour<OnlyShootAfterAnimation> ();
		QuittingAnimation = _MyAnimator.GetBehaviour<OnlyQuitAfterAnimation> ();

		_LineOfSight = lineOfSight;

	
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

		_MyRigidbody2D.velocity = new Vector2 (0, 0);
		_MyAnimator.SetFloat ("ChangeAnimation", 5);
		targetPoss = _TargetInfo.transform.position;
		ShootingAnimation.Shoot = false;
		QuittingAnimation.ImQuitting = false;
	
		_ReturnState = "";
		return _ReturnState;
	}

	public override string ProcessState() {//this is called every frame
		if (ShootingAnimation.Shoot == true) {
			ShootingAnimation.Shoot = false;

			if (CanIRanged [0] == true) {
				_MyInfo.AttackTarget (targetPoss);
			} else {
				if (Vector2.Distance ((Vector2)_MyTransform.position, targetPos) < _Range) {//checking if im withing range of the target 
					if (Physics2D.Linecast ((Vector2)_MyTransform.position, targetPos, _LineOfSight).transform == null) {
						_MyInfo.AttackTarget (targetPoss);
					}
				}
			}
			targetPoss = _TargetInfo.transform.position;
		}

		if (QuittingAnimation.ImQuitting == true) {
			QuittingAnimation.ImQuitting = false;
			targetPos.x = _TargetInfo.myPos [0, 0];
			targetPos.y = _TargetInfo.myPos [0, 1];

			if (Vector2.Distance ((Vector2)_MyTransform.position, targetPos) < _Range) {//checking if im withing range of the target 
				if (Physics2D.Linecast ((Vector2)_MyTransform.position, targetPos, _LineOfSight).transform == null) {//check if there are something between me and the target
				} else {
					_MyAnimator.SetFloat ("ChangeAnimation", 1);
					return _ReturnState = "WalkToTargetState";
				}
			} else {
				_MyAnimator.SetFloat ("ChangeAnimation", 1);
				return _ReturnState = "WalkToTargetState";
			}
		}
	
		return _ReturnState = "";
	}

	public override void ExitState(){//when i want to switch to another state this is called before the enterstate of the next state
	}

}
