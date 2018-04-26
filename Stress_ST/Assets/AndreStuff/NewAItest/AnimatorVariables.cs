using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorVariables : MonoBehaviour {

	public CreatureRoot myVariables;
	public Animator MyAnimator;

	[HideInInspector]
	public bool AbilityRunning = false;
	[HideInInspector]
	public int AnimatorStage = 0;//To Be Able To Track The AnimatorStage Value In A Simple Way.
	[HideInInspector]
	public int SpellTransition = 0;

	[HideInInspector]
	public int MovementType = 0;
	[HideInInspector]
	public int RotationType = 0;


//	[HideInInspector]
	public bool CanIMove = false;//rotate the sprite to look at the target. if target is 27.5* to the rigth, rotate the sprite 27.5* to the right.
	public bool CanIRotate = false;//rotate the sprite to look at the target. if target is 27.5* to the rigth, rotate the sprite 27.5* to the right.

	int AnimatorStageHash = Animator.StringToHash ("AnimatorStages");



	public void SetRotationType(int s){
		RotationType = s;

	}

	public void SetMovementType(int s){
		MovementType = s;

	}

	public void SetCanRotate(int s){
		if (s == 0) {
			CanIRotate = false;
		} else {
			CanIRotate = true;
		}
	}

	public void SetCanMove(int s){
		if (s == 0) {
			CanIMove = false;
		} else {
			CanIMove = true;
		}
	}

	public void ActivateScinematic(int val){
		if (val == 0) {
			myVariables.DissableForScenario = false;
		} else {
			myVariables.DissableForScenario = true;
		}
	}

	public void ShootBullet(The_Default_Bullet s){
		(Instantiate (s, transform.position, Quaternion.identity)).SetMethod(myVariables);
	}

	public void SetAnimatorStage(int animatorStageValue){
		AnimatorStage = animatorStageValue; 
		MyAnimator.SetInteger (AnimatorStageHash, animatorStageValue);
	} 

	public void SetTransitionValue(int transitionStage){//New Transition, Means New Spells, Which Means New CD's
		SpellTransition = transitionStage; 
		myVariables.GetAbilityInfo().SetSpellCD ();//Setting CD Timers
	}

	public void AbilityCompleted (The_Default_Bullet AbilityID){
		myVariables.GetAbilityInfo().AddLostTime (AbilityID.bulletID);
		AbilityRunning = false;
		SetAnimatorStage (0);
	}

	public void SetTargetVectorToTarget(){
		myVariables.GetWhatToTarget ().SetTargetVectorToTarget ();
	}

}