using UnityEngine;
using System.Collections;

public class FindTarget : DefaultState {

	public FindTarget() {//setter id
		Id = "FindTargetState";
	}

	public override void EnterState() {//skriver ut statId når staten starter
		base.EnterState();
//		Robot.Out.WriteLine(Id);
	}


	public override string ProcessState() {//det eneste denne gjør er å snu radaren til den treffe en fiende
	//	Robot.SetTurnRadarLeft(15);
//		Debug.Log(CreatureObject.gameObject.name);

		string retState = null;//denne er enten null eller den staten som den skal forandres til
		if (GameObject.Find("Player1") != null) {
			Debug.Log(CreatureObject.gameObject.name);
			retState = "RadarFollowState";
		}
		retState = "RadarFollowState";

		return retState;
	}
}