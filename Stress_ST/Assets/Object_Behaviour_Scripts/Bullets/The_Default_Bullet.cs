using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Default_Bullet : MonoBehaviour {
	
	public Animator MyAnimator;
	public Rigidbody2D MyRigidbody2D;

	public bool RotateToTargetInStart = true;
	public bool RotateToTargetInUpdate = false;
	public bool IgnoreOnTrigger2D = false;

	protected GameObject _ImTheShooter;
	protected GameManagerTestingWhileWaiting.SpellAttackInfo _SpellInfo;
	protected Vector3 _MyShootingDirection;

	Vector3 _Direction = Vector3.zero;
	The_Object_Behaviour Shooter;

	protected Vector3 _TargetStartPosition = Vector3.zero;
	public Vector3 _MyStartPosition = Vector3.zero;


	///<summary>
	///[0] == Stop (True == Stop Movement), [1] == AnimatorStage (Animator Stage Value), [2] == Rotation (True == Rotate)
	/// </summary>
	protected int[] AnimatorVariables = new int[3] {
		Animator.StringToHash ("Stop"),
		Animator.StringToHash ("AnimatorStage"),
		Animator.StringToHash ("Rotate"),
	};



	public void SetMethod (GameManagerTestingWhileWaiting.SpellAttackInfo SpellInfo, The_Object_Behaviour MySender){

		_SpellInfo = SpellInfo;
		Shooter = MySender;
		_ImTheShooter = Shooter._TheObject.gameObject;
		_TargetStartPosition = Shooter._Target.position;
		_MyStartPosition = transform.position;
		_MyShootingDirection = (_TargetStartPosition - _MyStartPosition).normalized;

		if (RotateToTargetInStart) {
			Rotations ();
		}

	}

	public virtual void Rotations(){

		if (MyAnimator.GetBool(AnimatorVariables[2]) == true) {

			_Direction.z = Vector3.Angle (Vector3.right, _MyShootingDirection);

			if (_MyShootingDirection.y < 0) {
				_Direction.z = _Direction.z * -1;
			}

			transform.rotation = Quaternion.Euler (_Direction);
			_MyShootingDirection = (Shooter._Target.position - transform.position).normalized;
		}

	}

	public virtual void Movement(){

		if (MyAnimator.GetBool (AnimatorVariables [0]) == false) {
			if (MyRigidbody2D != null) {
				MyRigidbody2D.velocity = _MyShootingDirection * _SpellInfo.MovementMultiplyer;
			} else {
				transform.position += _MyShootingDirection * _SpellInfo.MovementMultiplyer;
			}
		} else {
			if (MyRigidbody2D != null) {
				MyRigidbody2D.velocity = Vector3.zero;
			} else {
				transform.position += Vector3.zero;
			}
		}

	}

	public virtual void AnimatorStage(){//Does'nt Do Anything Right Now. So Just Pseudocode

		//if (MyAnimator.GetBool (AnimatorVariables [1]) == 0) {
			//Speed Increase
		//}else if (MyAnimator.GetBool (AnimatorVariables [1]) == 1){
			//Explode And Do Physics2d.CicleCast And DMG To Other
		//}

	}

	void SetRotate(int boolValue){
		if (boolValue == 0) {
			MyAnimator.SetBool (AnimatorVariables [2], false);
		} else {
			MyAnimator.SetBool(AnimatorVariables[2], true);
		}
	}

	void SetStop(int boolValue){
		if (boolValue == 0) {
			MyAnimator.SetBool (AnimatorVariables [0], false);
		} else {
			MyAnimator.SetBool(AnimatorVariables[0], true);
		}
	}

}
