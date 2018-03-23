using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorVariables : MonoBehaviour {

	public EnemyManaging MyManager;
	public Animator MyAnimator;

	[HideInInspector]
	public bool AbilityRunning = false;
	[HideInInspector]
	public int AnimatorStage = 0;//To Be Able To Track The AnimatorStage Value In A Simple Way.
	[HideInInspector]
	public int SpellTransition = 0;

//	[HideInInspector]
	public bool FlipToTarget = false;//Flip sprite 180* to look at the target. if target to the left of the sprite turn left.
//	[HideInInspector]
	public bool RotateToTarget = false;//rotate the sprite to look at the target. if target is 27.5* to the rigth, rotate the sprite 27.5* to the right.

//	[HideInInspector]
	public bool CanIMove = false;//rotate the sprite to look at the target. if target is 27.5* to the rigth, rotate the sprite 27.5* to the right.

	int AnimatorStageHash = Animator.StringToHash ("AnimatorStages");



	public void SetRotateRotation(int s){
		if (s == 0) {
			RotateToTarget = false;
		} else {
			RotateToTarget = true;
		}
	}

	public void SetFlipRotation(int s){
		if (s == 0) {
			FlipToTarget = false;
		} else {
			FlipToTarget = true;
		}
	}

	public void SetMovement(int s){
		if (s == 0) {
			CanIMove = false;
		} else {
			CanIMove = true;
		}
	}

	public void ShootBullet(The_Default_Bullet s){
		(Instantiate (s, transform.position, Quaternion.identity)).SetMethod(MyManager);
	}

	public void SetAnimatorStage(int animatorStageValue){
		AnimatorStage = animatorStageValue; 
		MyAnimator.SetInteger (AnimatorStageHash, animatorStageValue);
	} 

	public void SetTransitionValue(int transitionStage){//New Transition, Means New Spells, Which Means New CD's
		SpellTransition = transitionStage; 
		MyManager.MyAbilityInfo.SetSpellCD ();//Setting CD Timers
	}

	public void AbilityCompleted (){
		MyManager.MyAbilityInfo.AddLostTime ();
		AbilityRunning = false;
		SetAnimatorStage (0);
	}


}
