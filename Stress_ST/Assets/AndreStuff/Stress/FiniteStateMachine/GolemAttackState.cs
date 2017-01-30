using UnityEngine;
using System.Collections;

public class GolemAttackState : DefaultState {
	string ReturnState = "";
	DefaultBehaviour TargetInfo;
	DefaultBehaviour MyInfo;

	Transform MyTransform;

	Rigidbody2D myrigid;
	bool[] CanIRanged;
	Animator MyAnimator;

	AnimationClip[] testing;
	ShootingAfterAnimation test;

	LayerMask LineOfSight;
	float range = 1;
	public GolemAttackState(CreatureOneBehaviour myInfo, bool[] canIRanged, float theRange) {//giving copies of info to this class
		Id = "AttackState";
		MyInfo = myInfo;
		MyTransform = MyInfo.transform;
		myrigid = MyInfo.GetComponent<Rigidbody2D> ();
		CanIRanged = canIRanged;
		MyAnimator = myInfo.GetComponent<Animator> ();

		LineOfSight = 1 << LayerMask.NameToLayer ("Walls");

		test = MyAnimator.GetBehaviour<ShootingAfterAnimation> ();
		/*	testing = MyAnimator.runtimeAnimatorController.animationClips;
		for (int i = 0; i < testing.Length; i++) {
			Debug.Log (testing [i].name);
				
		}*/
		range = theRange;

	}

	public override string EnterState() {//When it switches to this state this is the first thing thats being called

		if (MyInfo.GetTraget () == null) {//having a failsafe here, so if i dont have a target, ill switch state to something that can search
			return ReturnState; //TODO TODO "TargetSearch";
		}else{
			if (TargetInfo != MyInfo.GetTargetBehaviour ()) {//if this target isnt the same as what im after, change it
				TargetInfo = MyInfo.GetTargetBehaviour ();
			}
		}
		AnimationStarted = false;
		ReturnState = "";
		return ReturnState;
	}
	bool removeme = false;
	bool AnimationStarted = false;

	public override string ProcessState() {//this is called every frame
		if (AnimationStarted == false) {
			MyAnimator.SetFloat ("ChangeAnimation", 5);
			AnimationStarted = true;
		} else {
			if (test.testingthistoo == true) {
				test.testingthistoo = false;
				MyAnimator.SetFloat ("ChangeAnimation", 0);
				AnimationStarted = false;
				if (Vector2.Distance ((Vector2)MyTransform.position, TargetInfo.GetMyPositionVector2 ()) < range) {//checking if im withing range of the target 
					//ReturnState = "WalkToTargetState";
				 
					if (Physics2D.Linecast ((Vector2)MyTransform.position, TargetInfo.GetMyPositionVector2 (), LineOfSight).transform == null) {//if im in range do a raycast and see if there is an obsacle in the way
						MyInfo.AttackTarget ();
					} else {
						ReturnState = "WalkToTargetState";
					}
				} else {
					ReturnState = "WalkToTargetState";
				}
			}

		}

		myrigid.velocity = new Vector2(0,0);
		//todo start animation, check distance when done hitting or when the animation hits
		if(removeme == true){
			MyTransform.position = MyTransform.position;
		}
		return ReturnState;
	}

	public override void ExitState(){//when i want to switch to another state this is called before the enterstate of the next state

	}

}
