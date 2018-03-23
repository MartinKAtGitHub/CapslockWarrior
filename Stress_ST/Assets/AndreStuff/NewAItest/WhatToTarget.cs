using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WhatToTarget  {

	[HideInInspector]
	public bool AttackClass = false;

	[HideInInspector]
	public CreatureRoot MyAttackTarget;
	[HideInInspector]
	public ObjectNodeInfo MyMovementTarget;

	public string[] TargetHierarchy;//This List Contains The Current TargetList, Which Is 
	string saver;

	GameObject[] _Targets;
	GameObject _Closest;

	public void SearchAfterTarget(Vector3 myPos){

		for (int i = 0; i < TargetHierarchy.Length; i++) {//Going Through The TargetHierarchy List.
		
			_Targets = GameObject.FindGameObjectsWithTag (TargetHierarchy [i]);//If Not Null Then There Are Objects In The Scene That Have The Desierd Tag

			if (_Targets.Length > 0) {
				_Closest = _Targets [0];
				for (int j = 1; j < _Targets.Length; j++) {
					if (Vector3.Distance (myPos, _Closest.transform.position) > Vector3.Distance (myPos, _Targets [j].transform.position)) {//Going Through The Objects Found And Targets The Closes
						_Closest = _Targets [j];
					}
				}
				MyMovementTarget = _Closest.GetComponent<ObjectNodeInfo> ();
				MyAttackTarget = _Closest.GetComponent<CreatureRoot> ();
				if (MyAttackTarget == null) {
					AttackClass = false;
				} else {
					AttackClass = true;
				}
				return;
			}

		}

	}

	public void TurnTargetList (){//Switches The Last And First Place And So On Until It Reaches The Middle

		for(int i = 0; i < TargetHierarchy.Length / 2; i++){
			saver = TargetHierarchy [i];
			TargetHierarchy [i] = TargetHierarchy[(TargetHierarchy.Length - 1) - i];
		}

	}

	public void ChengePositionOfTargets (int[] index){//Switching Two Target Tags Index

		saver = TargetHierarchy [index [0]];
		TargetHierarchy [index [0]] = TargetHierarchy [index [1]];
		TargetHierarchy [index [1]] = saver;

	}

}