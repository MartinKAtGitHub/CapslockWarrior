using System.Collections.Generic;
using System;
using UnityEngine;


[System.Serializable]
public class TargetHierarchy {//TODO improve searching choises based on distance or something 
	/*TAKE A LOOK AT THIS
https://msdn.microsoft.com/en-us/library/essfb559(v=vs.110).aspx
	*/

	List<DefaultBehaviour> Targets = new List<DefaultBehaviour>();
	GameObject[] FoundTargets;

	public List<string> TheTargetHierarchy = new List<string>();
	DefaultBehaviour Object;

	public TargetHierarchy(DefaultBehaviour ThisObject){
		Object = ThisObject;
	}

	public void AddTarget(DefaultBehaviour ATarget){
		Targets.Add (ATarget);
	}

	public void SetTargetHierarchy(DefaultBehaviour ThisObject){
		Object = ThisObject;
	}

	public void SearchAfterNewTargets(){//this searches through all tags in the hiearchy for the given tag.TODO instead of a search, make it so that object tell a manager that they exist, then get the objects from there
		Targets = new List<DefaultBehaviour>();
		for (int i = 0; i < TheTargetHierarchy.Count; i++) {
			FoundTargets = GameObject.FindGameObjectsWithTag (TheTargetHierarchy [i]);
			if (FoundTargets.Length > 0) {
				for (int j = 0; j < FoundTargets.Length; j++) {
					Targets.Add (FoundTargets[j].GetComponent<DefaultBehaviour>());
				}
			}
		}
	}

	public void TargetDead(){//when the target dies call this to clean up the empty space in the list, then search after a new

		for (int i = 0; i < Targets.Count; i++) {
			if (Targets [i] == null) {
				Targets.Remove (Targets [i]);
				GC.Collect ();//it might cause alot of GC trouble. TODO search for answers
			}
		}

		for (int i = 0; i < TheTargetHierarchy.Count; i++) {
			for (int j = 0; j < Targets.Count; j++) {
				if (TheTargetHierarchy [i] == Targets [j].tag) {//might become abit expensive, TODO create an enum for tags and do enum.parse() on targets[j].tag instead of comparing so many strings, could also have a seperate list for just the tag/enum of the targets[j] to optimalize it abit more
					Object.SetTarget (Targets [j].gameObject);
					return;
				}
			}
		}

		for (int i = 0; i < TheTargetHierarchy.Count; i++) {//going to change this 
			FoundTargets = GameObject.FindGameObjectsWithTag (TheTargetHierarchy [i]);
			if (FoundTargets.Length > 0) {
				Object.SetTarget(FoundTargets[0]);
					return;
			}
		}

		Object.SetTarget(Object.gameObject);//if i come this far then set myself as the target 
		return;
	}

	public void CheckIfICanSwitchTarget(){//if you just want to do a random search do this

		for (int i = 0; i < TheTargetHierarchy.Count; i++) {
			for (int j = 0; j < Targets.Count; j++) {
				if (TheTargetHierarchy [i] == Targets [j].tag) {//might become abit expensive, TODO create an enum for tags and do enum.parse() on targets[j].tag instead of comparing so many strings, could also have a seperate list for just the tag/enum of the targets[j] to optimalize it abit more
					Object.SetTarget (Targets [j].gameObject);
					return;
				}
			}
		}

		for (int i = 0; i < TheTargetHierarchy.Count; i++) {//going to change this 
			FoundTargets = GameObject.FindGameObjectsWithTag (TheTargetHierarchy [i]);
			if (FoundTargets.Length > 0) {
				Object.SetTarget(FoundTargets[0]);
				return;
			}
		}

		Object.SetTarget(Object.gameObject);//if i come this far then set myself as the target 
		return;
	}


}
