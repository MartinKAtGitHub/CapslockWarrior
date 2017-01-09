using UnityEngine;
using System.Collections;

public class GolemAttackState : DefaultState {
	string ReturnState = "";
	DefaultBehaviour TargetInfo;
	DefaultBehaviour MyInfo;

	Transform MyTransform;

	Rigidbody2D myrigid;


	public GolemAttackState(CreatureOneBehaviour myInfo) {//giving copies of info to this class
		Id = "AttackState";
		MyInfo = myInfo;
		MyTransform = MyInfo.transform;
		myrigid = MyInfo.GetComponent<Rigidbody2D> ();
	}

	public override string EnterState() {//When it switches to this state this is the first thing thats being called

		if (MyInfo.GetTraget () == null) {//having a failsafe here, so if i dont have a target, ill switch state to something that can search
			return ReturnState; //TODO TODO "TargetSearch";
		}else{
			if (TargetInfo != MyInfo.GetTargetBehaviour ()) {//if this target isnt the same as what im after, change it
				TargetInfo = MyInfo.GetTargetBehaviour ();
			}
		}

		return ReturnState;
	}
	bool removeme = false;

	public override string ProcessState() {//this is called every frame
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
